using Microsoft.EntityFrameworkCore;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Rat> Rats => Set<Rat>();
        public DbSet<Trainer> Trainers => Set<Trainer>();
        public DbSet<Session> Sessions => Set<Session>();
        public DbSet<Trial> Trials => Set<Trial>();
        public DbSet<RatWeight> RatWeights => Set<RatWeight>();
        public DbSet<BehaviorLog> BehaviorLogs => Set<BehaviorLog>();
        public DbSet<FoodIntakeLog> FoodIntakeLogs => Set<FoodIntakeLog>();
        public DbSet<Intervention> Interventions => Set<Intervention>();
        public DbSet<ExperimentalNote> ExperimentalNotes => Set<ExperimentalNote>();
        public DbSet<TransferTest> TransferTests => Set<TransferTest>();
        public DbSet<Odor> Odors => Set<Odor>();
        public DbSet<TrialOdor> TrialOdors => Set<TrialOdor>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply all Fluent API configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);


            base.OnModelCreating(modelBuilder);
        }
    }
}
