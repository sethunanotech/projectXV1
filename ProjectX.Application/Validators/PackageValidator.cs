using FluentValidation;
using ProjectX.Application.Usecases.Package;
using ProjectX.Application.Usecases.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public static class PackageValidator
    {
        public class PackageAddValidator : AbstractValidator<PackageAddRequest>
        {
            public PackageAddValidator()
            {
                RuleFor(x => x.Version)
               .NotEmpty().WithMessage("Version is Required.");
                RuleFor(x => x.CreatedBy)
               .NotEmpty().WithMessage("CreatedBy is Required.");
            }

        }
        public class PackageUpdateValidator : AbstractValidator<PackageUpdateRequest>
        {
            public PackageUpdateValidator()
            {
                RuleFor(x => x.ProjectID.ToString())
               .NotEmpty().WithMessage("ProjectID is Required.");
                RuleFor(x => x.Version)
              .NotEmpty().WithMessage("Version is Required.");
                RuleFor(x => x.LastModifiedBy)
               .NotEmpty().WithMessage("ModifiedBy is Required.");
            }

        }
    }
}
