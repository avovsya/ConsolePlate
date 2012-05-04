using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ConsolePlate.Abstract;

namespace ConsolePlate.Service
{
    [Export]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public sealed class CommandCollection : IPartImportsSatisfiedNotification
    {
        [ImportMany]
        public Lazy<ICommand, ICommandInfo>[] Commands { get; set; }

        private Dictionary<string, ICommand> _commandMap;
        private Dictionary<string, string> _commandDescriptions;

        public ICommand this[string name]
        {
            get
            {
                ICommand command;
                _commandMap.TryGetValue(name.ToLower(), out command);
                return command;
            }
        }

        public void OnImportsSatisfied()
        {
            _commandMap = new Dictionary<string, ICommand>(Commands.Length);
            _commandDescriptions = new Dictionary<string, string>(Commands.Length);

            foreach (var command in Commands)
            {
                if (!_commandMap.ContainsKey(command.Metadata.CommandName))
                {
                    _commandMap.Add(command.Metadata.CommandName.ToLower(), command.Value);
                    _commandDescriptions.Add(command.Metadata.CommandName.ToLower(), command.Metadata.CommandDescription);
                }
            }
        }

        public void WriteHelp()
        {
            Console.WriteLine("\nCommands: ");
            var i = 1;
            foreach (var command in _commandMap)
            {
                Console.WriteLine();
                Console.WriteLine(i++ + ". " + command.Key + ":");
                Console.WriteLine("  " + _commandDescriptions[command.Key]);
                Console.WriteLine();
                command.Value.Parameters.WriteOptionDescriptions(Console.Out);
            }
        }
    }
}
