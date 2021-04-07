using System.Threading.Tasks;
using FluentValidation.Results;

namespace Domain.Entities.Abstractions
{
    public interface IValidationEnabled
    {
        public Task<ValidationResult> IsValid();
    }
}