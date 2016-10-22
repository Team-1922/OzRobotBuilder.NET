using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Team1922.MVVM.Models
{
    /// <summary>
    /// Contains the <see cref="XmlAttributeOverrides"/> for Robot Serization
    /// </summary>
    public static class RobotStateValues
    {
        private static XmlAttributeOverrides _overrides;

        /// <summary>
        /// The XML Attribute Overrides for Serialization
        /// </summary>
        /// <remarks>
        /// This is very helpful so the state properties, such as a sensor value, are not serialized to 
        /// xml files in the editor or on the robot.  This way the robot xml file is not constantly changing 
        /// and thus being very inconvenient for source control.
        /// </remarks>
        public static XmlAttributeOverrides Overrides
        {
            get
            {
                if(null == _overrides)
                {
                    _overrides = new XmlAttributeOverrides();

                    //iterate through each type
                    foreach(var type in _stateValueNames)
                    {
                        //create a new set of attributes
                        XmlAttributes attributes = new XmlAttributes();
                        attributes.XmlIgnore = true;

                        //iterate through each property
                        foreach (var property in type.Value)
                        {
                            //add each property to tha attribute element list
                            attributes.XmlElements.Add(new XmlElementAttribute(property));
                        }

                        //finally add this attribute and type to the overrides list
                        _overrides.Add(type.Key, attributes);
                    }
                }

                return _overrides;
            }
        }


        private static Dictionary<Type, string[]> _stateValueNames =
            new Dictionary<Type, string[]>
            {
                {
                    typeof(Joystick), new string[]
                    {
                        "Axis",
                        "Button"
                    }
                },
                {
                    typeof(EventHandler), new string[]
                    {
                        "ConditionMet"
                    }
                },
                {
                    typeof(Solenoid), new string[]
                    {
                        "Value"
                    }
                },
                {
                    typeof(DoubleSolenoid), new string[]
                    {
                        "Value"
                    }
                },
                {
                    typeof(PWMOutput), new string[]
                    {
                        "Value"
                    }
                },
                {
                    typeof(RelayOutput), new string[]
                    {
                        "Value"
                    }
                },
                {
                    typeof(AnalogInput), new string[]
                    {
                        "RawValue",
                        "RawAverageValue",
                        "RawAccumulatorValue",
                        "Value",
                        "AverageValue",
                        "AccumulatorCount",
                        "AccumulatorValue"
                    }
                },
                {
                    typeof(DigitalInput), new string[]
                    {
                        "Value"
                    }
                },
                {
                    typeof(QuadEncoder), new string[]
                    {
                        "RawValue",
                        "Value",
                        "RawVelocity",
                        "Velocity"
                    }
                },
                {
                    typeof(CANTalon), new string[]
                    {
                        "Value",
                        "ForwardLimitSwitch",
                        "ReverseLimitSwitch",
                        "ForwardSoftLimitTripped",
                        "ReverseSoftLimitTripped",
                    }
                },
                {
                    typeof(CANTalonAnalogInput), new string[]
                    {
                        "RawValue",
                        "Value",
                        "RawVelocity",
                        "Velocity"
                    }
                },
                {
                    typeof(CANTalonQuadEncoder), new string[]
                    {
                        "RawValue",
                        "Value",
                        "RawVelocity",
                        "Velocity"
                    }
                }
            };
    }
}
