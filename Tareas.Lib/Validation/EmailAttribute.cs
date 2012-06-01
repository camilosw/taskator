using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Tareas.Lib.Validation
{
    /// <summary>
    /// Fuente: http://www.pagedesigners.co.nz/2011/02/asp-net-mvc-3-email-validation-with-unobtrusive-jquery-validation/
    /// Expresión regular tomada de http://www.regular-expressions.info/email.html
    /// </summary>
    public class EmailAttribute : RegularExpressionAttribute, IClientValidatable
    {
        public EmailAttribute() :
            base(@"[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?")
        { }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata,
                                                                               ControllerContext context)
        {
            var errorMessage = FormatErrorMessage(metadata.GetDisplayName());

            yield return new EmailValidationRule(errorMessage);
        }
    }

    public class EmailValidationRule : ModelClientValidationRule
    {
        public EmailValidationRule(string errorMessage)
        {
            ErrorMessage = errorMessage;
            ValidationType = "email";
        }
    }
}
