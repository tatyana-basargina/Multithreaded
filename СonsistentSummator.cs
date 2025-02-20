namespace Multithreaded;

public class СonsistentSummator : ISummator
{
    public int GetSum(int[] numbers)
    {
        int sum = 0;

        for (int i = 0; i < numbers.Length; i++)
        {
            sum += numbers[i];
        }

        return sum;
    }
}
