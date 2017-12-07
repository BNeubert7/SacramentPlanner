using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SacramentProject.Models
{
    public partial class SacramentPlannerContext : DbContext
    {
        public virtual DbSet<SacramentProgram> SacramentProgram { get; set; }
        public virtual DbSet<Speakers> Speakers { get; set; }

        public SacramentPlannerContext(DbContextOptions<SacramentPlannerContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SacramentProgram>(entity =>
            {
                entity.Property(e => e.SacramentProgramId).HasColumnName("SacramentProgramID");

                entity.Property(e => e.ClosingPrayer)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.Conducting)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.Property(e => e.MeetingDate).HasColumnType("date");

                entity.Property(e => e.OpeningPrayer)
                    .IsRequired()
                    .HasColumnType("nchar(50)");
            });

            modelBuilder.Entity<Speakers>(entity =>
            {
                entity.HasKey(e => e.SpeakerProgramId);

                entity.Property(e => e.SpeakerProgramId).HasColumnName("SpeakerProgramID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.SacramentProgramId).HasColumnName("SacramentProgramID");

                entity.Property(e => e.Topic)
                    .IsRequired()
                    .HasColumnType("nchar(50)");

                entity.HasOne(d => d.SacramentProgram)
                    .WithMany(p => p.Speakers)
                    .HasForeignKey(d => d.SacramentProgramId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Speakers_SacramentProgram");
            });
        }
    }
}
