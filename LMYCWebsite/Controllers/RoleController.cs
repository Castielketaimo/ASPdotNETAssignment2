using LmycDataLib.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace LMYCWebsite.Controllers
{
    [Authorize (Roles="Admin")]
    public class RoleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private RoleManager<IdentityRole> roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));

        // GET: Role
        public ActionResult Index()
        {
            var roles = db.Roles.ToList();
            return View(roles);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name")] RoleView Role)
        {
            if (!roleManager.RoleExists(Role.Name))
                roleManager.Create(new IdentityRole(Role.Name));
            return RedirectToAction("Index","Role");
        }

        // GET: Role/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null || id == "Admin")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = roleManager.FindByName(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: Role/Remove/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            IdentityRole role = roleManager.FindByName(id);
            roleManager.Delete(role);
            return RedirectToAction("Index");
        }

        public ActionResult Remove(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = roleManager.FindByName(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleUsers = userManager.Users.Where(u => u.Roles.Any(r => r.RoleId == role.Id));
            ViewBag.RoleName = role.Name;
            return View(roleUsers);
        }

        // POST: Role/Remove/5
        [HttpPost, ActionName("Remove")]
        [ValidateAntiForgeryToken]
        public ActionResult Remove(string id, string userid)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var admin = userManager.FindByName("a");
            if (id == null || userid == "" || (userid == admin.Id && id == "Admin"))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = roleManager.FindByName(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            userManager.RemoveFromRole(userid, id);
            return RedirectToAction("Index");
        }

        public ActionResult Add(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = roleManager.FindByName(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var roleUsers = userManager.Users;
            ViewBag.RoleName = role.Name;
            return View(roleUsers);
        }

        // POST: Role/Add/5
        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult Add(string id, string userid)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (id == null || userid == "")
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IdentityRole role = roleManager.FindByName(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            userManager.AddToRole(userid, id);
            return RedirectToAction("Index");
        }

    }
}