using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibraryManagementSystem.Data;
using LibraryManagementSystem.Models;

namespace LibraryManagementSystem.Services
{
    public class LibraryService
    {
        private Library _library;           // Bellekteki tüm veri
        private JsonRepository _repo;       // Dosyaya kaydetmek için
        private int _nextBookId = 1;        // Yeni kitaba verilecek ID
        private int _nextMemberId = 1;      // Yeni üyeye verilecek ID
        private int _nextRecordId = 1;      // Yeni kayda verilecek 

        public LibraryService()
        {
            _repo = new JsonRepository();
            _library = _repo.Load();        // Program başlarken dosyadan yükle

            // Mevcut verilerdeki en yüksek ID'yi bul, üstünden devam et
            if (_library.Books.Count > 0)
                _nextBookId = _library.Books.Max(b => b.Id) + 1;
            if (_library.Members.Count > 0)
                _nextMemberId = _library.Members.Max(m => m.Id) + 1;
            if (_library.BorrowRecords.Count > 0)
                _nextRecordId = _library.BorrowRecords.Max(r => r.Id) + 1;
        }
        // ── KİTAP İŞLEMLERİ ──

        public void AddBook(string title, string author, string genre)
        {
            var book = new book { Id = _nextBookId++, Title = title, Author = author, Genre = genre };
            _library.Books.Add(book);
            _repo.Save(_library);
            Console.WriteLine($"Kitap eklendi: {book.Title} (ID: {book.Id})");
        }
        public void ListBooks()
        {
            if (_library.Books.Count == 0) { Console.WriteLine("Hiç kitap yok."); return; }
            foreach (var b in _library.Books)
                Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author} | {b.Genre} | {(b.IsAvailable ? "Rafta" : "Ödünçte")}");
        }
        public void DeleteBook(int id)
        {
            var book = _library.Books.FirstOrDefault(b => b.Id == id);
            if (book == null) { Console.WriteLine("Kitap bulunamadı."); return; }
            _library.Books.Remove(book);
            _repo.Save(_library);
            Console.WriteLine($"Kitap silindi: {book.Title}");
        }
        public void SearchBook(string keyword)
        {
            var results = _library.Books.Where(b => b.Title.Contains(keyword, StringComparison.OrdinalIgnoreCase)).ToList();
            if (results.Count == 0) { Console.WriteLine("Sonuç bulunamadı."); return; }
            foreach (var b in results)
                Console.WriteLine($"[{b.Id}] {b.Title} - {b.Author}");
        }
        // ── ÜYE İŞLEMLERİ ──

        public void AddMember(string name, string email)
        {
            var member = new member { Id = _nextMemberId++, Name = name, Email = email };
            _library.Members.Add(member);
            _repo.Save(_library);
            Console.WriteLine($"Üye eklendi: {member.Name} (ID: {member.Id})");
        }
        public void ListMembers()
        {
            if (_library.Members.Count == 0) { Console.WriteLine("Hiç üye yok."); return; }
            foreach (var m in _library.Members)
                Console.WriteLine($"[{m.Id}] {m.Name} - {m.Email}");
        }
        public void DeleteMember(int id)
        {
            var member = _library.Members.FirstOrDefault(m => m.Id == id);
            if (member == null) { Console.WriteLine("Üye bulunamadı."); return; }
            _library.Members.Remove(member);
            _repo.Save(_library);
            Console.WriteLine($"Üye silindi: {member.Name}");
        }
        // ── ÖDÜNÇ İŞLEMLERİ ──

        public void BorrowBook(int bookId, int memberId)
        {
            var book = _library.Books.FirstOrDefault(b => b.Id == bookId);
            var member = _library.Members.FirstOrDefault(m => m.Id == memberId);

            if (book == null) { Console.WriteLine("Kitap bulunamadı."); return; }
            if (member == null) { Console.WriteLine("Üye bulunamadı."); return; }
            if (!book.IsAvailable) { Console.WriteLine("Kitap şu an ödünçte."); return; }

            book.IsAvailable = false;
            var record = new BorrowRecord
            {
                Id = _nextRecordId++,
                BookId = bookId,
                MemberId = memberId,
                BorrowDate = DateTime.Now,
                DueDate = DateTime.Now.AddDays(14) // 2 hafta iade süresi
            };
            _library.BorrowRecords.Add(record);
            _repo.Save(_library);
            Console.WriteLine($"'{book.Title}' kitabı '{member.Name}' üyesine verildi. İade tarihi: {record.DueDate:dd.MM.yyyy}");
        }
        public void ReturnBook(int bookId)
        {
            var book = _library.Books.FirstOrDefault(b => b.Id == bookId);
            if (book == null) { Console.WriteLine("Kitap bulunamadı."); return; }
            if (book.IsAvailable) { Console.WriteLine("Bu kitap zaten rafta."); return; }

            var record = _library.BorrowRecords.FirstOrDefault(r => r.BookId == bookId && r.ReturnDate == null);
            if (record == null) { Console.WriteLine("Kayıt bulunamadı."); return; }

            record.ReturnDate = DateTime.Now;
            book.IsAvailable = true;

            // Gecikme kontrolü
            if (DateTime.Now > record.DueDate)
                Console.WriteLine($"Kitap gecikmeli iade edildi! {(DateTime.Now - record.DueDate).Days} gün gecikme.");
            else
                Console.WriteLine($"'{book.Title}' kitabı iade alındı.");

            _repo.Save(_library);
        }
    }   
}
