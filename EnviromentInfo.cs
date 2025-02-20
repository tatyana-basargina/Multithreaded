using System.Management;

namespace Multithreaded;

public static class EnviromentInfo
{
    public static void Print()
    {
        Console.WriteLine("Информация об окружении:");

        // Операционная система
        Console.WriteLine("\nОперационная система:");
        Console.WriteLine($"- OS: {Environment.OSVersion}");
        Console.WriteLine($"- 64-bit: {Environment.Is64BitOperatingSystem}");
        Console.WriteLine($"- Версия .NET: {Environment.Version}");

        // Процессор
        Console.WriteLine("\nПроцессор:");
        Console.WriteLine($"- Количество логических процессоров: {Environment.ProcessorCount}");

        try
        {
            // Дополнительная информация о процессоре через WMI
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    Console.WriteLine($"- Название: {obj["Name"]}");
                    Console.WriteLine($"- Архитектура: {obj["Architecture"]}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при получении информации о процессоре: " + ex.Message);
        }

        // Память
        Console.WriteLine("\nПамять:");
        Console.WriteLine($"- Объем физической памяти (MB): {Environment.WorkingSet / 1024 / 1024}");

        try
        {
            // Дополнительная информация о памяти через WMI
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory"))
            {
                long totalMemory = 0;
                foreach (ManagementObject obj in searcher.Get())
                {
                    if (obj["Capacity"] != null)
                    {
                        totalMemory += Convert.ToInt64(obj["Capacity"]);
                    }
                }
                Console.WriteLine($"- Общий объем RAM (GB): {totalMemory / (1024 * 1024 * 1024)}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка при получении информации о памяти: " + ex.Message);
        }

        // Диск
        Console.WriteLine("\nДиск:");
        foreach (var drive in Environment.GetLogicalDrives())
        {
            try
            {
                var driveInfo = new System.IO.DriveInfo(drive);
                Console.WriteLine($"- Диск: {drive}");
                Console.WriteLine($"  - Тип: {driveInfo.DriveType}");
                Console.WriteLine($"  - Общий объем (GB): {driveInfo.TotalSize / (1024 * 1024 * 1024)}");
                Console.WriteLine($"  - Свободное место (GB): {driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении информации о диске {drive}: {ex.Message}");
            }
        }

        Console.WriteLine();
    }
}
