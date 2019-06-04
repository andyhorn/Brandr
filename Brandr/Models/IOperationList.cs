using System.Collections.Generic;

namespace Brandr.Models
{
    public interface IOperationList
    {
        List<IEditOperation> Operations { get; }
        IEditOperation Get(OpType type);
        Dictionary<string, double> GetChanged();
        void Reset(OpType type);
        void Reset(string type);
        void ResetAll();
    }
}