using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using ProiectASE.Model;
using ProiectASE.View;

namespace ProiectASE.ViewModel
{
    public sealed class MainService
    {
        private SQLiteConnection m_dbConnection;
        private string initSql = "CREATE TABLE Client (Id INT PRIMARY KEY, Name VARCHAR(50), Budget FLOAT);" +
                                 "CREATE TABLE Product (Id INT PRIMARY KEY, Name VARCHAR(50), Price FLOAT, Production_Date DATETIME, Expiration_Date DATETIME, Client_Id INT, FOREIGN KEY(Client_Id) REFERENCES Client(Id));" +
                                 "CREATE TABLE GeneralProduct (Id INT PRIMARY KEY, Name VARCHAR(50), Price FLOAT, Production_Date DATETIME, Expiration_Date DATETIME);" +
                                 "CREATE TABLE Trx (Id INT PRIMARY KEY, Client_Id INT);";
        private string baseDir = Environment.CurrentDirectory;
        private static readonly Lazy<MainService> _instance = new Lazy<MainService>(() => new MainService());
        public static MainService Instance
        {
            get { return _instance.Value; }
        }

        public int GetCount(string tableName)
        {
            string selectAllSql = "SELECT COUNT(*) FROM " + tableName;
            SQLiteCommand queryAllCommand = new SQLiteCommand(selectAllSql, m_dbConnection);
            int alreadyInLen = Convert.ToInt32(queryAllCommand.ExecuteScalar());
            return alreadyInLen;
        }

        public void DeleteClient(Client c)
        {
            string deleteClientSql = "DELETE FROM Client WHERE Id = " + c.Id.ToString();
            string deleteTxSql = "DELETE FROM Trx WHERE Client_Id = " + c.Id.ToString();
            string deleteProductSql = "DELETE FROM Product WHERE Client_Id = " + c.Id.ToString();
            SQLiteCommand command1 = new SQLiteCommand(deleteClientSql, m_dbConnection);
            SQLiteCommand command2 = new SQLiteCommand(deleteTxSql, m_dbConnection);
            SQLiteCommand command3 = new SQLiteCommand(deleteProductSql, m_dbConnection);
            command1.ExecuteNonQuery();
            command2.ExecuteNonQuery();
            command3.ExecuteNonQuery();
        }

        public void DeleteProduct(Product p)
        {
            string deleteProductSql = "DELETE FROM GeneralProduct WHERE Id = " + p.Id.ToString();
            SQLiteCommand command1 = new SQLiteCommand(deleteProductSql, m_dbConnection);
            command1.ExecuteNonQuery();
        }


        private MainService()
        {

            if (!File.Exists(baseDir + "\\Vanzari.sqlite"))
            {
                SQLiteConnection.CreateFile("Vanzari.sqlite");
                m_dbConnection = new SQLiteConnection("Data Source=Vanzari.sqlite;Version=3;");
                m_dbConnection.Open();
                SQLiteCommand command = new SQLiteCommand(initSql, m_dbConnection);
                command.ExecuteNonQuery();
            } else
            {
                m_dbConnection = new SQLiteConnection("Data Source=Vanzari.sqlite;Version=3;");
                m_dbConnection.Open();
            }
        }

        public void RegisterTransaction(Transaction tx)
        {
            string regTrxSql = "INSERT INTO Trx (Id, Client_Id) values (" + (tx.Id).ToString() + ", " + tx.Client.Id.ToString() + ")";
            SQLiteCommand regCommand = new SQLiteCommand(regTrxSql, m_dbConnection);
            regCommand.ExecuteNonQuery();
        }

        public void RegisterProduct(Product product)
        {
            string regProductSql = "INSERT INTO GeneralProduct (Id, Name, Price, Production_Date, Expiration_Date) values (" + (product.Id).ToString() + ", '" + product.Name + "', " + product.Price.ToString() + ", '" + product.ProductionDate.ToString("yyyy-MM-dd") + "', " + "'" + product.ExpirationDate.ToString("yyyy-MM-dd") + "')";
            SQLiteCommand regCommand = new SQLiteCommand(regProductSql, m_dbConnection);
            regCommand.ExecuteNonQuery();
            Message message = new Message("Produsul a fost inregistrat");
            message.Show();
        }

        public Client UpdateClient(Client client, Product product)
        {
            string newBudget = (client.Budget - product.Price).ToString();
            string regClientSql = "UPDATE Client SET Budget = " + newBudget + " WHERE Id = " + client.Id + "";
            SQLiteCommand regCommand = new SQLiteCommand(regClientSql, m_dbConnection);
            regCommand.ExecuteNonQuery();

            int alreadyInLen = GetCount("Product");
            string regProductSql = "INSERT INTO Product (Id, Name, Price, Production_Date, Expiration_Date, Client_Id) values (" + (alreadyInLen + 1).ToString() + ", '" + product.Name + "', " + product.Price.ToString() + ", '" + product.ProductionDate.ToString("yyyy-MM-dd") + "', " + "'" + product.ExpirationDate.ToString("yyyy-MM-dd") + "', " + client.Id.ToString() + ")";
            SQLiteCommand regCommand2 = new SQLiteCommand(regProductSql, m_dbConnection);
            regCommand2.ExecuteNonQuery();

            client.Products.Add(product);
            client.Budget -= product.Price;
            return client;
        }

        public void RegisterClient(Client client)
        {
            string regClientSql = "INSERT INTO Client (Id, Name, Budget) values (" + (client.Id).ToString() + ", '" + client.Name + "', " + client.Budget.ToString() + ")";
            SQLiteCommand regCommand = new SQLiteCommand(regClientSql, m_dbConnection);
            int code = regCommand.ExecuteNonQuery();
            Message message = new Message("Clientul a fost inregistrat");
            message.Show();
        }

        public Product[] QueryAllGeneralProducts()
        {
            int results = GetCount("GeneralProduct");
            Product[] products = new Product[results];

            string selectAllProductsSql = "SELECT * FROM GeneralProduct";
            SQLiteCommand command = new SQLiteCommand(selectAllProductsSql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            
            int incrementor = 0;
            while (reader.Read())
            {
                Product p = new Product(Convert.ToInt32(reader["Id"]), reader["Name"].ToString(), Convert.ToSingle(reader["Price"]), Convert.ToDateTime(reader["Production_Date"]), Convert.ToDateTime(reader["Expiration_Date"]));
                products[incrementor] = p;
                incrementor++;
            }
            return products;
        }



        public Client[] QueryAllClients()
        {
            int results = GetCount("Client");
            Client[] clients = new Client[results];

            string selectAllClientsSql = "SELECT * FROM Client";
            SQLiteCommand queryClientsCommand = new SQLiteCommand(selectAllClientsSql, m_dbConnection);
            SQLiteDataReader clientReader = queryClientsCommand.ExecuteReader();

            int incrementor = 0;
            while (clientReader.Read())
            {
                Client c = new Client(Convert.ToInt32(clientReader["Id"]), clientReader["Name"].ToString(), Convert.ToSingle(clientReader["Budget"]));
                clients[incrementor] = c;
                incrementor++;
            }
            return clients;
        }

        public Transaction[] QueryAllTx()
        {
            int results = GetCount("Trx");
            Transaction[] trx = new Transaction[results];

            string selectAllTrxSql = "SELECT * FROM Trx";
            SQLiteCommand command = new SQLiteCommand(selectAllTrxSql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();

            int incrementor = 0;
            while (reader.Read())
            {
                Client c = FindClientById(reader["Client_Id"].ToString());
                Transaction t = new Transaction(Convert.ToInt32(reader["Id"]), c);
                trx[incrementor] = t;
                incrementor++;
            }
            return trx;
        }

        public List<Product> FindClientProducts(Client c)
        {
            string findByIdSql = "SELECT * FROM Product WHERE Client_Id = " + c.Id + "";
            SQLiteCommand command = new SQLiteCommand(findByIdSql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Product> products = new List<Product>();
            while (reader.Read())
            {
                Product p = new Product(Convert.ToInt32(reader["Id"]), reader["Name"].ToString(), Convert.ToSingle(reader["Price"]), Convert.ToDateTime(reader["Production_Date"]), Convert.ToDateTime(reader["Expiration_Date"]));
                products.Add(p);
            }
            return products;
        }

        private Client FindClientById(string Id)
        {
            string findByIdSql = "SELECT * FROM Client WHERE Id = " + Id + "";
            SQLiteCommand command = new SQLiteCommand(findByIdSql, m_dbConnection);
            SQLiteDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Client c = new Client(Convert.ToInt32(reader["Id"]), reader["Name"].ToString(), Convert.ToSingle(reader["Budget"]));
                return c;
            }
            return null;
        }

        public void CloseConnection() { m_dbConnection.Close(); }

    }
}
