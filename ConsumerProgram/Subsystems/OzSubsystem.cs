using System;
using WPILib.Commands;

namespace ConsumerProgram.Subsystems
{
    public class OzSubsystem : Subsystem, ILuaExt
    {
        public void SetExtScript(string resPath)
        {

        }

        public string GetFormattedExtScriptText()
        {
            return "";
        }

        // Put methods for controlling this subsystem
        // here. Call these from Commands.

        protected override void InitDefaultCommand()
        {
            // Set the default command for a subsystem here.
            //SetDefaultCommand(new MySpecialCommand());
        }
    }
}
