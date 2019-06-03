namespace Brandr.Models
{
    public enum OpType
    {
        Alpha,
        Saturation,
        Exposure,
        Contrast
    }
    public interface IEditOperation
    {
        double Value { get; set; }
        bool Changed { get; }
        OpType Type { get; }
        void Reset();
    }
}
