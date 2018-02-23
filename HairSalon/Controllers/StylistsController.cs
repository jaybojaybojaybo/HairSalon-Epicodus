using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;

namespace HairSalon.Controllers
{
    public class StylistsController : Controller
    {
      [HttpGet("/stylists/new")]
      public ActionResult CreateForm()
      {
        List<Stylist> allStylists = Stylist.GetAll();
        return View(allStylists);
      }

      [HttpPost("/stylists/new")]
      public ActionResult Create()
      {
        string newName = Request.Form["new-name"];
        Stylist newStylist = new Stylist(newName);
        newStylist.Save();

        List<Stylist> allStylists = Stylist.GetAll();
        return View("~/Views/Home/Index.cshtml", allStylists);
      }

      [HttpPost("/stylists/delete")]
        public ActionResult DeleteAll()
        {
          Stylist.DeleteAll();
          return View();
        }
    }
}
