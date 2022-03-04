# WendiPluginsShop
Данное ASP.NET Core MVC веб-приложение представляет из себя онлайн магазин для продажи цифровых продуктов (плагинов) для игры Unturned
## Техническое описание проекта
### Технологии и фреймворки
	C#, ASP.NET Core MVC, .NET Core 5, EntityFrameworkCore, MS-SQL, использвание API Юкасса, LINQ
### Описание
####	-Логин и регистрацию аккаунта обрабатывает контроллер "AccountController", новые пользователи добавлются в базу данных приложения в таблицу Users
####	-Передачу файлов плагинов обрабатывает контроллер "apiController"
####	-Отображение данных профиля, пополнение баланса, обновление баланса обрабатывает контроллер "ProfileController"
####	-Отображение данных всех плагинов в магазине, пользовательское соглашение и инструкцию об установке обрабатывает контроллер "HomeController"
####	-Покупку плагинов и отображение данных о конкретном плагине обрабатывает контроллер "PluginController"
	-Для подключения к базе данных используется строка подключения определенная в файле appsettings.json

## Планы на будущее
####    -Добавить страницу с формой добавления плагинов (админ-панель)
####	-Добавить возможность пользователям добавлять свои плагины
####	-Добавить возможность создавать заявки на написание плагинов
## Внешний вид приложения
 ### Главная страница сайта
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Main1.png)
 ### Страницы логина и регистрации
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Log1.png)
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Log2.png)
 ### Страница с инструкцией по установке
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/HowBuy.png)
 ### Станица загрузки лоадера
  ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Loader.png)
 ### Станица c пользовательским соглашением
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Contract.png)
 ### Страница профиля
 ![Image alt](https://github.com/MVPWendi/WendiPluginsShop/blob/master/wwwroot/GithubImages/Profile.png)

