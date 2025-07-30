using RatSniffingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.DTOs
{
    public class TransferTestDto
    {
        public Guid Id { get; set; }


        public string NewOdor { get; set; } = string.Empty; 
        public int SessionToSuccess { get; set; } 
        public bool WasSuccessful { get; set; }
        public string? Notes { get; set; }
    }
}
