namespace Brandr.Models.Operations
{
    public class Saturation : IEditOperation
    {
        private double _saturation;
        private bool _changed;
        public OpType Type => OpType.Saturation;
        public bool Changed => _changed;
        public double Get() => _saturation;
        public void Set(double value)
        {
            _saturation = value;
            _changed = true;
        }

        public void Reset()
        {
            _saturation = 0;
            _changed = false;
        }

        public Saturation()
        {
            _saturation = 0;
            _changed = false;
        }
    }
}
