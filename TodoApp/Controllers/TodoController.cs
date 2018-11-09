using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [Authorize]
    public class TodoController : Controller
    {
        private ITodoItemService _todoItemService;
        private UserManager<IdentityUser> _userManager;

        public TodoController(ITodoItemService todoItemService,UserManager<IdentityUser> userManager)
        {
            _todoItemService = todoItemService;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {

            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser==null)
            {
                return Challenge();
            }
            // Get to-do items from database
            var items = await _todoItemService.GetInCompleteItemsAsync(currentUser);

            // Put items into a model
            var model = new TodoViewModel()
            {
                Items = items
            };
            // Pass the view to a model and render
            return View(model);
        }
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddItem(TodoItem newItem)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var currentUser = await _userManager.GetUserAsync(User);

            if (currentUser==null)
            {
                return Challenge();
            }
            var success = await _todoItemService.AddItemAsync(newItem,currentUser);
            if (!success)
            {
                return BadRequest("Could not add Item");
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MarkDone(Guid id)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser==null)
            {
                return Challenge();
            }
            if (id==Guid.Empty)
            {
                return RedirectToAction("Index");
            }
            var successful = await _todoItemService.MarkDoneAsync(id,currentUser);
            if (!successful)
            {
                return BadRequest("Could not mark item as done");
            }
            return RedirectToAction("Index");
        }
    }
}