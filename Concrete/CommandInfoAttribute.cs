using System;
using System.ComponentModel.Composition;
using ICommandInfo = ConsolePlate.Abstract.ICommandInfo;

namespace ConsolePlate.Concrete
{
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class CommandInfoAttribute : ExportAttribute
    {
        public string CommandName { get; set; }
        public string CommandDescription { get; set; }

        public CommandInfoAttribute()
            : base(typeof(ICommandInfo))
        {
        }
    }
}
