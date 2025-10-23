# Eatabase
A curated database of food products with verified nutritional information for accurate macro tracking.

## Pre-requisites

* .NET SDK 9.0.304
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

* In VS Code: from the `Run and Debug` menu, select `Eatabase.API (Debug)` then click :arrow_forward:, or press `F5`.

* Using the CLI:

	```sh
	dotnet run --project src/Eatabase.API/Eatabase.API.csproj
	```
