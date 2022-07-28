using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using restaurant.Models;
using restaurant.Services;
using restaurant.Areas.Identity.Data;
using System;
using System.Security.Claims;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

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


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Post post)
        {
            
            if (!ModelState.IsValid)
            {
                TempData["createFailed"] = "Fill in everything";
                return View();
            }

            postService.Create(post);
            TempData["createMessage"] = "Successfully created one post!";
            return RedirectToAction(nameof(Create));
        }


        [HttpGet]
        public IActionResult Delete()
        {           
            return View();
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            
            Post post = postService.GetById(id);

            if (post == null)
            {
                TempData["noPostFound"] = "There is no post with this id";
                return View();
            }

            postService.Delete(id);
            TempData["deleteMessage"] = "Successfully deleted post!";
            return RedirectToAction(nameof(Delete));
        }


        [HttpGet]
        public IActionResult SelectPostEdit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(int id)
        {
            Post post = postService.GetById(id);  //include not working?

            if (post == null)
            {
                TempData["noPostFound"] = "There is no post with this id";
                return RedirectToAction(nameof(SelectPostEdit));
            }

            return View(post);
        }
        [HttpPost]
        public IActionResult EditPost( Post post)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            postService.Edit(post);
            TempData["editMessage"] = "Successfully edited post!";
            return RedirectToAction(nameof(SelectPostEdit));
        }
    }
}
