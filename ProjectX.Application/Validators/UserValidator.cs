using FluentValidation;
using ProjectX.Application.Usecases.User;

namespace ProjectX.Application.Validators
{
    public class UserValidator
    {
        public class AddUserValidator : AbstractValidator<UserAddRequest>
        {
            public AddUserValidator()
            {
                RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50);
                RuleFor(user => user.UserName).NotEmpty().WithMessage("UserName is required").MaximumLength(50);
                RuleFor(user => user.Password).NotEmpty().WithMessage("Password is required").MaximumLength(16).
                    Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[#$^+=!*()@%&]).{8,16}$").
                    WithMessage("Invalid Password");
                RuleFor(user => user.CreatedBy).NotEmpty().WithMessage("CreatedBy is required");
            }
        }
        public class UpdateUserValidator : AbstractValidator<UserUpdateRequest>
        {
            public UpdateUserValidator()
            {
                RuleFor(user => user.Name).NotEmpty().WithMessage("Name is required").MaximumLength(50);
                RuleFor(user => user.UserName).NotEmpty().WithMessage("UserName is required").MaximumLength(50);
                RuleFor(user => user.LastModifiedBy).NotEmpty().WithMessage("ModifiedBy is required");
            }
        }
    }
   
}
