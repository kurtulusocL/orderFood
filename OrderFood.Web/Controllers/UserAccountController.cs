using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using OrderFood.Data.Data;
using OrderFood.Data.Identity;
using OrderFood.Entity.UserModels;
using OrderFood.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace OrderFood.Web.Controllers
{
    [Audit]   
    public class UserAccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public UserAccountController()
        {
            ApplicationDbContext db = new ApplicationDbContext();
            UserStore<ApplicationUser> userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
            RoleStore<ApplicationRole> roleStore = new RoleStore<ApplicationRole>(db);
            roleManager = new RoleManager<ApplicationRole>(roleStore);
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login model)
        {
            ApplicationUser user = userManager.Find(model.Username, model.Password);
            if (user != null)
            {
                if (user.IsConfirmed)
                {
                    var username = user.UserName;
                    Session["KullanıcıAdıSoyadı"] = user.NameLastname;
                    Session["Kullanıcı"] = username;
                    Session["userId"] = user.Id;
                    Session["userName"] = User.Identity.Name;
                    Session["cityName"] = user.City;

                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProps, identity);
                    return RedirectToAction("Index", "HomeUser");
                }
                else
                {
                    TempData["confirm"] = "Su cuenta ha sido suspendida temporalmente. Si cree que se debe a un error, no dude en hacérnoslo saber.";
                    return RedirectToAction("LoginUser");
                }
            }
            else
            {
                TempData["msg"] = "Su nombre de usuario y contraseña son incorrectos. Inténtalo de nuevo.";
                return RedirectToAction("LoginUser");
            }
        }
        public ActionResult LoginUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LoginUser(Login model)
        {
            ApplicationUser user = userManager.Find(model.Username, model.Password);
            if (user != null)
            {
                if (user.IsConfirmed == true)
                {
                    var username = user.UserName;
                    Session["KullanıcıAdıSoyadı"] = user.NameLastname;
                    Session["Kullanıcı"] = username;
                    Session["userId"] = user.Id;
                    Session["userName"] = User.Identity.Name;
                    Session["cityName"] = user.City;

                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProps, identity);
                    return RedirectToAction("Index", "HomeUser");
                }
                else
                {
                    TempData["confirm"] = "Su cuenta ha sido suspendida temporalmente. Si cree que se debe a un error, no dude en hacérnoslo saber.";
                    return RedirectToAction("LoginUser");
                }
            }
            else
            {
                TempData["msg"] = "Su nombre de usuario y contraseña son incorrectos. Inténtalo de nuevo.";
                return RedirectToAction("LoginUser");
            }
        }

        [Authorize(Roles = "Users")]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Register model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();

                user.UserName = model.Username;
                user.NameLastname = model.NameSurname;
                user.Email = model.Email;
                user.Birthdate = model.Birthdate;
                user.Province = model.Province;
                user.City = model.City;
                user.Country = model.Country;
                user.Gender = model.Gender;
                user.PhoneNumber = model.PhoneNumber;
                user.CreatedDate = model.CreatedDate.ToLocalTime();

                IdentityResult iResult = userManager.Create(user, model.Password);
                if (iResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Users");
                    return RedirectToAction("LoginUser");
                }
                if (!iResult.Succeeded)
                {
                    TempData["err"] = "Este usuario está utilizado.";
                    TempData["msg"] = "Tu registro falló. Inténtalo de nuevo.";
                    return RedirectToAction("Register");
                }
            }
            return View(model);
        }

        [Authorize(Roles = "Users")]
        public ActionResult ChangeProfile()
        {
            ApplicationUser user = userManager.FindByName(HttpContext.User.Identity.Name);
            ChangeProfile model = new ChangeProfile();

            model.Birthdate = Convert.ToDateTime(user.Birthdate);
            model.Country = user.Country;
            model.Province = user.Province;
            model.City = user.City;
            model.PhoneNumber = user.PhoneNumber;
            model.UpdatedDate = Convert.ToDateTime(user.UpdatedDate);
            model.IsConfirm = user.IsConfirmed;

            return View(model);
        }

        [Authorize(Roles = "Users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeProfile(ChangeProfile model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = userManager.FindByName(HttpContext.User.Identity.Name);

                user.Birthdate = model.Birthdate;
                user.Country = model.Country;
                user.Province = model.Province;
                user.City = model.City;
                user.PhoneNumber = model.PhoneNumber;
                user.UpdatedDate = model.UpdatedDate;
                user.IsConfirmed = model.IsConfirm;

                IdentityResult result = userManager.Update(user);
                if (result.Succeeded)
                {
                    ViewBag.Message = "Se ha cambiado la información del perfil.";
                    return RedirectToAction("Index", "HomeUser");
                }
                else
                {
                    TempData["msg"] = "Su nombre de usuario y contraseña son incorrectos. Inténtalo de nuevo.";
                    return RedirectToAction("ChangeProfile");
                }
            }
            return View(model);
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForgotPassword(ForgotPassword model)
        {
            if (ModelState.IsValid)
            {
                var user = userManager.FindByEmail(model.Email);
                if (user == null || !(userManager.IsEmailConfirmed(user.Id)))
                {
                    return View(nameof(ForgotPasswordConfirmation));
                }
                var provider = new DpapiDataProtectionProvider("OrderFood.Web");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
                var code = userManager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "UserAccount",
                new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);

                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("emailaddress", "www.provechoo.com");
                mail.Priority = MailPriority.High;
                mail.Subject = "Forgot to My Password";
                mail.To.Add(new MailAddress(model.Email, ""));
                mail.Body = "Reset Password" + " " + "Please reset your password by clicking here: <a href=\"" + callbackUrl + "\">link</a>";
                mail.IsBodyHtml = true;

                NetworkCredential enter = new NetworkCredential("emailaddress", "password");
                client.UseDefaultCredentials = false;
                client.EnableSsl = true;
                client.Credentials = enter;
                client.Send(mail);

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View(model);
        }

        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPassword model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user = userManager.FindByEmail(model.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                else
                {
                    var provider = new DpapiDataProtectionProvider("OrderFood.Web");
                    userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
                    var result = userManager.ResetPassword(user.Id, model.Code, model.Password);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("LoginUser");
                    }
                }
            }
            return View();
        }
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize(Roles = "Users")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Users")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = userManager.FindByName(HttpContext.User.Identity.Name);
                IdentityResult result = userManager.ChangePassword(user.Id, model.OldPassword, model.NewPassword);
                if (result.Succeeded)
                {
                    IAuthenticationManager authenticationManager = HttpContext.GetOwinContext().Authentication;
                    authenticationManager.SignOut();
                    return RedirectToAction("LoginUser");
                }
                else
                {
                    TempData["msg"] = "Se produjo un error al cambiar la contraseña. Verifique su transacción y vuelva a intentarlo.";
                    return RedirectToAction("ChangePassword");
                }
            }
            return View(model);
        }
    }
}