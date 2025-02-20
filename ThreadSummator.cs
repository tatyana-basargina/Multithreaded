using System.Collections.Concurrent;

namespace Multithreaded;

public class ThreadSummator : ISummator
{
    private const int ThreadCount = 2;
    public int GetSum(int[] numbers)
    {
        int sum = 0;
        List<Thread> _threads = new List<Thread>();

        if (numbers == null)
        {
            return 0;
        }

        OrderablePartitioner<Tuple<int, int>> partitioner = Partitioner.Create(0, numbers.Length);

        foreach (IEnumerator<Tuple<int, int>> range in partitioner.GetPartitions(ThreadCount))
        {
            var thread = new Thread(() =>
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
            });

            _threads.Add(thread);
            thread.Start();
        }

        _threads.ForEach(thread => thread.Join());

        return sum;
    }
}
