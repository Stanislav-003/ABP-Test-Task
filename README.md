Цей проект є системою управління конференц-залами, яка дозволяє користувачам бронювати конференц-зали, перевіряти доступність, 
додавати додаткові послуги, та керувати залами. Система побудована на основі ASP.NET Core з використанням Entity Framework Core
для роботи з базою даних та MediatR для організації логіки запитів та команд.

Перед встановленням проекту потрібно переконатись, що у вас встановлені наступні залежності:
.NET 8.0 SDK або новіший
SQL Server або інша підтримувана база даних
Entity Framework Core
MediatR
CSharpFunctionalExtensions

Особливості:
Бронювання конференц-залів з можливістю вказати дату, час та додаткові послуги.
Керування конференц-залами (створення, редагування, видалення).
Пошук доступних залів за місткістю та датою/часом.
Додаткові послуги для залів (наприклад, проектор, інтернет, тощо).
Unit of Work та репозиторії для управління збереженням результату у базу даних та роботу з транзакціями.

Структура проекту:
ConferenceRoomsReservation.Application – Основна логіка обробки запитів та команд (CQRS).
ConferenceRoomsReservation.Core – Базові моделі та доменні логіка.
ConferenceRoomsReservation.DataAccess – Робота з базою даних, репозиторії.
ConferenceRoomsReservation.Presentation - Відповідає за обробку запитів від користувача. Цей рівень містить контролери, які обробляють вхідні дані та надсилають їх на прикладний рівень.
ConferenceRoomsReservation.API – Налаштування проекту.

Встановлення:
Клонувати репозиторій: git clone https://github.com/Stanislav-003/ABP-Test-Task.git;
Перейти в директорію проекту: cd ConferenceRoomsReservation;
Налаштувати підключення до бази даних у файлі appsettings.json. Наприклад: {
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=ConferenceRooms;Trusted_Connection=True;"
  }
}
Виконати міграції для створення бази даних: dotnet ef database update;
Запустити проект: dotnet run;

Використання:
API Ендпоінти

1) Додати конференц-зал:
Метод: POST /api/conference-rooms/add-conference-room
Тіло запиту:
{
  "name": "Великий зал",
  "capacity": 100,
  "basePricePerHour": 500.00,
  "addServiceIds": ["guid1", "guid2"]
}

2) Редагувати інформацію про зал: 
Метод: PUT /api/conference-rooms/update-conference-room
Тіло запиту: 
{
  "conferenceRoomId": "guid",
  "name": "Updated Conference Room A",
  "capacity": 120,
  "basePricePerHour": 600.00,
  "addServiceIds": ["guid1", "guid3"]
}

3) Видалити зал: 
Метод: DELETE /api/conference-rooms/delete-conference-room/{conferenceRoomId}
Параметри запиту:
conferenceRoomId

4) Пошук доступних залів:
Метод: GET /api/conference-rooms/available-rooms
Параметри запиту:
year, month, day, hours, minutes, durationHours, requiredCapacity

5) Бронювання конференц-залу:
Метод: POST /api/bookings/create
Тіло запиту:
{
  "conferenceRoomId": "guid",
  "year": 2024,
  "month": 9,
  "day": 15,
  "hours": 10,
  "minutes": 0,
  "durationHours": 4,
  "addServiceIds": ["guid1", "guid2"]
}


Внесок у проект:
Форкнути репозиторій.
Створити нову гілку (git checkout -b feature-branch).
Виконати зміни та закомітити їх (git commit -m "Опис змін").
Відправити зміни (git push origin feature-branch).
Створити pull request.
