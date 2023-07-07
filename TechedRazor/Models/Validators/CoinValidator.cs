using FluentValidation;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Models.Validators
{
    public class CoinValidator : AbstractValidator<CoinDTO>
    {
        public CoinValidator()
        {
            RuleFor(coinDTO => coinDTO.CurrentPrice)
                .NotEmpty().WithMessage("Trenutačna cijena ne smije biti prazna")
                .NotNull().WithMessage("Trenutačna cijena ne smije biti null")
                .GreaterThan(0).WithMessage("Trenutačna cijena ne smije biti manja od 0");

            RuleFor(coinDTO => coinDTO.Name)
                .NotNull().WithMessage("Naziv ne smije biti null")
                .NotEmpty().WithMessage("Naziv ne smije biti prazan")
                .Must(IsStringValid).WithMessage("Naziv smije sadržavati samo slova");

            RuleFor(coinDTO => coinDTO.MarketCapRank)
                .NotEmpty()
                .NotNull()
                .GreaterThan(0);

            /*
            // TODO: Provjera iz baze
            RuleFor(coinDTO => coinDTO.LastUpdated)
                .NotNull().WithMessage("Zadnje ažurirano ne smije biti null")
                .NotEmpty().WithMessage("Zadnje ažurirano ne smije biti prazno")
                .Must(date => date <= DateTime.Now && date >= DateTime.Now.AddDays(-1))
                .WithMessage("Last Updated must be within the current date and not older than a day.");
            */
        }

        private bool IsStringValid(string? value)
        {
            if (string.IsNullOrWhiteSpace(value)) return false;

            return value.Replace(" ", "").All(Char.IsLetter);
        }

    }
}
