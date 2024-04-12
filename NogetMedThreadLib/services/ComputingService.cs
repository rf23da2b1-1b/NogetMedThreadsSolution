using System.Diagnostics;

namespace NogetMedThreadLib.services
{
    public class ComputingService : IComputingService
    {
        private const int LowMillis = 200;
        private const int HighMillis = 300;
        private Random random = new Random(DateTime.Now.Millisecond);

        public long NoThreading(int value)
        {
            Stopwatch watch = new Stopwatch();

            watch.Start();
            DoWork(value);
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        public long WithThreading(int value, int noOfThreads)
        {
            Stopwatch watch = new Stopwatch();

            int eachThreadValue = value / noOfThreads;
            eachThreadValue = (eachThreadValue == 0) ? 1 : eachThreadValue;
            int restValue = value % noOfThreads;

            int noThreadsExt = restValue;
            int noThreadsNormal = noOfThreads - noThreadsExt;



            watch.Start();

            List<Task> tasks = new List<Task>();
            for (int i = 0; i < noThreadsExt; i++)
            {
                tasks.Add(Task.Run(() => DoWork(eachThreadValue + 1)));
            }
            for (int i = 0; i < noThreadsNormal; i++)
            {
                tasks.Add(Task.Run(() => DoWork(eachThreadValue)));
            }
            Task.WaitAll(tasks.ToArray());
            watch.Stop();
            return watch.ElapsedMilliseconds;
        }

        private void DoWork(int value)
        {
            for (int i = 0; i < value; i++)
            {
                Thread.Sleep(random.Next(LowMillis, HighMillis + 1));

            }
        }
    }
}
