using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateTrialOdorDto
    {
        public Guid OdorId { get; set; }
        public OdorType OdorType { get; set; }
        public int PositionIndex { get; set; }
        public bool IsFalsePositive { get; set; }
    }
}
