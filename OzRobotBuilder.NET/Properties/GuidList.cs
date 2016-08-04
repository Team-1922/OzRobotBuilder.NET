using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.OzRobotBuilder.NET
{
    class GuidList
    {
        public const string guidRobotDesignerPkgString = "D4547002-D1FF-44CF-8861-AF62C449A5E6";
        public const string guidRobotDesignerCmdSetString = "6FD4EC0D-3577-490C-8C88-164AECCD09C8";
        public const string guidRobotDesignerEditorFactoryString = "A50A15A8-3924-4B0C-A767-C894466AA808";

        public static readonly Guid guidRobotDesignerCmdSet = new Guid(guidRobotDesignerCmdSetString);
        public static readonly Guid guidRobotDesignerEditorFactory = new Guid(guidRobotDesignerEditorFactoryString);
        public const string guidXmlChooserEditorFactory = @"{32CC8DFA-2D70-49b2-94CD-22D57349B778}";
    }
}
