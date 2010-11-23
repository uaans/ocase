using System;
using System.Linq;
using System.Collections.Generic;

namespace Ocase.Main.Model
{
	public class CustomFieldCollection
	{
		private Dictionary<string, string> customFields = new Dictionary<string, string>();
		
		public string this[string fieldName]
		{
			get
			{
				CheckCustomFieldName(fieldName);
				if (!customFields.ContainsKey(fieldName))
				{
					return string.Empty;
				}
				return customFields[fieldName];
			}
			set
			{
				CheckCustomFieldName(fieldName);
				if (!customFields.ContainsKey(fieldName))
				{
					customFields.Add(fieldName, value);
				}
				else
				{
					customFields[fieldName] = value;
				}
			}
		}
		
		public List<string> Keys
		{
			get { return new List<string>(customFields.Keys); }
		}
		
		public CustomFieldCollection () {}
		
		private static void CheckCustomFieldName(string fieldName)
		{
				if (!Configuration.CustomFieldNames.Contains(fieldName))
				{
					throw new ArgumentException(string.Format("Field name {0} is not defined.", fieldName));
				}
		}
		
		public override string ToString ()
		{
			string[] values = (from f in customFields select (f.Key + "=" + f.Value).ToString()).ToArray();
			return string.Format ("[CustomFieldCollection: {0}]", string.Join(",", values));
		}
		
	}
}

