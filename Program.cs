using Microsoft.Data.SqlClient;
using System;
using System.Data;
using System.Data.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;
internal class Program
{
    static async Task Main(string[] args)
    {
        //string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = master; Trusted_Connection=True; TrustServerCertificate = True";

        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{

        //    await connection.OpenAsync();

        //    SqlCommand command = new SqlCommand();

        //    command.CommandText = "CREATE DATABASE DataSetApp";

        //    command.Connection = connection;

        //    await command.ExecuteNonQueryAsync();
        //    Console.WriteLine("DB created");

        //}



        //string sqlQuery = "CREATE TABLE Customers (CustomerID INT IDENTITY PRIMARY KEY, CustomerName NVARCHAR(50) CHECK (CustomerName != '') NOT NULL,Email NVARCHAR(100) CHECK (Email != '' AND Email LIKE '%@%.%') UNIQUE NOT NULL)";
        //await ExecuteCommand(sqlQuery);

        ////string sqlQuery_DROPProducts = "DROP TABLE Products ";
        ////await ExecuteCommand(sqlQuery_DROPProducts);

        //sqlQuery = "CREATE TABLE Products (ProductId  INT PRIMARY KEY IDENTITY, ProductName NVARCHAR(40) NOT NULL,Price DECIMAL(10,2) DEFAULT 0 ,Category NVARCHAR(50) CHECK (Category != '') NOT NULL,Stock INT)";
        //await ExecuteCommand(sqlQuery);

        //string sqlQuery = "DROP TABLE Orders";
        //await ExecuteCommand(sqlQuery);

        //sqlQuery = "CREATE TABLE Orders (OrderID INT IDENTITY PRIMARY KEY, CustomerID INT NOT NULL REFERENCES Customers(CustomerID) ON DELETE NO ACTION,ProductId INT NOT NULL REFERENCES Products(ProductId) ON DELETE NO ACTION, OrderDate DATETIME NOT NULL)";
        //await ExecuteCommand(sqlQuery);




        //string sqlQuery = "INSERT INTO Customers VALUES ('Sam','sam1@gmail.com'),('Bob','bob2@gmail.com'),('Den','den3@gmail.com'),('Ann','ann4@gmail.com')";
        //await ExecuteCommand(sqlQuery);

        //sqlQuery = "INSERT INTO Products VALUES ('Xiaomi Redmi Note 13 Pro 5G',21000.00,'смартфон',5),('iPhone 15',55000.00,'смартфон',7),('Xiaomi 14 Ultra',24000.00,'смартфон',3)";
        //await ExecuteCommand(sqlQuery);

        //sqlQuery = "INSERT INTO Orders VALUES ((SELECT CustomerID FROM Customers WHERE Email = 'sam1@gmail.com' ),(SELECT ProductId FROM Products WHERE ProductName = 'Xiaomi 14 Ultra' ),'2024-07-01'),((SELECT CustomerID FROM Customers WHERE Email = 'bob2@gmail.com' ),(SELECT ProductId FROM Products WHERE ProductName = 'Xiaomi Redmi Note 13 Pro 5G' ),'2024-07-01'),((SELECT CustomerID FROM Customers WHERE Email = 'den3@gmail.com' ),(SELECT ProductId FROM Products WHERE ProductName = 'iPhone 15' ),'2024-07-01')";
        //await ExecuteCommand(sqlQuery);



        static async Task ExecuteCommand(string sqlQuery)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(sqlQuery, connection);

                await command.ExecuteNonQueryAsync();
                Console.WriteLine("Table created");


            }

        }

        Menu();
        void Menu()
        {

            //Необходимо разработать консольное приложение на C#, которое будет управлять данными о клиентах, товарах и заказах, используя DataSet.
            //Приложение должно уметь:

            //Загружать данные о клиентах, товарах и заказах из базы данных.
            //Добавлять новые заказы, обновлять существующие и удалять их.
            //Осуществлять поиск и фильтрацию заказов по различным параметрам(например, по клиенту, дате или номеру заказа).
            //Сохранять изменения в базу данных.


            Console.WriteLine("Выберите:");
            int action;


            Console.WriteLine("1 - Загрузить данные о клиентах");
            Console.WriteLine("2 - Загрузить данные о товарах");
            Console.WriteLine("3 - Загрузить данные о заказах");

            Console.WriteLine("4 - Добавлить новые заказы.");
            Console.WriteLine("5 - Обновлять существующие заказы.");
            Console.WriteLine("6 - Удалить заказ.");

            Console.WriteLine("7 - Найти заказ по клиенту.");
            Console.WriteLine("8 - Найти заказ по дате заказа.");
            Console.WriteLine("9 - Найти заказ по номеру заказа.");

            Console.WriteLine("0 - выход");


            Console.Write("действие - ");
            while (!Int32.TryParse(Console.ReadLine(), out action) || action < 1 || action > 11)
            {
                Console.WriteLine("Не верный ввод.Введите число:");
                Console.Write("действие - ");
            }

            string login = "";
            string password = "";

            switch (action)
            {
                case 1:
                    string sql = "SELECT * FROM Customers";
                    GetInfo(sql);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;
                case 2:
                    sql = "SELECT * FROM Products";
                    GetInfo(sql);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;
                case 3:
                    sql = "SELECT * FROM Orders";
                    GetInfo(sql);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 4:
                    string name;
                    string prod;
                    string dateNum;

                    name = GetInfoNameCustomer();
                    if (name == "")
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    prod = GetInfoNameProduct();
                    if (prod == "")
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }


                    int idCustomer = GetIdCustomer(name);
                    

                    if (idCustomer == -1)
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    int idProd = GetIdProduct(prod);
                    if (idProd == -1)
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }

                    Console.WriteLine("Введите дату заказа");
                    dateNum = Console.ReadLine();
                    var date = DateTime.Parse(dateNum);


                    sql = "SELECT * FROM Orders";
                    AddOrder(sql, idCustomer, idProd, date);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 5:
                    sql = "SELECT * FROM Orders";

                    Console.WriteLine("Введите Id заказа");
                    int idOrders = Int32.Parse(Console.ReadLine());

                    name = GetInfoNameCustomer();
                    if (name == "")
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    prod = GetInfoNameProduct();
                    if (prod == "")
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }

                    Console.WriteLine("Введите дату заказа");
                    dateNum = Console.ReadLine();

                    date = DateTime.Parse(dateNum);


                    idCustomer = GetIdCustomer(name);
                    if (idCustomer == -1)
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    idProd = GetIdProduct(prod);
                    if (idProd == -1)
                    {
                        Thread.Sleep(5000);
                        Console.Clear();
                        Menu();
                    }
                    UppdateOrder(sql, idOrders, idCustomer, idProd, date);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 6:
                    sql = "SELECT * FROM Orders";

                    Console.WriteLine("Введите Id заказа");
                    idOrders = Int32.Parse(Console.ReadLine());

                    DeleteOrder(sql, idOrders);

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 7:

                    name = GetInfoNameCustomer();
                    idCustomer = GetIdCustomer(name);
                    sql = $"SELECT * FROM Orders WHERE CustomerID = {idCustomer}";
                    GetInfo(sql);


                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 8:


                    Console.WriteLine("Введите дату заказа (в формате ГГГГ-ММ-ДД)");
                    date = DateTime.Parse(Console.ReadLine());

                    sql = "SELECT * FROM Orders WHERE OrderDate = @OrderDate";
                    FindOrderByDate(sql, date);
                   

                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 9:
                    Console.WriteLine("Введите Id заказа");
                    idOrders = Int32.Parse(Console.ReadLine());

                    sql = $"SELECT * FROM Orders WHERE OrderID = {idOrders}";
                    GetInfo(sql);


                    Thread.Sleep(5000);
                    Console.Clear();
                    Menu();
                    break;

                case 0:
                    break;

            }

        }
        
        static string GetInfoNameCustomer()
        {
            string name;
            string connectionString = @"Data Source=DESKTOP-ITRLGSN; Initial Catalog=DataSetApp; Trusted_Connection=True; TrustServerCertificate=True";
            string sqlExpression = "SELECT * FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                while (true)
                {
                    Console.WriteLine("Введите имя клиента:");
                    name = Console.ReadLine();

                    var selectedUsers = ds.Tables[0].Select($"CustomerName = '{name}'");

                    if (selectedUsers.Length > 0)
                    {
                        break;
                    }
                    else

                    {
                        if (!AskToAdd(name))
                        {
                            name = "";
                            break;
                        }
                        AddCustomerToDatabase(ds, adapter, name);
                    }

                    ds.Clear();
                    adapter.Fill(ds);
                }
            }
            return name;
        }

        static void AddCustomerToDatabase(DataSet ds, SqlDataAdapter adapter, string name)
        {
            DataTable dt = ds.Tables[0];
            DataRow newRow = dt.NewRow();
            newRow["CustomerName"] = name;

            string Email = Console.ReadLine();
            newRow["Email"] = Email;
            
            dt.Rows.Add(newRow);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(ds);
            Console.WriteLine("Клиент добавлен в базу.");
        }


        static string GetInfoNameProduct()
        {
            string prodName;
            string connectionString = @"Data Source=DESKTOP-ITRLGSN; Initial Catalog=DataSetApp; Trusted_Connection=True; TrustServerCertificate=True";
            string sqlExpression = "SELECT * FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sqlExpression, connection);
                DataSet ds = new DataSet();
                adapter.Fill(ds);

                while (true)
                {
                    Console.WriteLine("Введите название товара:");
                    prodName = Console.ReadLine();

                    var selectedProducts = ds.Tables[0].Select($"ProductName = '{prodName}'");

                    if (selectedProducts.Length > 0)
                    {
                        break;
                    }
                    else
                    {
                        if (!AskToAdd(prodName))
                        {
                            prodName = "";
                            break; 
                        } 
                        AddProductToDatabase(ds, adapter, prodName);
                    }

                    ds.Clear();
                    adapter.Fill(ds);
                }
            }
            return prodName;
        }

        static void AddProductToDatabase(DataSet ds, SqlDataAdapter adapter, string prodName)
        {
            DataTable dt = ds.Tables[0];
            DataRow newRow = dt.NewRow();
            newRow["ProductName"] = prodName;

            newRow["Price"] = GetValidDecimal("Введите Price товара");
            newRow["Category"] = GetValidInt("Введите Id категории");
            newRow["Stock"] = GetValidInt("Введите количество продукта на складе:");

            dt.Rows.Add(newRow);
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
            adapter.Update(ds);
            Console.WriteLine("Товар добавлен в базу.");
        }

        static bool AskToAdd(string prodName)
        {
            Console.WriteLine("Данных с таким названием не найдено.");
            Console.WriteLine("Добавить  в базу данных? (Y / N)");
            var input = Console.ReadLine()?.ToUpper();

            while (input != "Y" && input != "N")
            {
                Console.WriteLine("Не верный ввод. Добавить в базу? (Y/N)");
                input = Console.ReadLine()?.ToUpper();
            }

            return input == "Y";
        }

        

        static decimal GetValidDecimal(string prompt)
        {
            decimal value;
            Console.WriteLine(prompt);
            while (!decimal.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.WriteLine("Не верный ввод. Пожалуйста, введите положительное число:");
            }
            return value;
        }

        static int GetValidInt(string prompt)
        {
            int value;
            Console.WriteLine(prompt);
            while (!int.TryParse(Console.ReadLine(), out value) || value < 0)
            {
                Console.WriteLine("Не верный ввод. Пожалуйста, введите положительное число:");
            }
            return value;
        }



        static void GetInfo(string sql)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        foreach (DataColumn column in dt.Columns)
                            Console.Write($"{column.ColumnName,-20}\t");
                        Console.WriteLine();

                        foreach (DataRow row in dt.Rows)
                        {
                            var cells = row.ItemArray;
                            foreach (object cell in cells)
                                Console.Write($"{cell,-20}\t");
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Данных нет.");
                }
            }

        }

        static int GetIdCustomer(string customerName)
        {
            int ID = -1;
            string connectionString = @"Data Source=DESKTOP-ITRLGSN; Initial Catalog=DataSetApp; Trusted_Connection=True; TrustServerCertificate=True";
            string sql = "SELECT * FROM Customers";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();

                connection.Open();
                adapter.Fill(ds);
                var selectedCustomers = ds.Tables[0].Select($"CustomerName = '{customerName}'");

                if (selectedCustomers.Length > 0)
                {
                    ID = Convert.ToInt32(selectedCustomers[0]["CustomerID"]);
                }
                else
                {
                    Console.WriteLine("Клиент с таким именем не найден.");
                }

            }
            return ID;
        }
        static int GetIdProduct(string prodName)
        {
            int ID = -1;
            string connectionString = @"Data Source=DESKTOP-ITRLGSN; Initial Catalog=DataSetApp; Trusted_Connection=True; TrustServerCertificate=True";
            string sql = "SELECT * FROM Products";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                DataSet ds = new DataSet();

                connection.Open();
                adapter.Fill(ds);
                var selectedCustomers = ds.Tables[0].Select($"ProductName = '{prodName}'");

                if (selectedCustomers.Length > 0)
                {
                    ID = Convert.ToInt32(selectedCustomers[0]["ProductId"]);
                }
                else
                {
                    Console.WriteLine("Продукт с таким названием не найден.");
                }

            }
            return ID;
        }

        static void AddOrder(string sql, int id, int idProd, DateTime date)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";


            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);


                DataTable dt = ds.Tables[0];
                DataRow newRow = dt.NewRow();
                newRow["CustomerID"] = id;
                newRow["ProductID"] = idProd;
                newRow["OrderDate"] = date;
                dt.Rows.Add(newRow);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                adapter.Update(ds);

                ds.Clear();

                adapter.Fill(ds);



                foreach (DataColumn column in dt.Columns)
                    Console.Write($"{column.ColumnName}\t");
                Console.WriteLine();
                foreach (DataRow row in dt.Rows)
                {

                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write($"{cell}\t");
                    Console.WriteLine();
                }
                Console.Read();
            }
        }

        static void UppdateOrder(string sql, int idOrders, int idCustomer, int idProd, DateTime date)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);


                DataTable dt = ds.Tables[0];

                var foundRows = dt.Select($"OrderID = {idOrders}");

                if (foundRows.Length > 0)
                {

                    DataRow row = foundRows[0];
                    row["CustomerID"] = idCustomer;
                    row["ProductID"] = idProd;
                    row["OrderDate"] = date;


                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);
                    int updatedRows = adapter.Update(ds);
                    if (updatedRows > 0)
                    {
                        Console.WriteLine("Запись успешно обновлена.");
                    }
                    else
                    {
                        Console.WriteLine("Обновление не произошло.");
                    }

                    ds.Clear();

                    adapter.Fill(ds);



                    foreach (DataColumn column in dt.Columns)
                        Console.Write($"{column.ColumnName}\t");
                    Console.WriteLine();
                    foreach (DataRow r in dt.Rows)
                    {

                        var cells = r.ItemArray;
                        foreach (object cell in cells)
                            Console.Write($"{cell}\t");
                        Console.WriteLine();
                    }
                }

            }
        }

        static void DeleteOrder(string sql, int id)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);


                DataTable dt = ds.Tables[0];

                var foundRows = dt.Select($"Id = {id}");
                if (foundRows.Length > 0)
                {
                    foreach (DataRow row in foundRows)
                    {
                        dt.Rows.Remove(row);
                    }

                    SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter);

                    int updatedRows = adapter.Update(ds);

                    if (updatedRows > 0)
                    {
                        Console.WriteLine($"Заказ с Id {id} был успешно удалён.");
                    }
                    else
                    {
                        Console.WriteLine("Не удалось удалить заказ.");
                    }

                }
                else
                {
                    Console.WriteLine($"Заказ с Id {id} не найден.");
                }

                ds.Clear();
                adapter.Fill(ds);



                foreach (DataColumn column in dt.Columns)
                    Console.Write($"{column.ColumnName}\t");
                Console.WriteLine();
                foreach (DataRow row in dt.Rows)
                {

                    var cells = row.ItemArray;
                    foreach (object cell in cells)
                        Console.Write($"{cell}\t");
                    Console.WriteLine();
                }
            }
        }

        static void FindOrderByCustomerName(string sql, int customerID)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);

                DataSet ds = new DataSet();

                adapter.Fill(ds);

                DataTable dt = ds.Tables[0];

                var foundRows = dt.Select($"CustomerID = {customerID}");
                if (foundRows.Length > 0)
                {
                    foreach (var b in foundRows)
                        Console.WriteLine($"{"OrderID"},{"CustomerID"}, {"ProductId"}, {"OrderDate"}");
                }
                else
                {
                    Console.WriteLine("У данного клиента пока заказов нет.");
                }
            }
        }

        static void FindOrderByDate(string sql, DateTime date)
        {
            string connectionString = @"Data Source = DESKTOP-ITRLGSN; Initial Catalog = DataSetApp; Trusted_Connection=True; TrustServerCertificate = True";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(sql, connection);
                command.Parameters.AddWithValue("@OrderDate", date);
                SqlDataAdapter adapter = new SqlDataAdapter(command);


                DataSet ds = new DataSet();

                adapter.Fill(ds);



                DataTable dt = ds.Tables[0];

                if (ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataTable dt1 in ds.Tables)
                    {
                        foreach (DataColumn column in dt1.Columns)
                            Console.Write($"{column.ColumnName,-20}\t");
                        Console.WriteLine();

                        foreach (DataRow row in dt1.Rows)
                        {
                            var cells = row.ItemArray;
                            foreach (object cell in cells)
                                Console.Write($"{cell,-20}\t");
                            Console.WriteLine();
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Данных нет.");
                }
            }
        }


    }
}
