using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SourceCocktail.Controllers
{
    public class CocktailController : Controller
    {
        //
        // GET: /Cocktail/
        public ActionResult Index()
        {
            return View();
        }
	}
}