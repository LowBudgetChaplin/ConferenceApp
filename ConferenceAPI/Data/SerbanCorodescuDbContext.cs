using System;
using System.Collections.Generic;
using ConferenceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ConferenceAPI.Data;

public partial class SerbanCorodescuDbContext : DbContext
{
    public SerbanCorodescuDbContext()
    {
    }

    public SerbanCorodescuDbContext(DbContextOptions<SerbanCorodescuDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conference> Conferences { get; set; }

    public virtual DbSet<ConferenceXattendee> ConferenceXattendees { get; set; }

    public virtual DbSet<ConferenceXspeaker> ConferenceXspeakers { get; set; }

    public virtual DbSet<DictionaryCategory> DictionaryCategories { get; set; }

    public virtual DbSet<DictionaryCity> DictionaryCities { get; set; }

    public virtual DbSet<DictionaryConferenceType> DictionaryConferenceTypes { get; set; }

    public virtual DbSet<DictionaryCountry> DictionaryCountries { get; set; }

    public virtual DbSet<DictionaryCounty> DictionaryCounties { get; set; }

    public virtual DbSet<DictionaryStatus> DictionaryStatuses { get; set; }

    public virtual DbSet<EmailNotification> EmailNotifications { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Location> Locations { get; set; }

    public virtual DbSet<Log> Logs { get; set; }

    public virtual DbSet<LogsBackup> LogsBackups { get; set; }

    public virtual DbSet<Smsnotification> Smsnotifications { get; set; }

    public virtual DbSet<Speaker> Speakers { get; set; }

    public virtual DbSet<Test> Tests { get; set; }

    public virtual DbSet<Test1Bk> Test1Bks { get; set; }

    public virtual DbSet<Test2> Test2s { get; set; }

    public virtual DbSet<VwConferenceXspeaker> VwConferenceXspeakers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=TS2256\\SQLEXPRESS; Database=SERBAN_CORODESCU_DB; User Id=internship; Password=int; TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conference>(entity =>
        {
            entity.ToTable("Conference");

            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.OrganizerEmail).HasMaxLength(50);

            entity.HasOne(d => d.Category).WithMany(p => p.Conferences)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conference_DictionaryCategory");

            entity.HasOne(d => d.ConferenceType).WithMany(p => p.Conferences)
                .HasForeignKey(d => d.ConferenceTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conference_DictionaryConferenceType");

            entity.HasOne(d => d.Location).WithMany(p => p.Conferences)
                .HasForeignKey(d => d.LocationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Conference_Location");
        });

        modelBuilder.Entity<ConferenceXattendee>(entity =>
        {
            entity.ToTable("ConferenceXAttendee");

            entity.Property(e => e.AttendeeEmail).HasMaxLength(50);

            entity.HasOne(d => d.Conference).WithMany(p => p.ConferenceXattendees)
                .HasForeignKey(d => d.ConferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConferenceXAttendee_Conference");

            entity.HasOne(d => d.Status).WithMany(p => p.ConferenceXattendees)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConferenceXAttendee_DictionaryStatus");
        });

        modelBuilder.Entity<ConferenceXspeaker>(entity =>
        {
            entity.ToTable("ConferenceXSpeaker");

            entity.HasOne(d => d.Conference).WithMany(p => p.ConferenceXspeakers)
                .HasForeignKey(d => d.ConferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConferenceXSpeaker_Conference");

            entity.HasOne(d => d.Speaker).WithMany(p => p.ConferenceXspeakers)
                .HasForeignKey(d => d.SpeakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ConferenceXSpeaker_Speaker");
        });

        modelBuilder.Entity<DictionaryCategory>(entity =>
        {
            entity.ToTable("DictionaryCategory");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DictionaryCity>(entity =>
        {
            entity.ToTable("DictionaryCity");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DictionaryConferenceType>(entity =>
        {
            entity.ToTable("DictionaryConferenceType");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DictionaryCountry>(entity =>
        {
            entity.ToTable("DictionaryCountry");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DictionaryCounty>(entity =>
        {
            entity.ToTable("DictionaryCounty");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<DictionaryStatus>(entity =>
        {
            entity.ToTable("DictionaryStatus");

            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<EmailNotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__EmailNot__3214EC07CFAB1D31");

            entity.ToTable("EmailNotification");

            entity.Property(e => e.Cc).HasColumnName("CC");
            entity.Property(e => e.SentDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Feedback__3214EC074FD8E158");

            entity.ToTable("Feedback", tb => tb.HasTrigger("trgDeletedFeedback"));

            entity.Property(e => e.Rating).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Conference).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.ConferenceId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Conference_Feedback");

            entity.HasOne(d => d.Speaker).WithMany(p => p.Feedbacks)
                .HasForeignKey(d => d.SpeakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Speaker_Feedback");
        });

        modelBuilder.Entity<Location>(entity =>
        {
            entity.ToTable("Location");

            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Code).HasMaxLength(50);
            entity.Property(e => e.Latitude).HasColumnType("decimal(12, 9)");
            entity.Property(e => e.Longitude).HasColumnType("decimal(12, 9)");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.City).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CityId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_DictionaryCity");

            entity.HasOne(d => d.Country).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_DictionaryCountry");

            entity.HasOne(d => d.County).WithMany(p => p.Locations)
                .HasForeignKey(d => d.CountyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Location_DictionaryCounty");
        });

        modelBuilder.Entity<Log>(entity =>
        {
            entity.ToTable("__Logs");

            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<LogsBackup>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("__LOGS_BACKUP");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Level).HasMaxLength(128);
            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Smsnotification>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__SMSNotif__3214EC078807B469");

            entity.ToTable("SMSNotification");

            entity.Property(e => e.SentDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<Speaker>(entity =>
        {
            entity.ToTable("Speaker");

            entity.Property(e => e.Email).HasMaxLength(35);
            entity.Property(e => e.Image).HasColumnType("image");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Nationality).HasMaxLength(50);
            entity.Property(e => e.PhoneNumber).HasMaxLength(35);
            entity.Property(e => e.Rating).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<Test>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Test__3214EC07F5E2F737");

            entity.ToTable("Test");

            entity.Property(e => e.Info)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecordStamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<Test1Bk>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("test1_bk");

            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Info)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RecordStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<Test2>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__test2__3214EC07FFFAAD33");

            entity.ToTable("test2");

            entity.Property(e => e.Value).HasColumnType("numeric(4, 2)");
        });

        modelBuilder.Entity<VwConferenceXspeaker>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("vwConferenceXSpeaker");

            entity.Property(e => e.AttendeeEmail).HasMaxLength(50);
            entity.Property(e => e.ConferenceName).HasMaxLength(255);
            entity.Property(e => e.SpeakerName).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
