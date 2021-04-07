using System;
using System.Threading.Tasks;
using Domain.Entities.Abstractions;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Entities.Enterprise.Concepts
{
    public class Example : IPersistedEntity, IValidationEnabled
    {
        public Example(User user)
        {
            User = user;
        }

        public Guid Id { get; set; }
        
        public User User { get; set; }
        
        public Guid GetId()
        {
            return Id;
        }

        public async Task<ValidationResult> IsValid()
        {
            var validator = new ExampleValidation();
            
            var result = await validator.ValidateAsync(this);
            
            return result;
        }
        
        private class ExampleValidation : AbstractValidator<Example>
        {
            public ExampleValidation()
            {
                RuleFor(x => x.User.IsValid());
            }
        }
    }
}