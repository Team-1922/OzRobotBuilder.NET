<?xml version="1.0" encoding="utf-8"?>
<Robot xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" TeamNumber="1922" xmlns="http://github.com/Team-1922/OzRobotBuilder.NET/blob/master/Models/RobotSchema.xsd">
  <Subsystem Name="TestSubsystem" ID="0">
    <PWMOutput ID="0" Name="TestMotorModifiedXML_" />
    <AnalogInput ID="0" Name="TestInput" />
  </Subsystem>
  <EventHandler Name="NewEventHandler" Expression="([EventHandlers.NewEventHandler.ConditionEvaluated] + 9*9) + 60" Condition="5+6" />
  <RobotMap>
    <Entry Key="TestKey" Value="TestValueModified" />
  </RobotMap>
</Robot>