namespace NogetMedThreadLib.services
{
    public interface IComputingService
    {
        long NoThreading(int value);
        long WithThreading(int value, int noOfThreads);
    }
}