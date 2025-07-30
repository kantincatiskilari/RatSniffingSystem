using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateTrialOdorDtoValidator : AbstractValidator<CreateTrialOdorDto>
    {
        public CreateTrialOdorDtoValidator()
        {
            RuleFor(x => x.OdorId)
                .NotEmpty().WithMessage("OdorId is required.")
                .Must(id => id != Guid.Empty).WithMessage("OdorId cannot be an empty GUID.");
            
            RuleFor(x => x.OdorType)
                .IsInEnum().WithMessage("Koku tipi girilmelidir");

            RuleFor(x => x.PositionIndex)
                .GreaterThanOrEqualTo(0).WithMessage("Pozisyon indeksi 0'dan buyuk olmalidir.");

            RuleFor(x => x.IsFalsePositive)
                .NotNull().WithMessage("Yanlis pozitif degeri girilmelidir.");

        }
    }
}
