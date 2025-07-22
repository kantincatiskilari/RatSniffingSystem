using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entity
{
    public class Rat
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; } = true;
        public string Notes { get; set; } = string.Empty;
        public string ProjectTag { get; set; }
        public string? Breed { get; set; }

        //Navigation Properties
        public List<RatWeight> WeightRecords { get; set; }
        public List<Session> Sessions { get; set; }
    }
}
