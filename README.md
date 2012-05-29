### Description   
Simple console application boilerplate for .NET

Available on Nuget: [http://nuget.org/packages/ConsolePlate](https://nuget.org/packages/ConsolePlate)

### Introduction
There are many developers that use console applications for managing tasks like: 

* Creating databases
* Seeding data
* Configuring environment for their projects, etc

So for every project they must create console app with ability to determine commands, parse command line args and etc. ConsolePlate boilerplate can simplify and accelerate the development of such apps. It uses `MEF` to quickly plug-in commands and `NDesk.Options` to parse command line arguments. So all you need is create command that implements `ICommand` interface and export it to the console application project that uses *ConsolePlate*. 

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

Look at [ConsolePlate.Example](https://github.com/AzzNomad/ConsolePlate/tree/master/Src/ConsolePlate.Example) project to see practical example.