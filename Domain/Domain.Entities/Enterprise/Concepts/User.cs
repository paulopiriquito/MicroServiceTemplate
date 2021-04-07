using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Entities.Abstractions;
using Domain.Entities.Enterprise.Behaviours;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities.Enterprise.Concepts
{
    public class User : IPersistedEntity, IValidationEnabled
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
        public string Email { get; set; }
        
        
        public async Task<ValidationResult> IsValid()
        {
            var validator = new UserValidation();
            
            var result = await validator.ValidateAsync(this);
            
            return result;
        }

        private class UserValidation : AbstractValidator<User>
        {
            public UserValidation()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Surname).NotEmpty();
                RuleFor(x => x.Email).EmailAddress();
            }
        }
    }
}