# Rovecode.Lotos 

[![Build Status](https://img.shields.io/github/workflow/status/rovecode/Rovecode.Lotos/.NET-Commit)](https://www.fuget.org/packages/Rovecode.Lotos)
[![Build Status](https://img.shields.io/github/workflow/status/rovecode/Rovecode.Lotos/.NET-Publish)](https://www.fuget.org/packages/Rovecode.Lotos)
[![Rovecode.Lotos on fuget.org](https://www.fuget.org/packages/Rovecode.Lotos/badge.svg)](https://www.fuget.org/packages/Rovecode.Lotos)


## Features


## Code Example

You can find examples projects in __[Examples](https://github.com/rovecode/Rovecode.Lotos/tree/master/Examples)__ folder. But below I will briefly describe some patterns of code for fast introduction in current project.

### How connect to mongo and get storage

For connect to mongo you can use [LotosConnectFactory](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Factories/LotosConnectFactory.cs). This factory contains some methods for create IStorageContext. Create [IStorageContext](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Contexts/IStorageContext.cs) is needed to get [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs). [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs) is a base interface for work with db.

Use method __Connect__ for connect to db and get [IStorageContext](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Contexts/IStorageContext.cs).

``` csharp
var context = LotosConnectFactory.Connect("<your_connect_string>", "<your_db_name>");
```

__Connect__ method contains more overloading for many different situations, but we don't review this.

If connect to db is failed Lotos throw [LotosException](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Exceptions/LotosException.cs) with error details.

For get storage you must have __record__ and this __record__ must extended from [StorageData](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Models/StorageData.cs). For get storage invoke method __Get__ and in generic put your data __record__.

``` csharp
var userStorage = context.Get<UserData>();
```

Lotos auto generate collection in db.

### How create data model

Create data model really ease. Create record and extend them from [StorageData](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Models/StorageData.cs).

``` csharp
record UserData : StorageData
{
    public string Name { get; set; }
    public int Phone { get; set; }
}
```

### How create/keep data model in db

For keep model in db you create them and put in __Keep__ method of [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs). Same __Keep__ method return repository for work with created data.

``` csharp
var userData = new UserData() 
{ 
    Name = "Roman" 
};

var user = userStorage.Keep(userData);
```

### How find/get kepped datas

For find kepped datas use methods __Search__, __SearchMany__, __SearchAsync__, __SearchManyAsync__. In this methods you used expression filters for search.

Finds first user with name Roman else return null.

``` csharp
var user1 = userStorage.Search(e => e.Name == "Roman");

if (user1 is not null)
{
    // ..
}
```

Finds all users with name Roman. Return __IEnumerable__.

``` csharp
var users1 = userStorage.SearchMany(e => e.Name == "Roman");

foreach(var usr in users1)
{
    // ..
}
```

Also you can use __Async__ methods for search in other thread.

``` csharp
var user2 = await userStorage.SearchAsync(e => e.Name == "Roman");

var users2 = await userStorage.SearchManyAsync(e => e.Name == "Roman");

```

You can, for example, limit the number of search datas or set search offset.


``` csharp
var user1 = userStorage.Search(e => e.Name == "Roman", offset: 2);

var users1 = userStorage.SearchMany(e => e.Name == "Roman", 1 /* offset */, 1 /* count */);

var users2 = userStorage.SearchMany(e => e.Name == "Roman", count: 1);

var users3 = userStorage.SearchMany(e => e.Name == "Roman", offset: 2);

```

### How edit kepped datas

For change data you can change Data Property of [IStorageDataRepository](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorageDataRepository.cs). Lotos have are several ways to save changes to db. First invoke __Exchange__ method, second using __IDispose__.

__IDispose__ way.

``` csharp

using (user1)
{
    user1.Data.Phone = 12345;
}
```

__Exchange__ method way.

``` csharp
user1.Data.Phone = 12345;

user1.Exchange();
```

In addition, __ExchangeMode__ can be passed to the __Exchange__, which indicates how the data exchange process will take place. Default is __ExchangeMode__.__InOut__.

``` csharp
user1.Data.Phone = 12345;

user1.Exchange(ExchangeMode.In);

user2.Exchange(ExchangeMode.Out);
```

### How delete kepped datas

For delete kepped datas using filters you can use methods __Burn__, __BurnMany__ of [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs). This methods use expression filter.

__Burn__ method deletes first find data, matching expression filter.

``` csharp
usersStorage.Burn(e => e.Name == "Roman");
```

__BurnMany__ method deletes all find datas, matching expression filter.

``` csharp
usersStorage.BurnMany(e => e.Name == "Roman");
```

Or you can use __Burn__ method in [IStorageDataRepository](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorageDataRepository.cs).

``` csharp
user1.Burn();
```

### How count datas

For count datas you can use __Count__ method of [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs).

``` csharp
var count = usersStorage.Count(e => e.Name == "Roman");
```

### How check data is exists in db

For check data is exists in db you can use __Exist__ method of [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs), or __Exist__ method of [IStorageDataRepository](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorageDataRepository.cs).

[IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs) way.

``` csharp
var exist = usersStorage.Exist(e => e.Name == "Roman");
```

[IStorageDataRepository](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorageDataRepository.cs) way.

``` csharp
var exist = user1.Exist();
```

### How use this package with ASP.NET Core / Dependency Injection

Add Rovecode.Lotos.DependencyInjection package and use __AddLotos__ method for configure it. See example there.

### How get IStorage if i'm have only IStorageDataRepository

You can get IStorage from Storage property of [IStorageDataRepository](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorageDataRepository.cs).

``` csharp
var usersStorage = user1.Storage;
```

## How to use?

For use this package:

Add [Rovecode.Lotos](https://www.nuget.org/packages/Rovecode.Lotos/) package from NuGet.

If you use [Microsoft.Extensions.DependencyInjection](https://www.nuget.org/packages/Microsoft.Extensions.DependencyInjection/) or ASP.NET Core add also [Rovecode.Lotos.DependencyInjection](https://www.nuget.org/packages/Rovecode.Lotos.DependencyInjection/) package for Dependency Injection.

## Contribute

!!!!TODO

## License

Permissions of this strong copyleft license are conditioned on making available complete source code of licensed works and modifications, which include larger works using a licensed work, under the same license. Copyright and license notices must be preserved. Contributors provide an express grant of patent rights.

__GNU General Public License v3.0__ © __[Roman S](https://github.com/rovecode)__

2021
