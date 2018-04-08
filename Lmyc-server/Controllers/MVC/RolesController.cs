using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lmyc_server.Data;
using Lmyc_server.Models;
using Microsoft.AspNetCore.Identity;

namespace Lmyc_server.Controllers.MVC
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Roles
        public async Task<IActionResult> Index()
        {
            var roles = _roleManager.Roles;
            return View(await roles.ToListAsync());
        }

        // GET: Roles/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var identityRole = await _roleManager.FindByIdAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }
            var usersInRole = _userManager.GetUsersInRoleAsync(identityRole.Name).Result;
            var role = new RoleViewModel
            {
                RoleName = identityRole.Name,
                RoleId = identityRole.Id,
                Users = usersInRole
            };
            //Get the users that are not in the role
            var notInrole = await _userManager.Users.ToListAsync();
            foreach (var user in usersInRole)
            {
                if (notInrole.Contains(user))
                {
                    notInrole.Remove(user);
                }
            }
            ViewData["NotInRole"] = notInrole;
            return View(role);
        }

        // GET: Roles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Roles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] IdentityRole newRole)
        {
            if (ModelState.IsValid)
            {
                if (!await _roleManager.RoleExistsAsync(newRole.Name))
                {
                    await _roleManager.CreateAsync(newRole);
                    return RedirectToAction(nameof(Index));
                }
            }
            return View();
        }

        // GET: Roles/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            if(id == _roleManager.FindByNameAsync("Admin").Id.ToString())
            {
                return RedirectToAction(nameof(Index));
            }

            var identityRole = await _roleManager.FindByIdAsync(id);
            if (identityRole == null)
            {
                return NotFound();
            }

            var users = _userManager.GetUsersInRoleAsync(identityRole.Name).Result;
            var role = new RoleViewModel
            {
                RoleName = identityRole.Name,
                RoleId = identityRole.Id,
                Users = users
            };
            return View(role);
        }

        // POST: Roles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == _roleManager.FindByNameAsync("Admin").Id.ToString())
            {
                return RedirectToAction(nameof(Index));
            }
            var role = await _roleManager.FindByIdAsync(id);

            if (role != null)
            {
                await _roleManager.DeleteAsync(role);
            }

            return RedirectToAction(nameof(Index));
        }


        //POST: Roles/Remove/id
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(RoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user != null || user != await _userManager.FindByNameAsync("a@a.a"))
            {
                await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            }
            return RedirectToAction(nameof(Details), new { id = model.RoleId });
        }


        //POST: Roles/Remove/id
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(RoleViewModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user != null)
            {
                await _userManager.AddToRoleAsync(user, model.RoleName);
            }
            return RedirectToAction(nameof(Details), new { id = model.RoleId });
        }

        private bool RoleViewModelExists(string id)
        {
            return _context.RoleViewModel.Any(e => e.RoleId == id);
        }
    }
}
