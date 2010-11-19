using System;
using CaseTest.Data;
namespace CaseTest.Model
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
		
		public static User GetOne()
		{
			User test = new User();
			test.Name = PhraseGenerator.GetRandomPhrase(2, 3);
			return test;
		}
		
		public override string ToString ()
		{
			return string.Format ("[User: Id={0}, Name={1}, CreationDate={2}, ExpireDate={3}]", Id, Name, CreationDate, ExpireDate);
		}
	}
}

