using System;
using System.Collections.Generic;
using CaseTest.Data;

namespace CaseTest.Model
{
	public class CaseType
	{
		private static List<CaseType> types = new List<CaseType>();
		
		public int Id { get; set; }
		public string Name { get; set; }
		public static List<CaseType> CaseTypes
		{
			get { return types; }
		}
		
		static CaseType ()
		{
			types = new CaseTypeDataProvider().GetAll();
		}
		
		public override string ToString ()
		{
			return string.Format ("[CaseType: Id={0}, Name={1}]", Id, Name);
		}
	}
}

