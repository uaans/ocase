using System;
using CaseTest.Data;

namespace CaseTest.Model
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
		
		public static Case GetOne(Organization organization, User user)
		{
			if (null == organization || null == user)
			{
				throw new ArgumentNullException();
			}
			Random r = new Random();
			Case theCase = new Case()
			{
				Title = PhraseGenerator.GetRandomPhrase(1, 6),
				OrganizationUnit = organization,
				Responsible = user,
				Type = CaseType.CaseTypes[r.Next(0, CaseType.CaseTypes.Count - 1)]
			};

			for (int i = 0; i < r.Next(0, Configuration.CustomFieldNames.Count); i++)
			{
				theCase.CustomFields[Configuration.CustomFieldNames[i]] = PhraseGenerator.GetRandomPhrase(1, 5);
			}
			return theCase;
		}
		
		public override string ToString()
		{
			return string.Format ("[Case: Id={0},\n Year={1},\n SequenceNumber={2},\n Title={3},\n CreationDate={4},\n CustomFields={5},\n Type={6},\n OrganizationUnit={7},\n Responsible={8}, IsNew={9}]", Id, Year, SequenceNumber, Title, CreationDate, CustomFields, Type, OrganizationUnit, Responsible, IsNew);
		}
	}
}

