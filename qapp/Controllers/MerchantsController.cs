using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace qapp.Controllers
{
    public class MerchantsController : Controller
    {
        //
        // GET: /Merchants/

        /// <summary>
        /// 
        /// </summary>
        /// <param name="longitude">User location longitude (optional)</param>
        /// <param name="latitude">User location latitude (optional)</param>
        /// <param name="keywords">Comma-separated keywords</param>
        /// <returns></returns>
        public ActionResult Index(double? longitude, double? latitude, string keywords)
        {
            return View();
        }

    }
}
