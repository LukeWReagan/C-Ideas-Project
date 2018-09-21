using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using bank.Models;
using System.Linq;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;




namespace bank.Controllers {
    public class HomeController : Controller {
        private MyContext _context;
        public HomeController(MyContext context) {
            _context = context;
        }
        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
           
            return View();
        }

        [HttpPost]
        [Route("login")]
        public IActionResult login(loginVal newLogin)
        {
            if(ModelState.IsValid)
            {
            var user = _context.user.Where(a => a.Email == newLogin.emailLogin).SingleOrDefault();
            if(user == null)
            {
                ViewBag.Error="Invalid Ligin";
                return View("Index");
            }
            if(user != null && newLogin.passwordLogin != null)
            {
                var Hasher = new PasswordHasher<User>();
                
                if(0 != Hasher.VerifyHashedPassword(user, user.Password, newLogin.passwordLogin))
                {
                   HttpContext.Session.SetInt32("User_id", user.User_id);
                    return RedirectToAction("Dashboard");
                }
            }
            }
            ViewBag.Error="Invalid Ligin";
            return View("Index");
        }
        [HttpPost]
        [Route("register")]
        public IActionResult Register(User myreg)
        {
           
            
            if(ModelState.IsValid)
            {
                PasswordHasher<User> Hasher = new PasswordHasher<User>();
                myreg.Password = Hasher.HashPassword(myreg, myreg.Password);
                 _context.user.Add(myreg);
                 _context.SaveChanges();
                 List<User> allreg = _context.user.Where(a => a.Email == myreg.Email).ToList();
                 HttpContext.Session.SetInt32("User_id", allreg[0].User_id);
                 
                 
                 return RedirectToAction("Dashboard");
            }
            return View("Index"); 
            
        }
        [HttpGet]
        [Route("Dashboard")]
        public IActionResult Dashboard()
        {
            if(HttpContext.Session.GetInt32("User_id") == null) {
                return RedirectToAction("Index");
            }
            User allreg = _context.user.Include(user => user.Like).SingleOrDefault(a => a.User_id == HttpContext.Session.GetInt32("User_id"));
            List<Function> AllFunctions = _context.function
                                        .Include(a => a.post)
                                        .Include(a => a.user)
                                        .ToList();
            List<Key> UserLikes = _context.key.Where(c => c.user.Equals(allreg)).ToList();
            ViewBag.User_id = HttpContext.Session.GetInt32("User_id");
            ViewBag.AllFunctions = AllFunctions;
            ViewBag.CurrentUser = allreg;
            ViewBag.User_id = HttpContext.Session.GetInt32("User_id");
            ViewBag.UserLike = UserLikes;
            return View();
        }
        [HttpGet]
        [Route("Stats/{id}")]
        public IActionResult Info(int id){
            var UserInfo = _context.user.Include(a => a.AllPosts).Include(b => b.Like).SingleOrDefault(u => u.User_id == id);
            ViewBag.UserInfo = UserInfo;
            return View("Stats");
        }

        [HttpPost]
        [Route("Idea")]
        public IActionResult Idea(Function myFunction)
        {
            if (myFunction == null || myFunction.Posts == null) {
                TempData["idea_error"] = true;
                return RedirectToAction("Dashboard");
            }
            if (myFunction.Posts.Length <= 2) {
                TempData["idea_error"] = true;
                return RedirectToAction("Dashboard");
            }
            myFunction.Userid= (int)HttpContext.Session.GetInt32("User_id");
            _context.function.Add(myFunction);
            _context.SaveChanges();

            return RedirectToAction("Dashboard");
        }
            


        [HttpGet]
        [Route("Like/{Functionid}")]
        public IActionResult Like(int Functionid) {
            if(HttpContext.Session.GetInt32("User_id") == null) {
                return RedirectToAction("Index", "User");
            }
            User CurrentUser = _context.user.SingleOrDefault(user => user.User_id == HttpContext.Session.GetInt32("User_id"));

            Key NewLike = new Key {
                User_id = CurrentUser.User_id,
                Functionid = Functionid,
            };
            _context.key.Add(NewLike);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
              
        [HttpGet]
        [Route("Delete/{Functionid}")]
        public IActionResult Delete(int Functionid) {
            if(HttpContext.Session.GetInt32("User_id") == null) {
                return RedirectToAction("Index", "User");
            }
            Function CurrentFunction = _context.function
            .SingleOrDefault(a => a.Functionid == Functionid);
            _context.function.Remove(CurrentFunction);
            _context.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("Display/{Functionid}")]
        public IActionResult Display(int Functionid)
        {
            Function CurrentFunction = _context.function.Include(a => a.user).Include(b => b.post).ThenInclude(c => c.user)
            .SingleOrDefault(a => a.Functionid == Functionid);
            ViewBag.CurrentFunction = CurrentFunction;
            return View();
        }
        [HttpGet]
        [Route("/Loggout")]
        public IActionResult Loggout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}