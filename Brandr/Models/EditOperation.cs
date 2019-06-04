namespace Brandr.Models
{
    public class EditOperation : IEditOperation
    {
        public OpType Type { get; private set; }
        public bool Changed { get; private set; }
        public string Name { get => Type.ToString(); }

        private double _value;
        public double Value
        {
            get => _value;
            set
            {
                _value = value;
                Changed = true;
            }
        }

        public EditOperation(OpType type)
        {
            Type = type;
            Value = 0;
        }

        //public double Value
        //{
        //    get => _value;
        //    set
        //    {
        //        _value = value;
        //        Changed = true;
        //    }
        //}

        public virtual void Reset()
        {
            _value = 0;
            Changed = false;
        }
    }
}
