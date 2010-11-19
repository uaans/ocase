using System;
using CaseTest.Model;
using System.Collections.Generic;
using System.Data;

namespace CaseTest.Data
{
	public class CaseTypeDataProvider : DataProvider
	{
		
		public List<CaseType> GetAll()
		{
			List<CaseType> result = new List<CaseType>();
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = "SELECT Id, Name FROM CaseTypes";
					IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (reader.Read())
					{
						result.Add(new CaseType() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });
					}
					connection.Close();
					return result;
				}
			}
		}
	}
}

