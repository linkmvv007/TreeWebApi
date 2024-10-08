﻿using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer;

/// <summary>
/// EF Db context tree nodes
/// </summary>
public class TreeContext : DbContext
{
    public DbSet<TreeNode> TreeNodes { get; set; }

    public TreeContext(DbContextOptions<TreeContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TreeNode>()
            .HasOne(e => e.ParentNode)
            .WithMany(e => e.Childrens)
            .HasForeignKey(e => e.ParentNodeId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);


        modelBuilder.Entity<TreeNode>().ToTable("Trees", "dbo");
    }
}