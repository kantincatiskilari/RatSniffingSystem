using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateRatWeightDtoValidator : AbstractValidator<CreateRatWeightDto>
    {
        public CreateRatWeightDtoValidator()
        {
            RuleFor(x => x.RatId)
                .NotEmpty().WithMessage("Rat ID gereklidir.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("Tarih gereklidir.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("Tarih bugünden büyük olamaz.");
            RuleFor(x => x.WeightInGrams)
                .NotEmpty().WithMessage("Ağırlık gereklidir.")
                .GreaterThan(0).WithMessage("Ağırlık 0'dan büyük olmalıdır.");
        }
    }
}
