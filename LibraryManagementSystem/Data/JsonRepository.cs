using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LibraryManagementSystem.Models;
using LibraryManagementSystem.Data;
namespace LibraryManagementSystem.Data
{
    public class JsonRepository
    {
        private string filePath = "library.json"; // Verinin kaydedileceği dosya
        public Library Load()
        {
            if (!File.Exists(filePath))
                return new Library();
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<Library>(json) ?? new Library();
        }
        public void Save(Library library)
        {
            string json = JsonSerializer.Serialize(library, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }
      
    }
    public class Library
    {
        public List<book> Books { get; set; } = new List<book>();
        public List<member> Members { get; set; } = new List<member>();
        public List<BorrowRecord> BorrowRecords { get; set; } = new List<BorrowRecord>();
    }
}
