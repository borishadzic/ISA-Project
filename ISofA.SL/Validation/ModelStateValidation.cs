using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISofA.SL.Validation
{
    public class ModelState
    {

        public bool IsValid { get; }
        public ICollection<ValidationResult> Errors { get; }

        internal ModelState(bool isValid, ICollection<ValidationResult> errors)
        {
            IsValid = isValid;
            Errors = errors;
        }

        public ModelState Validate(object instance)
        {
            ValidationContext vc = new ValidationContext(instance);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(instance, vc, results, true);
            return new ModelState(isValid, results);
        }

    }

}
