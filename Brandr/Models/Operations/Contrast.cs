namespace Brandr.Models.Operations
{
    public class Contrast : IEditOperation
    {
        private bool _changed;
        private double _contrast;
        public bool Changed => _changed;

        public OpType Type => OpType.Contrast;

        public double Get()
        {
            return _contrast;
        }

        public void Reset()
        {
            _contrast = 0;
            _changed = false;
        }

        public void Set(double value)
        {
            _contrast = value;
            _changed = true;
        }
    }
}
