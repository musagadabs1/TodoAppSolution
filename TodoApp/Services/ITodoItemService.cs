using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoItemService
    {
        Task<TodoItem[]> GetInCompleteItemsAsync();
    }
}
