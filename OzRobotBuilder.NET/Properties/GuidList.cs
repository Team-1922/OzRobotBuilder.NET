using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Team1922.OzRobotBuilder.NET
{
    static class GuidList
    {
        public const string guidRobotBuilderPkgString = "09115FB6-7344-4BDF-A7C6-B1A8D7FCA1A8";
        public const string guidRobotBuilderCmdSetString = "231586E6-ECC0-4E85-9E52-13D22A853E44";
        public const string guidRobotBuilderEditorFactoryString = "C4985919-195B-4673-8053-635C6C74C2D1";

        public static readonly Guid guidRobotBuilderCmdSet = new Guid(guidRobotBuilderCmdSetString);
        public static readonly Guid guidRobotBuilderEditorFactory = new Guid(guidRobotBuilderEditorFactoryString);

        //public const string guidXmlChooserEditorFactory = @"{9BC76D79-547D-4626-8A86-56F73444B4C8}";
    }
}
