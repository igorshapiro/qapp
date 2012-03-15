using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using qapp.Models;

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
            string[] keywordsArray = new string[]{};
            if (keywords != null)
                keywordsArray = keywords.Split(',')
                              .Where(k => k != null)
                              .Select(k => k.Trim())
                              .Where(k => !string.IsNullOrEmpty(k))
                              .ToArray();
            var matches = from m in Merchant.GetAll(keywordsArray)
                          from q in m.GetQueues()
                          select new {
                              name = m.Name,
                              address = m.Address,
                              merchantId = m.Id,
                              queueId = q.Id,
                              latitude = m.Latitude,
                              longitude = m.Longitude
                          };
            return Json(matches.ToArray(), JsonRequestBehavior.AllowGet);
        }

    }
}
