using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CheeseMVC.Controllers
{
    public class CheeseController : Controller
    {
        static private SortedDictionary<string, string> Cheeses = new SortedDictionary<string, string>();

        public IActionResult Index()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [Route("/Cheese/Add")]
        public IActionResult NewCheese(string name, string description)
        {
            if (IsValid(name))             
            {
                //Cheeses[name] = description;
                if (Cheeses.ContainsKey(name) == false)
                {
                    Cheeses.Add(name, description);
                }
                return Redirect("/Cheese");
            }
            else {
                ViewData["Message"] = "Invalid cheese!";
                return View("Add");
            }
        }

        private bool IsValid(string name)
        {
            var validName = new Regex(@"^[a-zA-Z\s]+$", RegexOptions.IgnoreCase);
            if (name != null && validName.IsMatch(name)) {
                return true;
            } else 
                return false;
        }

        [HttpGet]
        public IActionResult Remove()
        {
            ViewBag.cheeses = Cheeses;
            return View();
        }


        [HttpPost]
        [Route("/Cheese/Remove")]
        public IActionResult Remove_Post(string[] chosenCheeses)
        {
            foreach (string cheese in chosenCheeses)
            {
                if (Cheeses.ContainsKey(cheese))
                {
                    Cheeses.Remove(cheese);
                }
            }
            return Redirect("/Cheese");
        }

        [HttpGet]
        [Route("/Cheese/Remove/{cheeseName}")]
        public IActionResult RemoveX(string cheeseName)
        {
            Cheeses.Remove(cheeseName);
            return Redirect("/cheese");
        }

    }

}
