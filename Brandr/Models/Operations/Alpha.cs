namespace Brandr.Models.Operations
{
    public class Alpha : EditOperation
    {
        public Alpha() => _type = OpType.Alpha;

        public override void Reset()
        {
            base.Reset();
            _value = 100;
        }
    }
}
