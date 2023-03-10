## Вступление
В качестве тестового задания я выбрал **«Справочник автомобилей»**, где необходимо реализовать сервис справочника автомобилей с хранением данных в базе или файле.

Тестовое задание выполнено на языке **C#** и представляет собой **REST Web API**. В качестве базы данных я выбрал компактную **SQLite**, а для работы с данными - технологию **Entity Framework**.

[Фронтенд](https://github.com/shamsipav/cars-catalog-client) для сервиса реализован на новом развивающимся фреймворке **Svelte.js**.

Общие дополнительные условия, которые я выполнил:
- Сохранение запросов к сервису и вывод лога в API;
- Кэширование запросов;
- Фронтенд для сервиса;
- Unit тесты;
- GraphQL для API.

## Описание архитектуры приложения

Решение REST Web API содержит два проекта:
- «CarsCatalogAPI»;
- «CarsCatalogAPI.Tests».

![image](https://user-images.githubusercontent.com/56552046/215064665-dd681106-7fdf-4d2b-8fcc-4e4f0b4c9c5b.png)

В директории **Models** находится модель **«Car»** со следующими свойствами:
- *int* Id (идентификатор)
- *string* LicensePlate (регистрационный знак);
- *string* Brand (марка);
- *string* Color (цвет);
- *DateTime* ReleaseYear (год выпуска);
- *DateTime* CreateTime (время добавления);
- *DateTime* UpdateTime (время обновления).

В директории **Controllers** содержатся два контроллера:
- **«CarsController»**;
- **«StatsController»**.

В контроллере **«CarsController»** (route: "/api/cars") реализованы методы REST API:
- GET(); // Вывод списка автомобилей
- GET(*int* id). // Вывод информации по определенному автомобилю
- POST(*Car* car); // Добавление автомобиля (результат операции: "успех", "ошибка", "объект уже существует")
- PUT(*int* id, *Car* car); // Редактирование автомобиля (результат операции: "успех", "ошибка", "объект не найден")
- DELETE(*int* id). // Удаление автомобиля (результат операции: "успех", "ошибка", "объект не найден")

В контроллере **«StatsController»** (route: "/api/stats") реализован GET-метод, отвечающий за получение статистики по базе данных. Он возвращает экземпляр класса **«Statistics»** (/Models/Statistics.cs), содержащий количество записей (ObjectsCount), дату добавления первой записи (FirstObjectAddedTime) и дату добавления последней записи (LastObjectAddedTime).

### Сохранение запросов к сервису и вывод лога в API

Для реализации сохранения запросов к сервису в базе данных и вывода лога в API был реализован и подключен middleware **«RequestResponseLoggerMiddleware»** (/Middlewares/RequestResponseLoggerMiddleware.cs). 

Для сохранения запросов (и ответов) к сервису были созданы классы:
- **«Request»** (/Models/Request.cs) - содержит поля запроса клиента;
- **«Response»** (/Models/Response.cs) - содержит поля ответа сервера;
- **«ResponseInfo»** (/Models/ResponseInfo.cs) - содержит классы **«Request»** и **«Response»**.

### Кэширование запросов

Кэшироване запросов реализовано с помощью класса **MemoryCache** (объект Microsoft.Extensions.Caching.Memory.IMemoryCache)

### Docker файл для для развертывания сервиса

При создании решения в Visual Studio я выбрал поддержку Docker, однако на текущий момент проверить развертывание сервиса в программе Docker Desktop не удалось из-за того, что программа не запускается на моём ПК

### [Фронтенд](https://github.com/shamsipav/cars-catalog-client) для сервиса

[Фронтенд](https://github.com/shamsipav/cars-catalog-client) для сервиса реализован на новом развивающимся фреймворке **Svelte.js**

### Unit тесты

Проект **«CarsCatalogAPI.Tests»** содержит два класса:
- **«CustomWebApplicationFactory»** - класс с настройкой подключения к базе данных из проекта **«CarsCatalogAPI»**, основанный на **WebApplicationFactory<TEntryPoint>**, который используется для создания тестового сервера (**TestServer**) для интеграционных тестов.
- **«UnitTest»** -  класс с Unit тестами.

### GraphQL для API

В директории **Data** находится контекст базы данных **«CarsDBContext»** и класс **«Query»** с методом **GetCars()**, 
отвечающий за взаимодействие с таблицей **Cars** с помощью **GraphQL** (route: "/graphql").

Для настройки **GraphQL** был создан интерфейс **«ICarRepository»** (/Repositories/CarRepository.cs) и класс **«CarRepository»** (/Repositories/CarRepository.cs), содержащий конструктор с инициализацией контекста базы данных, а также обновлена конфигурация в файле **Program.cs**
