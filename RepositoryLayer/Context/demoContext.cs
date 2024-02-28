using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class demoContext:DbContext

    {
        public demoContext(DbContextOptions options) : base(options) { }
        public DbSet<DemoEntity> DemoTable { get; set; }
        public DbSet<user> UserTable { get; set; }
        public DbSet<NotesEntity> NotesTable { get; set; }
    }
}
 