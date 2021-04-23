using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AlphaS_Web.Areas.Identity.Data;
using AlphaS_Web.Models.Utils;
using AlphaS_Web.Contexts;
using Microsoft.AspNetCore.Authorization;

namespace CustomIdentityApp.Controllers
{
    [Authorize(Roles = "admin")]
    public class UsersController : Controller
    {
        UserManager<ApplicationUser> _userManager;
        private readonly CounterContext _counterContext;

        public UsersController(UserManager<ApplicationUser> userManager, CounterContext counterContext)
        {
            _userManager = userManager;
            _counterContext = counterContext;
        }

        public IActionResult Index() => View(_userManager.Users.ToList());

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                int new_id = _counterContext.GetNextId("user");
                ApplicationUser user = new ApplicationUser { UserId = new_id, Email = model.Email, UserName = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }



        [HttpPost]
        public async Task<ActionResult> Delete(string id)
        {
            ApplicationUser user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }
    }
}