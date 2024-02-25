using FluentValidation;
using ProjectX.Application.Usecases.Entity;
using ProjectX.Application.Usecases.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectX.Application.Validators
{
    public class EntityValidator
    {
        public class EntityAddValidator : AbstractValidator<EntityAddRequest>
        {
            public EntityAddValidator()
            {
              
            }
        }
        public class EntityUpdateValidator : AbstractValidator<EntityUpdateRequest>
        {
            public EntityUpdateValidator()
            {
               
            }
        }
    }
}
