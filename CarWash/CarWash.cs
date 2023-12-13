using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarWash
{
    public class CarWash
    {
        private Queue<Car> waitingCars; // Очередь автомобилей, ожидающих мойку
        private int totalPlaceCount; // Общее количество мест на автомойке
        private int availablePlace; // Доступное количество мест на автомойке
        public int totalEarnings; // Общая сумма выручки
        private Random random; // Рандом
        public CarWash(int placeCount) 
        {
            totalPlaceCount = placeCount;
            availablePlace = placeCount;
            waitingCars = new Queue<Car>();
            random = new Random();
            StartArrival(); // Запуск метода, прибытие автомобилей
        }
        public async Task StartArrival() // Метод прибытия автомобилей
        {
            while (true) // Бесконечный цикл
            {
                await Task.Delay(random.Next(5000, 10000)); // Остановка метода на заданное время (5-10секунд)
                Arrive(GetRandomCar()); // Вызов метода для прибытия разных типов автомобилей
            }
        }
        private Car GetRandomCar() // Метод для случайного типа автомобилей
        {
            Array carTypes = Enum.GetValues(typeof(CarType)); // Получение всех значений CarType
            CarType randomCarType = (CarType)carTypes.GetValue(random.Next(carTypes.Length)); // Выбор случайного из перечисленных
            return new Car { Type = randomCarType }; // Создание Car с выбранным типом
        }
        public void Arrive(Car car) // Метод для прибытия на автомойку
        {
            if (availablePlace > 0) // Если есть свободное место на мойке
            {
                SetCarPrice(car); // Устанавливает цену для автомобиля
                WashCar(car); // Запуск процесса мойки
                availablePlace--; // Уменьшение количества доступных мест
            }
            else // Если свободных мест не оказалось
            {
                waitingCars.Enqueue(car); // Добавление автомобиля в очередь
                car.QueueNumber = waitingCars.Count; // Номер в очереди
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Автомобиль {car.Type} прибыл на автомойку и ожидает свободного места. Место в очереди {car.QueueNumber}");
                Console.ResetColor();
            }
        }
        private async Task WashCar(Car car) // Метод для мойки автомобиля
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Мойка началась для автомобиля {car.Type}");
            await Task.Delay(10_000); // Приостановка метода на 10 секунд
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Мойка завершена для автомобиля {car.Type}. Оплата: {car.Price}");
            Console.ResetColor();
            totalEarnings += car.Price; // Увелечение общей суммы выручки
            availablePlace++; // Увелечение доступных мест на мойке
            if (waitingCars.Count > 0) // Если есть автомобили в очереди
            {
                var nextCar = waitingCars.Dequeue(); // Следующий автомобиль из очереди
                SetCarPrice(nextCar); // Установка цены для следующего автомобиля
                WashCar(nextCar); // Мойка для следующего автомобиля
                availablePlace--; // Уменьшение доступных мест
            }
        }
        private void SetCarPrice(Car car) // Метод для установки цены
        {
            switch (car.Type)
            {
                case CarType.Passenger:
                    car.Price = 1000;
                    break;
                case CarType.Jeep:
                    car.Price = 1500;
                    break;
                case CarType.Minibus:
                    car.Price = 3000;
                    break;
            }
        }
    }
}