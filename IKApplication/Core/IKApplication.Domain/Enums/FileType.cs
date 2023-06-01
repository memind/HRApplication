using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IKApplication.Domain.Enums
{
    public enum FileType
    {
        [Display(Name = "PDF documnet")]
        pdf = 1,
        [Display(Name = "Excel Worksheet")]
        xls = 2
    }
}
