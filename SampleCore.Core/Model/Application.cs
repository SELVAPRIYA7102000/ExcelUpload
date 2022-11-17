using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Core.Model
{
    public class Application
    {
        public int PersonID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
       
        public string FullName { get; set; }
       
        public string SSLCMarks { get; set; }
        public string HSCMarks { get; set; }
        public string GraduationPercent { get; set; }
        public string? Skills { get; set; }
        public string? Interests { get; set; }
        public bool Is_Deleted { get; set; }
        public DateTime Created_Time_Stamp { get; set; }
        public DateTime Updated_Time_Stamp { get; set; }
        public string? ApplicantResume { get; set; }

  
        }
}
