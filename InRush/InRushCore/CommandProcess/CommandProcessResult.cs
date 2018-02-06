using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.CommandProcess
{
    public class CommandProcessResult
    {
        public string PathToOutputFile { get; set; }

        /// <summary>
        /// Program RAM usage during execution in Kb
        /// </summary>
        public int RamUsage { get; set; }

        /// <summary>
        /// Program execution time in ms
        /// </summary>
        public int ExecutionTime { get; set; }

        public CommandProcessorError Error { get; set; }

        public bool HasError { get { return Error != null; } }
    }
}
