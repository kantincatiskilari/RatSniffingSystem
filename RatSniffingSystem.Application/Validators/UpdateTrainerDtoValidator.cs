using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class UpdateTrainerDtoValidator : AbstractValidator<UpdateTrainerDto>
    {
        public UpdateTrainerDtoValidator()
        {
            RuleFor(expression => expression.Id)
                .NotEmpty().WithMessage("Id alanı boş olamaz.")
                .Must(id => id != Guid.Empty).WithMessage("Id boş bir GUID olamaz.");
            RuleFor(expression => expression.FullName)
                .NotEmpty().WithMessage("Tam ad alanı boş olamaz.")
                .MaximumLength(100).WithMessage("Tam ad en fazla 100 karakter olabilir.");
            RuleFor(expression => expression.Email)
                .NotEmpty().WithMessage("E-posta alanı boş olamaz.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi girilmelidir.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.");
            RuleFor(expression => expression.IsActive)
                .NotNull().WithMessage("Aktiflik durumu belirtilmelidir.")
                .Must(isActive => isActive == true || isActive == false)
                .WithMessage("Aktiflik durumu yalnızca true veya false olabilir.");
        }
    }
}
