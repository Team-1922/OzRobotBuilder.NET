using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Team1922.MVVM.Models.XML;

namespace Team1922.MVVM.Models
{
    public partial class TypeRestrictions
    {
        /// <summary>
        /// Whether exceptions are thrown on validation failures
        /// </summary>
        public static bool ThrowsExceptionsOnValidationFailure { get; set; } = true;

        #region Retrieval Methods
        /// <summary>
        /// Gets a validation object associated with a given object member
        /// </summary>
        /// <param name="attributeName">the name of the member to check</param>
        /// <returns>the facet representing valid values for the data at <paramref name="attributeName"/></returns>
        public static IFacet GetValidationObject(string attributeName)
        {
            if (_attributeTypeDictionary.ContainsKey(attributeName))
            {
                string typeName = _attributeTypeDictionary[attributeName];
                return GetValidationObjectFromTypeName(typeName);
            }
            return _alwaysTrueFacet;
        }

        /// <summary>
        /// Gets the validation object associated with a given type
        /// </summary>
        /// <param name="typeName">The name of the type to get</param>
        /// <returns>the facet repsenting valid values for the given type</returns>
        public static IFacet GetValidationObjectFromTypeName(string typeName)
        {
            if(_typeFacetDictionary.ContainsKey(typeName))
            {
                return _typeFacetDictionary[typeName];
            }
            return _alwaysTrueFacet;
        }
        #endregion

        #region Validation Methods
        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for <paramref name="attributeName"/>
        /// </summary>
        /// <param name="attributeName">the name of the attribute to check against</param>
        /// <param name="value">the value to check</param>
        public static void Validate(string attributeName, object value)
        {
            Validate(GetValidationObject(attributeName), value);
        }

        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for the type <paramref name="typeName"/>
        /// </summary>
        /// <param name="typeName">the type to check against</param>
        /// <param name="value">the value to check</param>
        public static void ValidateType(string typeName, object value)
        {
            Validate(GetValidationObjectFromTypeName(typeName), value);
        }

        /// <summary>
        /// Tests the given value against the given facet
        /// </summary>
        /// <param name="facet">the valid values <paramref name="value"/> can be</param>
        /// <param name="value">the value to check</param>
        public static void Validate(IFacet facet, object value)
        {
            if (ThrowsExceptionsOnValidationFailure)
            {
                if (!facet.TestValue(value))
                {
                    throw new FacetValidationException(facet.Stringify());
                }
            }
        }
        #endregion

        #region No-Throw Validation Methods
        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for <paramref name="attributeName"/>
        /// </summary>
        /// <remarks>
        /// This behaves like <see cref="Validate(string, object)"/> except this does not throw exceptions upon failure
        /// </remarks>
        /// <param name="attributeName">the name of the attribute to check against</param>
        /// <param name="value">the value to check</param>
        /// <returns>whether <paramref name="value"/> is a valid value for <paramref name="attributeName"/></returns>
        public static bool ValidateNoThrow(string attributeName, object value)
        {
            return ValidateNoThrow(GetValidationObject(attributeName), value);
        }


        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for <paramref name="typeName"/>
        /// </summary>
        /// <remarks>
        /// This behaves like <see cref="ValidateType(string, object)"/> except this does not throw exceptions upon failure
        /// </remarks>
        /// <param name="typeName">the type to check against</param>
        /// <param name="value">the value to check</param>
        /// <returns>whether <paramref name="value"/> is a valid value for <paramref name="typeName"/></returns>
        public static bool ValidateTypeNoThrow(string typeName, object value)
        {
            return ValidateNoThrow(GetValidationObjectFromTypeName(typeName), value);
        }

        /// <summary>
        /// Tests the given value against the given facet
        /// </summary>
        /// <remarks>
        /// This behaves like <see cref="Validate(IFacet, object)"/> except this does not throw exceptions upon failure
        /// </remarks>
        /// <param name="facet">the valid values <paramref name="value"/> can be</param>
        /// <param name="value">the value to check</param>
        /// <returns>whether <paramref name="value"/> passes the <paramref name="facet"/> tests</returns>
        public static bool ValidateNoThrow(IFacet facet, object value)
        {
            return facet.TestValue(value);
        }
        #endregion

        #region DataErrorString Methods
        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for <paramref name="attributeName"/>
        /// </summary>
        /// <param name="attributeName">the name of the attribute to check against</param>
        /// <param name="value">the value to check</param>
        /// <returns>the string representation of the error</returns>
        public static string DataErrorString(string attributeName, object value)
        {
            return DataErrorString(GetValidationObject(attributeName), value);
        }


        /// <summary>
        /// Checks whether <paramref name="value"/> is a valid value for the type <paramref name="typeName"/>
        /// </summary>
        /// <param name="typeName">the type to check against</param>
        /// <param name="value">the value to check</param>
        /// <returns>the string representation of the error</returns>
        public static string DataErrorStringType(string typeName, object value)
        {
            return DataErrorString(GetValidationObjectFromTypeName(typeName), value);
        }

        /// <summary>
        /// Tests the given value against the given facet
        /// </summary>
        /// <param name="facet">the valid values <paramref name="value"/> can be</param>
        /// <param name="value">the value to check</param>
        /// <returns>the string representation of the error</returns>
        public static string DataErrorString(IFacet facet, object value)
        {
            if (!facet.TestValue(value))
            {
                return facet.Stringify();
            }
            return string.Empty;
        }
        #endregion

        #region Clamp Methods
        /// <summary>
        /// Clamps <paramref name="value"/> to within the valid range for <paramref name="attributeName"/>
        /// </summary>
        /// <param name="attributeName">the name of the attribute to test against</param>
        /// <param name="value">the value to test</param>
        /// <returns>a value within the range of <paramref name="attributeName"/></returns>
        public static double Clamp(string attributeName, double value)
        {
            return Clamp(GetValidationObject(attributeName), value);
        }

        /// <summary>
        /// Clamps <paramref name="value"/> to within the valid range for <paramref name="typeName"/>
        /// </summary>
        /// <param name="typeName">the name of the type to test against</param>
        /// <param name="value">the value to test</param>
        /// <returns>a value within the range of <paramref name="typeName"/></returns>
        public static double ClampType(string typeName, double value)
        {
            return Clamp(GetValidationObjectFromTypeName(typeName), value);
        }

        /// <summary>
        /// Clamps <paramref name="value"/> to within the valid range for <paramref name="facet"/>
        /// </summary>
        /// <param name="facet">the facet to test against</param>
        /// <param name="value">the value to test</param>
        /// <returns>a value within the range of <paramref name="facet"/></returns>
        public static double Clamp(IFacet facet, double value)
        {
            return facet.ClampValue(value);
        }
        #endregion
    }
}
