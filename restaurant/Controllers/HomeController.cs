using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using restaurant.Models;
using restaurant.Models.Commands;
using restaurant.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace restaurant.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private CommentService _commentService;
        private RestaurantService _restaurantService;

        public HomeController(ILogger<HomeController> logger,
            CommentService commentService,
            RestaurantService restaurantService)
        {
            _logger = logger;
            _commentService = commentService;
            _restaurantService = restaurantService;
        }

        public IActionResult Index()
        {
            return RedirectToAction(nameof(Index), "Post");
        }

        public async Task<IActionResult> Comment()
        {
            var allRest = await _restaurantService.GetAll();
            return View(allRest);
        }

        [HttpPost]
        public async Task<IActionResult> PostComment(AddCommentCommand cmd)
        {
            await _commentService.AddComment(cmd);
            return Ok();
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
