using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using System.Linq;
using System.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Newtonsoft.Json;

namespace InRushCore.Compilers
{
    public class CompilersManager : ISupportedCompilers
    {
        public string PathToConfig { get; set; } = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\Configs\Compilers\compilers_config_sample.json");

        private JObject JsonConfig { get => JObject.Parse(File.ReadAllText(PathToConfig)); }

        public CompilersManager()
        {
        }

        public void AddCompiler(SupportedCompilers compiler)
        {
            CompilersConfigHelper.ValidateCompilerObject(compiler);

            if (JsonConfig["compilers"].Where(x => (string)x["id"] == compiler.Id).FirstOrDefault() != null)
                throw new Exception($"Compiler with id: {compiler.Id} already existed");

            JObject json = (JObject)JsonConfig.DeepClone();
            JObject compilerJson = CompilersConfigHelper.GenerateCompilerJson(compiler);

            json["compilers"].LastOrDefault().AddAfterSelf(compilerJson);
            
            File.WriteAllText(PathToConfig, json.ToString());
        }

        public void DeleteCompiler(string id)
        {
            if (JsonConfig["compilers"].Where(x => (string)x["id"] == id).FirstOrDefault() == null)
                throw new Exception($"Compiler with id: {id} does not existed");

            var json = JsonConfig.DeepClone();
            json["compilers"].Where(x => (string)x["id"] == id).FirstOrDefault().Remove();

            File.WriteAllText(PathToConfig, json.ToString());
        }

        public SupportedCompilers GetCompiler(string id)
        {
            var compiler = from c in JsonConfig["compilers"]
                           where (string)c["id"] == id
                           select new SupportedCompilers
                           {
                               Name = (string)c["name"],
                               Lang = (string)c["lang"],
                               Id = (string)c["id"],
                               Invocation = (string)c["invocation"],
                               VersionCommand = (string)c["version"],
                               TimeOut = Convert.ToInt32((double)c["timeout"]),
                               Commands = c["commands"].Values<string>()
                           };

            if (compiler.FirstOrDefault() == null)
                throw new Exception("Compiler with provided id not found");

            return compiler.FirstOrDefault();
        }

        public IEnumerable<SupportedCompilers> GetSupportedCompilers()
        {
            var compilers = from c in JsonConfig["compilers"]
                            select new SupportedCompilers
                            {
                                Name = (string)c["name"],
                                Lang = (string)c["lang"],
                                Id = (string)c["id"],
                                Invocation = (string)c["invocation"],
                                VersionCommand = (string)c["version"],
                                TimeOut = Convert.ToInt32((double)c["timeout"]),
                                Commands = c["commands"].Values<string>()
                            };

            return compilers;
        }

        public void UpdateCompiler(string id, SupportedCompilers compiler)
        {
            CompilersConfigHelper.ValidateCompilerObject(compiler);

            if (JsonConfig["compilers"].Where(x => (string)x["id"] == compiler.Id).FirstOrDefault() == null)
                throw new Exception($"Compiler with id: {compiler.Id} does not existed");

            var newCompoler = CompilersConfigHelper.GenerateCompilerJson(compiler);

            var json = JsonConfig.DeepClone();
            json["compilers"].Where(x => (string)x["id"] == id).FirstOrDefault().Replace(newCompoler);

            File.WriteAllText(PathToConfig, json.ToString());
        }
    }
}
