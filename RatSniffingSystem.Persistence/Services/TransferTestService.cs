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
    public class TransferTestService : GenericServiceBase<TransferTest, TransferTestDto, CreateTransferTestDto, object>, ITransferTestService<TransferTestDto, CreateTransferTestDto>
    {
        public TransferTestService(AppDbContext context, IMapper mapper, ILogger logger) : base(context, mapper, logger)
        {
        }

        public async Task<TransferTestDto?> GetBySuccessStatusAsync(bool status)
        {
            var transferTest = await _dbSet
                .Where(tt => tt.WasSuccessful == status)
                .FirstOrDefaultAsync();

            if (transferTest == null)
            {
                _logger.LogWarning("No TransferTest found with success status {Status}.", status);
                throw new NotFoundException($"No TransferTest found with success status {status}.");
            }
            return _mapper.Map<TransferTestDto>(transferTest);
        }

        public async Task<int> GetSessionToSuccessCountAsync(int minCount, int maxCount)
        {
            var countToSuccess = await _dbSet
                .Where(tt => tt.SessionToSuccess >= minCount && tt.SessionToSuccess <= maxCount)
                .Select(tt => tt.SessionToSuccess)
                .FirstOrDefaultAsync();

            return countToSuccess;
        }
    }
}
