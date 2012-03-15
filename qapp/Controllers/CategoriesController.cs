using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qapp.Controllers
{
    public class CategoriesController : Controller
    {
        //
        // GET: /Categories/

        public ActionResult Index()
        {
            return Json(new string[] {"Banks", "Shops"}, JsonRequestBehavior.AllowGet);
        }

    }
}
