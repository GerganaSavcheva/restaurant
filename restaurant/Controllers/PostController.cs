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
        private List<Post> filteredPosts;

        public PostController(PostService postService)
        {
            this.postService = postService;
            this.filteredPosts = new List<Post>();
        }


        public IActionResult Index()
        {
            List<Post> posts = postService.GetAll();
            TempData["GoToHomePage"] = null;

            return View(posts);
        }

        [HttpGet]
        public IActionResult ShowOne(int id,string searchedWord, string filterType)
        {
            TempData["filterTypeAndsearchedWord"] = filterType+" "+ searchedWord;
            if (filterType!=null || searchedWord!=null)
            {
                TempData["GoToHomePage"] = false;

            }

            return View(postService.GetById(id));
        }



        [HttpGet]
        public IActionResult Filter(string filterTypeAndsearchedWord)
        {
            List<Post> posts = new List<Post>();

            if (filterTypeAndsearchedWord!=null)
            {
                string[] filterType_searchedWord = filterTypeAndsearchedWord.Split(" ");
                TempData["filterType"] = filterType_searchedWord[0];
                TempData["searchedWord"] = filterType_searchedWord[1];

                filteredPosts=postService
                    .FilteredPosts(TempData["filterType"].ToString(), TempData["searchedWord"].ToString());
            }

            return View(filteredPosts);
        }
        [HttpPost]
        public IActionResult Filter(string filterType, string searchedWord)
        {
                        
            TempData["GoToHomePage"] = false;

            if (filterType!=null || searchedWord!=null)
            {
                TempData["filterType"] = filterType;
                TempData["searchedWord"] = searchedWord;
            }

            try
            {
                filteredPosts = postService
                    .FilteredPosts(TempData["filterType"].ToString(), TempData["searchedWord"].ToString());

                if (filteredPosts.Count == 0)
                {
                    TempData["noRestaurantFoundMessage"] = "No such restaurant found";
                }

            }
            catch (NullReferenceException e)
            {
                TempData["nullReferenceMessage"] = "Choose a filter and write a key word";
            }


            

            return View(filteredPosts);
        }
    }
}
