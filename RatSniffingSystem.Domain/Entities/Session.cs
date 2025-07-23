using RatSniffingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entity
{
    public class Session
    {
        public Guid Id { get; set; }

        public Guid RatId { get; set; }
        public Rat Rat { get; set; } = null!;

        public Guid TrainerId { get; set; }
        public Trainer Trainer { get; set; } = null!;

        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string CageCode { get; set; } = string.Empty;

        public string? MaterialType { get; set; }
        public DateTime? MaterialThawedAt { get; set; }
        public DateTime StartTime { get; set; }
        public List<Trial> Trials { get; set; } = new List<Trial>();
        public List<BehaviorLog> BehaviorLogs { get; set; } = new();
        public List<FoodIntakeLog> FoodIntakeLogs { get; set; } = new();
        public List<Intervention> Interventions { get; set; } = new();
        public List<TransferTest> TransferTests { get; set; } = new();
        public List<ExperimentalNote> Notes { get; set; } = new();

    }
}
