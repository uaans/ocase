using System;
using System.Collections.Generic;
using CaseTest.Model;
using CaseTest.Data;
using System.Threading;

namespace CaseTest
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			try
			{
				// organizations and users
				List<Organization> organizations = CreateOrganization();
				List<User> users = CreateUsers();
				
				// creating cases
				CreateCases(100, organizations, users);
				CreateCases(1000, organizations, users);
				CreateCases(10000, organizations, users);
				CreateCases(500000, organizations, users);
				CreateCases(1000, organizations, users);
				
				// search cases
				CaseDataProvider provider = new CaseDataProvider();
				List<Case> cases;
				Random r = new Random();

				TimeCounter counter = new TimeCounter("C. Search by year and number");
				int found = 0;
				for (int i = 0; i < Configuration.SearchesNumber; i++)
				{
					counter.Start();
					cases = provider.GetByYearAndNumber(2010, r.Next(1, 100000));
					counter.Stop();
					found += cases.Count;
				}
				counter.PrintTime();
				counter.PrintLine(string.Format("Found {0} cases, avg. {1} per search.\n", found, found / Configuration.SearchesNumber));
				
				found = 0;
				counter = new TimeCounter("D. Search by case ID");
				for (int i = 0; i < Configuration.SearchesNumber; i++)
				{
					counter.Start();
					Case theCase = provider.GetById(r.Next(1, 100000));
					counter.Stop();
					if (theCase != null)
					{
						found++;
					}
				}
				counter.PrintTime();
				counter.PrintLine(string.Format("Found {0} cases, avg. {1} per search.\n", found, found / Configuration.SearchesNumber));
				
				found = 0;
				counter = new TimeCounter("E. Search by case title");
				for (int i = 0; i < Configuration.SearchesNumber; i++)
				{
					counter.Start();
					cases = provider.GetByTitle(PhraseGenerator.GetRandomPhrase(1) + "%", 20);
					counter.Stop();
					found += cases.Count;
				}
				counter.PrintTime();
				counter.PrintLine(string.Format("Found {0} cases, avg. {1} per search.\n", found, found / Configuration.SearchesNumber));
				
				found = 0;
				counter = new TimeCounter("F. Search by custom field value");
				for (int i = 0; i < Configuration.SearchesNumber; i++)
				{
					counter.Start();
					cases = provider.GetByCustomFieldValue(PhraseGenerator.GetRandomPhrase(1) + "%", 20);
					counter.Stop();
					found += cases.Count;
				}
				counter.PrintTime();
				counter.PrintLine(string.Format("Found {0} cases, avg. {1} per search.\n", found, found / Configuration.SearchesNumber));
				/*
				if (cases.Count > 0)
				{
					Console.WriteLine(cases[0]);
				}
				*/
			}
			catch (Exception e)
			{
				Console.WriteLine("Error occured: {0}", e);
			}
			Console.WriteLine("Done, press a key...");
			Console.ReadKey();
		}
		
		private static void CreateCases(int number, List<Organization> organizations, List<User> users)
		{
			Random r = new Random();
			TimeCounter counter = new TimeCounter(string.Format("Create {0} cases", number));
			for (int i = 0; i < number; i++)
			{
				counter.Start();
				Case theCase = Case.GetOne(organizations[r.Next(0, organizations.Count - 1)], users[r.Next(0, users.Count - 1)]);
				theCase.Save();
				counter.Stop();
				Thread.Sleep(r.Next(10, 50));
			}
			counter.PrintTime();
		}
		
		private static List<Organization> CreateOrganization()
		{
			List<Organization> organizations = new List<Organization>();
			TimeCounter counter = new TimeCounter("Create 100 organization units");
			if (Configuration.GenerateOrganization)
			{
				Console.WriteLine("Creating organizations...");
				for (int i = 0; i < 100; i++)
				{
					counter.Start();
					Organization org = Organization.GetOne();
					org.Save();
					counter.Stop();
					organizations.Add(org);
				}
				counter.PrintTime();
			}
			else
			{
				organizations = new OrganizationDataProvider().GetAll();
			}	
			return organizations;
		}
		
		private static List<User> CreateUsers()
		{
			List<User> users = new List<User>();
			if (Configuration.GenerateOrganization)
			{
				Console.WriteLine("Creating users...");
				TimeCounter counter = new TimeCounter("Creating 1000 users");
				for (int i = 0; i < 1000; i++)
				{
					counter.Start();
					User user = User.GetOne();
					user.Save();
					counter.Stop();
					users.Add(user);
				}
				counter.PrintTime();
			}
			else
			{
				users = new UserDataProvider().GetAll();
			}
			return users;
		}
	}
}

