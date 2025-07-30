using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RatSniffingSystem.Application.DTOs;
using RatSniffingSystem.Application.Interfaces;
using RatSniffingSystem.Domain.Entities;
using RatSniffingSystem.Domain.Enums;
using RatSniffingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RatSniffingSystem.Application.Exceptions;
using RatSniffingSystem.Contracts.Interfaces;

namespace RatSniffingSystem.Persistence.Services
{
    public class OdorService : GenericServiceBase<Odor, OdorDto, CreateOdorDto, object>, IOdorService<Odor,OdorDto, CreateOdorDto>
    {
        public OdorService(AppDbContext context, IMapper mapper, ILogger<OdorService> logger) : base(context, mapper, logger)
        {
        }

        public Task<IReadOnlyList<OdorDto>> GetByCategoryAsync(
            OdorCategory category, CancellationToken ct = default)
            => WhereAsync(o => o.OdorCategory == category, ct);

        public Task<OdorDto> GetByExternalCodeAsync(string externalCode) => FindFirstAsync(o => o.ExternalCode == externalCode);


        public Task<IReadOnlyList<OdorDto?>> GetByHazardousStatusAsync(bool isHazardous) => WhereAsync(o => o.IsHazardous == isHazardous);


        public Task<IReadOnlyList<OdorDto?>> GetByNameAsync(string name) => WhereAsync(o => o.Name.ToLower() == name.ToLower());



    }
}
