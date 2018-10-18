using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class FakeTodoItemService : ITodoItemService
    {
        public Task<TodoItem[]> GetInCompleteItemsAsync()
        {
            var item1 = new TodoItem()
            {
                Title="Learn ASP.NET Core",
                DueAt=DateTimeOffset.Now.AddDays(1)
            };
            var item2 = new TodoItem()
            {
                Title = "Build Awesome Apps",
                DueAt=DateTimeOffset.Now.AddDays(2)
            };

            return Task.FromResult(new[] { item1, item2 });
        }
    }
}
