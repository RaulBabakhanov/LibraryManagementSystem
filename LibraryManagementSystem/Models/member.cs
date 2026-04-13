using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class member
    {
        public int Id { get; set; }          // Üyenin benzersiz numarası
        public string Name { get; set; }     // Üyenin adı soyadı
        public string Email { get; set; }    // Üyenin e-postası
    }
}
