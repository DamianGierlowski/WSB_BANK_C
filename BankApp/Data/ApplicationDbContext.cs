using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BankApp.Models;
using Microsoft.AspNetCore.Identity;

namespace BankApp.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<BankApp.Models.Account>? Account { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Ignore <IdentityUserLogin<string>>();
        
        string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
        string ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";

        //seed admin role
        builder.Entity<IdentityRole>().HasData(new IdentityRole { 
            Name = "Admin", 
            NormalizedName = "ADMIN", 
            Id = ROLE_ID,
            ConcurrencyStamp = ROLE_ID
        });

        //create user
        var appUser = new IdentityUser() { 
            Id = ADMIN_ID,
            Email = "admin@admin.com",
            EmailConfirmed = true,
            UserName = "admin@admin.com",
            NormalizedUserName = "ADMIN@ADMIN.COM"
        };

        //set user password
        PasswordHasher<IdentityUser> ph = new PasswordHasher<IdentityUser>();
        appUser.PasswordHash = ph.HashPassword(appUser, "Test1234*");

        //seed user
        builder.Entity<IdentityUser>().HasData(appUser);

        //set user role to admin
        builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { 
            RoleId = ROLE_ID, 
            UserId = ADMIN_ID 
        });
    }

    public DbSet<BankApp.Models.Transactions>? Transactions { get; set; }
}