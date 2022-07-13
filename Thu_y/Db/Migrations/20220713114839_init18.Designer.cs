﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Thu_y.Db.DbContext;

#nullable disable

namespace Thu_y.Db.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220713114839_init18")]
    partial class init18
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("Thu_y.Modules.AbttoirModule.Core.AbattoirDetailEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("AnimalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AbattoirDetail");
                });

            modelBuilder.Entity("Thu_y.Modules.AbttoirModule.Core.AbattoirEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ManagerName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Abattoir");
                });

            modelBuilder.Entity("Thu_y.Modules.ReceiptModule.Core.ReceiptAllocateEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int?>("Amount")
                        .HasColumnType("int");

                    b.Property<string>("CodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("ReceiptId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ReceiptName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<int?>("TotalPage")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ReceiptId");

                    b.ToTable("ReceiptAllocate");
                });

            modelBuilder.Entity("Thu_y.Modules.ReceiptModule.Core.ReceiptEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("EffectiveDate")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Page")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Receipt");
                });

            modelBuilder.Entity("Thu_y.Modules.ReceiptModule.Core.ReceiptReportEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CodeNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateUse")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("PageUse")
                        .HasColumnType("int");

                    b.Property<string>("ReceiptAllocateId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReceiptName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReceiptReport");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.FormAttributeEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Col_Design")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ControlType")
                        .HasColumnType("int");

                    b.Property<int>("DataType")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FormId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("SortNo")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("api_DropDownlist")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("FormId");

                    b.ToTable("FormAttribute");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.FormEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FormCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Form");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ListAnimalEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("AnimalId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AnimalName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AnimalSex")
                        .HasColumnType("int");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DayAge")
                        .HasColumnType("int");

                    b.Property<bool>("IsCar")
                        .HasColumnType("bit");

                    b.Property<string>("Purpose")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportTicketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,4)");

                    b.HasKey("Id");

                    b.HasIndex("ReportTicketId");

                    b.ToTable("ListAnimalEntity");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ReportTicketEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ApproveId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApproveName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FormId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SerialNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<decimal>("TotalPrice")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ReportTicket");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ReportTicketValueEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttributeControlType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttributeDataType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttributeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AttributeName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("FormCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FormNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ReportId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AttributeId");

                    b.HasIndex("ReportId");

                    b.ToTable("ReportTicketValue");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.SealTabEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("CodeSeal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Content")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Id_Pricing")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,4)");

                    b.Property<string>("ReportTicketId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ReportTicketId");

                    b.ToTable("SealTabEntity");
                });

            modelBuilder.Entity("Thu_y.Modules.ShareModule.Core.AnimalEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int?>("DayAge")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Pricing")
                        .HasColumnType("decimal(18,4)");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Animal");
                });

            modelBuilder.Entity("Thu_y.Modules.ShareModule.Core.VacineEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AnimalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateInjected")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("VaccinationFacilityAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("VaccinationFacilityName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("AnimalId");

                    b.ToTable("Vacine");
                });

            modelBuilder.Entity("Thu_y.Modules.UserModule.Core.UserEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Account")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Address")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<int>("Sex")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Thu_y.Modules.UserModule.Core.UserScheduleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("AbattoirAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AbattoirId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AbattoirName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("DateCreated")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateDeleted")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset?>("DateStart")
                        .HasColumnType("datetimeoffset");

                    b.Property<DateTimeOffset>("DateUpdated")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserSchedule");
                });

            modelBuilder.Entity("Thu_y.Modules.ReceiptModule.Core.ReceiptAllocateEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ReceiptModule.Core.ReceiptEntity", "Receipt")
                        .WithMany("Allocates")
                        .HasForeignKey("ReceiptId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receipt");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.FormAttributeEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ReportModule.Core.FormEntity", "Form")
                        .WithMany("FormAttributes")
                        .HasForeignKey("FormId");

                    b.Navigation("Form");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ListAnimalEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ReportModule.Core.ReportTicketEntity", "ReportTicket")
                        .WithMany("ListAnimals")
                        .HasForeignKey("ReportTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportTicket");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ReportTicketValueEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ReportModule.Core.FormAttributeEntity", "Attribute")
                        .WithMany("ReportTicketValues")
                        .HasForeignKey("AttributeId");

                    b.HasOne("Thu_y.Modules.ReportModule.Core.ReportTicketEntity", "ReportTicket")
                        .WithMany("Values")
                        .HasForeignKey("ReportId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Attribute");

                    b.Navigation("ReportTicket");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.SealTabEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ReportModule.Core.ReportTicketEntity", "ReportTicket")
                        .WithMany("SealTabs")
                        .HasForeignKey("ReportTicketId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ReportTicket");
                });

            modelBuilder.Entity("Thu_y.Modules.ShareModule.Core.VacineEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.ShareModule.Core.AnimalEntity", "Animal")
                        .WithMany("Vacines")
                        .HasForeignKey("AnimalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Animal");
                });

            modelBuilder.Entity("Thu_y.Modules.UserModule.Core.UserScheduleEntity", b =>
                {
                    b.HasOne("Thu_y.Modules.UserModule.Core.UserEntity", "User")
                        .WithMany("UserSchedules")
                        .HasForeignKey("UserId");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Thu_y.Modules.ReceiptModule.Core.ReceiptEntity", b =>
                {
                    b.Navigation("Allocates");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.FormAttributeEntity", b =>
                {
                    b.Navigation("ReportTicketValues");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.FormEntity", b =>
                {
                    b.Navigation("FormAttributes");
                });

            modelBuilder.Entity("Thu_y.Modules.ReportModule.Core.ReportTicketEntity", b =>
                {
                    b.Navigation("ListAnimals");

                    b.Navigation("SealTabs");

                    b.Navigation("Values");
                });

            modelBuilder.Entity("Thu_y.Modules.ShareModule.Core.AnimalEntity", b =>
                {
                    b.Navigation("Vacines");
                });

            modelBuilder.Entity("Thu_y.Modules.UserModule.Core.UserEntity", b =>
                {
                    b.Navigation("UserSchedules");
                });
#pragma warning restore 612, 618
        }
    }
}
