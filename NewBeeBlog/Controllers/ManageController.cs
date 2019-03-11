﻿using NewBeeBlog.App_Code;
using NewBeeBlog.Models;
using NewBeeBlog.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace NewBeeBlog.Controllers
{
    public class ManageController : Controller
    {
        
        // GET: Manage
        public ActionResult Index()
        {
            return View();
        }
        // GET: Register
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        // Post: Register
        [HttpPost]
        public ActionResult Register(RegisterUser model)
        {
            if (ModelState.IsValid)
            //判断是否验证通过
            {
                string sessionValidCode = Session["validatecode"]==null?string.Empty: Session["validatecode"].ToString();
                if (!model.Code.Equals(sessionValidCode))
                {
                    return Content("验证码输入错误");
                }
                var user = new User();
                user.Account = model.Account;
                user.Password = md5tool.GetMD5(model.Password);//需要md5加密否则是明文传输
                int res = 0;
                try
                {
                    using (NewBeeBlogContext dbContent = new NewBeeBlogContext())
                    //防止并发错误
                    {
                        dbContent.Users.Add(user);
                        dbContent.SaveChanges();
                        //保存数据库
                        //SaveChanges返回的是一个int，大于0则正确直接调转到首页
                    }

                }
                catch (Exception)
                {
                    return Redirect("数据库异常");
                    throw;
                }           
            }
            return Redirect("/");
        }
        // GET: Manage
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(LoginUser model)
        {
            if (ModelState.IsValid)
            {
                string sessionValidCode = Session["validatecode"] == null ? string.Empty : Session["validatecode"].ToString();
                if (!model.Code.Equals(sessionValidCode))
                {
                    return Content("验证码输入错误");
                }
                var user = new User();
                user.Account = model.Account;
                user.Password = md5tool.GetMD5(model.Password);
                int res = 0;
                //根据用户名查找实体
                using (NewBeeBlogContext dbContent = new NewBeeBlogContext())
                {
                    var nameUser = dbContent.Users.FirstOrDefault(m => m.Account == model.Account);
                    if (nameUser == null)
                    {
                        return Content("账号或密码不正确");
                    }
                    else
                    {
                        if (user.Password == nameUser.Password)
                        {
                            Session["loginuser"] = nameUser;
                            return Redirect("/");
                        }
                        else
                        {
                            return Content("账号或密码不正确");
                        }
                    }
              
                    
                }
            }
            return View();
        }
    }
}