/***************************************************************************

Copyright (c) Microsoft Corporation. All rights reserved.
THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.

***************************************************************************/
// Guids.cs
// MUST match guids.h
using System;

namespace Team1922.OzRobotBuilder.NET
{
    static class GuidList
    {
        public const string guidRobotDesignerPkgString = "28d60403-f5aa-4745-9e52-ac634cbf0c5c";
        public const string guidRobotDesignerCmdSetString = "22de8a49-aa75-49f7-9180-83d225bbc303";
        public const string guidRobotDesignerEditorFactoryString = "6bf3ea12-98bb-41e2-ba01-8662f713d293";

        public static readonly Guid guidRobotDesignerCmdSet = new Guid(guidRobotDesignerCmdSetString);
        public static readonly Guid guidRobotDesignerEditorFactory = new Guid(guidRobotDesignerEditorFactoryString);

        public const string guidXmlChooserEditorFactory = @"{32CC8DFA-2D70-49b2-94CD-22D57349B778}";
    };
}