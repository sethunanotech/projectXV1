using FluentValidation;
using ProjectX.Application.Usecases.Login;
using ProjectX.Application.Usecases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public class AuthenticateValidator
    {
        public class LoginValidator : AbstractValidator<UserLogin>
        {
            public LoginValidator()
            {               
                RuleFor(user => user.UserName).NotEmpty().WithMessage("UserName is required").MaximumLength(50);
                RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required").
                     Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,16}$").
                   WithMessage("Invalid Password");
            }
        }
    }
}
