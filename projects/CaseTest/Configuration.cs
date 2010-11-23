using System;
using System.Collections.Generic;

namespace CaseTest
{
	public class Configuration
	{
		private static readonly List<string> customFieldNames = new List<string>();
		private static readonly bool generateOrganization = false;
		private static readonly string connectionString;
		private static readonly int searchesNumber;
		
		public static int SearchesNumber 
		{
			get { return searchesNumber; }
		}

		public static List<string> CustomFieldNames 
		{ 
			get { return customFieldNames; } 
		}
		
		public static string ConnectionString 
		{
			get { return connectionString; }
		}

		public static bool GenerateOrganization 
		{ 
			get { return generateOrganization; }
		}
		

		
		static Configuration ()
		{
			customFieldNames = new List<string>(new string[] { "FieldOne", "FieldTwo", "FieldThree", "FieldFour" });
			generateOrganization = true;
			searchesNumber = 1000;
			connectionString =
	          "Server=localhost;Database=oCase;User ID=root;Pooling=false;Allow User Variables=True";
		}
	}
}

