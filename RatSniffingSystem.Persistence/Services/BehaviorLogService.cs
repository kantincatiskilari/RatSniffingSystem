using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Contracts.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Persistence.Services
{
    public class BehaviorLogService : SessionLinkedServiceBase<BehaviorLog, BehaviorLogDto, CreateBehaviorLogDto, object> , IBehaviorLogService<BehaviorLog,BehaviorLogDto, CreateBehaviorLogDto>
    {
        public BehaviorLogService(AppDbContext context, IMapper mapper, ILogger<BehaviorLogService> logger) : base(context, mapper, logger)
        {
        }

        public Task<IReadOnlyList<BehaviorLogDto>> GetByBehaviorTypeAsync(BehaviorType type, CancellationToken ct = default)
                => WhereAsync(e => e.BehaviorType == type, ct);
    }
}
