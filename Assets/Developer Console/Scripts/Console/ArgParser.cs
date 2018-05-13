﻿using System;
using System.Text.RegularExpressions;

namespace Console {

    public class ArgParser {

        internal readonly Regex eventNameRegex;
        internal readonly Regex boolRegex;
        internal readonly Regex charRegex;
        internal readonly Regex intRegex;
        internal readonly Regex floatRegex;
        internal readonly Regex stringRegex;

        public ArgParser() {
            eventNameRegex  = new Regex(@"\s");
            boolRegex       = new Regex(@"^(?i)(true|false)$");
            charRegex       = new Regex(@"^\s{1}$");
            intRegex        = new Regex(@"^\d$");
            floatRegex      = new Regex(@"^[0-9]*(?:\.[0-9]*)?$");
            stringRegex     = new Regex(@"^.+");
        }

        public ArgParser(string eventPattern, string boolPattern, string charPattern, string intPattern, string floatPattern, string stringPattern) {
            eventNameRegex  = new Regex(eventPattern);
            boolRegex       = new Regex(boolPattern);
            charRegex       = new Regex(charPattern);
            intRegex        = new Regex(intPattern);
            floatRegex      = new Regex(floatPattern);
            stringRegex     = new Regex(stringPattern);
        }
        
        /// <summary>
        /// Parses the string and returns the value of the interpretted argument.
        /// </summary>
        /// <param name="arg">The argument to parse.</param>
        /// <returns>The interpretted value of the string.</returns>
        internal dynamic GetParameterValue(string arg) {
            if (IsArgBool(arg)) {
                return TryParseBool(arg);
            } else if (IsArgInt(arg)) {
                return TryParseInt(arg);
            } else if (IsArgFloat(arg)) {
                return float.Parse(arg);
            } else if (IsArgChar(arg)) {
                return char.Parse(arg);
            } else {
                return arg;
            }
        }
        
        /// <summary>
        /// Is the argument a bool?
        /// </summary>
        internal bool IsArgBool(string arg) {
            return boolRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Is the argument an integer?
        /// </summary>
        internal bool IsArgInt(string arg) {
            return intRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Is the argument a floating point number?
        /// </summary>
        internal bool IsArgFloat(string arg) {
            return floatRegex.IsMatch(arg);
        }
            
        /// <summary>
        /// Is the argument a string?
        /// </summary>
        internal bool IsArgString(string arg) {
            return stringRegex.IsMatch(arg);
        }
        
        /// <summary>
        /// Is the argument a single character?
        /// </summary>
        internal bool IsArgChar(String arg) {
            return charRegex.IsMatch(arg);
        }

        /// <summary>
        /// Checks if the eventName has no spaces in between the value.
        /// </summary>
        /// <returns>True, if there is no space within the eventName".</returns>
        internal bool IsEventNameValid(string eventName) {
            return !eventNameRegex.IsMatch(eventName);
        }
        
        /// <summary>
        /// Attempst to parse a string to a bool if able, otherwise the default value
        /// of the bool is returned.
        /// </summary>
        /// <param name="arg">The string to parse.</param>
        /// <returns>The bool value of the arg.</returns>
        internal bool TryParseBool(string arg) {
            bool value;
            bool.TryParse(arg, out value);
            return value;
        }
        
        /// <summary>
        /// Attempts to parse a string to an integer if able, otherwise the default
        /// value of the float is returned.
        /// </summary>
        /// <param name="arg">The string to parse.</param>
        /// <returns>The integer value of arg.</returns>
        internal int TryParseInt(string arg) {
            int value;
            int.TryParse(arg, out value);
            return value;
        }
        
        /// <summary>
        /// Attempts to parse a string to a float if able, otherwise the default
        /// value of the float is returned.
        /// </summary>
        /// <param name="arg">The string to parse.</param>
        /// <returns>The float value of the arg.</param>
        internal float TryParseFloat(string arg) {
            float value;
            float.TryParse(arg, out value);
            return value;
        }
        
        /// <summary>
        /// Parses all parameters into their value types.
        /// </summary>
        /// <param="args">The string based arguments to interpret and get the value.</param>
        /// <returns>An array of objects with their value parsed counterparts.</returns>
        internal object[] ParseParameters(string[] args) {
            switch(args.Length) {
                case 1:
                    return new object[] { 
                        GetParameterValue(args[0])
                    };
                case 2:
                    return new object[] { 
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1])
                    };
                case 3:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2])
                    };
                case 4:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2]),
                        GetParameterValue(args[3]),
                    };
                case 5:
                    return new object[] {
                        GetParameterValue(args[0]),
                        GetParameterValue(args[1]),
                        GetParameterValue(args[2]),
                        GetParameterValue(args[3]),
                        GetParameterValue(args[4])
                    };
                default:
                    return new object[] {};
            }
        }

        /// <summary>
        /// Stores the type of each argument in an array. This is only used for .NET 3.5 for reflections.
        /// </summary>
        /// <param name="args">The parsed arguments.</param>
        /// <returns>An array of types respective to each argument.</returns>
        internal static Type[] GetParameterTypes(object[] args) {
            switch(args.Length) {
                case 1:
                    return new Type[] {
                        args[0].GetType()
                    };
                case 2:
                    return new Type[] {
                        args[0].GetType(),
                        args[1].GetType()
                    };
                case 3:
                    return new Type[] {
                        args[0].GetType(),
                        args[1].GetType(),
                        args[2].GetType()
                    };
                case 4:
                    return new Type[] {
                        args[0].GetType(),
                        args[1].GetType(),
                        args[2].GetType(),
                        args[3].GetType()
                    };
                default:
                    return new Type[] {};
            }
        }
    }
}
