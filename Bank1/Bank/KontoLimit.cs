namespace Bank;

public class KontoLimit
{
    private Konto konto;
    private decimal jednorazowyLimitDebetowy;
    private bool wykorzystanoLimit = false;

    public KontoLimit(string klient, decimal bilansNaStart, decimal limitDebetowy)
    {
        if (limitDebetowy < 0)
            throw new ArgumentException("Limit debetowy nie może być ujemny.");

        konto = new Konto(klient, bilansNaStart);
        jednorazowyLimitDebetowy = limitDebetowy;
    }

    public string Nazwa => konto.Nazwa;
    public decimal Bilans => konto.Bilans;  
    public bool CzyZablokowane => konto.CzyZablokowane;

    public decimal LimitDebetowy
    {
        get => wykorzystanoLimit ? 0 : jednorazowyLimitDebetowy;
        set
        {
            if (value < 0)
                throw new ArgumentException("Limit debetowy nie może być ujemny.");
            if (konto.CzyZablokowane)
                throw new InvalidOperationException("Nie można zmieniać limitu dla zablokowanego konta.");

            jednorazowyLimitDebetowy = value;
        }
    }

    public void Wplata(decimal kwota)
    {
        if (kwota < 0)
            throw new ArgumentException("Kwota wpłaty nie może być ujemna.");

        konto.Wplata(kwota);

        if (konto.Bilans > 0)
        {
            konto.OdblokujKonto();
            wykorzystanoLimit = false;
        }
    }

    public void Wyplata(decimal kwota)
    {
        if (kwota < 0)
            throw new ArgumentException("Kwota wypłaty nie może być ujemna.");
        if (CzyZablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");

        decimal dostepneSrodki = konto.Bilans + (wykorzystanoLimit ? 0 : jednorazowyLimitDebetowy);

        if (kwota > dostepneSrodki)
            throw new InvalidOperationException("Nie masz wystarczającego salda na koncie.");

        if(kwota > konto.Bilans){
            konto.Wyplata(kwota);
            wykorzystanoLimit = true;
            konto.BlokujKonto();
        }
        else{
            konto.Wyplata(kwota);
        }
    }
}