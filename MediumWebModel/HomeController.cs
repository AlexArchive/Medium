﻿using System.Web.Mvc;

namespace Medium.WebModel
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}