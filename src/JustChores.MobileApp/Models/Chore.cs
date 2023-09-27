using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
