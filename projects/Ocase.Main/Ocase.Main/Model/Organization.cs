using System;
using Norm;

namespace Ocase.Main.Model
{
	public class Organization
	{
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		
		public void Save()
		{
			using(var db = Mongo.Create(System.Configuration.ConfigurationManager.AppSettings["MongoConnection"]))
			{
				db.GetCollection<Organization>().Save(this);
			}
		}
		
		public override string ToString ()
		{
			return string.Format ("[Organization: Id={0}, Name={1}]", Id, Name);
		}
	}
}

