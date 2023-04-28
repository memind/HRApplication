using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKApplication.Domain.Entites
{
    public class Company 
    {
        //Id guid olabilir??
        public int Id { get; set; }
        public string CompanyName { get; set; }
   
        public string CompanyEmail { get; set;}

        public string CompanyDirector { get; set; }

        //To do: site yöneticisi sınıfından al string yerine
        public string CompanyPhoneNumber { get; set; }

        public string CompanySector { get; set; }

        public int NumberOfEmployees { get; set; }





    }
}
