using RatSniffingSystem.Domain.Entity;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateInterventionDto
    {


        public Guid SessionId { get; set; }


        public string Substance { get; set; } = string.Empty;
        public string Dose { get; set; } = string.Empty;
        public DateTime AppliedAt { get; set; }
        public InterventionType InterventionType { get; set; } = InterventionType.Drug;
        public string? Notes { get; set; }
    }
}
