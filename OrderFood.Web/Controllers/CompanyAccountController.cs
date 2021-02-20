using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using OrderFood.Data.Data;
using OrderFood.Data.Identity;
using OrderFood.Entity.CompanyModels;
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
    public class CompanyAccountController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private RoleManager<ApplicationRole> roleManager;

        public CompanyAccountController()
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
                if (user.IsConfirmed == true)
                {
                    var username = user.UserName;
                    Session["KullanıcıAdıSoyadı"] = user.NameLastname;
                    Session["Kullanıcı"] = username;
                    Session["TiendaId"] = user.Id;

                    IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
                    ClaimsIdentity identity = userManager.CreateIdentity(user, "ApplicationCookie");
                    AuthenticationProperties authProps = new AuthenticationProperties();
                    authProps.IsPersistent = model.RememberMe;
                    authManager.SignIn(authProps, identity);
                    return RedirectToAction("Index", "HomeCompany");
                }
                else
                {
                    TempData["confirm"] = "Hesabınız geçici olarak askıya alınmıştır. Bunun bir yanlışlıktan kaynaklandığını düşünüyorsanız lütfen bize bildirmekten çekinmeyin.";
                    return RedirectToAction("Login");
                }
            }
            else
            {
                TempData["msg"] = "Kullanıcı adı ve şifreniz yanlış. Lütfen tekrar deneyiniz.";
                return RedirectToAction("Login");
            }
        }

        [Authorize(Roles = "Tienda")]
        public ActionResult Logout()
        {
            IAuthenticationManager authManager = HttpContext.GetOwinContext().Authentication;
            authManager.SignOut();
            return RedirectToAction(nameof(Login));
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
                user.City = model.City;
                user.Country = model.Country;
                user.PhoneNumber = model.PhoneNumber;
                user.Province = model.Province;
                user.RespondTitle = model.RespondTitle;

                IdentityResult iResult = userManager.Create(user, model.Password);
                if (iResult.Succeeded)
                {
                    userManager.AddToRole(user.Id, "Tienda");
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["msg"] = "Kayıt işleminiz başarısız oldu. Lütfen tekrar deneyiniz";
                    return RedirectToAction("Register");
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
                ApplicationUser user = new ApplicationUser();
                user = userManager.FindByEmail(model.Email);
                if (user == null || !(userManager.IsEmailConfirmed(user.Id)))
                {
                    return View(nameof(ForgotPasswordConfirmation));
                }
                var provider = new DpapiDataProtectionProvider("OrderFood.Web");
                userManager.UserTokenProvider = new DataProtectorTokenProvider<ApplicationUser>(provider.Create("EmailConfirmation"));
                var code = userManager.GeneratePasswordResetToken(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "CompanyAccount", new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);

                SmtpClient client = new SmtpClient("smtp.yandex.com.tr", 587);
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("emailaddress", "www.provecho.com");
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

                return RedirectToAction("ForgotPasswordConfirmation");
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
                        return RedirectToAction("Login");
                    }
                }
            }

            return View();
        }
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Authorize(Roles = "Tienda")]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [Authorize(Roles = "Tienda")]
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
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    TempData["msg"] = "Şifre değiştirilirken hata meydana geldi. Lütfen işleminizi kontrol edip yeniden deneyiniz.";
                    return RedirectToAction("ChangePassword");
                }
            }
            return View(model);
        }
    }
}