using System;
using Ocase.Main.Data;

namespace Ocase.Main.Model
{
	public class Case
	{
		public int Id { get; set; }
		public int Year { get; set; }
		public int SequenceNumber { get; set; }
		public string Title { get; set; }
		public DateTime CreationDate { get; set; }
		public CustomFieldCollection CustomFields { get; set; }
		public CaseType Type { get; set; }
		public Organization OrganizationUnit { get; set; }
		public User Responsible { get; set; }
		
		public bool IsNew
		{
			get { return Id <= 0; }
		}
		
		public Case () 
		{
			Id = -1;
			CreationDate = DateTime.Now;
			Year = CreationDate.Year;
			CustomFields = new CustomFieldCollection();
		}
		
		public void Save()
		{
			Id = new CaseDataProvider().Save(this);
		}
		
		public string YearAndSequenceNumber()
		{
			return String.Format("{0}/{1}", Year.ToString().Substring(2, 2), SequenceNumber);	
		}
		
		public override string ToString()
		{
			return string.Format ("[Case: Id={0},\n Year={1},\n SequenceNumber={2},\n Title={3},\n CreationDate={4},\n CustomFields={5},\n Type={6},\n OrganizationUnit={7},\n Responsible={8}, IsNew={9}]", Id, Year, SequenceNumber, Title, CreationDate, CustomFields, Type, OrganizationUnit, Responsible, IsNew);
		}
	}
}

