using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Webpage.Models;

namespace Webpage.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {


        private RoleManager<ApplicationRole> _roleManager;

        public AdminController(RoleManager<ApplicationRole> roleManager)
        {
            this._roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateRole()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(UserRole userRole)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.CreateAsync(new ApplicationRole() { Name = userRole.RoleName });
                if (result.Succeeded)
                {
                    ViewBag.Message = "Role Created Successfully";
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View();
        }
    }
}
