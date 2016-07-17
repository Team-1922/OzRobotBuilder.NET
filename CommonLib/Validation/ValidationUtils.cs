using CommonLib.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CommonLib.Validation
{
    /// <summary>
    /// A static class of helper methods for validation
    /// </summary>
    public static class ValidationUtils
    {        
        /// <summary>
        /// Extends the working path with given path and next sublevel
        /// </summary>
        /// <param name="path">the path to extend</param>
        /// <param name="nextLocation">the name of the next sublevel to extend to</param>
        /// <returns></returns>
        public static string ExtendWorkingPath(string path, string nextLocation)
        {
            return string.Format("{0}{1}{2}", path, Path.DirectorySeparatorChar, nextLocation);
        }
        /// <summary>
        /// Gets the names of the objects in <paramref name="items"/> with each id; used to get which id's have overlap
        /// </summary>
        /// <typeparam name="T">Adds generalization to this method so any type with a name and ID can be used</typeparam>
        /// <param name="items">the items to process</param>
        /// <param name="workingPath">the current working path to prepend each name in <paramref name="items"/> with in <paramref name="output"/></param>
        /// <param name="output">a map of each used ID and the path to which item is using it</param>
        public static void GetNamesOfId<T>(List<T> items, string workingPath, ref Dictionary<uint, List<string>> output) where T : INamedClass, IIdentificationNumber
        {
            if (output == null)
                return;
            if (items == null)
                return;
            foreach (var item in items)
            {
                if (item == null)
                    continue;
                string workingItemPath = ExtendWorkingPath(workingPath, item.Name);
                if (output.ContainsKey(item.ID))
                    output[item.ID].Add(workingItemPath);
                else
                    output.Add(item.ID, new List<string>() { workingItemPath });
            }
        }
        /// <summary>
        /// Gets the number of different items with the same path
        /// </summary>
        /// <typeparam name="T">adds generalization to this method so any type with a name can be used</typeparam>
        /// <param name="items">the items to get the name from</param>
        /// <param name="workingPath">the current working path to prepend each name in <paramref name="items"/> with in <paramref name="output"/></param>
        /// <param name="output">a map of each name used and the number of times each is used</param>
        public static void GetNamesCount<T>(List<T> items, string workingPath, ref Dictionary<string, int> output) where T : INamedClass
        {
            if (output == null)
                return;
            if (items == null)
                return;
            foreach (var item in items)
            {
                if (item == null)
                    continue;

                string workingItemPath = ExtendWorkingPath(workingPath, item.Name);
                if (output.ContainsKey(workingItemPath))
                    output[workingItemPath]++;
                else
                    output.Add(workingItemPath, 1);
            }
        }
        /// <summary>
        /// Gets a list of validation issues from different items (helpful extension to <see cref="GetNamesCount{T}(List{T}, string, ref Dictionary{string, int})"/>)
        /// </summary>
        /// <typeparam name="T">adds generalization to this method so any type with a name can be used</typeparam>
        /// <param name="items">the items to get the name from</param>
        /// <param name="workingPath">the current working path to prepend each name in <paramref name="items"/> with</param>
        /// <returns>a list of validation issues</returns>
        public static List<IValidationIssue> ReusedNamesValidation<T>(List<T> items, string workingPath) where T : INamedClass
        {
            if (items == null)
                return null;
            List<IValidationIssue> ret = new List<IValidationIssue>();

            //a map for each name used
            var usedNames = new Dictionary<string, int>();

            //names for subsystems
            GetNamesCount(items, workingPath, ref usedNames);

            //get the issues from these names
            foreach (var name in usedNames)
            {
                if (name.Value > 1)
                    ret.Add(new ReusedNameValidationIssue(name.Key));
            }

            return ret;
        }
        /// <summary>
        /// Converts the output of <see cref="GetNamesOfId{T}(List{T}, string, ref Dictionary{uint, List{string}})"/> to a list of <see cref="ReusedIdValidationIssue"/>
        /// </summary>
        /// <param name="map">the output to <see cref="GetNamesOfId{T}(List{T}, string, ref Dictionary{uint, List{string}})"/></param>
        /// <returns>a list of <see cref="ReusedIdValidationIssue"/></returns>
        public static List<ReusedIdValidationIssue> GetIssuesFromIdMap(Dictionary<uint, List<string>> map)
        {
            List<ReusedIdValidationIssue> ret = new List<ReusedIdValidationIssue>();
            foreach (var mapItem in map)
            {
                if (mapItem.Value.Count > 1)
                    ret.Add(new ReusedIdValidationIssue(mapItem.Value[0], mapItem.Value.GetRange(1, mapItem.Value.Count - 1).ToArray()));
            }
            return ret;
        }
        /// <summary>
        /// Checks the given name to see if it is a valid name for the <see cref="INamedClass"/>
        /// </summary>
        /// <param name="name">the name to check</param>
        /// <returns>whether or not the name is valid</returns>
        public static bool CheckName(string name)
        {
            return ValidNames.IsMatch(name);
        }
        private static Regex ValidNames = new Regex(@"^[a-zA-Z][a-zA-Z0-9]*$");
    }
}
