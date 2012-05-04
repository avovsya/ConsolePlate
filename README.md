### Description
Simple console application boilerplate for .NET

### License
ConsolePlate is licensed under the [Apache Licence, Version 2.0](http://www.apache.org/licenses/LICENSE-2.0.html).

### Installation
*Install from NuGet:*

    Install-Package ConsolePlate


### How to use
In your console application Main() method do as follows:

`var plate = new Plate();`

`plate.Start(args, Assembly.GetExecutingAssembly());`

This will start ConsolePlate and indicate to look commands in current assembly.

Also you can pass folder name to look commands instead of assembly.

To create command:

 1. Create class that implements interface ICommand
 2. Mark it with 'Export' attribute(from `System.ComponentModel.Composition`):
`Export(typeof(ICommand))`
 3. Implement `Execute` method
 4. Implement `Parameters` property. It would be parsed automatically. For more info about parsing command line arguments look at [NDesk.Options](http://www.ndesk.org/Options)

Look at `ConsolePlate.Example` project to see practical example.