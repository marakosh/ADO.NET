using System.Data;
using System.Data.SqlClient;

string connectionString = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Stock;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

using (SqlConnection connection = new SqlConnection(connectionString))
{
    try
    {
        connection.Open();
        Console.WriteLine("Подключение к базе данных выполнено успешно!");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка подключения к базе данных: " + ex.Message);
        return;
    }

    while (true)
    {
        Console.WriteLine("Выберите действие:");
        Console.WriteLine("1 - Вывести всю информацию о товаре");
        Console.WriteLine("2 - Вывести все типы товаров");
        Console.WriteLine("3 - Вывести всех поставщиков");
        Console.WriteLine("4 - Показать товар с максимальным количеством");
        Console.WriteLine("5 - Показать товар с минимальным количеством");
        Console.WriteLine("6 - Показать товар с минимальной себестоимостью");
        Console.WriteLine("7 - Показать товар с максимальной себестоимостью");
        Console.WriteLine("8 - Показать товары заданной категории");
        Console.WriteLine("9 - Показать товары заданного поставщика");
        Console.WriteLine("10 - Показать самый старый товар на складе");
        Console.WriteLine("11 - Показать среднее количество товаров по каждому типу товара");
        Console.WriteLine("0 - Выход");

        int choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 0:
                return;

            case 1:
                ShowAllProducts(connection);
                break;

            case 2:
                ShowAllProductTypes(connection);
                break;

            case 3:
                ShowAllSuppliers(connection);
                break;

            case 4:
                ShowProductWithMaxQuantity(connection);
                break;

            case 5:
                ShowProductWithMinQuantity(connection);
                break;

            case 6:
                ShowProductWithMinCost(connection);
                break;

            case 7:
                ShowProductWithMaxCost(connection);
                break;

            case 8:
                Console.WriteLine("Введите тип товара:");
                string productType = Console.ReadLine();
                ShowProductsByType(connection, productType);
                break;

            case 9:
                Console.WriteLine("Введите название поставщика:");
                string supplierName = Console.ReadLine();
                ShowProductsBySupplier(connection, supplierName);
                break;

            case 10:
                ShowOldestProduct(connection);
                break;

            case 11:
                ShowAverageQuantityByProductType(connection);
                break;

            default:
                Console.WriteLine("Неверныйвыбор действия. Попробуйте еще раз.");
                break;
        }
    }
}












static void ShowAllProducts(SqlConnection connection)
{
    string query = "SELECT * FROM Goods";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}

static void ShowAllProductTypes(SqlConnection connection)
{
    string query = "SELECT DISTINCT [Type_Goods] FROM Goods";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Тип товара"]);
    }
}

static void ShowAllSuppliers(SqlConnection connection)
{
    string query = "SELECT DISTINCT [Поставщик товара] FROM Goods";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Поставщик товара"]);
    }
}

static void ShowProductWithMaxQuantity(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Goods ORDER BY [Количество товара] DESC";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}

static void ShowProductWithMinQuantity(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Goods ORDER BY [Количество товара]";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}

static void ShowProductWithMinCost(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Goods ORDER BY [Себестоимость]";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}

static void ShowProductWithMaxCost(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Goods ORDER BY [Себестоимость] DESC";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);
    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}
static void ShowProductsByType(SqlConnection connection, string productType)
{
    string query = "SELECT * FROM Goods WHERE [Тип товара] = @ProductType";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    adapter.SelectCommand.Parameters.AddWithValue("@ProductType", productType);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);
    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}


static void ShowProductsBySupplier(SqlConnection connection, string supplierName)
{
    string query = "SELECT * FROM Goods WHERE [Поставщик товара] = @SupplierName";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    adapter.SelectCommand.Parameters.AddWithValue("@SupplierName", supplierName);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);
    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}


static void ShowOldestProduct(SqlConnection connection)
{
    string query = "SELECT TOP 1 * FROM Goods ORDER BY [Дата поставки]";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Название товара"] + "\t" + row["Тип товара"] + "\t" + row["Поставщик товара"] + "\t" + row["Количество товара"] + "\t" + row["Себестоимость"] + "\t" + row["Дата поставки"]);
    }
}

static void ShowAverageQuantityByType(SqlConnection connection)
{
    string query = "SELECT [Тип товара], AVG([Количество товара]) AS 'Среднее количество товаров' FROM Goods GROUP BY [Тип товара]";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);

    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Тип товара"] + "\t" + row["Среднее количество товаров"]);
    }
}


static void ShowAverageQuantityByProductType(SqlConnection connection)
{
    string query = "SELECT [Тип товара], AVG([Количество товара]) AS [Среднее количество] FROM Goods GROUP BY [Тип товара]";
    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
    DataTable dataTable = new DataTable();
    adapter.Fill(dataTable);
    foreach (DataRow row in dataTable.Rows)
    {
        Console.WriteLine(row["Тип товара"] + "\t" + row["Среднее количество"]);
    }
}