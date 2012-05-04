using System.Collections.Generic;
using NDesk.Options;

namespace ConsolePlate.Abstract
{
    public interface ICommand
    {
        OptionSet Parameters { get; }
        void Execute(IEnumerable<string> args);
    }

    public interface ICommandInfo
    {
        string CommandName { get; }
        string CommandDescription { get; }
    }
}
