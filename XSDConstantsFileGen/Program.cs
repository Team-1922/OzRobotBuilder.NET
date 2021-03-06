﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Schema;
using Team1922.MVVM.Models.XML;

namespace XSDConstantsFileGen
{
    class Program
    {
        static string Header =
@"//
//  Generated by XSDConstantsFileGen
//
//  This tool was designed to temporarily 
//      serve as a way to make use of XML
//      schemas without them being available
//      in .NET core yet.  
//      
//  The XSD.exe generator
//      does a lot of the work, but does not
//      insert the object validation features
//      Below is a class hierarchy of the 
//      schema types and means of validating
//      them

using Team1922.MVVM.Models.XML;
";
        static string LookupMethod =
@"
        public static IFacet GetValidationObject(string attributeName)
        {
            if(_attributeTypeDictionary.ContainsKey(attributeName))
            {
                string key = _attributeTypeDictionary[attributeName];
                if(_typeFacetDictionary.ContainsKey(key))
                {
                    return _typeFacetDictionary[key];
                }
            }
            return _alwaysTrueFacet;
        }
";

        static string ValidateMethod =
@"
        // NOTE: this throws and exception when validation fails
        public static void Validate(string attributeName, object value)
        {
            var facet = GetValidationObject(attributeName);
            if(!facet.TestValue(value))
            {
                throw new System.ArgumentException(facet.Stringify());
            }
        }
";

        static string DataErrorStringMethod =
@"
        // NOTE: this returns a string representation of the error
        public static string DataErrorString(string attributeName, object value)
        {
            var facet = GetValidationObject(attributeName);
            if(!facet.TestValue(value))
            {
                return facet.Stringify();
            }
            return string.Empty;
        }
";

        static string ClampMethod =
@"
        public static double Clamp(string attributeName, double value)
        {
            var facet = GetValidationObject(attributeName);
            return facet.ClampValue(value);
        }
";
        //delegate void WL(string line);
        //delegate void GenerateSourceRecursive(Dictionary<string, FacetCollection> sub);
        static void MakeLine(string line, int tabCount, ref StringWriter writer)
        {
            if (null == writer)
                return;
            for (int i = 0; i < tabCount; ++i)
                writer.Write("\t");
            writer.WriteLine(line);
        }
        //
        //Generates the entire source code file from the collected dinformation
        static string GenerateSourceFromAttributes(Dictionary<string, FacetCollection> attributes, Dictionary<string, string> attributeTypes, string namespaceName)
        {
            int tabCount = 1;
            StringWriter outputString = new StringWriter();

            //write the header to the beginning of the file
            outputString.WriteLine(Header);

            //write the namespace declaration
            outputString.WriteLine($"namespace {namespaceName}");
            outputString.WriteLine("{");

            //write the class declaration
            MakeLine("public static partial class TypeRestrictions", tabCount, ref outputString);
            MakeLine("{", tabCount++, ref outputString);

            //generate the Type Facet Dictionary
            MakeLine("private static System.Collections.Generic.Dictionary<string, IFacet> _typeFacetDictionary = new System.Collections.Generic.Dictionary<string, IFacet>()", tabCount, ref outputString);

            StringWriter dictionaryLine = new StringWriter();
            dictionaryLine.Write("{");
            bool isFirst = true;
            foreach (var element in attributes)
            {
                if (!isFirst)
                    dictionaryLine.Write(", ");
                if (isFirst)
                    isFirst = false;
                dictionaryLine.Write($"{{ \"{element.Key}\",{element.Value.GetConstructionString()} }}");
            }
            dictionaryLine.Write("};");
            MakeLine(dictionaryLine.ToString(), tabCount + 3, ref outputString);
        
            //generate the attribute type dictionary
            MakeLine(GenerateAttributeTypeDictionaryConstructor(attributeTypes), tabCount, ref outputString);
            
            //generate the always true facet
            MakeLine("private static IFacet _alwaysTrueFacet = new FacetCollection(new IFacet[]{ new PatternFacet(\"^.*$\") });", tabCount, ref outputString);
            
            //finally write the lookup method into the class
            //outputString.WriteLine(LookupMethod);
            //outputString.WriteLine(ValidateMethod);
            //outputString.WriteLine(DataErrorStringMethod);
            //outputString.WriteLine(ClampMethod);

            MakeLine("}", --tabCount, ref outputString);

            outputString.WriteLine("}");
            return outputString.ToString();
        }
        [Obsolete]
        static string Recurse(Dictionary<string, FacetCollection> atbs, int tabCount)
        {
            StringWriter outputString = new StringWriter();
            Dictionary<string, Dictionary<string, FacetCollection>> toRecurse = new Dictionary<string, Dictionary<string, FacetCollection>>();
            foreach (var attribute in atbs)
            {
                var splitName = attribute.Key.Split(new char[] { '.' }, 2, StringSplitOptions.None);
                if (null == splitName)
                    continue;
                if (splitName.Length == 0)
                    continue;
                if (splitName.Length == 1)
                {
                    string line = $"public static FacetCollection {attribute.Key} {{ get; }} = {attribute.Value.GetConstructionString()};";
                    MakeLine(line, tabCount, ref outputString);
                }
                else
                {
                    if (!toRecurse.ContainsKey(splitName[0]))
                        toRecurse[splitName[0]] = new Dictionary<string, FacetCollection>();
                    toRecurse[splitName[0]].Add(splitName[1], attribute.Value);
                }
            }
            foreach (var next in toRecurse)
            {
                if (next.Value == null)
                    continue;

                MakeLine($"public static class {next.Key}", tabCount, ref outputString);
                MakeLine("{", tabCount, ref outputString);

                //this is the meat of it
                outputString.WriteLine(Recurse(next.Value, tabCount + 1));

                MakeLine("}", tabCount, ref outputString);
            }
            return outputString.ToString();
        }
        static FacetCollection TranslateFacets(XmlSchemaObjectCollection facets)
        {
            FacetCollection ret = new FacetCollection();
            foreach (XmlSchemaFacet facet in facets)
            {
                if (facet is XmlSchemaEnumerationFacet)
                {
                    ret.AddFacet(new EnumerationFacet(facet.Value));
                }
                else if (facet is XmlSchemaMaxExclusiveFacet)
                {
                    ret.AddFacet(new MaxExclusiveFacet(facet.Value));
                }
                else if (facet is XmlSchemaMaxInclusiveFacet)
                {
                    ret.AddFacet(new MaxInclusiveFacet(facet.Value));
                }
                else if (facet is XmlSchemaMinExclusiveFacet)
                {
                    ret.AddFacet(new MinExclusiveFacet(facet.Value));
                }
                else if (facet is XmlSchemaMinInclusiveFacet)
                {
                    ret.AddFacet(new MinInclusiveFacet(facet.Value));
                }
                else if (facet is XmlSchemaPatternFacet)
                {
                    ret.AddFacet(new PatternFacet(facet.Value));
                }
                else if (facet is XmlSchemaFractionDigitsFacet)
                {
                    ret.AddFacet(new FractionDigitsFacet(facet.Value));
                }
                else if (facet is XmlSchemaLengthFacet)
                {
                    ret.AddFacet(new LengthFacet(facet.Value));
                }
                else if(facet is XmlSchemaMinLengthFacet)
                {
                    ret.AddFacet(new MinLengthFacet(facet.Value));
                }
                else if(facet is XmlSchemaMaxLengthFacet)
                {
                    ret.AddFacet(new MaxLengthFacet(facet.Value));
                }
                else if (facet is XmlSchemaTotalDigitsFacet)
                {
                    ret.AddFacet(new TotalDigitsFacet(facet.Value));
                }
            }
            return ret;
        }
        //
        // Generates a the attribute type dictionary member definition from a dictionary
        static string GenerateAttributeTypeDictionaryConstructor(Dictionary<string, string> attributeTypeDictionary)
        {
            StringWriter outputString = new StringWriter();
            Dictionary<string, string> test = new Dictionary<string, string>() { { "me", "you" }, { "foo", "bar" } };

            //write the constant definition
            outputString.Write("private static System.Collections.Generic.Dictionary<string,string> _attributeTypeDictionary = new System.Collections.Generic.Dictionary<string,string>()");
            
            //generate the elements
            outputString.Write("{");
            bool isFirst = true;
            foreach (var element in attributeTypeDictionary)
            {
                if (!isFirst)
                    outputString.Write(", ");
                if (isFirst)
                    isFirst = false;
                outputString.Write($"{{ \"{element.Key}\",\"{element.Value}\" }}");
            }
            outputString.Write("};");
            return outputString.ToString();
        }
        static void IterateTypes(XmlSchemaObjectTable schemaTypes, Dictionary<string, FacetCollection> testDictionary, Dictionary<string, string> attributeTypeMap)
        {
            // First iterate through all of the simple types
            foreach (XmlSchemaType schemaObject in schemaTypes.Values)
            {
                Console.WriteLine("Type Name: {0}", schemaObject.Name);

                var simpleType = schemaObject as XmlSchemaSimpleType;
                if(null != simpleType)
                {
                    //does this simple type have restrictions?
                    var restrictions = simpleType.Content as XmlSchemaSimpleTypeRestriction;
                    if(null != restrictions)
                    {
                        testDictionary.Add(schemaObject.Name, TranslateFacets(restrictions.Facets));
                    }
                }
            }
            // Second iterate through all of the complex types and map their attribute restrictions
            foreach (XmlSchemaType schemaObject in schemaTypes.Values)
            {
                var complexType = schemaObject as XmlSchemaComplexType;
                if (null != complexType)
                {
                    // iterate through each attribute 
                    foreach (XmlSchemaAttribute attribute in complexType.Attributes)
                    {

                        //also generate a dictionary of attribute names and their types
                        string schemaTypeName = attribute.SchemaTypeName.ToString();
                        if (schemaTypeName.Length == 0)
                            attributeTypeMap.Add($"{schemaObject.Name}.{attribute.Name}", $"{schemaObject.Name}.{attribute.Name}");
                        else
                        {
                            //only get what's after the ':'
                            var splitSchemaTypeName = schemaTypeName.Split(':');
                            if (splitSchemaTypeName.Length >= 2)
                            {
                                schemaTypeName = splitSchemaTypeName.Last();
                            }
                            attributeTypeMap.Add($"{schemaObject.Name}.{attribute.Name}", $"{schemaTypeName}");
                        }


                        var schemaType = attribute.SchemaType;
                        if (null == schemaType)
                            continue;

                        // note only null names are accepted here, becuase non-null names
                        //  would have already been added in the first names
                        if (schemaType.Name == null)
                        {
                            var restrictions = schemaType.Content as XmlSchemaSimpleTypeRestriction;
                            if (null != restrictions)
                            {
                                Console.WriteLine("Attribute Name: {0}.{1}", schemaObject.Name, attribute.Name);
                                testDictionary.Add($"{schemaObject.Name}.{attribute.Name}", TranslateFacets(restrictions.Facets));
                            }
                        }
                    } 
                    
                    //iterate through each element (this is to check for elements with restrictions on the number of items it can have
                    //TODO:                      
                }                
            }
        }

        static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Please enter arguments in the following way: <in-file.xsd> <out-file.cs> <out-namespace>");
                return;
            }
            
            // Add the customer schema to a new XmlSchemaSet and compile it.
            // Any schema validation warnings and errors encountered reading or 
            // compiling the schema are handled by the ValidationEventHandler delegate.
            XmlSchemaSet schemaSet = new XmlSchemaSet();
            schemaSet.ValidationEventHandler += new ValidationEventHandler(ValidationCallback);
            schemaSet.Add("http://github.com/Team-1922/OzRobotBuilder.NET/blob/master/Models/RobotSchema.xsd", args[0]);
            schemaSet.Compile();

            // Retrieve the compiled XmlSchema object from the XmlSchemaSet
            // by iterating over the Schemas property.
            XmlSchema schema = null;
            foreach (XmlSchema sch in schemaSet.Schemas())
            {
                schema = sch;
            }

            var testDictionary = new Dictionary<string, FacetCollection>();
            var attributeTypeMap = new Dictionary<string, string>();
            using (var outFile = new StreamWriter(File.Open(args[1], FileMode.Create)))
            {
                IterateTypes(schema.SchemaTypes, testDictionary, attributeTypeMap);
                outFile.Write(GenerateSourceFromAttributes(testDictionary, attributeTypeMap, args[2]));
            }
        }

        static void ValidationCallback(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.Write("WARNING: ");
            else if (args.Severity == XmlSeverityType.Error)
                Console.Write("ERROR: ");

            Console.WriteLine(args.Message);
        }
    }
}
