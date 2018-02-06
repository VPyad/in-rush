using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.CommandProcess
{
    public class CommandProcessorError
    {
        ErrorType ErrorType { get; set; }

        public string Content { get; set; }

        public string DiagnosticContent { get; set; }
    }

    public enum ErrorType
    {
        CompilationError = 0,
        RuntimeError = 1,
        TimeOut = 2,
        OutputIncorrect = 3,
        OutOfMemmory = 4,
        UnexpexctedError = 5
    }
}
