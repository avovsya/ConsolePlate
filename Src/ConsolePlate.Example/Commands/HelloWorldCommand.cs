using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using ConsolePlate.Abstract;
using NDesk.Options;

namespace ConsolePlate.Example.Commands
{
    [Export(typeof(ICommand))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    [Concrete.CommandInfo(CommandName = "Hello", CommandDescription = "Example 'Hello, World' command")]
    public class HelloWorldCommand : ICommand
    {
        private string _name;
        private string _greeting;

        public OptionSet Parameters
        {
            get
            {
                return new OptionSet
                    {
                        {"n|name=", "Name to show(optional)", o => _name = o},
                        {"g|greeting=", "Greeting to show(optional)", o => _greeting = o},
                    };
            }
        }

        public void Execute(IEnumerable<string> args)
        {
            Console.WriteLine("{0}, {1}!", !String.IsNullOrEmpty(_greeting) ? _greeting : "Hello", !String.IsNullOrEmpty(_name) ? _name : "World");
        }
    }
}
