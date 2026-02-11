using Microsoft.EntityFrameworkCore;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<RoleType>().HasData(
            new RoleType
            {
                ROLETYPE_ID = new Guid("257b4011-ae28-4054-9194-c6045ab11c81"),
                ROLETYPE_NAME = "Admin",
                ROLETYPE_DESCRIPTION = "Administrator",
                ROLETYPE_LOCKED = false
            },
            new RoleType
            {
                ROLETYPE_ID = new Guid("d7d33ce7-1200-4bce-9b7e-1381061e713b"),
                ROLETYPE_NAME = "Staff",
                ROLETYPE_DESCRIPTION = "Staff",
                ROLETYPE_LOCKED = false
            },
            new RoleType
            {
                ROLETYPE_ID = new Guid("ce92ed6e-69a4-45fd-a5be-40c382cfdff0"),
                ROLETYPE_NAME = "Client",
                ROLETYPE_DESCRIPTION = "Client",
                ROLETYPE_LOCKED = false
            }
        );
    }
}