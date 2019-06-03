namespace Brandr.Models
{
    public abstract class EditOperation : IEditOperation
    {
        protected OpType _type;

        protected double _value;

        protected EditOperation()
        {
            _value = 0;
        }

        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed = true;
            }
        }
        public bool Changed { get; private set; }
        public OpType Type => _type;

        public virtual void Reset()
        {
            _value = 0;
            Changed = false;
        }
    }
}
