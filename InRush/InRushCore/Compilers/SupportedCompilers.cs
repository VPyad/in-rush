using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.Compilers
{
    public class SupportedCompilers
    {
        /// <summary>
        /// Throws ValidationException if params are null or empty
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="lang"></param>
        /// <param name="invocation"></param>
        /// <param name="versionCommand"></param>
        /// <param name="timeOut"></param>
        /// <param name="commands"></param>
        public SupportedCompilers(string id, string name, string lang, string invocation, string versionCommand, int timeOut, IEnumerable<string> commands)
        {
            Id = CompilersConfigHelper.ValidateNullOrEmpty(id);
            Name = CompilersConfigHelper.ValidateNullOrEmpty(name);
            Lang = CompilersConfigHelper.ValidateNullOrEmpty(lang);
            Invocation = CompilersConfigHelper.ValidateNullOrEmpty(invocation);
            VersionCommand = CompilersConfigHelper.ValidateNullOrEmpty(versionCommand);
            TimeOut = timeOut;
            Commands = CompilersConfigHelper.ValidateNullOrEmpty(commands);
        }

        public SupportedCompilers()
        { }

        public SupportedCompilers(SupportedCompilers supportedCompilers)
        {
            Id = supportedCompilers.Id;
            Name= supportedCompilers.Name;
            Lang = supportedCompilers.Lang;
            Invocation = supportedCompilers.Invocation;
            VersionCommand = supportedCompilers.VersionCommand;
            TimeOut = supportedCompilers.TimeOut;
            Commands = supportedCompilers.Commands;
        }
                
        public string Id { get; set; }

        public string Name { get; set; }

        public string Lang { get; set; }

        public string Invocation { get; set; }

        public string VersionCommand { get; set; }

        public int TimeOut { get; set; }

        public IEnumerable<string> Commands { get; set; }
    }
}
