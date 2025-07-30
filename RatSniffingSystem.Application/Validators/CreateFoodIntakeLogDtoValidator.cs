using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateFoodIntakeLogDtoValidator : AbstractValidator<CreateFoodIntakeLogDto>
    {
        public CreateFoodIntakeLogDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session ID girmek zorunludur.");
            RuleFor(x => x.AmountInCc)
                .NotEmpty().WithMessage("Miktar girmek zorunludur.")
                .GreaterThan(0).WithMessage("Miktar 0'dan büyük olmalıdır.");
            RuleFor(x => x.TimeGiven)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Veriliş zamanı gelecekte olamaz.");

        }
    }
}
