using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ColorMixERP.Server.Entities.Tables;

namespace ColorMixERP.Server.Config
{
    public sealed class LinqContext : DataContext
    {
        public static readonly string DB_CONNECTION = LinqDB.Instance;
          //  $@"Data Source=DESKTOP-MKVETFI\SQLEXPRESS;Initial Catalog=TashTracker;Persist Security Info=True;User ID=Iskandar;Password=11111;Pooling=True;Min Pool Size=5;Max Pool Size=20";

        public LinqContext(string conn) : base(conn)
        {

        }

        public Table<AccountUser> AccountUsers;
        public Table<Category> Categories;
        public Table<Client> Clients;
        public Table<ClientOrder> ClientOrders;
        public Table<DebtCover> DebtCovers;
        public Table<Expense> Expenses;
        public Table<InnerMovement> InnerMovements;
        public Table<Product> Products;
        public Table<ProductStock> ProductStocks;
        public Table<ReturnedSale> ReturnedSales;
        public Table<Sale> Sales;
        public Table<Supplier> Suppliers;
        public Table<WorkPlace> WorkPlaces;
        public Table<Income> Incomes;
        public Table<IncomeProduct> IncomeProducts;
        public Table<DebtorCreditor> DebtorCreditors;
        public Table<DailyBalance> DailyBalances;
        public Table<CompanyInfo> CompanyInfos;
    }
}
