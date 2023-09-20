using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BulkInsertData.Models
{
    public class PatientInfo
    {
        [Key]
        public int PId{ get; set; }
        public string PName { get; set; }
        public string Address { get; set; }
        public string TestName { get; set; }
        public string TestResult { get; set; }

      
        public static PatientInfo FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            PatientInfo oPatientInfoCSV = new PatientInfo();
            oPatientInfoCSV.PName = values[0].ToString();
            oPatientInfoCSV.Address = values[1].ToString();
            oPatientInfoCSV.TestName = values[2].ToString();
            oPatientInfoCSV.TestResult = values[3].ToString();
            return oPatientInfoCSV;
        }

    }
}
