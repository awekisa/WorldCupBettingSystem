using BettingSystem.CustomLibraries;
using BettingSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BettingSystem.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using(var db = new BettingSystemDbContext())
            {
                var usernameCheck = db.Users.FirstOrDefault(u => u.Username == model.Username);
                var getPassword = db.Users.Where(u => u.Username == model.Username).Select(u => u.Password);
                var materializePassword = getPassword.ToList();
                var password = materializePassword.Count > 0? materializePassword[0] : "";
                var decryptedPassword = CustomDecrypt.Decrypt(password);

                if (model.Username != null & model.Password == decryptedPassword)
                {
                    var getUsername = db.Users.Where(u => u.Username == model.Username).Select(u => u.Username);
                    var materializeName = getUsername.ToList();
                    var name = materializeName.Count > 0? materializeName[0] : "";
                    var getRoleStatus = db.Users.Where(u => u.Username == model.Username).Select(u => u.IsAdmin);
                    var materializeRole = getRoleStatus.ToList();
                    var role = materializeRole.Count > 0 ? materializeName[0] : "";

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, name),
                        new Claim(ClaimTypes.Role, role)
                    }, "ApplicationCookie");

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut("ApplicationCookie");
            return RedirectToAction("Login", "Auth");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registration(User model)
        {
            if (ModelState.IsValid)
            {
                using (var db = new BettingSystemDbContext())
                {
                    var encryptedPassword = CustomEncrypt.Encrypt(model.Password);
                    var user = db.Users.Create();
                    user.Username = model.Username;
                    user.Password = encryptedPassword;
                    db.Users.Add(user);
                    db.SaveChanges();

                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ModelState.AddModelError("", "One or more fields have been missing!");
            }

            return View();
        }
    }
}