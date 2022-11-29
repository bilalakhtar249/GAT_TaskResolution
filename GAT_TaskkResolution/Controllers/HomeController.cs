using GAT_TaskResolutionEntity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GAT_TaskkResolution.Controllers
{
    public class HomeController : Controller
    {

        public HomeController(TaskResolutionContext context)
        {

        }

        public ActionResult Index()
        {
            return View();
        }

    }
}