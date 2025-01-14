# PokémonTeambuilder

## installation instructions

### Requirements

-	MSSQL server
-	A program to run docker on (i.e. Docker Desktop)

### Database

Run a MSSQL server on the standard port (1433) \
If you need help, Microsoft has resources for setting up the database here

### Pulling

Open your docker program (i.e. Docker Desktop) and pull the following image. In Docker Desktop you can do this by opening the terminal.
```
docker pull ghcr.io/wesleyburgt/pokemonteambuilder:latest
```

### Running

Run the image with the following code.
```
docker run -d -p 7010:8080 ghcr.io/wesleyburgt/pokemonteambuilder
```
7010 is the port the front-end uses, don’t change this. \
8080 is the port the container is running on, this can be changed if needed.

Running the image should also result in the pokemon_teambuilder database being created on your MSSQL server

#### Troubleshooting

If the image stops immediately, or after about 5 seconds, it is likely that it cannot create the database and there is likely a database with the same name. If this is the case and you want to keep the same database name you can try the following steps:

-	Stop the MSSQL server
-	Go to C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA
-	Delete “pokemon_teambuilder.mdf” and “pokemon_teambuilder_log.ldf”
-	Start the MSSQL server
-	Run the docker again

If you don’t mind changing the database name, you can change the connectionstring in the appsettings.json.

### Updating Database

When the database doesn’t have data yet, or you want the latest Pokémon data that is available on https://pokeapi.co/, follow these steps.

-	Run the Docker image
-	Open your command prompt
-	Input the following code
```
curl -X POST http://localhost:7010/api/Database/UpdateDatabase
```
This may take some time, at the end you should receive "Database updated successfully".

#### Alternative

-	Run the Docker image
-	Open command prompt
-	Input the following codes, all can be copy and pasted at the same time
```
curl -X POST http://localhost:7010/api/Database/UpdateNatures
curl -X POST http://localhost:7010/api/Database/UpdateItems 
curl -X POST http://localhost:7010/api/Database/UpdateAbilities 
curl -X POST http://localhost:7010/api/Database/UpdateTypings 
curl -X POST http://localhost:7010/api/Database/UpdateMoves
curl -X POST http://localhost:7010/api/Database/UpdateBasePokemons
```
Doing this, you can see at which part of the process it currently is. Each process should return with “… updated successfully”. This will not result in a faster time or different result, but it may be better while troubleshooting.