using System;
using System.Data;
using CaseTest.Model;
using System.Text;
using System.Linq;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace CaseTest.Data
{
	public class CaseDataProvider : DataProvider
	{
		const string caseSelect = @"
			SELECT
				c.Id,
				c.Year,
				c.Sequence,
				c.Title,
				c.CaseTypeId,
				c.CreationDate,
				c.OrganizationId,
				o.Name as OrganizationName,
				c.UserId,
				u.Name as UserName
			FROM
				Cases C INNER JOIN Organization o ON c.OrganizationId = o.Id
					INNER JOIN Users u ON c.UserId = u.Id 
			{0}";
		
		const string caseFieldsSelect = @"
			SELECT
				cf.FieldName,
				cfv.CaseId,
				cfv.FieldValue
			FROM
				CustomFieldValues cfv INNER JOIN CustomFields cf ON cfv.FieldId = cf.Id
			WHERE
				CaseId IN ({0})";

		
		public List<Case> GetByCustomFieldValue(string titlePattern, int count)
		{
			return GetCases(string.Format("cfv.FieldValue LIKE '{0}'", titlePattern.Replace('*', '%')), 
			                " INNER JOIN CustomFieldValues cfv ON c.Id = cfv.CaseId ",
			                count);
		}
		
		public List<Case> GetByYearAndNumber(int year, int sequenceNumber)
		{
			return GetCases(string.Format("Year = {0} AND Sequence = {1}", year, sequenceNumber), 1);
		}
				
		public List<Case> GetByTitle(string titlePattern, int count)
		{
			return GetCases(string.Format("Title LIKE '{0}'", titlePattern.Replace('*', '%')), count);
		}
				
		public Case GetById(int id)
		{
			List<Case> result = GetCases(string.Format("c.Id = {0}", id), 1);
			if (0 == result.Count)
			{
				//throw new ArgumentOutOfRangeException("Case with specified ID was not found.");
				return null;
			}
			return result[0];
		}
		
		public int Save(Case theCase)
		{
			int result = -1;
			using (IDbConnection connection = CreateConnection())
			{
				using (MySqlCommand command = new MySqlCommand())
				{
					try
					{
						command.Connection = connection as MySqlConnection;
						StringBuilder builder = new StringBuilder();
						builder.Append(@"			
							START TRANSACTION;
							SET @SequenceNumber = 1 + (SELECT IFNULL(MAX(Sequence),0) FROM Cases WHERE Year = @Year);
							INSERT INTO Cases (Year, Sequence, Title, CaseTypeId, CreationDate, OrganizationId, UserId) 
								VALUES (@Year, @SequenceNumber, @Title, @CaseTypeId, @CreationDate, @OrganizationId, @UserId);
							SET @CurrentCaseId = LAST_INSERT_ID();");

						foreach (string key in theCase.CustomFields.Keys)
						{
							builder.AppendFormat(@"
								SET @CustomFieldId = (SELECT Id FROM CustomFields WHERE FieldName = '{0}');
								INSERT INTO CustomFieldValues (FieldId, CaseId, FieldValue) VALUES (@CustomFieldId, @CurrentCaseId, '{1}');",
							                     key, theCase.CustomFields[key]);
						}
						builder.Append(@"
								COMMIT;
								SELECT @CurrentCaseId;");

						command.Parameters.AddWithValue("@Year", theCase.Year);
						command.Parameters.AddWithValue("@Title", theCase.Title);
						command.Parameters.AddWithValue("@CaseTypeId", theCase.Type.Id);
						command.Parameters.AddWithValue("@CreationDate", theCase.CreationDate);
						command.Parameters.AddWithValue("@OrganizationId", theCase.OrganizationUnit.Id);
						command.Parameters.AddWithValue("@UserId", theCase.Responsible.Id);
						
						command.CommandText = builder.ToString();
						result = Convert.ToInt32(command.ExecuteScalar());
					}
					catch
					{
						command.CommandText = "ROLLBACK";
						command.ExecuteNonQuery();
						throw;
					}
					finally
					{
						connection.Close();
					}
				}
			}
			return result;
		}
		
		private List<Case> GetCases(string casePredicate, int limit)
		{
			return GetCases(casePredicate, string.Empty, limit);
		}
		
		private List<Case> GetCases(string casePredicate, string joinExpression, int limit)
		{
			if (string.IsNullOrEmpty(casePredicate))
			{
				throw new ArgumentException("casePredicate");
			}
			
			List<Case> result = new List<Case>();
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Format(caseSelect, 
						joinExpression + "WHERE " + casePredicate + (limit > 0 ? (" LIMIT 0," + limit.ToString()) : string.Empty));
					using (IDataReader reader = command.ExecuteReader())
					{
						while (reader.Read())
						{
							result.Add(new Case() 
							    { 
									Id = Convert.ToInt32(reader["Id"]), 
									Year = Convert.ToInt32(reader["Year"]), 
									SequenceNumber = Convert.ToInt32(reader["Sequence"]), 
									Title = reader["Title"].ToString(),
									Type = CaseType.CaseTypes[Convert.ToInt32(reader["CaseTypeId"])],
									CreationDate = Convert.ToDateTime(reader["CreationDate"]),
									OrganizationUnit = new Organization() { Id = Convert.ToInt32(reader["OrganizationId"]), Name = reader["OrganizationName"].ToString() },
									Responsible = new User() { Id = Convert.ToInt32(reader["UserId"]), Name = reader["UserName"].ToString() }
								});
						}
					}
				}
				
				if (result.Count > 0)
				{
					using (IDbCommand command = connection.CreateCommand())
					{
						string[] caseIds = (from c in result
							select c.Id.ToString()).ToArray();
						command.CommandText = string.Format(caseFieldsSelect,
							string.Join(",", caseIds));
						using (IDataReader reader = command.ExecuteReader())
						{
							while (reader.Read())
							{
								Case theCase = result.Find(c => Convert.ToInt32(reader["CaseId"]) == c.Id);
								theCase.CustomFields[reader["FieldName"].ToString()] = reader["FieldValue"].ToString();
							}
						}
						connection.Close();
					}
				}

			}
			return result;			
		}
		
	}
}

