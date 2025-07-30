using RatSniffingSystem.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class CreateOdorDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public OdorCategory OdorCategory { get; set; } = OdorCategory.Unknown;
        public bool IsHazardous { get; set; } = false;
        public string? ExternalCode { get; set; }
    }
}
