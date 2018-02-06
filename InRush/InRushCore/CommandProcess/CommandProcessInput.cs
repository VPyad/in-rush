using InRushCore.Compilers;
using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.CommandProcess
{
    public class CommandProcessInput
    {
        SupportedCompilers Compiler { get; set; }

        public string PathToSrcFile { get; set; }

        public string PathToInputFile { get; set; }

        /// <summary>
        /// Optional. If specified CommandProcessor will compare output.txt content witn answer.txt
        /// </summary>
        public string PathToAnswerFile { get; set; }
    }
}
