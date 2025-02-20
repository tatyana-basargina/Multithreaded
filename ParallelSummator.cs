namespace Multithreaded;

public class ParallelSummator : ISummator
{
    public int GetSum(int[] numbers)
    {
        return numbers
            .AsParallel()
            .WithExecutionMode(ParallelExecutionMode.ForceParallelism)
            .WithDegreeOfParallelism(2)
            .Sum();
    }
}