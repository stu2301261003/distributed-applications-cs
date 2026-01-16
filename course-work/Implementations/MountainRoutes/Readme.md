# Mountain Routes

**Факултетен номер:** 2301261003  
**Име на проекта:** Mountain Routes  

## Кратко описание
Проектът представлява уеб приложение за управление и визуализация на планински маршрути. Системата позволява на потребителите да разглеждат информация за планини, маршрути и хижи, докато администраторите могат да създават, редактират и изтриват данни. Приложението поддържа сортиране, търсене и странициране на данните и има три нива на достъп:

- **Гост (нерегистриран потребител)** – може само да чете.  
- **Потребител (регистриран)** – достъп до търсене, сортиране и странициране.  
- **Администратор** – пълен достъп до създаване, редакция и изтриване.

## Структура на проекта
- **Controllers/** – контролери за управление на планини, маршрути и хижи.  
- **Models/** – модели за база данни.  
- **Views/** – изгледи за визуализация и CRUD операции.  
- **Data/ApplicationDbContext.cs** – контекст за достъп до базата данни.  
- **wwwroot/** – статични файлове (CSS, JS, изображения).

## Изисквания за стартиране
1. Visual Studio 2022 или по-нова версия с .NET 7 SDK.  
2. SQL Server (може и локален).  
3. Пакети NuGet:
   - Microsoft.EntityFrameworkCore.SqlServer  
   - Microsoft.EntityFrameworkCore.Tools  
   - Microsoft.AspNetCore.Identity.EntityFrameworkCore  
   - X.PagedList.Mvc.Core  

## Инструкции за инсталация и стартиране
1. Клонирайте проекта:

    ```bash
   
   git clone https://github.com/YourUsername/course-repo.git
   
3. Отворете решението .sln във Visual Studio.
4. В appsettings.json настройте връзката към вашия SQL Server:

   ```json
   
   "ConnectionStrings":
   {
   "DefaultConnection": "Server=localhost;Database=MountainRoutesDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   
5. Стартирайте миграциите:

- **Package Manager Console**:

```powershell

Update-Database
```
