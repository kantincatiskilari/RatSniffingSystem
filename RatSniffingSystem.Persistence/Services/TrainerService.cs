using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class TrainerService : GenericServiceBase<Trainer, TrainerDto, CreateTrainerDto, UpdateTrainerDto> ,ITrainerService<Trainer,TrainerDto, CreateTrainerDto, UpdateTrainerDto>
    {
        public TrainerService(AppDbContext context, IMapper mapper, ILogger<TrainerService> logger) : base(context, mapper, logger)
        {
        }

        public async Task<TrainerDto?> GetByEmailAsync(string email)
        {
            var formattedEmail = email.Trim().ToLower();
            var trainer = await _dbSet
                .Where(t => t.Email == formattedEmail)
                .FirstOrDefaultAsync();

            if (trainer == null)
            {
                _logger.LogWarning("Trainer with email {Email} not found.", formattedEmail);
                throw new NotFoundException($"Trainer with email: {email} not found.");
            }

            _logger.LogInformation("Trainer with email {Email} retrieved successfully.", email);
            return _mapper.Map<TrainerDto>(trainer);
        }
    }
}
