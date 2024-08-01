﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mvc01.Models;

public partial class MvcFriendDBContext : DbContext
{
    public MvcFriendDBContext(DbContextOptions<MvcFriendDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Friend> Friends { get; set; }

    public virtual DbSet<Manufacturer> Manufacturers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Friend>(entity =>
        {
            entity.Property(e => e.Email).IsRequired();
            entity.Property(e => e.Mobile).IsRequired();
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<Manufacturer>(entity =>
        {
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Telephone).IsRequired();
            entity.Property(e => e.Website).IsRequired();
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}