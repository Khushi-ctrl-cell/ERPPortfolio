# ERPPortfolio

Production-grade ASP.NET MVC 5 ERP showcase built for portfolio and recruiter evaluation.

## Stack

- ASP.NET MVC 5 on .NET Framework 4.8
- Entity Framework 6
- SQL Server / LocalDB
- Razor multi-page architecture

## Seeded Credentials

- Username: `admin`
- Password: `Admin@123`

## Run Instructions

1. Open `ERPPortfolio.csproj` in Visual Studio.
2. Restore NuGet packages.
3. Ensure LocalDB or SQL Server Express is available.
4. Build the project.
5. Run with IIS Express.
6. On first launch, Entity Framework creates and seeds the database automatically.
7. Sign in with the seeded admin account.

## Optional SQL Bootstrap

If you prefer a manual database setup, execute `Sql/erp-schema-and-seed.sql`, then update the `ERPDbContext` connection string in `Web.config` to match your SQL Server instance.
