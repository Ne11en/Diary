using System;
using Npgsql;

namespace Diary;

public static class DatabaseRequests
{
    /// <summary>
    /// Вывод ВСЕЙ ИНФОРМАЦИИ
    /// </summary>
    ///
    ///
    public static string user = "";
    public static void GetAllInfo()
    {
        var querySql = $@"SELECT * FROM Ежедневник WHERE Пользователь = '{user}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(
                $"Пользователь: {reader[0]} Id задачи: {reader[1]} Название задачи: {reader[2]} Описание задачи: {reader[3]} Дата: {reader[4]}");
        }
    }

    /// <summary>
    /// Метод UpcomingTasks
    /// отправляет запрос в БД на ПОЛУЧЕНИЕ ПРЕДСТОЯЩИХ ЗАДАЧ
    /// выводит в консоль информацию о ПРЕДСТОЯЩИХ ЗАДАЧАХ
    /// </summary>
    public static void UpcomingTasks()
    {
        DateTime date = DateTime.Now;
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");

        var querySql = $@"SELECT Название_задачи, Описание_задачи, Дата
                      FROM Ежедневник
                      WHERE Дата >= '{formattedDate}' AND Пользователь = '{user}'";

        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Название задачи: {reader[0]} Описание задачи: {reader[1]} Дата: {reader[2]}");
        }
    }

    /// <summary>
    /// Метод UpcomingTasks
    /// отправляет запрос в БД на ПОЛУЧЕНИЕ ПРОШЕДШИХ ЗАДАЧ
    /// выводит в консоль информацию о ПРОШЕДШИХ ЗАДАЧАХ
    /// </summary>
    public static void PastTasks()
    {
        DateTime date = DateTime.Now;
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");

        var querySql = $@"SELECT Название_задачи, Описание_задачи, Дата
                      FROM Ежедневник
                      WHERE Дата < '{formattedDate}' AND Пользователь = '{user}'";

        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Название задачи: {reader[0]} Описание задачи: {reader[1]} Дата: {reader[2]}");
        }
    }

    /// <summary>
    /// Метод TodayTasks
    /// отправляет запрос в БД на ПОЛУЧЕНИЕ ЗАДАЧ НА СЕГОДНЯ
    /// выводит в консоль информацию о ЗАДАЧАХ НА СЕГОДНЯ
    /// </summary>
    public static void TodayTasks()  
    {
        DateTime date = DateTime.Now;
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");

        var querySql = $@"SELECT Название_задачи, Описание_задачи, Дата
                      FROM Ежедневник
                      WHERE Дата = '{formattedDate}' AND Пользователь = '{user}'";

        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Название задачи: {reader[0]} Описание задачи: {reader[1]} Дата: {reader[2]}");
        }
    }

    /// <summary>
    /// Метод TomorrowTasks
    /// отправляет запрос в БД на ПОЛУЧЕНИЕ ЗАДАЧ НА ЗАВТРА
    /// выводит в консоль информацию о ЗАДАЧАХ НА ЗАВТРА
    /// </summary>
    public static void TomorrowTasks()
    {
        DateTime date = DateTime.Now.AddDays(1);
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");

        var querySql = $@"SELECT Название_задачи, Описание_задачи, Дата
                      FROM Ежедневник
                      WHERE Дата = '{formattedDate}' AND Пользователь = '{user}'";

        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Название задачи: {reader[0]} Описание задачи: {reader[1]} Дата: {reader[2]}");
        }
    }

    /// <summary>
    /// Метод WeekTasks
    /// отправляет запрос в БД на ПОЛУЧЕНИЕ ЗАДАЧ НА НЕДЕЛЮ
    /// выводит в консоль информацию о ЗАДАЧАХ НА НЕДЕЛЮ
    /// </summary>
    public static void WeekTasks()
    {
        string formattedDate = "";
        int weekConvert = (int)DateTime.Today.DayOfWeek;
        if (weekConvert == 0)
        {
            weekConvert = 7;
        }

        formattedDate = DateTime.Today.ToString("yyyy-MM-dd HH:mm:ss");
        string formattedDateWeekConvert = DateTime.Today.AddDays(7 - weekConvert).ToString("yyyy-MM-dd HH:mm:ss");

        var querySql = $@"SELECT Название_задачи, Описание_задачи, Дата
                      FROM Ежедневник
                      WHERE Дата >= '{formattedDate}' AND Дата <= '{formattedDateWeekConvert}' AND Пользователь = '{user}'";

        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"Название задачи: {reader[0]} Описание задачи: {reader[1]} Дата: {reader[2]}");
        }
    }
    ///<summary>
    /// Добавление ЗАДАЧИ
    /// </summary>
    /// <param name="Название_задачи"> </param>
    /// <param name="Описание_задачи"> </param>
    /// <param name="Дата"> </param>
    public static void AddTasks()
    {
        DateTime date = new DateTime();
        string formattedDate = "";
        
        Console.WriteLine("Введите название новой задачи: ");
        string nameTask = Console.ReadLine();
        
        Console.WriteLine("Введите описание новой задачи: ");
        string descTask = Console.ReadLine();
        
        Console.WriteLine("Введите дату, до которой нужно выцполнить задачу (в формате гггг-мм-дд): ");
        try
        {
            date = DateTime.Parse(Console.ReadLine());
            formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch
        {
            Console.WriteLine("Неверный формат даты");
            return;
        }
        
        var querySql =
            $"INSERT INTO Ежедневник(Пользователь, Название_задачи, Описание_задачи, Дата) VALUES ('{user}', '{nameTask}', '{descTask}', '{formattedDate}')";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// УДАЛЕНИЕ ЗАДАЧИ    /// </summary>
    public static void DeleteTasks(int idTask)
    {
        var querySql = $"DELETE FROM Ежедневник WHERE id_задачи = idTask";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    /// <summary>
    /// РЕДАКТИРОВАНИЕ ЗАДАЧИ
    /// </summary>
    public static void UpdateTask(int id)
    {
        DateTime date = new DateTime();
        string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        
        Console.WriteLine("Введите заголовок ");
        string title = Console.ReadLine();
        Console.WriteLine("Введите задачу ");
        string description = Console.ReadLine();
        Console.WriteLine("Введите дату в формате гггг-мм-дд ");
        try
        {
            date = DateTime.Parse(Console.ReadLine());
            formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss");
        }
        catch
        {
            Console.WriteLine("Неверный формат даты");
            return;
        }

        var querySql =
            $"UPDATE Ежедневник SET Название_задачи = '{title}', Описание_задачи = '{description}', Дата = '{formattedDate}' WHERE id_задачи = {id} AND Пользователь = '{user}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
    }

    public static bool Login()
    {
        string login; 
        string password;
        
        Console.Write("Введите логин: ");
        login = Console.ReadLine();

        Console.Write("Введите пароль: ");
        password = Console.ReadLine();

        var querySql =
            $"SELECT COUNT(*) FROM Пользователи WHERE Логин = '{login}' AND Пароль = '{password}'";
        using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
        cmd.ExecuteNonQuery();
        
        cmd.Parameters.AddWithValue("Логин", login);
        cmd.Parameters.AddWithValue("Пароль", password);
        int count = Convert.ToInt32(cmd.ExecuteScalar());

        if (count > 0)
        {
            Console.WriteLine("Вход выполнен успешно.");
            user = login;
            return true;
        }
        
        else
        {
            Console.WriteLine("Неверное имя пользователя или пароль.");
            return false;
        }
    }

    public static void Register()
    {
        
        static bool CheckIfUserExists(string login)
        {
            {
                var querySql = $"SELECT COUNT(*) FROM Пользователи WHERE Логин = '{login}'";
                using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
                cmd.Parameters.AddWithValue("Логин", login);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0;
            }
        }
        
        
        string login;

        Console.Write("Введите логин: ");
        login = Console.ReadLine();

        if (CheckIfUserExists(login))
        {
            Console.WriteLine("Такой пользователь уже существует, выберите другое имя.");
            return;
        }
        else
        {
            string password;

            Console.Write("Введите пароль: ");
            password = Console.ReadLine();
            
            var querySql =
                $"INSERT INTO Пользователи(Логин, Пароль) VALUES ('{login}', '{password}') ";
                    using var cmd = new NpgsqlCommand(querySql, DatabaseService.GetSqlConnection());
                    cmd.ExecuteNonQuery();
        }
    }
    
}