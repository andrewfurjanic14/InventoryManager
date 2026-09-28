using InventoryManager.Data.Models;
using InventoryManager.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManager.Data
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Oil> Oils => Set<Oil>();
        public DbSet<Provider> Providers => Set<Provider>();
        public DbSet<OilBatch> OilBatches => Set<OilBatch>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ---- Oil ----
            modelBuilder.Entity<Oil>(entity =>
            {
                entity.HasKey(o => o.OilId);

                entity.Property(o => o.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(o => o.BotanicalName).HasMaxLength(150);
                entity.Property(o => o.ExtractionMethod).HasMaxLength(100);
                entity.Property(o => o.PlantPart).HasMaxLength(100);

                // Prevent duplicate oil entries by name
                entity.HasIndex(o => o.Name).IsUnique();
            });

            // ---- Provider ----
            modelBuilder.Entity<Provider>(entity =>
            {
                entity.HasKey(p => p.ProviderId);

                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(200);

                entity.Property(p => p.ContactEmail).HasMaxLength(200);
                entity.Property(p => p.ContactPhone).HasMaxLength(50);
                entity.Property(p => p.Website).HasMaxLength(200);
            });

            // ---- OilBatch ----
            modelBuilder.Entity<OilBatch>(entity =>
            {
                entity.HasKey(b => b.BatchId);

                entity.Property(b => b.CountryOfOrigin)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(b => b.LotNumber).HasMaxLength(100);

                entity.Property(b => b.ArrivalWeight_Oz)
                    .IsRequired()
                    .HasColumnType("decimal(10,3)");

                entity.Property(b => b.CurrentWeight_Oz)
                    .IsRequired()
                    .HasColumnType("decimal(10,3)");

                entity.Property(b => b.Cost_USD).HasColumnType("decimal(10,2)");
                entity.Property(b => b.StorageLocation).HasMaxLength(100);
                entity.Property(b => b.COAFilePath).HasMaxLength(300);

                // Store enum as readable string rather than int
                entity.Property(b => b.Status)
                    .HasConversion<string>()
                    .HasMaxLength(30)
                    .HasDefaultValue(BatchStatus.Active);

                entity.Property(b => b.CreatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                entity.Property(b => b.UpdatedAt)
                    .HasDefaultValueSql("SYSUTCDATETIME()");

                // Relationships: restrict delete so removing an Oil/Provider
                // doesn't silently wipe out batch history
                entity.HasOne(b => b.Oil)
                    .WithMany(o => o.Batches)
                    .HasForeignKey(b => b.OilId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(b => b.Provider)
                    .WithMany(p => p.Batches)
                    .HasForeignKey(b => b.ProviderId)
                    .OnDelete(DeleteBehavior.Restrict);

                // Useful lookups: batches for a given oil, batches still active
                entity.HasIndex(b => new { b.OilId, b.Status });
                entity.HasIndex(b => b.LotNumber);
            });
        }

    }
}
