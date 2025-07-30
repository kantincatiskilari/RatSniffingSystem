using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateRatDtoValidator : AbstractValidator<CreateRatDto>
    {
        public CreateRatDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Rat kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Rat kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi zorunludur.")
                .LessThan(DateTime.Today).WithMessage("Doğum tarihi bugünden ileri bir tarih olamaz.");

            RuleFor(x => x.ProjectTag)
                .NotEmpty().WithMessage("Proje etiketi girilmelidir.");

            RuleFor(x => x.Breed)
                .MaximumLength(50).When(x => !string.IsNullOrWhiteSpace(x.Breed))
                .WithMessage("Irk bilgisi en fazla 50 karakter olabilir.");
        }
    }
}
