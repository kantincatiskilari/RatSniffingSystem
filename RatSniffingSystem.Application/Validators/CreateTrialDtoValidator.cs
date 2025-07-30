using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateTrialDtoValidator : AbstractValidator<CreateTrialDto>
    {
        public CreateTrialDtoValidator()
        {
            RuleFor(x => x.TrialNumber)
                .GreaterThan(0).WithMessage("Deney numarasi 0'dan buyuk olmalidir.");
            RuleFor(x => x.TargetOdor)
                .NotEmpty().WithMessage("Hedef koku bos olamaz.")
                .MaximumLength(100).WithMessage("Hedef koku 100 karakteri gecemez.");
            RuleFor(x => x.FirstResponseTime)
                .NotEmpty().WithMessage("Ilk tepki zamani bos olamaz.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Ilk tepki zamani gelecekte olamaz.");
            RuleFor(x => x.FirstCorrectTime)
                .LessThanOrEqualTo(DateTime.Now).When(x => x.FirstCorrectTime.HasValue)
                .WithMessage("Ilk duzeltme zamani gelecekte olamaz.");
            RuleFor(x => x.IsCorrectNegative)
                .NotNull().WithMessage("Dogru negatif durumu belirtilmelidir.");
            RuleFor(x => x.IsCorrectPositive)
                .NotNull().WithMessage("Dogru pozitif durumu belirtilmelidir.");
            RuleFor(x => x.IsFalseNegative)
                .NotNull().WithMessage("Yanlis negatif durumu belirtilmelidir.");
            RuleFor(x => x.IsFalsePositive)
                .NotNull().WithMessage("Yanlis pozitif durumu belirtilmelidir.");

        }
    }
}
