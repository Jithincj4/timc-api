using FluentValidation;
using TimcApi.Application.DTOs;

namespace TimcApi.Application.Validators
{
    public class CreatePatientDtoValidator : AbstractValidator<CreatePatientDto>
    {
        public CreatePatientDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Full Name is required")
                .MaximumLength(150);

            RuleFor(x => x.Gender)
                .NotEmpty().WithMessage("Gender is required")
                .Must(g => new[] { "Male", "Female", "Other" }.Contains(g))
                .WithMessage("Gender must be Male, Female or Other");

            RuleFor(x => x.DateOfBirth)
                .NotEmpty().LessThan(DateTime.Today).WithMessage("Invalid Date of Birth");

            RuleFor(x => x.Nationality)
                .NotEmpty().WithMessage("Nationality is required");

            RuleFor(x => x.PassportNumber)
                .NotEmpty().WithMessage("Passport number is required");

            RuleFor(x => x.PassportExpiryDate)
                .GreaterThan(DateTime.Today).WithMessage("Passport must not be expired");

            RuleFor(x => x.Email)
                .NotEmpty().EmailAddress();

            RuleFor(x => x.Phone)
                .NotEmpty().MinimumLength(8);

            RuleFor(x => x.PreferredCity)
                .NotEmpty().WithMessage("Preferred City is required");

            RuleFor(x => x.TravelDate)
                .NotEmpty().GreaterThan(DateTime.Today).WithMessage("Travel date must be in future");

            RuleFor(x => x.IsSelfFunded)
                .NotNull();

            // Optional:
            When(x => !x.IsSelfFunded, () =>
            {
                RuleFor(x => x.SponsorName)
                    .NotEmpty().WithMessage("Sponsor name is required when not self-funded");
            });
        }
    }
}
