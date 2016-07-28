//
//
// XSDFixer.EXE
//
//  This application is run after running xsd on the XML schema to
//      remove certain attributes of the generated .cs file
//      and put others in preprocessor blocks to allow use of this
//      .cs file in .net core applications
//
//
//
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace XSDFixer
{
    public class Program
    {
        delegate void WL(string value);
        public static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Please Enter the Path to the .xsd File to Fix, the code name of the .NET framework to switch against, and the namespace to surround the file with");
                Console.WriteLine("Example: \"<thisprogram.exe> MySchema.cs NET461 Team1922.MVVM.Models");
                return;
            }

            string line;

            try
            {
                //create the output stream
                var tmpFileName = $"{args[0]}.tmp";
                File.Delete(tmpFileName);
                bool abort = false;
                using (var outFile = new StreamWriter(File.Open(tmpFileName, FileMode.CreateNew)))
                {
                    // Read the file and display it line by line.
                    using (var file = new StreamReader(File.Open(args[0], FileMode.Open)))
                    {
                        bool startTabbing = false;
                        WL WriteLine = (string value) => outFile.WriteLine(startTabbing ? "\t" + value : value);

                        //start by writing our signature
                        WriteLine("//This File Has Been Modified by XSDFixer");
                        while ((line = file.ReadLine()) != null)
                        {
                            if(line.Contains("//This File Has Been Modified by XSDFixer"))
                            {
                                //STOP; the work has already been done
                                abort = true;
                                break;
                            }
                            else if (line.Contains("[System.ComponentModel.DesignerCategoryAttribute(\"code\")]"))
                            {
                                outFile.WriteLine($"#if {args[1]}");
                                WriteLine(line);
                                outFile.WriteLine("#endif");
                            }
                            else if(line.Contains("[System.SerializableAttribute()]"))
                            {
                                //skip this line
                            }
                            else
                            {
                                WriteLine(line);
                            }
                        }
                        //after the last line close the namespace definition
                        //outFile.WriteLine("}");
                    }
                }
                //delete the original
                if (!abort)
                {
                    File.Delete(args[0]);
                    File.Move(tmpFileName, args[0]);
                }
                File.Delete(tmpFileName);
            }
            catch(FileNotFoundException e)
            {
                Console.WriteLine($"Failed to Open File: \"{e.Message}\"");
            }
            catch(IOException e)
            {
                Console.WriteLine($"Failed to Create Temp File: \"{e.Message}\"");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Other Exception: \"{e.Message}\"");
            }
        }
    }
}
