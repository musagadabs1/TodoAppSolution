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

        public async Task<bool> AddItemAsync(TodoItem newItem, ApplicationUser user)
        {
            newItem.Id = Guid.NewGuid();
            newItem.IsDone = false;
            newItem.DueAt = DateTimeOffset.Now.AddDays(3);
            _db.Items.Add(newItem);

            var saveResult = await _db.SaveChangesAsync();
            return saveResult == 1;
            //throw new NotImplementedException();
        }

        public async Task<TodoItem[]> GetInCompleteItemsAsync(ApplicationUser user)
        {
            var items = await _db.Items.Where(x => x.IsDone == false && x.UserId==user.Id).ToArrayAsync();
            return items;
        }

        public async Task<bool> MarkDoneAsync(Guid id,ApplicationUser user)
        {
           // var currentUser= await 
            var item = await _db.Items.Where(x => x.Id == id && x.UserId==user.Id).SingleOrDefaultAsync();
            if (item==null)
            {
                return false;
            }
            item.IsDone = true;
            var saveResult = await _db.SaveChangesAsync();
            return saveResult == 1;
        }

    }
}
