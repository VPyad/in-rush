using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.Compilers
{
    public class SupportedCompilers
    {
        public string Name { get; set; }

        public string Lang { get; set; }

        public string Invocation { get; set; }

        public string VersionCommand { get; set; }

        public int TimeOut { get; set; }

        public IEnumerable<string> Commands { get; set; }
    }
}
