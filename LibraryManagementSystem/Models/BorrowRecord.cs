using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Models
{
    public class BorrowRecord
    {
        public int Id { get; set; }              // Kaydın benzersiz numarası
        public int BookId { get; set; }          // Hangi kitap ödünç alındı
        public int MemberId { get; set; }        // Kim ödünç aldı
        public DateTime BorrowDate { get; set; } // Ne zaman alındı
        public DateTime DueDate { get; set; }    // Ne zamana kadar iade edilmeli
        public DateTime? ReturnDate { get; set; }// Ne zaman iade edildi (null = henüz iade edilmedi)
    }
}
