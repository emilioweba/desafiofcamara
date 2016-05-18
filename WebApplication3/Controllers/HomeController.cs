using DesafioFCamara.Models;
using DesafioFCamaraWCF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Web;
using System.Web.Mvc;

namespace DesafioFCamara.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View(new HomeViewModel());
        }

        //public ActionResult Index(HomeViewModel vm)
        //{
        //    ViewBag.Title = "Home Page";

        //    return View(vm);
        //}

        public JsonResult GenerateToken()
        {
            var myChannelFactory = new ChannelFactory<IDesafioFCamaraWCF>(new WebHttpBinding(),
                "http://www.emilioweba.com/DesafioFCamaraWCF.svc");

            myChannelFactory.Endpoint.Behaviors.Add(new WebHttpBehavior());

            try
            {
                return Json(myChannelFactory.CreateChannel().GenerateToken(), JsonRequestBehavior.AllowGet);
            }
            catch
            {
                return null; // se chegou ate aqui, algo errado aconteceu
            }
        }
    }
}
