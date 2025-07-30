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
    public class ExperimentalNoteService : SessionLinkedServiceBase<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto, object>, IExperimentalNoteService<ExperimentalNote, ExperimentalNoteDto, CreateExperimentalNoteDto>
    {
        public ExperimentalNoteService(AppDbContext context, IMapper mapper, ILogger<ExperimentalNoteService> logger) : base(context, mapper, logger)
        {
        }

        public async Task<ExperimentalNoteDto> GetByTitleAsync(string title)
        {

            var normalizedTitle = title.ToLower().Trim();

            var note = await _dbSet
                .Where(n => n.Title.ToLower().Trim() == normalizedTitle)
                .FirstOrDefaultAsync();

            if (note == null)
            {
                _logger.LogWarning("Experimental note with title '{Title}' not found.", title);
                return null;
            }

            return _mapper.Map<ExperimentalNoteDto>(note);
        }

    }
}
