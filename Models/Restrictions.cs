//
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

namespace Team1922.MVVM.Models
{
	public static partial class TypeRestrictions
	{
		private static System.Collections.Generic.Dictionary<string, IFacet> _typeFacetDictionary = new System.Collections.Generic.Dictionary<string, IFacet>()
					{{ "ItemName",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.PatternFacet(@"^([0-9]|[a-z]|[A-Z]|_)+$")}) }, { "PathName",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.PatternFacet(@"^([0-9]|[a-z]|[A-Z]|_|\.)+$")}) }, { "AnalogInputId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(3)}) }, { "DigitalInputId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(9)}) }, { "PWMOutputId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(9)}) }, { "RelayOutputId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(3)}) }, { "CANId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1)}) }, { "JoystickId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(5)}) }, { "JoystickButtonId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.PatternFacet(@"^[^0].*"),new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(12)}) }, { "JoystickAxisId",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(4)}) }, { "RawAnalogInput",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(0),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(4095)}) }, { "RawSRXInput",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(0),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(1023)}) }, { "NormalizedDouble",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(-1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(1)}) }, { "XPath",new FacetCollection(new IFacet[]{}) }, { "TeamNumber",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(1)}) }, { "AnalogInputSampleRate",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(1),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(62500)}) }, { "RelayDirection",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("ForwardOnly"),new Team1922.MVVM.Models.XML.EnumerationFacet("ReverseOnly"),new Team1922.MVVM.Models.XML.EnumerationFacet("Both")}) }, { "RelayValue",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("Off"),new Team1922.MVVM.Models.XML.EnumerationFacet("Forward"),new Team1922.MVVM.Models.XML.EnumerationFacet("Reverse"),new Team1922.MVVM.Models.XML.EnumerationFacet("On")}) }, { "CANTalonFeedbackDevice",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("QuadEncoder"),new Team1922.MVVM.Models.XML.EnumerationFacet("AnalogPotentiometer"),new Team1922.MVVM.Models.XML.EnumerationFacet("AnalogEncoder"),new Team1922.MVVM.Models.XML.EnumerationFacet("EncoderRising"),new Team1922.MVVM.Models.XML.EnumerationFacet("EncoderFalling"),new Team1922.MVVM.Models.XML.EnumerationFacet("CtreMagEncoderRelative"),new Team1922.MVVM.Models.XML.EnumerationFacet("CtreMagEncoderAbsolute"),new Team1922.MVVM.Models.XML.EnumerationFacet("PulseWidth")}) }, { "CANTalonControlMode",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("PercentVbus"),new Team1922.MVVM.Models.XML.EnumerationFacet("Position"),new Team1922.MVVM.Models.XML.EnumerationFacet("Speed"),new Team1922.MVVM.Models.XML.EnumerationFacet("Current"),new Team1922.MVVM.Models.XML.EnumerationFacet("Voltage"),new Team1922.MVVM.Models.XML.EnumerationFacet("Follower"),new Team1922.MVVM.Models.XML.EnumerationFacet("MotionProfile"),new Team1922.MVVM.Models.XML.EnumerationFacet("Disabled")}) }, { "CANTalonNeutralMode",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("Jumper"),new Team1922.MVVM.Models.XML.EnumerationFacet("Brake"),new Team1922.MVVM.Models.XML.EnumerationFacet("Coast")}) }, { "CANTalonLimitMode",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("SwitchInputsOnly"),new Team1922.MVVM.Models.XML.EnumerationFacet("SoftPositionLimits"),new Team1922.MVVM.Models.XML.EnumerationFacet("SrxDisableSwitchInputs")}) }, { "CANTalonDifferentiationLevel",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.EnumerationFacet("Displacement"),new Team1922.MVVM.Models.XML.EnumerationFacet("Speed")}) }, { "CANTalonNativeUnits",new FacetCollection(new IFacet[]{new Team1922.MVVM.Models.XML.MinInclusiveFacet(0),new Team1922.MVVM.Models.XML.MaxInclusiveFacet(1023)}) }};
		private static System.Collections.Generic.Dictionary<string,string> _attributeTypeDictionary = new System.Collections.Generic.Dictionary<string,string>(){{ "Robot.TeamNumber","TeamNumber" }, { "Robot.AnalogInputSampleRate","AnalogInputSampleRate" }, { "JoystickAxis.ID","JoystickAxisId" }, { "JoystickAxis.Value","NormalizedDouble" }, { "JoystickButton.ID","JoystickButtonId" }, { "JoystickButton.Value","boolean" }, { "Joystick.Name","ItemName" }, { "Joystick.ID","JoystickId" }, { "EventHandler.Name","string" }, { "EventHandler.Expression","string" }, { "EventHandler.Condition","string" }, { "EventHandler.ConditionMet","boolean" }, { "RobotMapEntry.Key","ItemName" }, { "RobotMapEntry.Value","RobotMapEntry.Value" }, { "Subsystem.Name","ItemName" }, { "Subsystem.SoftwarePIDEnabled","boolean" }, { "AnalogInput.ID","AnalogInputId" }, { "AnalogInput.Name","ItemName" }, { "AnalogInput.AccumulatorCenter","RawAnalogInput" }, { "AnalogInput.AccumulatorDeadband","RawAnalogInput" }, { "AnalogInput.AccumulatorInitialValue","RawAnalogInput" }, { "AnalogInput.AverageBits","int" }, { "AnalogInput.OversampleBits","int" }, { "AnalogInput.ConversionRatio","double" }, { "AnalogInput.SensorOffset","double" }, { "AnalogInput.RawValue","RawAnalogInput" }, { "AnalogInput.RawAverageValue","long" }, { "AnalogInput.RawAccumulatorValue","long" }, { "AnalogInput.Value","double" }, { "AnalogInput.AverageValue","double" }, { "AnalogInput.AccumulatorCount","long" }, { "AnalogInput.AccumulatorValue","double" }, { "PWMOutput.ID","PWMOutputId" }, { "PWMOutput.Name","ItemName" }, { "PWMOutput.Value","NormalizedDouble" }, { "RelayOutput.ID","RelayOutputId" }, { "RelayOutput.Name","ItemName" }, { "RelayOutput.Direction","RelayDirection" }, { "RelayOutput.Value","RelayValue" }, { "DigitalInput.ID","DigitalInputId" }, { "DigitalInput.Name","ItemName" }, { "DigitalInput.Value","boolean" }, { "QuadEncoder.ID","DigitalInputId" }, { "QuadEncoder.ID1","DigitalInputId" }, { "QuadEncoder.Name","ItemName" }, { "QuadEncoder.ConversionRatio","double" }, { "QuadEncoder.RawValue","long" }, { "QuadEncoder.Value","double" }, { "QuadEncoder.RawVelocity","double" }, { "QuadEncoder.Velocity","double" }, { "PIDControllerSoftware.P","double" }, { "PIDControllerSoftware.I","double" }, { "PIDControllerSoftware.D","double" }, { "PIDControllerSoftware.F","double" }, { "PIDControllerSoftware.Tolerance","double" }, { "PIDControllerSoftware.CycleDuration","double" }, { "PIDControllerSoftware.Continuous","boolean" }, { "PIDControllerSRX.P","double" }, { "PIDControllerSRX.I","double" }, { "PIDControllerSRX.D","double" }, { "PIDControllerSRX.F","double" }, { "PIDControllerSRX.IZone","int" }, { "PIDControllerSRX.CloseLoopRampRate","double" }, { "PIDControllerSRX.AllowableCloseLoopError","RawSRXInput" }, { "PIDControllerSRX.SourceType","CANTalonDifferentiationLevel" }, { "CANTalonAnalogInput.RawValue","CANTalonNativeUnits" }, { "CANTalonAnalogInput.RawVelocity","double" }, { "CANTalonAnalogInput.Value","double" }, { "CANTalonAnalogInput.Velocity","double" }, { "CANTalonAnalogInput.ConversionRatio","double" }, { "CANTalonAnalogInput.SensorOffset","double" }, { "CANTalonQuadEncoder.RawValue","long" }, { "CANTalonQuadEncoder.RawVelocity","double" }, { "CANTalonQuadEncoder.Value","double" }, { "CANTalonQuadEncoder.Velocity","double" }, { "CANTalonQuadEncoder.ConversionRatio","double" }, { "CANTalonQuadEncoder.SensorOffset","double" }, { "CANTalon.ID","CANId" }, { "CANTalon.Name","ItemName" }, { "CANTalon.EnabledPIDProfile","boolean" }, { "CANTalon.FeedbackDevice","CANTalonFeedbackDevice" }, { "CANTalon.ControlMode","CANTalonControlMode" }, { "CANTalon.NeutralMode","CANTalonNeutralMode" }, { "CANTalon.ZeroSensorPositionOnIndexEnabled","boolean" }, { "CANTalon.ZeroSensorPositionOnRisingEdge","boolean" }, { "CANTalon.ReverseSensor","boolean" }, { "CANTalon.ReverseClosedLoopOutput","boolean" }, { "CANTalon.ReversePercentVBusOutput","boolean" }, { "CANTalon.ForwardLimitSwitchEnabled","boolean" }, { "CANTalon.ReverseLimitSwitchEnabled","boolean" }, { "CANTalon.ForwardSoftLimitEnabled","boolean" }, { "CANTalon.ReverseSoftLimitEnabled","boolean" }, { "CANTalon.ForwardSoftLimit","double" }, { "CANTalon.ReverseSoftLimit","double" }, { "CANTalon.NominalForwardVoltage","double" }, { "CANTalon.NominalReverseVoltage","double" }, { "CANTalon.PeakForwardVoltage","double" }, { "CANTalon.PeakReverseVoltage","double" }, { "CANTalon.Value","double" }, { "CANTalon.ForwardLimitSwitch","boolean" }, { "CANTalon.ReverseLimitSwitch","boolean" }, { "CANTalon.ForwardSoftLimitTripped","boolean" }, { "CANTalon.ReverseSoftLimitTripped","boolean" }};
		private static IFacet _alwaysTrueFacet = new FacetCollection(new IFacet[]{ new PatternFacet("^.*$") });
	}
}
