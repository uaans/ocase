using System;
using Norm;
using System.Linq;
using System.Collections.Generic;

namespace Ocase.Main.Model
{
	public class Case
	{
		public ObjectId Id { get; set; }
		public int Year { get; set; }
		public int SequenceNumber { get; set; }
		public string Title { get; set; }
		public DateTime CreationDate { get; set; }
		public CustomFieldCollection CustomFields { get; set; }
		public CaseType Type { get; set; }
		public Organization OrganizationUnit { get; set; }
		public User Responsible { get; set; }
		
		public Case () 
		{
			CreationDate = DateTime.Now;
			Year = CreationDate.Year;
			CustomFields = new CustomFieldCollection();
		}
		
		public static List<Case> GetAll()
		{
			using(var db = Configuration.CreateMongoConnection())
			{
				var cases = db.GetCollection<Case>().Find();
				return cases.ToList();
			}
		}
		
		public void Save()
		{
			if(SequenceNumber < 1) SequenceNumber = SequenceNumbers.NextCaseSequenceNumber();
			using(var db = Configuration.CreateMongoConnection())
			{
				db.GetCollection<Case>().Save(this);
			}
		}
		
		public string YearAndSequenceNumber()
		{
			return String.Format("{0}/{1}", Year.ToString().Substring(2, 2), SequenceNumber);	
		}
		
		public override string ToString()
		{
			return string.Format ("[Case: Id={0},\n Year={1},\n SequenceNumber={2},\n Title={3},\n CreationDate={4},\n CustomFields={5},\n Type={6},\n OrganizationUnit={7},\n Responsible={8}]", Id, Year, SequenceNumber, Title, CreationDate, CustomFields, Type, OrganizationUnit, Responsible);
		}
	}
}

