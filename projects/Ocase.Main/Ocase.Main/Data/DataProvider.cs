using System;
using System.Data;
using MySql.Data.MySqlClient;

namespace Ocase.Main.Data
{
	public class DataProvider
	{
		public IDbConnection CreateConnection()
		{
			IDbConnection connection = new MySqlConnection(Configuration.ConnectionString);
			connection.Open();
			return connection;
		}
		
		public string PrepareParameter(object parameterValue)
		{
			if (null == parameterValue)
			{
				return "NULL";
			}
			if (parameterValue.GetType().Equals(typeof(DateTime)))
			{
				DateTime date = (DateTime)parameterValue;
				return DateTime.MinValue == date ? "NULL" : string.Format("'{0}'", date.ToString("yyyy-MM-dd"));
			}
			return string.Format("'{0}'", parameterValue.ToString());
		}
	}
}

