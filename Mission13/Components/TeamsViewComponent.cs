using Microsoft.AspNetCore.Mvc;
using Mission13.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mission13.Components
{
    public class TeamsViewComponent : ViewComponent
    {
        private BowlingDbContext context { get; set; }

        public TeamsViewComponent (BowlingDbContext temp)
        {
            context = temp;
        }

        public IViewComponentResult Invoke()
        {
            // This helps build the filter list and filter the heading
            ViewBag.SelectedTeam = RouteData?.Values["teamName"]; //lets it be okay when null

            var teams = context.Teams
                .Select(x => x.TeamName)
                .Distinct()
                .OrderBy(x => x);

            return View(teams);
        }

    }
}
