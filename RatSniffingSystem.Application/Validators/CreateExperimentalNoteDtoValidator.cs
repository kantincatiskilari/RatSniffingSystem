using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateExperimentalNoteDtoValidator : AbstractValidator<CreateExperimentalNoteDto>
    {
        public CreateExperimentalNoteDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session ID girmek zorunludur.");
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Başlık girmek zorunludur.")
                .MaximumLength(100).WithMessage("Başlık en fazla 100 karakter olmalıdır.");

        }
    }
}
