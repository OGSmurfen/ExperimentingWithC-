namespace WorkShiftsConsole
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1 - ПЪРВА; 2 - ВТОРА; 0 - ПОЧИВКА");
            Console.WriteLine("");
            Console.WriteLine("Коя смяна си днеска?");
            var isParsed = int.TryParse(Console.ReadLine(), out int sh1);
            Console.WriteLine("А коя смяна си утре?");
            isParsed = int.TryParse(Console.ReadLine(), out int sh2) && isParsed;

            if(!isParsed) throw new ArgumentException("Invalid input");

            Shift todayShift = ShiftCalculator.IntegerToShift(sh1);
            Shift tomorrowShift = ShiftCalculator.IntegerToShift(sh2);

            ShiftCalculator.CalculateShifts(todayShift, tomorrowShift).ToList().ForEach(x => Console.WriteLine($"{x.Key} - {x.Value}"));

        }


    }
}
