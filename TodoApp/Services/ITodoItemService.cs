using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetInCompleteItemsAsync(ApplicationUser user);
        Task<bool> AddItemAsync(TodoItem newItem,ApplicationUser user);
        Task<bool> MarkDoneAsync(Guid id,ApplicationUser user);
    }
}
