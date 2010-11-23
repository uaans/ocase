using System;
using Ocase.Main.Data;
namespace Ocase.Main.Model
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public DateTime ExpireDate  { get; set; }
		
		public User ()
		{
			Id = -1;
			CreationDate = DateTime.Now;
			ExpireDate = DateTime.MinValue;
		}
		
		public void Save()
		{
			Id = new UserDataProvider().Save(this);
		}
		
		public override string ToString ()
		{
			return string.Format ("[User: Id={0}, Name={1}, CreationDate={2}, ExpireDate={3}]", Id, Name, CreationDate, ExpireDate);
		}
	}
}

