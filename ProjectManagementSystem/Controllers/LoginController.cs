using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjectManagementSystem.Models;

namespace ProjectManagementSystem.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autherize(ProjectManagementSystem.Models.EMPLOYEE userModel)
        {
            using (ProjectManagementSystemEntities db = new ProjectManagementSystemEntities())
            {
                var userDetails = db.EMPLOYEEs.Where(x => x.F_name == userModel.F_name && x.L_name == userModel.L_name).FirstOrDefault();
                if (userDetails == null)
                {
                    userModel.LoginErrorMessage = "Wrong username or password.";
                    return View("Index", userModel);
                }
                else
                {
                    if (userDetails.Employee_type == "2")
                    {
                        return RedirectToAction("Index", "HomeForClient");
                    }
                    else
                    {
                        if (userDetails.Employee_type == "3")
                        {
                            return RedirectToAction("Index", "HomeForEmployee");
                        }
                        else
                        {
                            return RedirectToAction("Index", "Home");
                        }
                    }
                }
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }
    }
}