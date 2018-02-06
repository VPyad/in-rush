using System;
using System.Collections.Generic;
using System.Text;

namespace InRushCore.CommandProcess
{
    public interface ICommandProcessor
    {
        CommandProcessResult Process(CommandProcessInput commandProcessInput);
    }
}
