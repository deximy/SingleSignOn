using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace SingleSignOn.Backend
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<LinkedSteamAccount> LinkedSteamAccounts { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<LinkedSteamAccount>()
                .HasOne(linked_steam_account => linked_steam_account.ApplicationUser)
                .WithMany(application_user => application_user.LinkedSteamAccounts)
                .HasForeignKey(linked_steam_account => linked_steam_account.ApplicationUserId);
            builder.Entity<LinkedSteamAccount>().HasKey(linked_steam_account => linked_steam_account.steam_id);
        }
    }
}
