using System;
using CaseTest.Data;

namespace CaseTest.Model
{
	public class Organization
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public Organization ()
		{
			Id = -1;
		}
		
		public void Save()
		{
			Id = new OrganizationDataProvider().Save(this);
		}
		
		public static Organization GetOne()
		{
			Organization test = new Organization();
			test.Name = PhraseGenerator.GetRandomPhrase(2, 4);
			return test;
		}
		
		public override string ToString ()
		{
			return string.Format ("[Organization: Id={0}, Name={1}]", Id, Name);
		}
	}
}

