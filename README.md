# Backend расширения

### Бэк написан на ASP .NET CORE (.NET 7) (C#)
## Запуск происходит docker 

#### Все переменные среды можно найти в **docker-compose.yml**
### Для запуска необходимо заполнить 
* OZON_TOKEN
* OZON_COMPANY_ID
##### которые можно посмотреть в инпекторе пакетов chrome на странице https://seller.ozon.ru/

### NuGet Packages:
* Newtonsoft.Json
* RestSharp
* Mickrosoft.EntityFrameworkCore

#

## Бек предоставляет информацию от api Ozon со сотраницы https://seller.ozon.ru/app/analytics/what-to-sell/all-queries (проводя дамп данных каждую неделю)


### Модели:
* ## Category:
    * ### int Id
    * ### string? Name
    * ### int? ParentId
    * ### Category? Parent
    * ### ICollection\<Category\> Children

* ## SiteDump:
    * ### Guid Id
    * ### DateTime Date
    * ### DumpWeeks DumpWeeks:
        * #### One = 1,
        * #### Two = 2,
        * #### Four = 4
    * ### List\<Search\> Searches

* ## Search:
    * ### Guid Id
    * ### string? Query
    * ### int SearchCount
    * ### int AddedToCard
    * ### int AveragePrice
    * ### Guid? DumpId
    * ### SiteDump? Dump
