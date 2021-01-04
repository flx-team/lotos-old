# Rovecode.Lotos 

## Motivation

## Features

## Code Example

You can find examples projects in [Examples](https://github.com/rovecode/Rovecode.Lotos/tree/master/Examples) folder. But below I will briefly describe some patterns of code for fast introduction in current project.

### How connect to mongo and get storage

For connect to mongo you can use [LotosConnectFactory](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Factories/LotosConnectFactory.cs). This factory contains some methods for create IStorageContext. Create [IStorageContext](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Contexts/IStorageContext.cs) is needed to get [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs). [IStorage](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Repositories/IStorage.cs) is a base interface for work with db.

Use method __Connect__ for connect to db and get [IStorageContext](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Contexts/IStorageContext.cs).

``` csharp
var context = LotosConnectFactory.Connect("<your_connect_string>", "<your_db_name>");
```

__Connect__ method contains more overloading for many different situations, but we don't review this.

If connect to db is failed Lotos throw [LotosException](https://github.com/rovecode/Rovecode.Lotos/blob/master/Sources/Rovecode.Lotos/Exceptions/LotosException.cs) with error details.

For get storage you must have __record__ and this __record__ must extended from StorageData. For get storage invoke method __Get__ and in generic put your data __record__.

``` csharp
var userStorage = context.Get<UserData>();
```

Lotos auto generate collection in db.

### How create data model

Create data model really ease. Create record and extend them from StorageData.

``` csharp
record UserData : StorageData
{
    public string Name { get; set; }
    public int Phone { get; set; }
}
```

### How create/keep data model in db

For keep model in db you create them and put in Keep method in IStorage. Same Keep method return repository for work with created data.

``` csharp
var userData = new UserData() 
{ 
    Name = "Roman" 
};

var user = userStorage.Keep(userData);
```

### How find/get kepped datas

For find kepped datas use methods Search, SearchMany, SearchAsync, SearchManyAsync. In this methods you used expression filters for search.

Finds first user with name Roman else return null.

``` csharp
var user1 = userStorage.Search(e => e.Name == "Roman");

if (user1 is not null)
{
    // ..
}
```

Finds all users with name Roman. Return IEnumerable.

``` csharp
var users1 = userStorage.SearchMany(e => e.Name == "Roman");

foreach(var usr in users1)
{
    // ..
}
```

Also you can use Async methods for search in other thread.

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

``` csharp

using (user1)
{
    user1.Data.Phone = 12345;
}
```

``` csharp
user1.Data.Phone = 12345;

user1.Exchange();
```

### How delete kepped datas

For delete kepped datas using filters you can use methods _Burn_, _BurnMany_ in IStorage. This methods use expression filter.

_Burn_ method deletes first find data, matching expression filter.

``` csharp
usersStorage.Burn(e => e.Name == "Roman");
```

_BurnMany_ method deletes all find datas, matching expression filter.

``` csharp
usersStorage.BurnMany(e => e.Name == "Roman");
```

Or you can use _Burn_ method in IStorageDataRepository.

``` csharp
user1.Burn();
```

## Installation

## Tests

## How to use?

## Contribute

## License

Permissions of this strong copyleft license are conditioned on making available complete source code of licensed works and modifications, which include larger works using a licensed work, under the same license. Copyright and license notices must be preserved. Contributors provide an express grant of patent rights.

__GNU General Public License v3.0__ © [Roman S](https://github.com/rovecode)

2021
