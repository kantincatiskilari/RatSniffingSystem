using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateRatDto
    {
        public string Code { get; set; } = string.Empty;
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Notes { get; set; } = string.Empty;
        public string ProjectTag { get; set; }
        public string? Breed { get; set; }
    }
}
