using System;
using Retyped;
using Validator = Retyped.jquery_validation.JQueryValidation.Validator;

namespace WebScripts
{
    public class Forms
    {
        public Forms()
        {
            InitValidators();
            InitCurrentErrors();
        }

        private void InitValidators()
        {
            var validator = (Validator)jquery.jQuery.@select(jquery.jQuery.@select("form")[0]).data("validator");
            if (validator != null)
            {
                validator.settings.errorClass = "is-invalid";
            }
        }

        private void InitCurrentErrors()
        {
            jquery.jQuery.@select(".input-validation-error").addClass("is-invalid")
                .removeClass("input-validation-error");
        }
    }
}