using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateBehaviorLogDto
    {

        public Guid SessionId { get; set; }

        public BehaviorType BehaviorType { get; set; }
        public DateTime TimeObserved { get; set; }
        public string? Notes { get; set; }
    }
}
