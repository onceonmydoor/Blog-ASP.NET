﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Blog_ASP.NET.Models
{
    public class Blog_ASPNETContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public Blog_ASPNETContext() : base("name=Blog_ASPNETContext")
        {
        }

        public System.Data.Entity.DbSet<Blog_ASP.NET.Models.User> Users { get; set; }

        public System.Data.Entity.DbSet<Blog_ASP.NET.Models.TextList> TextLists { get; set; }

        public System.Data.Entity.DbSet<Blog_ASP.NET.Models.CommitList> CommitLists { get; set; }
    }
}
