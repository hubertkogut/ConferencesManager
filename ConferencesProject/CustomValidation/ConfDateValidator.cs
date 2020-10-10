using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferencesProject.CustomValidation
{
    public class ConfDateValidator : ValidationAttribute
    {

        public ConfDateValidator()
        {
            ErrorMessage = "The Conference must takes places in teh future";
        }

        public override bool IsValid(object value)
        {
            DateTime enteredDate;
            if (DateTime.TryParse(value.ToString(), out enteredDate))
            {
                if (enteredDate < DateTime.Now)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
