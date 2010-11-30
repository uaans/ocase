using System;
using System.Linq;
using System.Collections.Generic;
using Norm;

namespace Ocase.Main.Model
{
	public class CaseType
	{
		private static List<CaseType> types = new List<CaseType>();
		
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		public static List<CaseType> CaseTypes
		{
			get { return types; }
		}
		
		static CaseType ()
		{
			using(var db = Mongo.Create(System.Configuration.ConfigurationManager.AppSettings["MongoConnection"]))
			{
				types = db.GetCollection<CaseType>().Find().ToList();
			}
		}
		
		public override string ToString ()
		{
			return string.Format ("[CaseType: Id={0}, Name={1}]", Id, Name);
		}
	}
}

