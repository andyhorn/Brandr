using Brandr.Models.Operations;

namespace Brandr.Models
{
    public class OperationList
    {
        public Saturation Saturation { get; }
        public Exposure Exposure { get; }

        public OperationList()
        {
            Saturation = new Saturation();
            Exposure = new Exposure();
        }

        public void ResetAll()
        {
            Saturation.Reset();
            Exposure.Reset();
        }
    }
}
