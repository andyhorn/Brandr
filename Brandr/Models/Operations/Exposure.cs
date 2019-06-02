namespace Brandr.Models.Operations
{
    public class Exposure : IEditOperation
    {
        private double exposure;
        private bool _changed;
        public OpType Type => OpType.Exposure;
        public double Get() => exposure;
        public bool Changed => _changed;
        public void Reset()
        {
            exposure = 0;
            _changed = false;
        }
        public void Set(double value)
        {
            exposure = value;
            _changed = true;
        }

        public Exposure()
        {
            exposure = 0;
            _changed = false;
        }
    }
}
