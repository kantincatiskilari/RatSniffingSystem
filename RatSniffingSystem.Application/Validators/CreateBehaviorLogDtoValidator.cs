using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateBehaviorLogDtoValidator : AbstractValidator<CreateBehaviorLogDto>
    {
        public CreateBehaviorLogDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session ID girmek zorunludur.");
            RuleFor(x => x.BehaviorType)
                .NotEmpty().WithMessage("Davranış tipi girmek zorunludur.");
            RuleFor(x => x.TimeObserved)
                .NotEmpty().WithMessage("Gözlemlenen zamanı girmek zorunludur.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Gözlemlenen zaman gelecekte olamaz.");
            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.Notes)); 
        }
    }
}
