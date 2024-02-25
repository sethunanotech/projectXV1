using FluentValidation;
using ProjectX.Application.Usecases.Projects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public static class ProjectValidator
    {
        public class ProjectAddValidator : AbstractValidator<ProjectAddRequest>
        {
            public ProjectAddValidator()
            {
                RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is Required.").Length(3, 20);           
                RuleFor(x => x.CreatedBy)
               .NotEmpty().WithMessage("CreatedBy is Required.");
            }

        }
        public class ProjectUpdateValidator : AbstractValidator<ProjectUpdateRequest>
        {
            public ProjectUpdateValidator()
            {
                RuleFor(x => x.Title)
               .NotEmpty().WithMessage("Title is Required.").Length(3, 20);
       
                RuleFor(x => x.LastModifiedBy)
               .NotEmpty().WithMessage("ModifiedBy is Required.");
            }

        }
    }
  
}
