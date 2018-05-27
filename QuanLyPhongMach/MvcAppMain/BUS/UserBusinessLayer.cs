using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcAppMain.Models;
namespace MvcAppMain.BUS
{
    public class UserBusinessLayer
    {
        public UserStatus GetUserStatus(string userName, string passWord)
        {
            QLPMContext db = new QLPMContext();
            UserDetail user = db.UserDetails.Find(userName.Trim());
            if (user != null)
            {
                if (user.Password != passWord) return UserStatus.NonAuthenticatedUser;
                if (user.UserTypeID == 1)
                    return UserStatus.AuthenticatedAdmin;
                else if (user.UserTypeID == 2)
                    return UserStatus.AuthenticatedUser;
                else
                    return UserStatus.NonAuthenticatedUser;
            }
            else
                return UserStatus.NonAuthenticatedUser;
        }
    }
}