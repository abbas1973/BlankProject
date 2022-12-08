﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastracture.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Constant", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.HasKey("Id");

                    b.ToTable("Constants");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "تلفن",
                            Type = 0
                        },
                        new
                        {
                            Id = 2L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "ایمیل",
                            Type = 1
                        },
                        new
                        {
                            Id = 3L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "آدرس",
                            Type = 2
                        },
                        new
                        {
                            Id = 4L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "تعداد دفعات مجاز برای ورود ناموفق",
                            Type = 3,
                            Value = "5"
                        },
                        new
                        {
                            Id = 5L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "اجبار تغییر کلمه عبور کاربر بعد از چند روز؟",
                            Type = 4,
                            Value = "60"
                        },
                        new
                        {
                            Id = 6L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Title = "تعداد کاراکتر های تکراری مجاز در تغییر کلمه عبور",
                            Type = 5,
                            Value = "4"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Action")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Area")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Controller")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("HasLink")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("MaterialIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("NeedReAuthorize")
                        .HasColumnType("bit");

                    b.Property<string>("Parameters")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.Property<bool>("ShowInMenu")
                        .HasColumnType("bit");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Menus");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Action = "index",
                            Area = "admin",
                            Controller = "dashboard",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            MaterialIcon = "dashboard",
                            NeedReAuthorize = false,
                            ShowInMenu = true,
                            Sort = 1,
                            Title = "داشبورد"
                        },
                        new
                        {
                            Id = 2L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = false,
                            IsEnabled = true,
                            MaterialIcon = "account_circle",
                            NeedReAuthorize = false,
                            ShowInMenu = true,
                            Sort = 7,
                            Title = "امکانات مدیریتی"
                        },
                        new
                        {
                            Id = 3L,
                            Action = "index",
                            Area = "authsystem",
                            Controller = "users",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 2L,
                            ShowInMenu = true,
                            Sort = 1,
                            Title = "کاربران"
                        },
                        new
                        {
                            Id = 4L,
                            Action = "index",
                            Area = "authsystem",
                            Controller = "menus",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 2L,
                            ShowInMenu = true,
                            Sort = 2,
                            Title = "منو ها"
                        },
                        new
                        {
                            Id = 5L,
                            Action = "index",
                            Area = "authsystem",
                            Controller = "roles",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 2L,
                            ShowInMenu = true,
                            Sort = 3,
                            Title = "نقش ها"
                        },
                        new
                        {
                            Id = 10L,
                            Action = "index",
                            Area = "shared",
                            Controller = "constants",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 2L,
                            ShowInMenu = true,
                            Sort = 4,
                            Title = "تنظیمات"
                        },
                        new
                        {
                            Id = 6L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = false,
                            IsEnabled = true,
                            MaterialIcon = "assignment",
                            NeedReAuthorize = false,
                            ShowInMenu = true,
                            Sort = 10,
                            Title = "لاگ ها"
                        },
                        new
                        {
                            Id = 7L,
                            Action = "index",
                            Area = "logsystem",
                            Controller = "smslogs",
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 6L,
                            ShowInMenu = true,
                            Sort = 1,
                            Title = "پیام های ارسالی"
                        },
                        new
                        {
                            Id = 8L,
                            Action = "index",
                            Area = "logsystem",
                            Controller = "loginlogs",
                            CreateDate = new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 6L,
                            ShowInMenu = true,
                            Sort = 3,
                            Title = "ورود و خروج کاربران"
                        },
                        new
                        {
                            Id = 9L,
                            Action = "index",
                            Area = "logsystem",
                            Controller = "actionlogs",
                            CreateDate = new DateTime(2022, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            HasLink = true,
                            IsEnabled = true,
                            NeedReAuthorize = false,
                            ParentId = 6L,
                            ShowInMenu = true,
                            Sort = 4,
                            Title = "عملیات کاربران"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsEnabled = true,
                            Title = "ادمین کل"
                        });
                });

            modelBuilder.Entity("Domain.Entities.RoleMenu", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(0);

                    b.Property<long>("MenuId")
                        .HasColumnType("bigint")
                        .HasColumnOrder(1);

                    b.HasKey("RoleId", "MenuId");

                    b.HasIndex("MenuId");

                    b.ToTable("RoleMenus");

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            MenuId = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 3L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 4L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 5L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 6L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 7L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 8L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 9L
                        },
                        new
                        {
                            RoleId = 1L,
                            MenuId = 10L
                        });
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<int?>("ChangePasswordCycle")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<long?>("CreatorId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("Mobile")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("PasswordIsChanged")
                        .HasColumnType("bit");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("CreatorId");

                    b.HasIndex("RoleId");

                    b.HasIndex("Username")
                        .IsUnique();

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            CreateDate = new DateTime(2022, 7, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            IsEnabled = true,
                            Mobile = "09123456789",
                            Name = "ادمین",
                            Password = "??+c???V????????????	????{\\8???O??Zn1JM??W?N?O\n????>??ix",
                            PasswordIsChanged = false,
                            RoleId = 1L,
                            Type = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.HasOne("Domain.Entities.Menu", "Parent")
                        .WithMany("SubMenus")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Domain.Entities.RoleMenu", b =>
                {
                    b.HasOne("Domain.Entities.Menu", "Menu")
                        .WithMany("Roles")
                        .HasForeignKey("MenuId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("Menus")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Menu");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.User", b =>
                {
                    b.HasOne("Domain.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("CreatorId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Domain.Entities.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Domain.Entities.Menu", b =>
                {
                    b.Navigation("Roles");

                    b.Navigation("SubMenus");
                });

            modelBuilder.Entity("Domain.Entities.Role", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
