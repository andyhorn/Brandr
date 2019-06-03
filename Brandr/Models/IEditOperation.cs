namespace Brandr.Models
{
    public enum OpType
    {
        Saturation,
        Exposure,
        Contrast
    }
    public interface IEditOperation
    {
        bool Changed { get; }
        OpType Type { get; }
        void Set(double value);
        void Reset();
        double Get();
    }
}
