using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateInterventionDtoValidator : AbstractValidator<CreateInterventionDto>
    {
        public CreateInterventionDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session ID girmek zorunludur.");
            RuleFor(x => x.Substance)
                .NotEmpty().WithMessage("Substance girmek zorunludur.")
                .MaximumLength(100).WithMessage("Substance en fazla 100 karakter olabilir.");
            RuleFor(x => x.Dose)
                .NotEmpty().WithMessage("Doz girmek zorunludur.")
                .MaximumLength(50).WithMessage("Doz en fazla 50 karakter olabilir.");
            RuleFor(x => x.AppliedAt)
                .NotEmpty().WithMessage("Dozun uygulandigi zamani girmek zorunludur.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Uygulanma zamani gelecekte olamaz.");
        }
    }
}
