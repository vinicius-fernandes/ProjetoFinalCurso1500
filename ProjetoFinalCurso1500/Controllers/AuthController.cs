
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalCurso1500.Data;
using ProjetoFinalCurso1500.Models;
using ProjetoFinalCurso1500.Models.Auth;
using System.Security.Claims;

namespace AppCurso1500.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ProjetoFinalCurso1500Context _context;
        public AuthController(SignInManager<User> singInManager, UserManager<User> userManager, ProjetoFinalCurso1500Context context)
        {
            _signInManager = singInManager;
            _userManager = userManager;
            _context= context;

        }
        #region Registro
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register([Bind("Email,Password,ConfirmPassword,Address,Name")] Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = register.Email, Email = register.Email,Address=register.Address,Name=register.Name };
                var result = await _userManager.CreateAsync(user, register.Password);
                if (result.Succeeded)
                {
                    var client = new Client { Id = Guid.NewGuid().ToString(), User = user };
                    _context.Add(client);
                    await _context.SaveChangesAsync();

                    List<Claim> newClaims = new List<Claim>();

                    var newUserClaim = new Claim("userType", "Salesman");




                    newClaims.Add(newUserClaim);
                    await _userManager.AddClaimsAsync(user, newClaims);

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View();
        }
        #endregion
        #region Login
        [HttpGet]
        public async Task<IActionResult> Login()
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Password,RememberMe")] Login login)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(login.Email, login.Password, login.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    var userId = _userManager.GetUserId(HttpContext.User);


                    var client = await _context.Client.FirstOrDefaultAsync(c => c.UserId == userId);
                    var salesman =await _context.Salesman.FirstOrDefaultAsync(c => c.UserId == userId);

                    string clientId = client != null ? client.Id : "";
                    string salesmanId = salesman != null ? salesman.Id : "";

                    HttpContext.Session.SetString("ClientId", clientId);

                    HttpContext.Session.SetString("SalesmanId", salesmanId);



                    return RedirectToAction("Index", "Home");



                }

                ModelState.AddModelError(string.Empty, "Tentativa de login inválida cheque seu e-mail e senha");

            }
            return View();
        }
        #endregion
        #region Logout
        [HttpGet]
        public IActionResult Logout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout(string? returnUrl = null)
        {
            await _signInManager.SignOutAsync();

            if (returnUrl != null)
            {
                return LocalRedirect(returnUrl);
            }
            return View();
        }
        #endregion
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AccessDenied() { return View(); }
    }
}