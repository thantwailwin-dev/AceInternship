using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace InternshipDotNetCore.DatabaseFirstAuto.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBlog> TblBlogs { get; set; }

    public virtual DbSet<TblLogEvent> TblLogEvents { get; set; }

    public virtual DbSet<TblPageStatistic> TblPageStatistics { get; set; }

    public virtual DbSet<TblRadarChart> TblRadarCharts { get; set; }    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBlog>(entity =>
        {
            entity.HasKey(e => e.BlogId);

            entity.ToTable("Tbl_Blog");

            entity.Property(e => e.BlogAuthor).HasMaxLength(50);
            entity.Property(e => e.BlogContent).IsUnicode(false);
            entity.Property(e => e.BlogTitle).HasMaxLength(50);
        });

        modelBuilder.Entity<TblLogEvent>(entity =>
        {
            entity.ToTable("Tbl_LogEvents");

            entity.Property(e => e.TimeStamp).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblPageStatistic>(entity =>
        {
            entity.HasKey(e => e.PageStatistics);

            entity.ToTable("Tbl_PageStatistics");

            entity.Property(e => e.CreatedDate)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblRadarChart>(entity =>
        {
            entity.ToTable("Tbl_RadarChart");

            entity.Property(e => e.Month)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
