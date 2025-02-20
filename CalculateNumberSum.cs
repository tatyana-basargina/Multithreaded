using System.Diagnostics;

namespace Multithreaded;

public static class CalculateNumberSum
{
    public static NumberSum GetResult(int numberIterations, ISummator summator, int[] numbers)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();

        int sum = 0;

        for (int i = 0; i < numberIterations; i++)
        {
            sum = summator.GetSum(numbers);
        }

        stopwatch.Stop();

        return new NumberSum { Sum = sum, ElapsedMilliseconds = stopwatch.ElapsedMilliseconds };
    }

    public static void PrintResult(int numberIterations, ISummator summator, int[] numbers)
    {
        var result = GetResult(numberIterations, summator, numbers);
        Console.Write($"Cумма: {result.Sum} ");
        Console.WriteLine($"Время выполнения вычисления {numberIterations} раз: {result.ElapsedMilliseconds} ms");
    }
}
