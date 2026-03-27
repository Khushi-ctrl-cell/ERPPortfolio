using System;
using System.Collections.Generic;
using System.Data.Entity;
using ERPPortfolio.Models;

namespace ERPPortfolio.Data
{
    public class ErpDbInitializer : CreateDatabaseIfNotExists<ERPDbContext>
    {
        private const string SeededAdminPasswordHash = "J9EpeEW7ArQwTHnXQWOEiQ==:ompkF6G4auEFrsT2b0pAuKsIeAnrqpUkfDtuy+Nf/i0=";

        protected override void Seed(ERPDbContext context)
        {
            var adminUser = new User
            {
                Username = "admin",
                FullName = "Aarav Malhotra",
                PasswordHash = SeededAdminPasswordHash
            };

            context.Users.Add(adminUser);

            var finance = new ProductItem { Name = "Finance Suite", Description = "Corporate finance command center with ledger automation and treasury controls.", Weight = 128.40m };
            var operations = new ProductItem { Name = "Operations Hub", Description = "Plant operations umbrella with sourcing, logistics, and fulfillment workstreams.", Weight = 236.80m };
            var procurement = new ProductItem { Name = "Procurement Console", Description = "Strategic sourcing and supplier coordination workspace.", Weight = 94.10m, ParentItem = operations };
            var warehouse = new ProductItem { Name = "Warehouse Grid", Description = "Bin movement, picking, and cycle count orchestration.", Weight = 76.50m, ParentItem = operations };
            var supplierPortal = new ProductItem { Name = "Supplier Portal", Description = "Vendor collaboration and contract milestone tracking.", Weight = 41.90m, ParentItem = procurement };
            var demandPlanning = new ProductItem { Name = "Demand Planner", Description = "Forecast intelligence and replenishment planning engine.", Weight = 54.30m, ParentItem = operations };
            var accounting = new ProductItem { Name = "Accounting Core", Description = "Journal, reconciliation, and close management domain.", Weight = 82.25m, ParentItem = finance };
            var receivables = new ProductItem { Name = "Receivables Desk", Description = "Collections, invoice visibility, and customer aging control.", Weight = 39.60m, ParentItem = accounting };
            var payables = new ProductItem { Name = "Payables Desk", Description = "Supplier invoice routing and payment scheduling cockpit.", Weight = 42.15m, ParentItem = accounting };
            var hr = new ProductItem { Name = "People Operations", Description = "HR master data and workforce service management.", Weight = 118.00m };
            var onboarding = new ProductItem { Name = "Onboarding Studio", Description = "Employee onboarding journeys and policy acknowledgements.", Weight = 24.35m, ParentItem = hr };
            var learning = new ProductItem { Name = "Learning Matrix", Description = "Training plans, certifications, and skills progression records.", Weight = 19.85m, ParentItem = onboarding };

            context.ProductItems.AddRange(new List<ProductItem>
            {
                finance, operations, procurement, warehouse, supplierPortal,
                demandPlanning, accounting, receivables, payables, hr, onboarding, learning
            });

            context.AuditLogs.Add(new AuditLog
            {
                Action = "Seed",
                Timestamp = DateTime.UtcNow,
                Details = "Initial ERP portfolio dataset was generated during first-run provisioning."
            });

            context.SaveChanges();
            base.Seed(context);
        }
    }
}
