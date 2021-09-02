using AspNetCoreMvcECommerce.Models;
using AspNetCoreMvcECommerce.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AspNetCoreMvcECommerce.Controllers
{
    [Route("customer")]
    public class CustomerController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        private SecurityManager securityManager = new SecurityManager();

        public CustomerController(DatabaseContext _db)
        {
            db = _db;
        }

        [HttpGet]
        [Route("register")]
        public IActionResult Register()
        {
            var account = new Account();
            return View("Register",account);
        }


        [HttpPost]
        [Route("register")]
        public IActionResult Register(Account account)
        {
            var exists = db.Accounts.Count(a => a.Username.Equals(account.Username)) > 0;
            if (!exists)
            {
                account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);
                account.Status = true;
                db.Accounts.Add(account);
                db.SaveChanges();


                //Add Role Customer to New Account
                var roleAccount = new RoleAccount()
                {
                    RoleId = 2,
                    AccountId = account.Id,
                    Status = true
                };


                db.RoleAccounts.Add(roleAccount);
                db.SaveChanges();

                return RedirectToAction("Login", "Account");
            }
            else
            {
                ViewBag.error = "Kullanıcı Adı Daha Önceden Kaydedilmiş";
                
                return View("Register", account);
            }
            

            
        }



        [HttpGet]
        [Route("login")]
        public IActionResult Login()
        {
            
            return View("Login");
        }



        [HttpPost]
        [Route("login")]
        public IActionResult Login(string username,string password)
        {
            var account = processLogin(username, password);
            if (account != null)
            {
                securityManager.SignIn(this.HttpContext, account);
                return RedirectToAction("dashboard", "customer");
            }
            else
            {
                ViewBag.error = "Geçersiz Hesap";
                return View("Login");
            }

            
        }



        private Account processLogin(string username, string password)
        {
            var account = db.Accounts.SingleOrDefault(a => a.Username.Equals(username) && a.Status == true);
            
            if (account != null )
            {
                var roleOfAccount = account.RoleAccounts.FirstOrDefault();
                if (roleOfAccount.RoleId == 2 && roleOfAccount.Status ==true && BCrypt.Net.BCrypt.Verify(password, account.Password))
                {
                    return account;
                }
            }
            return null;
        }


        [Route("logout")]
        public IActionResult Logout()
        {
            securityManager.SignOut(this.HttpContext);
            return RedirectToAction("login", "customer");
        }

        [Authorize(Roles ="Customer")]
        [HttpGet]
        [Route("profile")]
        public IActionResult Profile()
        {
            var user = User.FindFirst(ClaimTypes.Name);
            var customer = db.Accounts.SingleOrDefault(a => a.Username.Equals(user.Value));
            return View("Profile",customer);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        [Route("profile")]
        public IActionResult Profile(Account account)
        {
            var currentCustomer = db.Accounts.Find(account.Id);
            if (!string.IsNullOrEmpty(account.Password))
            {
                currentCustomer.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);

            }

            currentCustomer.FullName = account.FullName;
            currentCustomer.Email = account.Email;
            db.SaveChanges();
            return View("Profile", currentCustomer);
        }


        
        [HttpGet]
        [Route("dashboard")]
        public IActionResult DashBoard()
        {
            
            return View("DashBoard");
        }
    }
}
