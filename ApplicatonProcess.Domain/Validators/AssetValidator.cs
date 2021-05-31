using FluentValidation;
using System;
using ApplicatonProcess.Domain.Models;
using ApplicatonProcess.Domain.Repsitories;

namespace ApplicatonProcess.Domain.Validators
{
    public class AssetValidator : AbstractValidator<Asset>
    {
        private readonly IHttpClient _httpClient;
        public AssetValidator(IHttpClient httpClient)
        {
            _httpClient = httpClient;
            
            RuleFor(p => p.AssetName)
                .NotEmpty().MinimumLength(5).WithName("asset_name");
            RuleFor(p => p.Department).IsInEnum().WithName("department");
            RuleFor(p => p.CountryOfDepartment)
                .NotEmpty().Must(CountryIsValid).WithName("country")
                .WithMessage("{PropertyName} is not a valid country");
            RuleFor(p => p.EMailAddress)
                .NotEmpty().WithName("email")
                .Matches("^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,4}$");
            RuleFor(p => p.PurchaseDate)
                .NotEmpty().Must(NotOlderThanAYear).WithName("purchase_date")
                .WithMessage("{PropertyName} is older than one year");
        }

        private bool NotOlderThanAYear(DateTime pDate)
        {
            var oneYearAgo = DateTime.Today.AddYears(-1);
            return pDate >= oneYearAgo;
        }

        private bool CountryIsValid(string pCountry)
        {
            var countryName = _httpClient.GetCountryByName(pCountry).Result;
            if (countryName != string.Empty)
            {
                return true;
            }
            return false;
        }

    }
}
