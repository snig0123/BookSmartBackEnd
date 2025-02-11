using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookSmartBackEndDatabase;
public class BookSmartContext : DbContext
{
    public BookSmartContext()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("");

    public DbSet<User> USER { get; set; }
    public DbSet<Worker> WORKER { get; set; }
    public DbSet<Role> ROLE { get; set; }
}