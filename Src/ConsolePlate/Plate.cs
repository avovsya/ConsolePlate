using System;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO.Abstractions;
using System.Linq;
using System.Reflection;
using NDesk.Options;
using ConsolePlate.Service;

namespace ConsolePlate
{
    public class Plate
    {
        [Import]
        private CommandCollection CommandCollection { get; set; }

        /// <summary>
        /// Start ConsolePlate app
        /// </summary>
        /// <param name="args">command line arguments</param>
        /// <param name="commandFolderPath">path where to search commands</param>
        public void Start(string[] args, string commandFolderPath)
        {
            Start(args, new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()),
                                 new DirectoryCatalog(commandFolderPath)));
        }

        /// <summary>
        /// Start ConsolePlate app
        /// </summary>
        /// <param name="args">command line arguments</param>
        /// <param name="assembly">assembly where to search commands</param>
        public void Start(string[] args, Assembly assembly)
        {
            Start(args, new AggregateCatalog(new AssemblyCatalog(Assembly.GetExecutingAssembly()), new AssemblyCatalog(assembly)));
            
        }

        private void Start(string[] args, AggregateCatalog catalog)
        {
            var help = false;
            var defaultSet = new OptionSet{
                                    { "h|?|help", "Display help mode", p => help = true }
                                 };

            defaultSet.Parse(args);

            Compose(catalog);

            if (help || !args.Any())
            {
                CommandCollection.WriteHelp();
                return;
            }

            Run(args);
        }

        private void Run(string[] args)
        {
            var commandName = args[0];
            var commandArgs = args.Skip(1).ToArray();

            if (CommandCollection[commandName] == null)
            {
                Console.WriteLine("Can't find command: {0}", commandName);
                CommandCollection.WriteHelp();
                return;
            }

            CommandCollection[commandName].Parameters.Parse(commandArgs);
            CommandCollection[commandName].Execute(commandArgs);
            BeforeClose();
        }

        private void BeforeClose()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }

        private void Compose(AggregateCatalog aggregateCatalog)
        {
            try
            {
                var container = new CompositionContainer(aggregateCatalog);
                var batch = new CompositionBatch();
                batch.AddExportedValue<IFileSystem>(new FileSystem());
                batch.AddPart(this);
                container.Compose(batch);
            }
            catch (ReflectionTypeLoadException e)
            {
                Console.WriteLine("Can't load: \r\n{0}", string.Join("\r\n", e.LoaderExceptions.Select(ex => ex.Message)));
                throw;
            }
        }
    }
}
