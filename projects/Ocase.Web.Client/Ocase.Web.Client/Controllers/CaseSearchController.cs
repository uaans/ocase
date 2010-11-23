using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ocase.Main.Model;
using Ocase.Main.Data;

namespace Ocase.Web.Client
{
	public class CaseSearchController : Controller
	{
		public ActionResult Index()
		{
			var caseProvider = new CaseDataProvider();
			var cases = caseProvider.GetTop100();
			return View(cases);	
		}
	}
}

