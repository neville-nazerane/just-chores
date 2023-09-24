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

        public FrequencyType FrequencyType { get; set; }

    }
}
