using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Android.Graphics.ColorSpace;

namespace JustChores.MobileApp.Models
{
    public class Chore
    {

        public int Id { get; set; }

        public string Title { get; set; }

        public int Frequency { get; set; }

        public DateTime? DueOn { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public FrequencyType FrequencyType { get; set; }


        public string GetFrequencyTypeToDisplay()
        {
            var display = FrequencyType.ToString().ToLower();
            if (Frequency > 1)
                display += "s";

            return display;
        }

        public string GetSummary()
        {
            var dueOn = DueOn ?? DateTime.Now;

            string summary = FrequencyType switch
            {
                FrequencyType.Day => $"{GetFrequencyString()}{(Frequency > 1 ? " " : null)}day{(Frequency > 2 ? "s" : null)}",
                FrequencyType.Week => $"{GetFrequencyString()} {dueOn.DayOfWeek.ToString().ToLowerInvariant()}",
                FrequencyType.Month => $"{GetWithOrdinal(dueOn.Day)} of {GetFrequencyString()} month{GetFrequencyPlural()}",
                FrequencyType.Year => $"{GetWithOrdinal(dueOn.Day)} {dueOn:MMMM} {GetFrequencyString()} year{GetFrequencyPlural()}",
                _ => null,
            };
            return summary;
        }

        string GetFrequencyPlural() => Frequency > 1 ? "s" : null;

        string GetFrequencyString() => Frequency switch
        {
            1 => "every",
            2 => "every other",
            _ => $"every {(FrequencyType == FrequencyType.Week ? GetWithOrdinal(Frequency) : Frequency)}"
        };

        private static string GetWithOrdinal(int number) => $"{number}<sup>{GetOrdinalSuffix(number)}</sup>";

        private static string GetOrdinalSuffix(int number)
        {
            if (number % 100 >= 11 && number % 100 <= 13) return "th";

            return (number % 10) switch
            {
                1 => "st",
                2 => "nd",
                3 => "rd",
                _ => "th",
            };
        }

    }
}
