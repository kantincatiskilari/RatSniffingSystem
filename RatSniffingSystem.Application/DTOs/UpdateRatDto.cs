using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class UpdateRatDto
    {
        public Guid Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }
        public string ProjectTag { get; set; }
        public string? Breed { get; set; }
    }
}
