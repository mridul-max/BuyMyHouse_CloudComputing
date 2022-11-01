using BuyMyHouse.Model;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuyMyHouse.Validation
{
    public class HouseValidation : AbstractValidator<House>
    {
            public HouseValidation()
            {
                RuleFor(x => x.Address).NotEmpty().WithMessage("Email address is required")
                         .EmailAddress().WithMessage("A valid email is required"); ;
                RuleFor(x => x.Price).GreaterThan(0);
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.ZipCode).NotEmpty().NotNull();
        }
    }
}
