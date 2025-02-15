using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookSmartBackEndDatabase.Models;

namespace BookSmartBackEndDatabase;
public class BookSmartContext : DbContext
{
    public BookSmartContext(DbContextOptions<BookSmartContext> options) : base(options)
    {
    }

    public DbSet<User> USERS { get; set; }
    public DbSet<Role> ROLES { get; set; }
    public DbSet<RoleType> ROLETYPES { get; set; }
    public DbSet<Business> BUSINESSES { get; set; }
    public DbSet<Address> ADDRESSES { get; set; }
}