using Brandr.Models.Operations;
using System.Collections.Generic;
using System.Linq;

namespace Brandr.Models
{
    public class OperationList
    {
        public List<IEditOperation> Operations { get; }
        public IEditOperation Alpha { get => Operations.FirstOrDefault(op => op.Type == OpType.Alpha); }
        public IEditOperation Saturation { get => Operations.FirstOrDefault(op => op.Type == OpType.Saturation); }
        public IEditOperation Exposure { get => Operations.FirstOrDefault(op => op.Type == OpType.Exposure); }
        public IEditOperation Contrast { get => Operations.FirstOrDefault(op => op.Type == OpType.Contrast); }

        public OperationList()
        {
            Operations = new List<IEditOperation>
            {
                new Alpha(),
                new Contrast(),
                new Exposure(),
                new Saturation()
            };
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
