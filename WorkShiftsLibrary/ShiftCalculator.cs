using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkShiftsConsole
{
    public class ShiftCalculator
    {
        private static List<Shift> sequence = new List<Shift> { Shift.Day, Shift.Day, Shift.Night, Shift.Night, Shift.Off, Shift.Off };
        public static Shift IntegerToShift(int intShift)
        {
            Shift shift;

            switch(intShift)
            {
                case 1:
                    shift = Shift.Day;
                    break;
                case 2:
                    shift = Shift.Night;
                    break;
                case 0:
                    shift = Shift.Off;
                    break;
                default:
                    throw new ArgumentException("Invalid shift integer");
            }
            return shift;
        }

        public static Dictionary<DateOnly, Shift> CalculateShifts(Shift today, Shift tomorrow)
        {
            int startIndex = sequence.IndexOf(today);
            if (sequence[startIndex + 1] != tomorrow) startIndex++;

            Dictionary<DateOnly, Shift> result = new Dictionary<DateOnly, Shift>();
            DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);

            for(int i = 0; i < 365; i++)
            {
                result.Add(currentDate, sequence[(startIndex + i) % sequence.Count]);
                currentDate = currentDate.AddDays(1);
            }

            return result;
        }
    }
}
