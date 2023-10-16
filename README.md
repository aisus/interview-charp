# Sample service

An example application build with .NET and React.
This app is based on the official [react-spa](https://learn.microsoft.com/en-us/aspnet/core/client-side/spa/react?view=aspnetcore-7.0&tabs=visual-studio) project and custom backend service template.

## How it works
This app is a gambling game, where the player can choose a random *number* out of 10 and win or lose *points* depenging on their guess.

Each user has an account (standard ASP.NET Identity setup) and a ```Balance``` with their current number of *points*.

Users are able to play the game, deposit and withdraw *points* from their account (mocked behaviour, no real payments are made) and see their *operations* history.

Standard setup has a victory probability of 10% and pays 9x the stake for the correct guess.

### Registering and logging in
- Use any random email (e.g. sample@test.com) when registering
- There is no real email confirmation
- Starting balance of 10000 points will be deposited to the new account

## How to run

### Running locally

- Clone this repository
- Run the project from the [startup folder](Sample.Service\Sample.Service):
```bash
dotnet run
```
- Or open it and run in your IDE of choise using the ```src``` profile from [launchSettings.json](Sample.Service\Sample.Service\Properties\launchSettings.json)

Access the app via following URLs:
- [UI](https://localhost:44423) - https://localhost:44423
- [Swagger](https://localhost:7002/swagger/ui/index.html) - https://localhost:7002/swagger/ui/index.html

By default the project would use the in-memory database and the data would be reset with each run. 

To use a real PostgreSQL, set a valid connection string in [```appsettings.Development.json```](Sample.Service\Sample.Service\appsettings.Development.json) instead of ```"InMemory"```
```json
"ConnectionStrings": {
      "DefaultConnection": "your_connection_string"
    },
```
Database migrations would run on the first startup. 

### Using Docker

- Clone this repository
- Run the project using [docker-compose](docker-compose.local.yml):
```bash
docker-compose -f docker-compose.local.yml up --build -d
```
- docker-compose will create three containers: the web app, PostgreSQL and [Adminer](https://www.adminer.org). Database uses the default  credentials (```postgres postgres```).

Access the app via following URLs:
- [UI](http://localhost:8000) - http://localhost:8000
- [Swagger](http://localhost:8000/swagger/ui/index.html) - http://localhost:8000/swagger/ui/index.html
- [Adminer](http://localhost:8080) - http://localhost:8080
