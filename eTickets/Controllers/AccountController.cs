using eTickets.Data;
using eTickets.Data.Static;
using eTickets.Data.ViewModels;
using eTickets.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(AppDbContext context, UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public  IActionResult Login()=>View(new LoginVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginVM loginVM)
        {
            var user = await _userManager.FindByEmailAsync(loginVM.EmailAddress);
            if (!ModelState.IsValid) return View(loginVM);
            if (user !=null)
            {
                var passwordCheck =await  _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    var result =await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Movies");
                    }
                    TempData["Error"] = "Email Or Password Is Wrong";
                    return View(loginVM);
                }

                TempData["Error"] = "Email Or Password Is Wrong";
                return View(loginVM);


            }
            TempData["Error"] = "Email Or Password Is Wrong";
            return View(loginVM);
        }
        public IActionResult Register() => View(new RegisterVM());

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM)
        {
            var user = await _userManager.FindByEmailAsync(registerVM.EmailAddress);
            if (!ModelState.IsValid) return View(registerVM);
            if (user != null)
            {
                TempData["Error"] = "this Email Address is already Exist!!";
                return View(registerVM);
            }
            var newUser = new ApplicationUser()
            {
                FullName = registerVM.FullName,
                Email = registerVM.EmailAddress,
                UserName = registerVM.EmailAddress.ToUpper()
            };
        var newResponse=await _userManager.CreateAsync(newUser,registerVM.Password);

            if (newResponse.Succeeded)
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
            return View("RegisterCompleted");
            

        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Movies");
        }
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Users()
        {
            var users = await _context.Users.ToListAsync();
            return View(users);
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            return View();
        }

    }
}
