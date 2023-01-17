using HolidayAlgo;

class Program
{
    public static void ResultGenerator(int year, int firstSat, int secondSat)
    {
        List<DateTime> MyCalendar = new List<DateTime>();
        List<Holiday> holidayList = new List<Holiday>();    

        // Initiate The value using parameters
        DateTime currDate = new DateTime(year, 1, 1);
        int valueCount = 0;

        // Inserting all entered year dates
        while (currDate <= new DateTime(year, 12, 31))
        {
            MyCalendar.Add(currDate);
            currDate = currDate.AddDays(1);
        }

        // Fetching saturdays
        var saturdayResult = MyCalendar.Where(x => x.DayOfWeek == DayOfWeek.Saturday)
                        .GroupBy(x => x.Month)
                        // Destructuring the data
                        .SelectMany(grp => grp.Select((d, counter) => new { Month = grp.Key, PosInMonth = counter + 1, Day = d }))
                        .Where(x => x.PosInMonth == firstSat || x.PosInMonth == secondSat)
                        .ToList();

        // Fetching Sundays
        var sundayResult = MyCalendar.Where(x => x.DayOfWeek == DayOfWeek.Sunday)
                       .GroupBy(x => x.Month)
                        // Destructuring the data
                       .SelectMany(grp => grp.Select((d, counter) => new { Month = grp.Key, PosInMonth = counter + 1, Day = d }))
                       .ToList();

        // Print Saturday data
        //Console.WriteLine("");
        //Console.WriteLine("-------------------------Saturday----------------------------");
        //Console.WriteLine("");
        foreach (var d in saturdayResult)
        {
            //Console.WriteLine("Month={0} Day={1} DayCount={2}", d.Month, d.Day.Date.ToShortDateString(), d.Day.DayOfYear);
            holidayList.Add( new Holiday { Date = d.Day.Date, DayOfYear = d.Day.DayOfYear, Month=d.Month, Day=d.Day.DayOfWeek.ToString() });
            valueCount++;
        }

        // Print Sunday data
        //Console.WriteLine("");
        //Console.WriteLine("--------------------------Sunday-----------------------------");
        //Console.WriteLine("");
        foreach (var d in sundayResult)
        {
            holidayList.Add(new Holiday { Date = d.Day.Date, DayOfYear = d.Day.DayOfYear, Month = d.Month, Day = d.Day.DayOfWeek.ToString() });
            //Console.WriteLine("Month={0} Day={1} DayCount={2}", d.Month, d.Day.Date.ToShortDateString(), d.Day.DayOfYear);
            valueCount++;
        }

        holidayList.OrderBy(x => x.Date);

        foreach (var d in holidayList)
        {
            Console.WriteLine("Month={0} Day={1} DayCount={2} Day={3}", d.Month, d.Date.ToShortDateString(), d.DayOfYear, d.Day);
            valueCount++;
        }

        // Print Total Number of Holidays
        Console.WriteLine("");
        Console.WriteLine("Total Number of Holidays = {0}", valueCount);

        Console.Read();
    }   

    static void Main(string[] args)
    {
        // Take Parameter Values
        Console.WriteLine("Enter Your Year: ");

        int year = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Your First Saturday Value: ");
        int firstSat = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Enter Your Second Saturday Value: ");
        int secondSat = Convert.ToInt32(Console.ReadLine());

        // Calls the resultGenerator function and pass the values that are entered by the user
        ResultGenerator(year, firstSat, secondSat);
    }
}