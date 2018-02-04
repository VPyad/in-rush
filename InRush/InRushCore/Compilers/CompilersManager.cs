using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Linq;

namespace InRushCore.Compilers
{
    public class CompilersManager : ISupportedCompilers
    {
        public string PathToConfig { get; set; } = @"\Configs\Compilers\compilers_config_sample.json";

        private JObject JsonConfig;

        public CompilersManager()
        {
            JsonConfig = JObject.Parse(File.ReadAllText(PathToConfig));
        }

        public bool AddCompiler(SupportedCompilers compiler)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompiler(SupportedCompilers compiler)
        {
            throw new NotImplementedException();
        }

        public bool DeleteCompiler(string compilerName)
        {
            throw new NotImplementedException();
        }

        public SupportedCompilers GetCompiler(string compilerName)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<SupportedCompilers> GetSupportedCompilers()
        {
            var compilers = from c in JsonConfig["compilers"]
                            select new SupportedCompilers
                            {
                                Name = (string)c["name"],
                                Lang = (string)c["lang"],
                                Invocation = (string)c["invocation"],
                                VersionCommand = (string)c["version"],
                                TimeOut = Convert.ToInt32((double)c["timeout"]),
                                Commands = (IEnumerable<string>)c["commands"]
                            };

            return compilers;
        }

        public bool UpdateCompiler(string compilerName, SupportedCompilers compiler)
        {
            throw new NotImplementedException();
        }
    }
}
