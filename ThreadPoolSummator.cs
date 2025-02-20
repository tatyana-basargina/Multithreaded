using System.Collections.Concurrent;

namespace Multithreaded;

public class ThreadPoolSummator : ISummator
{
    private static int _threadCount = 2;
    public int GetSum(int[] numbers)
    {
        int sum = 0;

        if (numbers == null)
        {
            return 0;
        }

        OrderablePartitioner<Tuple<int, int>> partitioner = Partitioner.Create(0, numbers.Length);
        ManualResetEvent[] doneEvents = new ManualResetEvent[_threadCount];
        int i = 0;

        foreach (IEnumerator<Tuple<int, int>> range in partitioner.GetPartitions(_threadCount))
        {
            int index = i;
            doneEvents[i] = new ManualResetEvent(false);
            i++;
            ThreadPool.QueueUserWorkItem( state =>
            {
                int localSum = 0;

                while (range.MoveNext())
                {
                    for (int i = range.Current.Item1; i < range.Current.Item2; i++)
                    {
                        localSum += numbers[i];
                     }
                }

                if (localSum != 0)
                {
                    Interlocked.Add(ref sum, localSum);
                }
                doneEvents[index].Set();
            });
        }

        WaitHandle.WaitAll(doneEvents);

        return sum;
    }
}
