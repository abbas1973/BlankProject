﻿// <auto-generated />
using System;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace WindowsService.FajrLog.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20221228174353_init")]
    partial class init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("FajrLog.Domain.FajrLogEntity", b =>
                {
                    b.Property<string>("logId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("HDDSerial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("IP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MACAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OS")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("URL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("accessLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("actionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("actionFlag")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("actionId")
                        .HasColumnType("bigint");

                    b.Property<string>("actionSensitivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("actionSubType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("actionType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appDBIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appDBName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("appId")
                        .HasColumnType("bigint");

                    b.Property<string>("appName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appPortNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appServerHostName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appServerIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("appVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("carNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("cityId")
                        .HasColumnType("bigint");

                    b.Property<string>("cityName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("clientName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("clientUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("consoleUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("depName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("depUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("deviceBusType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deviceComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deviceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deviceStatus")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("deviceType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("deviceUniqueId")
                        .HasColumnType("bigint");

                    b.Property<long?>("employeeNum")
                        .HasColumnType("bigint");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("forceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("forceUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("formName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gateName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("groupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("logNum")
                        .HasColumnType("bigint");

                    b.Property<string>("membership")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nationalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("nationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("orgName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("orgUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("partName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("partUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("phoneNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("pic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("secName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("secUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("subSystemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetAccessLevel")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetAmount")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetBusType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetCarNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetComment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetConsolName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetContent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetCreateTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("targetEmployeeNum")
                        .HasColumnType("bigint");

                    b.Property<string>("targetFirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetGender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetGroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetHDDSerial")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("targetId")
                        .HasColumnType("bigint");

                    b.Property<string>("targetLastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetMACAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetMembership")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetMessure")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetModificationTime")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetNatioanalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetNationality")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetPath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetPhoneNum")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetPic")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetSensitivity")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetSize")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetSpec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetSubType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("targetUniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("targetUserAgent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetUserIP")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetUserName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetVendor")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("targetVersion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("timeDuration")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("timeFrom")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("timeOccurrence")
                        .HasColumnType("bigint");

                    b.Property<long>("timeRegister")
                        .HasColumnType("bigint");

                    b.Property<string>("timeSend")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("timeStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("timeTo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("uniqueId")
                        .HasColumnType("bigint");

                    b.Property<string>("userAgent")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("userName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("zoneId")
                        .HasColumnType("bigint");

                    b.Property<string>("zoneName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("logId");

                    b.ToTable("FajrLog");
                });
#pragma warning restore 612, 618
        }
    }
}
