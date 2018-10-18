using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private ITodoItemService _todoItemService;

        public TodoController(ITodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }
        public async Task<IActionResult> Index()
        {
            // Get to-do items from database
            var items = await _todoItemService.GetInCompleteItemsAsync();

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
            var success = await _todoItemService.AddItemAsync(newItem);
            if (!success)
            {
                return BadRequest("Could not add Item");
            }
            return RedirectToAction("Index");
        }
    }
}