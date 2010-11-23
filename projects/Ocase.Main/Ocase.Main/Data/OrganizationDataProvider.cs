using System;
using System.Data;
using System.Collections.Generic;
using Ocase.Main.Data;
using Ocase.Main.Model;

namespace Ocase.Main.Data
{
	public class OrganizationDataProvider : DataProvider
	{
		public int Save(Organization item)
		{
			int result = -1;
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Format(@"			
						INSERT INTO Organization (Name) VALUES ({0});
						SELECT LAST_INSERT_ID();", PrepareParameter(item.Name));
					result = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
					return result;
				}
			}
		}
		
		public List<Organization> GetAll()
		{
			List<Organization> result = new List<Organization>();
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = "SELECT Id, Name FROM Organization";
					IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (reader.Read())
					{
						result.Add(new Organization() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });
					}
					connection.Close();
					return result;
				}
			}
		}
	}
}

