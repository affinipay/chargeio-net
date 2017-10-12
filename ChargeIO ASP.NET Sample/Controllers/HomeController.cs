using System;
using System.Web.Mvc;
using ChargeIO.Services.Merchant;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ChargeIOASP.NETSample.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Merchant()
        {
            var service = new MerchantService();
            try
            {
                //TODO: try out API calls here
                var merchant = service.GetMerchant();
                ViewBag.Message = JsonConvert.SerializeObject(merchant,
                    Formatting.Indented, new StringEnumConverter());
            }
            catch (Exception exception)
            {
                ViewBag.Message = exception.Message;
            }

            return View();
        }
    }
}