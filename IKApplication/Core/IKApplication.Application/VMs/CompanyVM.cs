using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Application.VMs
{
    public class CompanyVM
    {
        //guid ıd yapılacak
        public string CompanyName { get; set; }

        public string CompanyEmail { get; set; }

        public string CompanyDirector { get; set; }

        public string CompanyPhoneNumber { get; set; }

        public string CompanySector { get; set; }

        public int NumberOfEmployees { get; set; }
    }
}
