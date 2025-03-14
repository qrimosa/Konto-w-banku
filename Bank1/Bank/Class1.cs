using System;

namespace Bank
{
    class Class1
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SYMULACJA DZIAŁANIA KONT BANKOWYCH\n");

            // 🔹 Konto standardowe
            Console.WriteLine("🔹 Tworzenie standardowego konta:");
            Konto kontoStandard = new Konto("Marat Iskhakov", 1000);
            Console.WriteLine($"Konto {kontoStandard.Nazwa}, Bilans: {kontoStandard.Bilans}\n");

            Console.WriteLine("🔹 Wpłata 500...");
            kontoStandard.Wplata(500);
            Console.WriteLine($"Nowy bilans: {kontoStandard.Bilans}\n");

            Console.WriteLine("🔹 Próba wypłaty 2000 (powinna się nie udać)...");
            try
            {
                kontoStandard.Wyplata(2000);
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Błąd: {e.Message}\n");
            }

            Console.WriteLine("🔹 Wypłata 700...");
            kontoStandard.Wyplata(700);
            Console.WriteLine($"Nowy bilans: {kontoStandard.Bilans}\n");

            // 🔹 KontoPlus
            Console.WriteLine("🔹 Tworzenie KontoPlus z limitem debetowym 500:");
            KontoPlus kontoPlus = new KontoPlus("Yaroslav Furmanov", 1000, 500);
            Console.WriteLine($"Konto {kontoPlus.Nazwa}, Bilans: {kontoPlus.Bilans}, Limit Debetowy: {kontoPlus.LimitDebetowy}\n");

            Console.WriteLine("🔹 Wypłata 1200 (przekroczy bilans, ale zmieści się w debecie)...");
            kontoPlus.Wyplata(1200);
            Console.WriteLine($"Nowy bilans: {kontoPlus.Bilans}, Zablokowane: {kontoPlus.CzyZablokowane}\n");

            Console.WriteLine("🔹 Próba kolejnej wypłaty 100 (nie powinna się udać)...");
            try
            {
                kontoPlus.Wyplata(100);
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Błąd: {e.Message}\n");
            }

            Console.WriteLine("🔹 Wpłata 500 (powinna odblokować konto)...");
            kontoPlus.Wplata(500);
            Console.WriteLine($"Nowy bilans: {kontoPlus.Bilans}, Zablokowane: {kontoPlus.CzyZablokowane}\n");

            // 🔹 KontoLimit (delegacja)
            Console.WriteLine("🔹 Tworzenie KontoLimit z limitem debetowym 300:");
            KontoLimit kontoLimit = new KontoLimit("Mariia Synoha", 800, 300);
            Console.WriteLine($"Konto {kontoLimit.Nazwa}, Bilans: {kontoLimit.Bilans}\n");

            Console.WriteLine("🔹 Wypłata 1000 (wykorzysta debet)...");
            kontoLimit.Wyplata(1000);
            Console.WriteLine($"Nowy bilans: {kontoLimit.Bilans}, Zablokowane: {kontoLimit.CzyZablokowane}\n");

            Console.WriteLine("🔹 Próba wypłaty 50 (nie powinna się udać)...");
            try
            {
                kontoLimit.Wyplata(50);
            }
            catch (Exception e)
            {
                Console.WriteLine($"❌ Błąd: {e.Message}\n");
            }

            Console.WriteLine("🔹 Wpłata 400 (odblokowanie konta)...");
            kontoLimit.Wplata(400);
            Console.WriteLine($"Nowy bilans: {kontoLimit.Bilans}, Zablokowane: {kontoLimit.CzyZablokowane}\n");

            Console.WriteLine("\n✅ Symulacja zakończona.");
        }
    }
}