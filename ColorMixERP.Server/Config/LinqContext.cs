using ColorMixERP.Server.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Config
{
    public sealed class LinqContext : DataContext
    {
        public static readonly string DB_CONNECTION = $@"Data Source=DESKTOP-MKVETFI\SQLEXPRESS;Initial Catalog=TashTracker;Integrated Security=True;";
        public LinqContext(string conn) : base(conn)
        {

        }
        // SINGLETON 
        private static LinqContext _instance = null;
        private static readonly Object _padLock = new object();
         //public static LinqContext Instance
         //{
         //    get
         //    {
         //        lock (_padLock)
         //        {
         //            if (_instance == null)
         //            {
         //                _instance = new LinqContext();;
         //            }
         //            return _instance;
         //        }
         //    }
         //}
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
    }
}
