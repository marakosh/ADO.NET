using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration.Json;

namespace ADO_1
{
    class Program
    {
        static void Main(string[] args)
        {
            int input = Convert.ToInt32(Console.ReadLine());
            ConfigurationBuilder builder = new();
            builder.AddJsonFile("appsettings.json");
            var res = builder.Build();
            using SqlConnection conn = new(res.GetConnectionString("VegetablesAndFruits"));
            try   
            {
                // Открытие соединения
                conn.Open();
                Console.WriteLine("Подключение к базе данных успешно!");
                Console.WriteLine("Choose opption:" +
               "\n0) Show all info" +
               "\n1) Show all names of fruits and vegetables" +
               "\n2) Show all colors" +
               "\n3) Show max coloricity" +
               "\n4) Show min coloricity" +
               "\n5) Show average coloricity" +

               "\n6) Show count of vegetables" +
               "\n7) Show count of fruits" +
               "\n8) Show count of fruits and vegetables by color" +
               "\n9) Show сount of vegetables and fruits by every color" +
               "\n10) Show vegetables and fruits by coloricity lower than chosen" +
               "\n11) Show vegetables and fruits by coloricity upper than chosen" +
               "\n12) Show vegetables and fruits by coloricity equals by chosen" +
               "\n13) Show vegetables and fruits whitch are red or yellow" +
               "\n14) Close connection");

                input = Convert.ToInt32(Console.ReadLine());
                Console.Clear();
                switch (input)
                {
                    case 0:
                        ShowFunctions.ShowAllInfo(conn);
                        break;
                    case 1:
                        ShowFunctions.ShowAllNames(conn);
                        break;
                    case 2:
                        ShowFunctions.ShowAllColors(conn);
                        break;
                    case 3:
                        ShowFunctions.ShowMaxColoricity(conn);
                        break;
                    case 4:
                        ShowFunctions.ShowMinColoricity(conn);
                        break;
                    case 5:
                        ShowFunctions.ShowAverageColoricity(conn);
                        break;

                    case 6:
                        ShowFunctions.ShowCountOfVegetables(conn);
                        break;
                    case 7:
                        ShowFunctions.ShowCountOfFruits(conn);
                        break;
                    case 8:
                        ShowFunctions.ShowCountOfVegetablesAndFruitsByColor(conn);
                        break;
                    case 9:
                        ShowFunctions.ShowCountOfVegetablesAndFruitsByEveryColor(conn);
                        break;
                    case 10:
                        ShowFunctions.ShowVegetablesAndFruitsByColoricityLowerThanChosen(conn);
                        break;
                    case 11:
                        ShowFunctions.ShowVegetablesAndFruitsByColoricityUpperThanChosen(conn);
                        break;
                    case 12:
                        ShowFunctions.ShowVegetablesAndFruitsByColoricityEqualsByChosen(conn);
                        break;
                    case 13:
                        ShowFunctions.ShowVegetablesAndFruitsWhitchAreRedOrYellow(conn);
                        break;
                    case 14:
                        conn.Close();
                        if (conn.State == System.Data.ConnectionState.Closed) Console.WriteLine("Lucky disconnection!");
                        else Console.WriteLine("Unlucky disconnection!");
                        break;
                    default:
                        Console.WriteLine("Error! You should choose 0 to 14! ");
                        break;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка при подключении к базе данных: " + ex.Message);
            }
            finally
            {
                // Закрытие соединения
                conn.Close();
                Console.WriteLine("Отключение от базы данных выполнено успешно!");
            }

            Console.ReadKey();
        }
    }
}
