﻿using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MedDataBaseImplement;

public partial class MedBdContext : DbContext
{
    public MedBdContext()
    {
    }

    public MedBdContext(DbContextOptions<MedBdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Academicrank> Academicranks { get; set; }

    public virtual DbSet<Contract> Contracts { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    public virtual DbSet<Doctorsservice> Doctorsservices { get; set; }

    public virtual DbSet<ExecutionStatus> Executionstatuses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<Patient> Patients { get; set; }

    public virtual DbSet<Service> Services { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MedBD;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Academicrank>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("academicranks_pkey");

            entity.ToTable("academicranks");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AcademicRankName)
                .HasMaxLength(50)
                .HasColumnName("academicrankname");
        });

        modelBuilder.Entity<Contract>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("contracts_pkey");

            entity.ToTable("contracts");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ContractDoctorsId).HasColumnName("contractdoctorsid");
            entity.Property(e => e.ContractServicesId).HasColumnName("contractservicesid");
            entity.Property(e => e.ExecutionStatusId).HasColumnName("executionstatusid");
            entity.Property(e => e.ExerciseDate).HasColumnName("exercisedate");
            entity.Property(e => e.PatientId).HasColumnName("patientid");

            entity.HasOne(d => d.ExecutionStatus).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.ExecutionStatusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contract_execution_status");

            entity.HasOne(d => d.Patient).WithMany(p => p.Contracts)
                .HasForeignKey(d => d.PatientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contract_patient");

            entity.HasOne(d => d.ContractNavigation).WithMany(p => p.Contracts)
                .HasForeignKey(d => new { d.ContractDoctorsId, d.ContractServicesId })
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_contract_doctors_services");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("doctors_pkey");

            entity.ToTable("doctors");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.AcademicRankId).HasColumnName("academicrankid");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Education)
                .HasMaxLength(50)
                .HasColumnName("education");
            entity.Property(e => e.JobId).HasColumnName("jobid");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(80)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(80)
                .HasColumnName("surname");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(16)
                .HasColumnName("telephonenumber");

            entity.HasOne(d => d.AcademicRank).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.AcademicRankId)
                .HasConstraintName("fk_doctors_academic_rank");

            entity.HasOne(d => d.Job).WithMany(p => p.Doctors)
                .HasForeignKey(d => d.JobId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctors_job");
        });

        modelBuilder.Entity<Doctorsservice>(entity =>
        {
            entity.HasKey(e => new { e.DoctorsId, e.ServicesId }).HasName("doctorsservices_pkey");

            entity.ToTable("doctorsservices");

            entity.Property(e => e.DoctorsId).HasColumnName("doctorsid");
            entity.Property(e => e.ServicesId).HasColumnName("servicesid");

            entity.HasOne(d => d.Doctors).WithMany(p => p.DoctorsServices)
                .HasForeignKey(d => d.DoctorsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctors_services_services");

            entity.HasOne(d => d.Services).WithMany(p => p.DoctorsServices)
                .HasForeignKey(d => d.ServicesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_doctors_services_doctors");
        });

        modelBuilder.Entity<ExecutionStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("executionstatus_pkey");

            entity.ToTable("executionstatus");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ExecutionStatus1)
                .HasMaxLength(80)
                .HasColumnName("executionstatus");
        });

        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("jobs_pkey");

            entity.ToTable("jobs");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.JobTitle)
                .HasMaxLength(50)
                .HasColumnName("jobtitle");
        });

        modelBuilder.Entity<Patient>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("patients_pkey");

            entity.ToTable("patients");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Birthdate).HasColumnName("birthdate");
            entity.Property(e => e.Name)
                .HasMaxLength(80)
                .HasColumnName("name");
            entity.Property(e => e.Passport)
                .HasMaxLength(10)
                .HasColumnName("passport");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(80)
                .HasColumnName("patronymic");
            entity.Property(e => e.Surname)
                .HasMaxLength(80)
                .HasColumnName("surname");
            entity.Property(e => e.TelephoneNumber)
                .HasMaxLength(16)
                .HasColumnName("telephone_number");
        });

        modelBuilder.Entity<Service>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.ServicesName)
                .HasMaxLength(30)
                .HasColumnName("servicesname");
            entity.Property(e => e.ServicesPrice)
                .HasPrecision(10, 2)
                .HasColumnName("servicesprice");

            entity.HasMany(d => d.Jobs).WithMany(p => p.Services)
                .UsingEntity<Dictionary<string, object>>(
                    "Servicesjob",
                    r => r.HasOne<Job>().WithMany()
                        .HasForeignKey("Jobid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fkservicesjobjob"),
                    l => l.HasOne<Service>().WithMany()
                        .HasForeignKey("Servicesid")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("fkservicesjobservices"),
                    j =>
                    {
                        j.HasKey("Servicesid", "Jobid").HasName("servicesjobs_pkey");
                        j.ToTable("servicesjobs");
                        j.IndexerProperty<int>("Servicesid").HasColumnName("servicesid");
                        j.IndexerProperty<int>("Jobid").HasColumnName("jobid");
                    });
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
