using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcAppMain.Models
{
    public enum UserStatus
    {
        AuthenticatedAdmin,
        AuthenticatedUser,
        NonAuthenticatedUser
    }
}