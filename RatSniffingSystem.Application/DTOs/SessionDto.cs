using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class SessionDto
    {
        public Guid Id { get; set; }

        public Guid RatId { get; set; }
        public string RatCode { get; set; } = string.Empty;

        public Guid TrainerId { get; set; }
        public string TrainerName { get; set; } = string.Empty;

        public DateTime SessionDate { get; set; }
        public int DurationMinutes { get; set; }
        public string CageCode { get; set; } = string.Empty;

        public string? MaterialType { get; set; }
        public DateTime? MaterialThawedAt { get; set; }
        public DateTime StartTime { get; set; }

        public List<TrialDto> Trials { get; set; } = new();
        public List<ExperimentalNoteDto> Notes { get; set; } = new();
        public List<BehaviorLogDto> BehaviorLogs { get; set; }
        public List<InterventionDto> Interventions { get; set; }
        public List<TransferTestDto> TransferTests { get; set; }
        public List<FoodIntakeLogDto> FoodIntakeLogs { get; set; } = new();


    }
}
