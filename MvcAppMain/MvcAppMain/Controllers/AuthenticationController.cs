using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcAppMain.BUS;
using MvcAppMain.Models;
using MvcAppMain.Help;
namespace QuanLyGaraOto.Controllers
{
    public class AuthenticationController : Controller
    {
        // GET: Authentication
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DoLogin(FormCollection form)
        {
            if (ModelState.IsValid)
            {
                UserBusinessLayer userBusiness = new UserBusinessLayer();
                var userName = form["UserName"].ToString();
                var passWord = form["Password"].ToString();
                passWord = Encryptor.MD5Hash(passWord);
                UserStatus userStatus  =  userBusiness.GetUserStatus(userName, passWord);
                bool IsAdmin = false;
                if (userStatus == UserStatus.AuthenticatedAdmin)
                {
                    IsAdmin = true;
                }
                else if (userStatus == UserStatus.AuthenticatedUser)
                {
                    IsAdmin = false;
                }
                else
                {
                    ModelState.AddModelError("CredentialError", "User name hoặc Password không hợp lệ.");
                    return View("Login");
                }
                FormsAuthentication.SetAuthCookie(userName, false);
                Session["UserName"] = userName;
                Session["IsAdmin"] = IsAdmin;
                if (IsAdmin == true)
                {
                    return RedirectToAction("Index", "UserDetails");
                }
                else
                {
                    return RedirectToAction("Index", "HoSoBenhNhans");
                }
            }
            return View("Login");
        }
        public ActionResult Logout()
        {
            Session["IsAdmin"] = null;
            Session["UserName"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    } 
}