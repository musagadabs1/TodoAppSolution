using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly ApplicationDbContext _db;
        public TodoItemService(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<TodoItem[]> GetInCompleteItemsAsync()
        {
            //var item1 = new TodoItem()
            //{
            //    Title="Learn ASP.NET Core",
            //    DueAt=DateTimeOffset.Now.AddDays(1)
            //};
            //var item2 = new TodoItem()
            //{
            //    Title = "Build Awesome Apps",
            //    DueAt=DateTimeOffset.Now.AddDays(2)
            //};
            var items = await _db.Items.Where(x => x.IsDone == false).ToArrayAsync();
            return items;
        }
    }
}
