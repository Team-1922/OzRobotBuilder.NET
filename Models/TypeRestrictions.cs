using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models.XML;

namespace Team1922.MVVM.Models
{
    public partial class TypeRestrictions
    {
        public static bool ThrowsExceptionsOnValidationFailure { get; set; } = true;


        public static IFacet GetValidationObject(string attributeName)
        {
            if (_attributeTypeDictionary.ContainsKey(attributeName))
            {
                string key = _attributeTypeDictionary[attributeName];
                if (_typeFacetDictionary.ContainsKey(key))
                {
                    return _typeFacetDictionary[key];
                }
            }
            return _alwaysTrueFacet;
        }

        // NOTE: this throws and exception when validation fails
        public static void Validate(string attributeName, object value)
        {
            if (ThrowsExceptionsOnValidationFailure)
            {
                var facet = GetValidationObject(attributeName);
                if (!facet.TestValue(value))
                {
                    throw new System.ArgumentException(facet.Stringify());
                }
            }
        }


        // NOTE: this returns a string representation of the error
        public static string DataErrorString(string attributeName, object value)
        {
            var facet = GetValidationObject(attributeName);
            if (!facet.TestValue(value))
            {
                return facet.Stringify();
            }
            return string.Empty;
        }


        public static double Clamp(string attributeName, double value)
        {
            var facet = GetValidationObject(attributeName);
            return facet.ClampValue(value);
        }
    }
}
