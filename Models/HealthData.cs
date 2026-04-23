using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StayFitz.Models
{
    public class HealthData
    {
        public List<HealthCategory> Categories { get; set; }
        public List<HealthArticle> Articles { get; set; }
    }
}
