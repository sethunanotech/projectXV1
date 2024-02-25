using FluentValidation;
using ProjectX.Application.Usecases.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public class ClientValidator 
    {
        public class AddClientValidator : AbstractValidator<ClientAddRequest>
        {
            public AddClientValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required.").MaximumLength(50);
                RuleFor(x => x.Address).NotEmpty().WithMessage("Address is Required.").MaximumLength(100);
                RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is Required.").MaximumLength(50);
            }
        }
        public class UpdateClientValidator : AbstractValidator<ClientUpdateRequest>
        {
            public UpdateClientValidator()
            {
                RuleFor(x => x.Name).NotEmpty().WithMessage("Name is Required.").MaximumLength(50);
                RuleFor(x => x.Address).NotEmpty().WithMessage("Address is Required.").MaximumLength(100);
                RuleFor(x => x.LastModifiedBy).NotEmpty().WithMessage("LastModifiedBy is Required.").MaximumLength(50);
            }
        }
    }
}
