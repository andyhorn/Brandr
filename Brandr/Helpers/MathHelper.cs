namespace Brandr.Helpers
{
    public static class MathHelper
    {
        public static double AdjustRange(double input, double fromMax, double fromMin, double toMax, double toMin)
        {
            //     Result:= ((Input - InputLow) / (InputHigh - InputLow))
            //     * (OutputHigh - OutputLow) + OutputLow;

            double result = (input - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;

            return result;
        }
    }
}
