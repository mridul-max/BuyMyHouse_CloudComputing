using BuyMyHouse.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Validation
{
    public class MortgageValidation : AbstractValidator<Mortgage>
    {
        public MortgageValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email address is required")
                     .EmailAddress().WithMessage("A valid email is required"); ;
            RuleFor(x => x.Amount).GreaterThan(0);
            RuleFor(x => x.ZipCode).NotEmpty().NotNull();
        }
    }
}
