﻿using HuiAdminNetCore.AdminModels;
using Microsoft.EntityFrameworkCore;

namespace HuiAdminNetInfrastructure.Database
{
    public partial class HuiAdminContext : DbContext
    {
        public HuiAdminContext()
        {
        }

        public HuiAdminContext(DbContextOptions<HuiAdminContext> options) : base(options)
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

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