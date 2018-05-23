namespace MvcAppMain.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;

    public partial class UserDetail
    {
        [StringLength(7, MinimumLength = 2, ErrorMessage = "UserName length should be between 2 and 7")]
        [Key]
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public virtual UserType UserType { get; set; }
    }
}