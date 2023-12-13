using System;

namespace CarWash.Consol
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Нажмите любую клавишу для завершения работы.");
            Console.ResetColor();
            CarWash carWash = new CarWash(2); // Создаем автомойку с 2 местами
            Task.Run(async () => await carWash.StartArrival()); // Запуск метода StartArrival
            Console.ReadKey(); // Ожидания нажамтия клавиши для завершения
            Console.ForegroundColor= ConsoleColor.Green;
            Console.WriteLine($"Общая сумма выручки: {carWash.totalEarnings}");
            Console.ResetColor();
        }
    }
}