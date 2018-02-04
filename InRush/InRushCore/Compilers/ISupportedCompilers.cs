using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.Compilers
{
    public interface ISupportedCompilers
    {
        IEnumerable<SupportedCompilers> GetSupportedCompilers();

        bool AddCompiler(SupportedCompilers compiler);

        bool DeleteCompiler(SupportedCompilers compiler);

        bool DeleteCompiler(string compilerName);

        bool UpdateCompiler(string compilerName, SupportedCompilers compiler);

        SupportedCompilers GetCompiler(string compilerName);
    }
}
