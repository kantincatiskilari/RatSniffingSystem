using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class UpdateRatDtoValidator : AbstractValidator<UpdateRatDto>
    {
        public UpdateRatDtoValidator()
        {
            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("Rat kodu boş olamaz.")
                .MaximumLength(20).WithMessage("Rat kodu en fazla 20 karakter olabilir.");

            RuleFor(x => x.BirthDate)
                .NotEmpty().WithMessage("Doğum tarihi boş olamaz.")
                .LessThan(DateTime.Today).WithMessage("Doğum tarihi bugünden ileri bir tarih olamaz.");

            RuleFor(x => x.ProjectTag)
                .NotEmpty().WithMessage("Proje etiketi girilmelidir.");

            RuleFor(x => x.Notes)
                .MaximumLength(500).WithMessage("Notlar en fazla 500 karakter olabilir.");

            RuleFor(x => x.Breed)
                .MaximumLength(50)
                .When(x => !string.IsNullOrWhiteSpace(x.Breed))
                .WithMessage("Irk bilgisi en fazla 50 karakter olabilir.");
        }
    }
}
