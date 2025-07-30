using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateTrainerDtoValidator : AbstractValidator<CreateTrainerDto>
    {
        public CreateTrainerDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Isim gereklidir.")
                .MaximumLength(100).WithMessage("Isim 100 karakteri gecemez.");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email gereklidir.")
                .EmailAddress().WithMessage("Hatali email formati.")
                .MaximumLength(100).WithMessage("Email 100 karakteri gecemez.");

        }
    }
}
