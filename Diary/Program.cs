using Diary;

public class Program
{
    public static void Main(string[] args)
    {
        bool run = false;

        while (!run)
        {
            Console.WriteLine("Вы зарегестрированы?");
            string answer = Console.ReadLine();
            switch (answer)
                    {
                        case "да":
                            run = DatabaseRequests.Login();
                            break;
                        
                        case "нет":
                            DatabaseRequests.Register();
                            run = DatabaseRequests.Login();
                            break;
                        
                        default:
                            Console.WriteLine("Введена неверная команда.");
                            break;
                    }
        }
        

        Console.WriteLine(DateTime.Today);
        Console.WriteLine($"Панель выбора \n1 - Вывод всей информации \n2 - Предстоящие задачи \n3 - Прошедшие задачи \n4 - Задачи на сегодня \n5 - Задачи на завтра \n6 - Задачи на неделю \n7 - Добавление задачи \n8 - Удаление задачи \n9 - Редактирование задачи \nQ - Выход ");
        string bang = Console.ReadLine();
        // Цикл будет работать, пока не ввести 'Q'
        while (bang != "Q")
        {
            int idDriver;
            switch (bang)
            {
                case "1":
                    // ВСЯ ИНФОРМАЦИЯ
                    Console.WriteLine("Вся информация: \n");
                    DatabaseRequests.GetAllInfo();
                    Console.WriteLine();
                    break;
                case "2":
                    // ПРЕДСТОЯЩИЕ ЗАДАЧИ
                    Console.WriteLine("Предстоящие задачи: \n");
                    DatabaseRequests.UpcomingTasks();
                    Console.WriteLine();
                    break;
                case "3":
                    // ПРОШЕДШИЕ ЗАДАЧИ
                    Console.WriteLine("Прошедшие задачи: \n");
                    DatabaseRequests.PastTasks();
                    Console.WriteLine();
                    break;
                case "4":
                    // ЗАДАЧИ НА СЕГОДНЯ
                    Console.Write("Задачи на сегодня: \n");
                    DatabaseRequests.TodayTasks();
                    Console.WriteLine();
                    break;
                case "5":
                    // ЗАДАЧИ НА ЗАВТРА
                    Console.WriteLine("Задачи на завтра: \n");
                    DatabaseRequests.TomorrowTasks();
                    Console.WriteLine();
                    break;
                case "6":
                    // ЗАДАЧИ НА НЕДЕЛЮ
                    Console.WriteLine("Задачи на неделю: \n");
                    DatabaseRequests.WeekTasks();
                    Console.WriteLine();
                    break;
                case "7":
                    // ДОБАВЛЕНИЕ ЗАДАЧИ
                    DatabaseRequests.AddTasks();
                    Console.WriteLine();
                    break;
                case "8":
                    // УДАЛЕНИЕ ЗАДАЧИ
                    Console.Write("Введите id задачи, которую хотите удалить: \n ");
                    int delId = Int32.Parse(Console.ReadLine());
                    DatabaseRequests.DeleteTasks(delId);
                    Console.WriteLine();
                    break;
                case "9":
                    // РЕДАКТИРОВАНИЕ ЗАДАЧИ
                    Console.Write("Введите id задачи, которую хотите отредактировать: \n ");
                    int updId = Int32.Parse(Console.ReadLine());
                    DatabaseRequests.UpdateTask(updId);
                    Console.WriteLine();
                    break;
                default:
                    Console.WriteLine($"Введена неверная команда");
                    break;
            }

            bang = Console.ReadLine();
        }
    }
}