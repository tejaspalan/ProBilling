using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProBilling.Class;
using ProBilling.Models;

namespace ProBilling.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
	        ActionResult result = View();

	        if (User.Identity.Name!= null && User.Identity.Name.Equals("vibhavmaheshwari@gmail.com"))
	        {
		        result =  RedirectToAction("ScrumMasterIndex");
	        }
	        return result;
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

	    public IActionResult ScrumMasterIndex()
	    {
		    var teams = new List<SelectListItem>();
		    teams.Add(new SelectListItem
		    {
			    Text = "Select",
			    Value = ""
		    });

		    foreach (Teams eVal in Enum.GetValues(typeof(Teams)))
		    {
			    teams.Add(new SelectListItem { Text = Enum.GetName(typeof(Teams), eVal), Value = eVal.ToString() });
		    }

			ViewBag.Teams = teams;
			return View();
		}
    }
}
