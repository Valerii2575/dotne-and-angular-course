﻿using DatingAppCourse.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace DatingAppCourse.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Photo> Photos { get; set; }
    }
}
