using FluentValidation;
using FluentValidation.Results;
using System.Diagnostics;
using TechedRazor.Models.ViewModel;

namespace TechedRazor.Services.CoinServices.Impl
{
    public class CoinValidadionService : ICoinValidationService
    {
        private readonly IValidator<CoinDTO> _validator;

        public CoinValidadionService(IValidator<CoinDTO> validator)
        {
            _validator = validator;
        }

        public Object IsCoinDTOValid(CoinDTO coinDTO)
        {
            ValidationResult result = _validator.Validate(coinDTO);

            if (!result.IsValid)
            {
                foreach (var item in result.Errors)
                {
                    Debug.WriteLine("Msg: " + item.ErrorMessage);
                    Debug.WriteLine("Val: " + item.AttemptedValue.ToString());
                }

                return result;
            }

            return true;
        }
    }
}
