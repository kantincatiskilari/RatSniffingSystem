using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateOdorDtoValidator : AbstractValidator<CreateOdorDto>
    {
        public CreateOdorDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Koku ismi gereklidir.")
                .MaximumLength(100).WithMessage("Koku ismi 100 karakterden fazla olamaz.");

        }
    }
}
