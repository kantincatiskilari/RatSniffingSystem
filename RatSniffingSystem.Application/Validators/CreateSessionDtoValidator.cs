using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class CreateSessionDtoValidator : AbstractValidator<CreateSessionDto>
    {
        public CreateSessionDtoValidator()
        {
            RuleFor(x => x.Date)
                .NotEmpty()
                .WithMessage("Tarih gereklidir.")
                .LessThan(DateTime.Today).WithMessage("Tarih bugünden ileri bir tarih olamaz."); 
            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0)
                .NotEmpty()
                .WithMessage("Süre gereklidir.")
                .WithMessage("Süre dakikası 0'dan büyük olmalıdır.");
            RuleFor(x => x.CageCode)
                .NotEmpty()
                .WithMessage("Kafes kodu gereklidir.")
                .MaximumLength(20)
                .WithMessage("Kafes kodu en fazla 20 karakter olabilir.");
            RuleFor(x => x.StartTime)
                .NotEmpty()
                .WithMessage("Başlangıç zamanı gereklidir.")
                .LessThan(DateTime.Today).WithMessage("Başlangıç zamanı bugünden ileri bir tarih olamaz.");
            RuleFor(x => x.RatId)
                .NotEmpty()
                .WithMessage("Rat ID gereklidir.");
            RuleFor(x => x.TrainerId)
                .NotEmpty()
                .WithMessage("Trainer ID gereklidir.");

        }
    }
}
