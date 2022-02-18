using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using CRUDelicious.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace CRUDelicious.Controllers
{
    public class HomeController : Controller
    {

        private CrudeliciousContext db;
        public HomeController(CrudeliciousContext context)
        {
            db = context;
        }


        [HttpGet("")]
        public IActionResult Index()
        {
            List<Post> allPosts = db.Posts.ToList();
            return View("Index", allPosts);
        }

        [HttpGet("/new")]
        public IActionResult New()
        {
            return View("New");
        }

        [HttpPost("create")]
        public IActionResult Create(Post newPost)
        {
            if(ModelState.IsValid == false)
            {
                return View("New");
            }

            db.Posts.Add(newPost);

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet("/{dishId}")]
        public IActionResult Details(int dishId)
        {
            Post post = db.Posts.FirstOrDefault(p => p.DishId == dishId);

            if(post == null)
            {
                return RedirectToAction("Index");
            }
            return View("Details", post);
        }

        [HttpPost("/{dishId}/delete")]
        public IActionResult Delete(int dishId)
        {
            Post post = db.Posts.FirstOrDefault(p => p.DishId == dishId);

            if(post != null)
            {
                db.Posts.Remove(post);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet("/edit/{dishId}")]
        public IActionResult Edit(int dishId)
        {
            Post post = db.Posts.FirstOrDefault(p => p.DishId == dishId);

            if (post == null)
            {
                return RedirectToAction("Index");
            }

            return View("Edit", post);
        }

        [HttpPost("/update/{dishId}")]
        public IActionResult Update(Post editedPost, int dishId)
        {
            if(ModelState.IsValid == false)
            {
                editedPost.DishId = dishId;
                return View("Edit", editedPost);
            }

            Post dbPost = db.Posts.FirstOrDefault(p => p.DishId == dishId);
            if(dbPost == null)
            {
                return RedirectToAction("Index");
            }

            dbPost.Name = editedPost.Name;
            dbPost.Chef = editedPost.Chef;
            dbPost.Tastiness = editedPost.Tastiness;
            dbPost.Calories = editedPost.Calories;
            dbPost.Description = editedPost.Description;
            dbPost.UpdatedAt = DateTime.Now;

            db.Posts.Update(dbPost);
            db.SaveChanges();

            return RedirectToAction("Details", new { dishId = dishId});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
