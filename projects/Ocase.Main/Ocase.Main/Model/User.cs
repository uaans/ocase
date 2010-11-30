using System;
using Norm;

namespace Ocase.Main.Model
{
	public class User
	{
		public ObjectId Id { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ExpireDate  { get; set; }
		
		public User ()
		{
			CreationDate = DateTime.Now;
			ExpireDate = DateTime.MinValue;
		}
		
		public void Save()
		{
			using(var db = Configuration.CreateMongoConnection())
			{
				db.GetCollection<User>().Save(this);
			}
		}
		
		public override string ToString ()
		{
			return string.Format ("[User: Id={0}, Name={1}, CreationDate={2}, ExpireDate={3}]", Id, Name, CreationDate, ExpireDate);
		}
	}
}

