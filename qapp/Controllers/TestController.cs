using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Text;

namespace qapp.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Test/

        public ActionResult Index()
        {
            return Json(new{Status = "ok"}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DB()
        {
            return Content(ConfigurationManager.ConnectionStrings["RavenDB"].ConnectionString, "text/plain", Encoding.UTF8);
        }
    }
}
