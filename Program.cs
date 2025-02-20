using Multithreaded;

// Окружение (характеристики компьютера и ОС)
EnviromentInfo.Print();

// Напишите вычисление суммы элементов массива интов:
int[] numbers = { 1 , 2, 3, 4, 5 };

// Обычное
// Время выполнения последовательного вычисления
Console.WriteLine($"Последовательное вычисление:");
CalculateNumberSum.PrintResult(100_000, new СonsistentSummator(), numbers);
CalculateNumberSum.PrintResult(1_000_000, new СonsistentSummator(), numbers);
CalculateNumberSum.PrintResult(10_000_000, new СonsistentSummator(), numbers);

// Параллельное(ThreadPool)
// Время выполнения параллельного вычисления
Console.WriteLine($"Параллельное вычисление ThreadPoolSummator:");
CalculateNumberSum.PrintResult(100_000, new ThreadPoolSummator(), numbers);
CalculateNumberSum.PrintResult(1_000_000, new ThreadPoolSummator(), numbers);
CalculateNumberSum.PrintResult(10_000_000, new ThreadPoolSummator(), numbers);

// Параллельное(для реализации использовать Thread, например List)
// Время выполнения параллельного вычисления
Console.WriteLine($"Параллельное вычисление ThreadSummator:");
CalculateNumberSum.PrintResult(100_000, new ThreadSummator(), numbers);
CalculateNumberSum.PrintResult(1_000_000, new ThreadSummator(), numbers);
CalculateNumberSum.PrintResult(10_000_000, new ThreadSummator(), numbers);

// Параллельное с помощью LINQ
// Время выполнения LINQ
Console.WriteLine($"Parallel LINQ вычисление:");
CalculateNumberSum.PrintResult(100_000, new ParallelSummator(), numbers);
CalculateNumberSum.PrintResult(1_000_000, new ParallelSummator(), numbers);
CalculateNumberSum.PrintResult(10_000_000, new ParallelSummator(), numbers);