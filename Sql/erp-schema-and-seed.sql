IF DB_ID('ERPPortfolio') IS NULL
BEGIN
    CREATE DATABASE ERPPortfolio;
END
GO

USE ERPPortfolio;
GO

IF OBJECT_ID('dbo.AuditLogs', 'U') IS NOT NULL DROP TABLE dbo.AuditLogs;
IF OBJECT_ID('dbo.ProductItems', 'U') IS NOT NULL DROP TABLE dbo.ProductItems;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;
GO

CREATE TABLE dbo.Users
(
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(512) NOT NULL,
    FullName NVARCHAR(100) NOT NULL
);
GO

CREATE TABLE dbo.ProductItems
(
    ItemId INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(120) NOT NULL,
    Description NVARCHAR(500) NOT NULL,
    Weight DECIMAL(18,2) NOT NULL,
    ParentItemId INT NULL,
    CONSTRAINT FK_ProductItems_ParentItem FOREIGN KEY (ParentItemId) REFERENCES dbo.ProductItems(ItemId)
);
GO

CREATE TABLE dbo.AuditLogs
(
    LogId INT IDENTITY(1,1) PRIMARY KEY,
    Action NVARCHAR(50) NOT NULL,
    [Timestamp] DATETIME NOT NULL,
    Details NVARCHAR(1000) NOT NULL
);
GO

INSERT INTO dbo.Users (Username, PasswordHash, FullName)
VALUES ('admin', 'J9EpeEW7ArQwTHnXQWOEiQ==:ompkF6G4auEFrsT2b0pAuKsIeAnrqpUkfDtuy+Nf/i0=', 'Aarav Malhotra');
GO

SET IDENTITY_INSERT dbo.ProductItems ON;
INSERT INTO dbo.ProductItems (ItemId, Name, Description, Weight, ParentItemId) VALUES
(1, 'Finance Suite', 'Corporate finance command center with ledger automation and treasury controls.', 128.40, NULL),
(2, 'Operations Hub', 'Plant operations umbrella with sourcing, logistics, and fulfillment workstreams.', 236.80, NULL),
(3, 'Procurement Console', 'Strategic sourcing and supplier coordination workspace.', 94.10, 2),
(4, 'Warehouse Grid', 'Bin movement, picking, and cycle count orchestration.', 76.50, 2),
(5, 'Supplier Portal', 'Vendor collaboration and contract milestone tracking.', 41.90, 3),
(6, 'Demand Planner', 'Forecast intelligence and replenishment planning engine.', 54.30, 2),
(7, 'Accounting Core', 'Journal, reconciliation, and close management domain.', 82.25, 1),
(8, 'Receivables Desk', 'Collections, invoice visibility, and customer aging control.', 39.60, 7),
(9, 'Payables Desk', 'Supplier invoice routing and payment scheduling cockpit.', 42.15, 7),
(10, 'People Operations', 'HR master data and workforce service management.', 118.00, NULL),
(11, 'Onboarding Studio', 'Employee onboarding journeys and policy acknowledgements.', 24.35, 10),
(12, 'Learning Matrix', 'Training plans, certifications, and skills progression records.', 19.85, 11);
SET IDENTITY_INSERT dbo.ProductItems OFF;
GO

INSERT INTO dbo.AuditLogs (Action, [Timestamp], Details) VALUES
('Seed', GETDATE(), 'Initial ERP portfolio dataset was seeded through the SQL bootstrap script.'),
('Create', DATEADD(MINUTE, -110, GETDATE()), 'Aarav Malhotra created item ''Supplier Portal'' with vendor collaboration controls.'),
('Update', DATEADD(MINUTE, -80, GETDATE()), 'Aarav Malhotra updated item ''Warehouse Grid'' to align with revised logistics weight baselines.'),
('Delete', DATEADD(MINUTE, -40, GETDATE()), 'Aarav Malhotra deleted a retired item and preserved the hierarchy by reattaching child records.');
GO
