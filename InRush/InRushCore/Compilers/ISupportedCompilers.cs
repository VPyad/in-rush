using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.Compilers
{
    public interface ISupportedCompilers
    {
        IEnumerable<SupportedCompilers> GetSupportedCompilers();

        void AddCompiler(SupportedCompilers compiler);

        void DeleteCompiler(string id);

        void UpdateCompiler(string id, SupportedCompilers compiler);

        SupportedCompilers GetCompiler(string id);
    }
}
