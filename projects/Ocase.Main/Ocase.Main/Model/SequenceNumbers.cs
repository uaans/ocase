using System;
using System.Linq;
using Norm;

namespace Ocase.Main.Model
{
	public class SequenceNumbers
	{
		[MongoIdentifier]
		public int Id {
			get;
			set;
		}
		
		public int CaseSequenceNumber {
			get;
			set;
		}
		
		public int CaseYear {
			get;
			set;
		}
		
		/// <summary>
		/// Resets the SequenceNumbers collection
		/// </summary>
		public static void InitializeMongoCollection(Boolean overwriteExistingCollection)
		{
			using(var db = Configuration.CreateMongoConnection())
			{
				var sequenceNumbers = db.GetCollection<SequenceNumbers>().Find();
				if(overwriteExistingCollection || sequenceNumbers.Count() == 0)
				{
					var sequenceNumber = new SequenceNumbers();
					sequenceNumber.CaseSequenceNumber = 1;
					sequenceNumber.CaseYear = DateTime.Now.Year;
					sequenceNumber.Save();
				}
			}
		}
		
		public void Save()
		{
			using(var db = Configuration.CreateMongoConnection())
			{
				db.GetCollection<SequenceNumbers>().Save(this);
			}
		}
		
		
		public static int NextCaseSequenceNumber()
		{
			using(var db = Configuration.CreateMongoConnection())
			{
				var seqNo = db.GetCollection<SequenceNumbers>().FindAndModify(new {Id = 0}, new {CaseSequenceNumber = M.Increment(1)});
				if(seqNo.CaseYear != DateTime.Now.Year)
				{
					SequenceNumbers.InitializeMongoCollection(true);
					seqNo = db.GetCollection<SequenceNumbers>().FindAndModify(new {Id = 0}, new {CaseSequenceNumber = M.Increment(1)});
				}
				return seqNo.CaseSequenceNumber;
			}
		}
	}
}

