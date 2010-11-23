using System;
using Ocase.Main.Data;

namespace Ocase.Main.Model
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
		
		public override string ToString ()
		{
			return string.Format ("[Organization: Id={0}, Name={1}]", Id, Name);
		}
	}
}

