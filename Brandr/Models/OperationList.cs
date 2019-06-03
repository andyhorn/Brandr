using Brandr.Models.Operations;

namespace Brandr.Models
{
    public class OperationList
    {
        public Saturation Saturation { get; }
        public Exposure Exposure { get; }

        public Contrast Contrast { get; }

        public OperationList()
        {
            Saturation = new Saturation();
            Exposure = new Exposure();
            Contrast = new Contrast();
        }

        public void ResetAll()
        {
            Saturation.Reset();
            Exposure.Reset();
            Contrast.Reset();
        }
    }
}
