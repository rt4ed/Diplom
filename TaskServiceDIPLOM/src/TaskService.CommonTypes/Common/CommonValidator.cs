using System.ComponentModel.DataAnnotations;

namespace TaskService.CommonTypes.Common
{
    /// <summary>
    /// Common generic validator
    /// </summary>
    public class CommonValidator
    {
        public bool IsValid<T>(T model, out ICollection<ValidationResult> result)
        {
            if (model is null)
                throw new ArgumentNullException(nameof(model));

            var context = new ValidationContext(model);
            result = new List<ValidationResult>();

            return !Validator.TryValidateObject(model, context, result, true);
        }
    }
}
