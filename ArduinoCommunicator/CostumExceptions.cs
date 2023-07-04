using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ArduinoCommunicator
{
    public class PortAlreadyOpenException : Exception
    {
        // Constructors
        public PortAlreadyOpenException() : base() { }
        public PortAlreadyOpenException(string message) : base($"{{{message}}}") { }
        public PortAlreadyOpenException(string message, Exception e) : base($"{{{message}}}", e) { }

        public override string Message 
        {
            get
            {
                return $"Cannot open port connection since port is already open.";
            }
        }
    }


    public class UnequalNumberOfBracketsException : Exception
    {
        // Constructors
        public UnequalNumberOfBracketsException() : base()
        {
            NumberOfCloseBrackets = 0;
            NumberOfOpenBrackets = 0;
        }

        public UnequalNumberOfBracketsException(string message) : base() { }

        public UnequalNumberOfBracketsException(string message, int numberOfOpenBrackets, int numberOfCloseBrackets) : base($"{{{message}}}")
        {
            NumberOfOpenBrackets = numberOfOpenBrackets;
            NumberOfCloseBrackets = numberOfCloseBrackets;
        }
        public UnequalNumberOfBracketsException(int numberOfOpenBrackets, int numberOfCloseBrackets) : base()
        {
            NumberOfOpenBrackets = numberOfOpenBrackets;
            NumberOfCloseBrackets = numberOfCloseBrackets;
        }


        public UnequalNumberOfBracketsException(string message, Exception e) : base($"{{{message}}}", e) { }




        public int NumberOfOpenBrackets { get; }

        public int NumberOfCloseBrackets { get; }

        public override string Message
        {
            get
            {
                return $"Cannot open port connection since port is already open.";
            }
        }
    }

    public class CommandListEmptyException : Exception
    {
        // Constructors
        public CommandListEmptyException() : base() { }
        public CommandListEmptyException(string message) : base($"{{{message}}}") { }
        public CommandListEmptyException(string message, Exception e) : base($"{{{message}}}", e) { }

        public override string Message
        {
            get
            {
                return $"The command list is empty.";
            }
        }
    }

    public class NoValidCommandFileException : Exception
    {
        // Constructors
        public NoValidCommandFileException(string File) : base() { _file = File; }
        public NoValidCommandFileException(string File, string message) : base(File) { }
        public NoValidCommandFileException(string File, string message, Exception e) : base(File) { }

        private readonly string _file;
        public override string Message
        {
            get
            {
                return $"The file \"{_file}\" is not a valid arduino communication file. Please chose a different file.";
            }
        }
    }

    public class ThemeNotFoundException : Exception
    {
        // Constructors
        public ThemeNotFoundException(string File) : base() { _theme = File; }
        public ThemeNotFoundException(string File, string message) : base(File) { }
        public ThemeNotFoundException(string File, string message, Exception e) : base(File) { }

        private readonly string _theme;
        public override string Message
        {
            get
            {
                return $"The Theme \"{_theme}\" is could not be loaded. Please chose a different theme.";
            }
        }
    }


}
