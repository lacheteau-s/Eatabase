# Eatabase
A curated database of food products with verified nutritional information for accurate macro tracking.

## Pre-requisites

* .NET SDK 9.0.304
* EF Core CLI Tools (`dotnet ef`)
* Docker

## Getting started

### Run the local database instance

1. Create a `.env` file in `infra/local`.

2. Add the following configuration:

	```text
	DB_PASSWORD=<password>
	```

	:warning: Password must follow the SQL Server [password policy](https://learn.microsoft.com/en-us/sql/relational-databases/security/password-policy?view=sql-server-ver17).

3. Run the container:

    ```sh
	cd infra/local
	docker compose up
	```

### Run the API

1. Define the connection string `Eatabase` in `appsettings.Development.json`:

    ```js
	"ConnectionStrings": {
		"Eatabase": "Server=<server>;Database=Eatabase;User Id=sa;Password=<password>;TrustServerCertificate=True"
	}
	```
	or in user secrets:

	```sh
	cd src/Eatabase.API
	dotnet user-secrets init
	dotnet user-secrets set "ConnectionStrings:Eatabase" "Server=<server>;Database=Eatabase;User Id=sa;Password=<password>;TrustServerCertificate=True"
	```

2. Apply migrations:

    ```sh
	dotnet ef database update -s src/Eatabase.API/Eatabase.API.csproj
	```

3. Run the project.
    
	* In VS Code: from the `Run and Debug` menu, select `Eatabase.API (Debug)` then click :arrow_forward:, or press `F5`.

    * Using the CLI:

	  ```sh
	  dotnet run --project src/Eatabase.API/Eatabase.API.csproj
	  ```
