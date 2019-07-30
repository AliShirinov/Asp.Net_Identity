using HRApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApp.Data
{
    public class HRDbContext:IdentityDbContext<User>
    {
        public DbSet<Warehouse> Warehouses { get; set; }
        public  HRDbContext(DbContextOptions<HRDbContext> option): base(option) { }
    }
}
