using Microsoft.AspNetCore.Mvc;
using restaurant.Models;
using restaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Controllers
{
    public class PostController : Controller
    {
        private PostService postService;

        public PostController(PostService postService)
        {
            this.postService = postService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filter()
        {
            List<Post> posts = new List<Post>();
            return View(posts);
        }
        [HttpPost]
        public IActionResult Filter(string filterType, string searchedWord)
        {
            List<Post> posts = postService.FilteredPosts(filterType,searchedWord);

            return View(posts);
        }
    }
}
