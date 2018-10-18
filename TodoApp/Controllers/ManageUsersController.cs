﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using Microsoft.EntityFrameworkCore;

namespace TodoApp.Controllers
{
    [Authorize(Roles="Administrator")]
    public class ManageUsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ManageUsersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var admins = (await _userManager.GetUsersInRoleAsync("Administrator")).ToArray();

            var everyone = await _userManager.Users.ToArrayAsync();

            var model = new ManageUserViewModel()
            {
                Administrators = admins,
                Everyone = everyone
            };

            return View(model);
        }
    }
}