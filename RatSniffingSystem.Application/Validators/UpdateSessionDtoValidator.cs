using FluentValidation;
using RatSniffingSystem.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RatSniffingSystem.Application.Validators
{
    public class UpdateSessionDtoValidator : AbstractValidator<UpdateSessionDto>
    {
        public UpdateSessionDtoValidator()
        {
            RuleFor(x => x.Id)
     .NotEmpty().WithMessage("Id alanı boş olamaz.")
     .Must(id => id != Guid.Empty).WithMessage("Id boş bir GUID olamaz.");

            RuleFor(x => x.DurationMinutes)
                .GreaterThan(0).WithMessage("Süre, 0 dakikadan büyük olmalıdır.");

            RuleFor(x => x.CageCode)
                .NotEmpty().WithMessage("Kafes kodu gereklidir.")
                .MaximumLength(50).WithMessage("Kafes kodu en fazla 50 karakter olabilir.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Başlangıç zamanı gereklidir.")
                .Must(time => time.TotalMinutes >= 0)
                .WithMessage("Başlangıç zamanı geçerli bir zaman olmalıdır (negatif olamaz).");

            RuleFor(x => x.MaterialType)
                .MaximumLength(100).WithMessage("Materyal türü en fazla 100 karakter olabilir.")
                .When(x => !string.IsNullOrEmpty(x.MaterialType));
            // Opsiyonel alan: yalnızca değer girilmişse kontrol et

            RuleFor(x => x.MaterialThawedAt)
                .Must(time => time == null || time.Value.TotalMinutes >= 0)
                .WithMessage("Çözdürme zamanı geçerli bir zaman aralığı olmalı ya da boş bırakılmalıdır.")
                .When(x => x.MaterialThawedAt.HasValue);
            // Opsiyonel alan: yalnızca bir değer varsa kontrol et

            RuleFor(x => x.MaterialThawedAt)
                .Must((x, time) => time == null || time.Value >= x.StartTime)
                .WithMessage("Çözdürme zamanı, başlangıç zamanından önce olamaz.")
                .When(x => x.MaterialThawedAt.HasValue && x.StartTime != default(TimeSpan));
            // Her iki alan da mevcutsa kontrol et


        }
    }
}
