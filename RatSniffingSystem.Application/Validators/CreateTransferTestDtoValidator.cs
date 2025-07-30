using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateTransferTestDtoValidator : AbstractValidator<CreateTransferTestDto>
    {
        public CreateTransferTestDtoValidator()
        {
            RuleFor(x => x.SessionId)
                .NotEmpty().WithMessage("Session Id gereklidir.");
            RuleFor(x => x.NewOdor)
                .NotEmpty().WithMessage("Yeni koku gereklidir.")
                .MaximumLength(100).WithMessage("Yeni koku en fazla 100 karakter olabilir.");
            RuleFor(x => x.SessionToSuccess)
                .GreaterThan(0).WithMessage("Başarı için gereken seans sayısı 0'dan büyük olmalıdır.");
            RuleFor(x => x.WasSuccessful)
                .NotNull().WithMessage("Başarılı olup olmadığı bilgisi gereklidir.");
        }
    }
}
