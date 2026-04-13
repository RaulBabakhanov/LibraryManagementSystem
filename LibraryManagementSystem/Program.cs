using LibraryManagementSystem.Services;

var service = new LibraryService();

while (true)
{
    Console.WriteLine("\n===== KÜTÜPHANe YÖNETİM SİSTEMİ =====");
    Console.WriteLine("1. Kitap Ekle");
    Console.WriteLine("2. Kitapları Listele");
    Console.WriteLine("3. Kitap Sil");
    Console.WriteLine("4. Kitap Ara");
    Console.WriteLine("5. Üye Ekle");
    Console.WriteLine("6. Üyeleri Listele");
    Console.WriteLine("7. Üye Sil");
    Console.WriteLine("8. Kitap Ödünç Ver");
    Console.WriteLine("9. Kitap İade Al");
    Console.WriteLine("0. Çıkış");
    Console.Write("Seçiminiz: ");

    string secim = Console.ReadLine();

    if (secim == "1")
    {
        Console.Write("Kitap adı: "); string title = Console.ReadLine();
        Console.Write("Yazar: "); string author = Console.ReadLine();
        Console.Write("Tür: "); string genre = Console.ReadLine();
        service.AddBook(title, author, genre);
    }
    else if (secim == "2")
    {
        service.ListBooks();
    }
    else if (secim == "3")
    {
        Console.Write("Silinecek kitabın ID'si: ");
        int id = int.Parse(Console.ReadLine());
        service.DeleteBook(id);
    }
    else if (secim == "4")
    {
        Console.Write("Aranacak kelime: "); string keyword = Console.ReadLine();
        service.SearchBook(keyword);
    }
    else if (secim == "5")
    {
        Console.Write("Üye adı soyadı: "); string name = Console.ReadLine();
        Console.Write("E-posta: "); string email = Console.ReadLine();
        service.AddMember(name, email);
    }
    else if (secim == "6")
    {
        service.ListMembers();
    }
    else if (secim == "7")
    {
        Console.Write("Silinecek üyenin ID'si: ");
        int id = int.Parse(Console.ReadLine());
        service.DeleteMember(id);
    }
    else if (secim == "8")
    {
        Console.Write("Kitap ID'si: ");
        int bookId = int.Parse(Console.ReadLine());
        Console.Write("Üye ID'si: ");
        int memberId = int.Parse(Console.ReadLine());
        service.BorrowBook(bookId, memberId);
    }
    else if (secim == "9")
    {
        Console.Write("İade edilecek kitabın ID'si: ");
        int bookId = int.Parse(Console.ReadLine());
        service.ReturnBook(bookId);
    }
    else if (secim == "0")
    {
        Console.WriteLine("Çıkılıyor...");
        break;
    }
    else
    {
        Console.WriteLine("Geçersiz seçim.");
    }
}