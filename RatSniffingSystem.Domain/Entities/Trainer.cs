using RatSniffingSystem.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Domain.Entities
{
    public class Trainer
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation Properties
        public List<Session>  Sessions { get; set; } = new();
    }
}
