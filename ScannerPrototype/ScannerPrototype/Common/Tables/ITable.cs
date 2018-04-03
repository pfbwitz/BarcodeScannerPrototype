using System;

namespace ScannerPrototype.Common.Tables
{
    public interface ITable
    {
        int Id { get; set; }

        DateTime CreationDateTime { get; set; }

        DateTime MutationDateTime { get; set; }
    }
}
