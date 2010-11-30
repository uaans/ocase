using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using Ocase.Main.Model;

namespace Ocase.Web.Client
{
	public class CaseController : Controller
	{
		public ActionResult Index()
		{
			var cases = Case.GetAll();
			return View(cases);	
		}
		
		public ActionResult Create()
		{
			return View(new Case());
		}
		
		[AcceptVerbs(HttpVerbs.Post)]
		public ActionResult Create(Case casefile)
		{
			casefile.Save();
			return RedirectToAction("Index");
		}
	}
}

