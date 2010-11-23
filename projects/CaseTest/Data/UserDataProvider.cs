using System;
using System.Collections.Generic;
using System.Data;
using CaseTest.Model;

namespace CaseTest.Data
{
	public class UserDataProvider : DataProvider
	{
		public int Save(User item)
		{
			int result = -1;
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = string.Format(@"			
						INSERT INTO Users (Name, CreationDate, ExpireDate) VALUES ({0}, {1}, {2});
						SELECT LAST_INSERT_ID();", PrepareParameter(item.Name), PrepareParameter(item.CreationDate), 
					                                    PrepareParameter(item.ExpireDate));
					result = Convert.ToInt32(command.ExecuteScalar());
					connection.Close();
					return result;
				}
			}
		}
		
		public List<User> GetAll()
		{
			List<User> result = new List<User>();
			using (IDbConnection connection = CreateConnection())
			{
				using (IDbCommand command = connection.CreateCommand())
				{
					command.CommandText = "SELECT Id, Name, CreationDate, ExpireDate FROM Users";
					IDataReader reader = command.ExecuteReader(CommandBehavior.CloseConnection);
					while (reader.Read())
					{
						result.Add(new User() 
						    { 
								Id = Convert.ToInt32(reader["Id"]), 
								Name = reader["Name"].ToString(),
								CreationDate = Convert.ToDateTime(reader["CreationDate"]),
								ExpireDate = DBNull.Value == reader["ExpireDate"] ? DateTime.MinValue : Convert.ToDateTime(reader["ExpireDate"])
							});
					}
					connection.Close();
					return result;
				}
			}
		}
		
	}
}

