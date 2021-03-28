using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using FlipShop_OnlineShopping.Models;
using FlipShop_OnlineShopping.Models.ViewModel.RoleViewModels;

namespace FlipShop_OnlineShopping.Areas.Admin.Controllers
{
    [Area("Admin")]
    //[Authorize(Roles = "Admin")]
    public class RoleController : Controller
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleController(RoleManager<Role> roleManager)
        {

            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var model = _roleManager.Roles.Select(r => new
            {
                RoleName = r.Name,
                Id = r.Id,
                Description = r.Description,
                NumberOfUsers = "-"
            }).ToList()
                .Select(r => new RoleListViewModel
                {
                    RoleName = r.RoleName,
                    Id = r.Id,
                    Description = r.Description,
                    NumberOfUsers = 0
                }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult New()
        {
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> New(NewRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new Role
                {
                    Name = model.Name,
                    Description = model.Description
                };
                var roleResult = await _roleManager.CreateAsync(role);
                if (roleResult.Succeeded)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            if (!string.IsNullOrEmpty(id))
            {
                var roleInDb = await _roleManager.FindByIdAsync(id);
                if (roleInDb != null)
                {
                    var roleResult = _roleManager.DeleteAsync(roleInDb).Result;
                    if (roleResult.Succeeded)
                    {
                        //return Json(new ResultJson() {Message = "Success", Status = true});
                        return RedirectToAction("Index");
                    }
                }
            }
            //return Json(new ResultJson() { Message = "Error", Status = false });
            return RedirectToAction("Error", "Home");

        }
    }
}
