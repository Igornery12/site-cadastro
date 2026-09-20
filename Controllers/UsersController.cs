using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Site_Cadastro.Data;
using Site_Cadastro.Models;
namespace Site_Cadastro
{
    public class UsersController : Controller
    {
        private readonly AppDbContext _context;

        public UsersController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Signup(string name, string email, string password, string confirmPassword)
        {
            var userExist = _context.Users.Any(u => u.Email == email);

            if (password != confirmPassword)
            {
                ViewBag.Error = "senhas não coincidem";
                return View();
            }

            if (userExist)
            {
                ViewBag.Error = "Email já cadastrado";
                return View();
            }

             var user = new User
            {
                Name = name,
                Email = email
            };
            
            var passwordHasher = new PasswordHasher<User>();

            user.PasswordHash = passwordHasher.HashPassword(
                user,
                password
            );
            _context.Users.Add(user);

            _context.SaveChanges();

            return RedirectToAction("Signin");
        }

        [HttpPost]
        public async Task<IActionResult> Signin(string email, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                ViewBag.Error = "Credenciais inválidas";
                return View();
            }

            var passwordHasher = new PasswordHasher<User>();

            var result = passwordHasher.VerifyHashedPassword(
                user,
                user.PasswordHash,
                password
            );

            if (result == PasswordVerificationResult.Failed)
            {
                ViewBag.Error = "Credenciais inválidas";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme
            );

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal
            );

            return RedirectToAction("HomeApp", "App");
        }
    }
}