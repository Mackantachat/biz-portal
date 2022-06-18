using BizPortal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Web.Mvc;

namespace BizPortal.Utils
{
    public class TooltipAttribute : Attribute
    {
        public string Position { get; set; }
        public string Message { get; set; }
        public string MessageResourceName { get; set; }
        public Type MessageResourceType { get; set; }

        public TooltipAttribute()
        {
            this.Message = "";
            this.Position = "";
            this.MessageResourceName = "";
            this.MessageResourceType = null;
        }
    }

    public class CustomRequiredAttribute : RequiredAttribute, IClientValidatable
    {
        public CustomRequiredAttribute()
        {

        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            ResourceManager rm = new ResourceManager(typeof(Global));
            this.ErrorMessage = rm.GetString("MSG_VALIDATE_REQUIRED", ci);
            var rule =new ModelClientValidationRule
            {
                ErrorMessage = string.Format(this.ErrorMessage, metadata.DisplayName),
                ValidationType = "required"
            };

            yield return rule;
        }
    }

    public class CustomRequiredIfAttribute : ValidationAttribute, IClientValidatable
    {
        private RequiredAttribute _innerAttribute = new RequiredAttribute();

        public string DependentProperty { get; set; }
        public object TargetValue { get; set; }

        public CustomRequiredIfAttribute(string dependentProperty, object targetValue)
        {
            this.DependentProperty = dependentProperty;
            this.TargetValue = targetValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get a reference to the property this validation depends upon
            var containerType = validationContext.ObjectInstance.GetType();
            var field = containerType.GetProperty(this.DependentProperty);

            if (field != null)
            {
                // get the value of the dependent property
                var dependentvalue = field.GetValue(validationContext.ObjectInstance, null);

                // compare the value against the target value
                if ((dependentvalue == null && this.TargetValue == null) ||
                    (dependentvalue != null && dependentvalue.Equals(this.TargetValue)))
                {
                    // match => means we should try validating this field
                    if (!_innerAttribute.IsValid(value))
                    {
                        // validation failed - return an error
                        return new ValidationResult(this.ErrorMessage, new[] { validationContext.MemberName });
                    }
                }
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            CultureInfo ci = CultureInfo.CurrentCulture;
            ResourceManager rm = new ResourceManager(typeof(Global));
            this.ErrorMessage = rm.GetString("MSG_VALIDATE_REQUIRED", ci);

            var rule = new ModelClientValidationRule()
            {
                ErrorMessage = string.Format(this.ErrorMessage, metadata.DisplayName),
                ValidationType = "customrequiredif",
            };

            string depProp = BuildDependentPropertyId(metadata, context as ViewContext);

            // find the value on the control we depend on;
            // if it's a bool, format it javascript style 
            // (the default is True or False!)
            string targetValue = (this.TargetValue ?? "").ToString();
            if (this.TargetValue.GetType() == typeof(bool))
                targetValue = targetValue.ToLower();

            rule.ValidationParameters.Add("dependentproperty", depProp);
            rule.ValidationParameters.Add("targetvalue", targetValue);

            yield return rule;
        }

        private string BuildDependentPropertyId(ModelMetadata metadata, ViewContext viewContext)
        {
            // build the ID of the property
            string depProp = viewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(this.DependentProperty);
            // unfortunately this will have the name of the current field appended to the beginning,
            // because the TemplateInfo's context has had this fieldname appended to it. Instead, we
            // want to get the context as though it was one level higher (i.e. outside the current property,
            // which is the containing object (our Person), and hence the same level as the dependent property.
            var thisField = metadata.PropertyName + "_";
            if (depProp.StartsWith(thisField))
                // strip it off again
                depProp = depProp.Substring(thisField.Length);
            return depProp;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class Column : Attribute
    {
        public int ColumnIndex { get; set; }


        public Column(int column)
        {
            ColumnIndex = column;
        }
    }
}