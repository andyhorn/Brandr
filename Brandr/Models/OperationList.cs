using System;
using System.Collections.Generic;
using System.Linq;

namespace Brandr.Models
{
    public class OperationList
    {
        public List<IEditOperation> Operations { get; }

        public OperationList()
        {
            Operations = new List<IEditOperation>();

            foreach (OpType type in Enum.GetValues(typeof(OpType)))
            {
                Operations.Add(new EditOperation(type));
            }
        }

        public IEditOperation Get(OpType type)
        {
            var val = Operations.FirstOrDefault(op => op.Type == type);
            return val;
        }

        public void Reset(OpType type)
        {
            var op = Get(type);

            if (op != null)
            {
                op.Reset();
            }
        }

        public void Reset(string type)
        {
            var op = Operations.FirstOrDefault(e => e.Type.ToString() == type);

            if (op != null)
            {
                op.Reset();
            }
        }

        public void ResetAll()
        {
            foreach(var op in Operations)
            {
                op.Reset();
            }
        }

        public Dictionary<string, double> GetChanged()
        {
            var changes = new Dictionary<string, double>();

            foreach(var op in Operations)
            {
                if(op.Changed)
                {
                    changes.Add(op.Type.ToString(), op.Value);
                }
            }

            return changes;
        }
    }
}
