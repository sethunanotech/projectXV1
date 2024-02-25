using FluentValidation;
using ProjectX.Application.Usecases.Clients;
using ProjectX.Application.Usecases.ProjectUsers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public class ProjectUserValidator
    {
        public class AddProjectUserValidator : AbstractValidator<ProjectUserAddRequest>
        {
            public AddProjectUserValidator()
            {
                RuleFor(x => x.CreatedBy).NotEmpty().WithMessage("CreatedBy is Required.").MaximumLength(50);
            }
        }
        public class UpdateProjectUserValidator : AbstractValidator<ProjectUserUpdateRequest>
        {
            public UpdateProjectUserValidator()
            {
                RuleFor(x => x.LastModifiedBy).NotEmpty().WithMessage("LastModifiedBy is Required.").MaximumLength(50);
            }
        }
    }
}
