using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DrinkWater.Models;
using System.Text.Json;



void drinkWater(User? user)
{

    if (user == null) throw new ArgumentNullException(nameof(user));
    if (int.TryParse(Console.ReadLine(), out var litr))
    {
        user.Current.Litr += litr;

    }
    else
        throw new FormatException("Invalid input");
}


void ShowHistory(User? user)
{
    if (user is null) throw new ArgumentNullException(nameof(user));

    if (user.History == null)
    {
        Console.WriteLine("History is not initialized.");
        return;
    }
    
    foreach (var info in user.History)
        {
            Console.WriteLine($"{info.Date} : {info.Litr} litr ");
        }
        Console.WriteLine("press [enter] to exit");
        Console.WriteLine();
    
}



void EndDate(User? user)
{
    if (user is null) throw new ArgumentNullException(nameof(user));

    user.History.Add(user.Current);
    user.Current = user.Current.Clone();
    user.Current.Date = user.Current.Date.AddDays(1);
    user.Current.Litr = 0;


    var json = JsonSerializer.Serialize(user);
    File.WriteAllText("user.json", json);



}

void AverageLitrForDay(User? user)
{
    var result = user.Weight / 20;
    Console.WriteLine($"siz gundelik {result} litr su icmelisiniz ");
}











string fileName = "user.json";
User? user = null;
if (File.Exists(fileName))
{
    var json = File.ReadAllText(fileName);
    user = JsonSerializer.Deserialize<User>(json);


}
else
{
    Console.Write("Ad daxil edin :");
    string? name = Console.ReadLine();
    Console.Write("Cekinizi daxil edin : ");
    var check = double.TryParse(Console.ReadLine(), out double weight);

    if (!string.IsNullOrWhiteSpace(name) && check)
    {
        var date = DateTime.Now;
        user = new User
        {
            Name = name,
            Weight = weight,
            Current = new DateInfo

            {
                Date = new DateOnly(date.Year, date.Month, date.Day),
                Litr = 0

            } // const- u invoke etmek ucun heap de yer vermishik, eslinde stackda yaranir 


        };
    }
}

while (true)
{
    Console.Clear();
    AverageLitrForDay(user);
    Console.WriteLine(" 1 - su icdim \n 2 - Tarixce \n 3 - Gunu bitir");
    int ch = int.Parse(Console.ReadLine()!);

    switch (ch)
    {

        case 1:
            try
            {
                Console.Clear();
                drinkWater(user);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);

            }
            break;

        case 2:
            try
            {
                Console.Clear();
                ShowHistory(user);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);

            }
            break;
        case 3:
            try
            {
                Console.Clear();
                EndDate(user);
                ShowHistory(user);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Thread.Sleep(1000);

            }
            break;
            default:
            Console.WriteLine("invalid operations");
            break;

    
    }

}

