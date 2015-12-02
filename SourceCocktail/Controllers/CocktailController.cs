using DatabaseC;
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
        /*public ActionResult Index()
        {
            return View();
        }*/

        public ActionResult Index()
        {
            Cocktail films = new Cocktail(Session["DB_CINEMA"]);

            String orderBy = "";
            if (Session["Film_SortBy"] != null)
                orderBy = (String)Session["Film_SortBy"] + " " + (String)Session["Film_SortOrder"];

            films.SelectAll(orderBy);
            return View(films.ToList());
        }
	}
}