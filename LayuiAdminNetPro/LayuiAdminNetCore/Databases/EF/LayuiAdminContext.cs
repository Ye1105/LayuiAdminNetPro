using LayuiAdminNetCore.AdminModels;
using Microsoft.EntityFrameworkCore;

namespace LayuiAdminNetCore.Database.EF
{
    public partial class LayuiAdminContext : DbContext
    {
        public LayuiAdminContext()
        {
        }

        public LayuiAdminContext(DbContextOptions<LayuiAdminContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region 映射

            modelBuilder.Entity<AdminRolePermission>()
            .HasOne(per => per.AdminModuleInfo)
            .WithOne(mInfo => mInfo.AdminRolePermission)
            .HasForeignKey<AdminRolePermission>(adminRolePermission => adminRolePermission.MId);

            modelBuilder.Entity<AdminAccountRole>()
            .HasOne(per => per.AdminRoleInfo)
            .WithOne(mInfo => mInfo.AdminAccountRole)
            .HasForeignKey<AdminAccountRole>(adminAccountRole => adminAccountRole.RId);

            modelBuilder.Entity<AdminAccountRole>()
            .HasOne(x => x.AdminAccount)
            .WithMany(t => t.AdminAccountRoles)
            .HasForeignKey(x => x.UId);

            modelBuilder.Entity<AdminSystemLog>()
            .HasOne(x => x.AdminAccount)
            .WithOne(t => t.AdminSystemLog)
            .HasForeignKey<AdminSystemLog>(x => x.UId);

            #endregion 映射

            base.OnModelCreating(modelBuilder);
        }

        //未生效
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        //    optionsBuilder.LogTo(Console.Write, (eventId, logLevel) => logLevel >= LogLevel.Error || eventId == RelationalEventId.ConnectionOpened
        //                           || eventId == RelationalEventId.ConnectionClosed);

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        { }

        #region 映射

        public virtual DbSet<AdminAccount> Admin_Account { get; set; }
        public virtual DbSet<AdminAccountRole> Admin_Account_Role { get; set; }
        public virtual DbSet<AdminLogTencentSMS> Admin_Log_Tencent_SMS { get; set; }
        public virtual DbSet<AdminRoleInfo> Admin_Role_Info { get; set; }
        public virtual DbSet<AdminRolePermission> Admin_Role_Permission { get; set; }
        public virtual DbSet<AdminModuleInfo> Admin_Module_Info { get; set; }
        public virtual DbSet<AdminSystemLog> Admin_System_Log { get; set; }

        #endregion 映射
    }
}