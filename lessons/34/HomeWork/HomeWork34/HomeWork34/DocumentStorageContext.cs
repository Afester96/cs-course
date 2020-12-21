using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace HomeWork34
{
    class DocumentStorageContext : DbContext
    {
        private const string ConnectionString =
            "Server=tcp:shadow-art.database.windows.net,1433;" +
            "Initial Catalog=reminder; " +
            "Persist Security Info=False;" +
            "User ID=k_gvozdev@shadow-art;" +
            "Password=Wuz74u6MZy7EYB2eZws8;" +
            "Encrypt=True;";

        public DbSet<Address> Addresses { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentStatus> DocumentStatuses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Status> Statuses { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnEntityModelCreating(modelBuilder.Entity<Address>());
            OnEntityModelCreating(modelBuilder.Entity<City>());
            OnEntityModelCreating(modelBuilder.Entity<Document>());
            OnEntityModelCreating(modelBuilder.Entity<DocumentStatus>());
            OnEntityModelCreating(modelBuilder.Entity<Employee>());
            OnEntityModelCreating(modelBuilder.Entity<Position>());
            OnEntityModelCreating(modelBuilder.Entity<Status>());
        }

        private void OnEntityModelCreating(EntityTypeBuilder<Address> addresses)
        {
            addresses.ToTable("Addresses");
            addresses.Property(address => address.Id);
            addresses
                .HasOne(address => address.City)
                .WithMany(city => city.Addresses)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade); 
            addresses.Property(address => address.Street)
                .IsRequired()
                .HasMaxLength(512); 
            addresses.Property(address => address.House)
                .IsRequired()
                .HasMaxLength(512); 
            addresses.HasKey(address => address.Id);
        }

        private void OnEntityModelCreating(EntityTypeBuilder<City> cities)
        {
            cities.ToTable("Cities");
            cities.Property(city => city.Id);
            cities.Property(city => city.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(512);
            cities.HasKey(city => city.Id);
            cities.HasIndex(city => city.Name).IsUnique();
        }

        private void OnEntityModelCreating(EntityTypeBuilder<Document> documents)
        {
            documents.ToTable("DocPositions");
            documents.Property(document => document.Id);
            documents.Property(document => document.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(512);
            documents.Property(document => document.SecondName)
               .IsRequired()
               .IsUnicode()
               .HasMaxLength(512);
            documents.Property(document => document.Pages)
                .IsRequired()
                .HasMaxLength(512); ;
            documents.HasKey(document => document.Id);
            documents.HasIndex(document => document.Name).IsUnique();

        }

        private void OnEntityModelCreating(EntityTypeBuilder<DocumentStatus> documentStatuses)
        {
            documentStatuses.ToTable("DocumentStatuses");
            documentStatuses.Property(documentStatus => documentStatus.Id);
            documentStatuses
                .HasOne(documentStatus => documentStatus.Document)
                .WithMany(document => document.DocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            documentStatuses
                .HasOne(documentStatus => documentStatus.SenderEmployee)
                .WithMany(senderEmployee => senderEmployee.SenderDocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            documentStatuses
                .HasOne(documentStatus => documentStatus.SenderAddress)
                .WithMany(senderAddress => senderAddress.SenderDocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            documentStatuses
                .HasOne(documentStatus => documentStatus.ReceiverEmployee)
                .WithMany(receiverEmployee => receiverEmployee.ReceiverDocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            documentStatuses
                .HasOne(documentStatus => documentStatus.ReceiverAddress)
                .WithMany(receiverAddress => receiverAddress.ReceiverDocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
            documentStatuses
                .HasOne(documentStatus => documentStatus.Status)
                .WithMany(status => status.DocumentStatuses)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            documentStatuses.Property(documentStatus => documentStatus.DateTime)
                .IsRequired();
            documentStatuses.HasKey(documentStatus => documentStatus.Id);
        }

        private void OnEntityModelCreating(EntityTypeBuilder<Employee> employees)
        {
            employees.ToTable("Employees");
            employees.Property(employee => employee.Id);
            employees.Property(employee => employee.FullName);
            employees
                .HasOne(employee => employee.Position)
                .WithMany(position => position.Employees)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
            employees.HasKey(employee => employee.Id);
            employees.HasIndex(employee => employee.FullName).IsUnique();
        }

        private void OnEntityModelCreating(EntityTypeBuilder<Position> positions)
        {
            positions.ToTable("Positions");
            positions.Property(position => position.Id);
            positions.Property(position => position.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(512);
            positions.HasKey(position => position.Id);
            positions.HasIndex(position => position.Name).IsUnique();
        }

        private void OnEntityModelCreating(EntityTypeBuilder<Status> statuses)
        {
            statuses.ToTable("Statuses");
            statuses.Property(status => status.Id);
            statuses.Property(status => status.Name)
                .IsRequired()
                .IsUnicode()
                .HasMaxLength(512);
            statuses.HasKey(status => status.Id);
            statuses.HasIndex(status => status.Name).IsUnique();
        }
    }
}
