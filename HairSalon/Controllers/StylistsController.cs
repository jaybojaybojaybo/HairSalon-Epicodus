using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System;

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
        return RedirectToAction("Index", "Home");
      }

      [HttpPost("/stylists/delete")]
      public ActionResult DeleteAll()
      {
        Stylist.DeleteAll();
        return View();
      }

      [HttpGet("/stylists/{id}/profile")]
      public ActionResult Profile(int id)
      {
        Stylist thisStylist = Stylist.Find(id);
        List<Client> stylistClientList = thisStylist.GetClients();

        return View(stylistClientList);
      }


    }
}