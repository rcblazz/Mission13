using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Controllers
{
    public class HomeController : Controller
    {

        private BowlingDbContext context { get; set; }


        //Constructor
        public HomeController(BowlingDbContext temp)
        {
            context = temp;
        }

        public IActionResult Index(string teamName)
        {
            ViewBag.SelectedTeam = RouteData?.Values["teamName"]; // maybe remove

            var blah = context.Bowlers
                .Where(b => b.Team.TeamName == teamName || teamName == null)
                .Include(x => x.Team)
                .ToList();

            return View(blah);
        }

        [HttpGet]
        public IActionResult AddEdit()
        {

            ViewBag.Teams = context.Teams.ToList();

            return View();

        }
        [HttpPost]
        public IActionResult AddEdit(Bowler bow)
        {
            if (ModelState.IsValid)
            {
                context.Add(bow);
                context.SaveChanges();

                return View("BowlerConfirmation", bow); //make BowlerConfirmation page
            }
            else //If Invalid
            {
                ViewBag.Teams = context.Teams.ToList();

                return View(bow);
            }
        }

        [HttpGet]
        public IActionResult Edit(int bowlerid)
        {
            ViewBag.Teams = context.Teams.ToList();

            var form = context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View("AddEdit", form);
        }

        [HttpPost]
        public IActionResult Edit(Bowler blah)
        {
            context.Update(blah);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int bowlerid)
        {
            var form = context.Bowlers.Single(x => x.BowlerID == bowlerid);

            return View(form);
        }

        [HttpPost]
        public IActionResult Delete(Bowler bow)
        {
            context.Bowlers.Remove(bow);
            context.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}
