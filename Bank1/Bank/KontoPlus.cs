namespace Bank;

public class KontoPlus : Konto
{
    private decimal jednorazowyLimitDebetowy;
    private bool wykorzystanoLimit = false;

    public KontoPlus(string klient, decimal bilansNaStart, decimal limitDebetowy)
        : base(klient, bilansNaStart)
    {
        if (limitDebetowy < 0)
            throw new ArgumentException("Limit debetowy nie może być ujemny.");

        this.jednorazowyLimitDebetowy = limitDebetowy;
    }

    public decimal LimitDebetowy
    {
        get => wykorzystanoLimit ? 0 : jednorazowyLimitDebetowy;
        set
        {
            if (value < 0)
                throw new ArgumentException("Limit debetowy nie może być ujemny.");
            if (CzyZablokowane)
                throw new InvalidOperationException("Nie można zmieniać limitu dla zablokowanego konta.");

            jednorazowyLimitDebetowy = value;
        }
    }

    public override decimal Bilans => base.Bilans;

    public override void Wyplata(decimal kwota)
    {
        if (kwota < 0)
            throw new ArgumentException("Kwota wypłaty nie może być ujemna.");
        if (CzyZablokowane)
            throw new InvalidOperationException("Konto jest zablokowane.");

        decimal dostepneSrodki = base.Bilans + (wykorzystanoLimit ? 0 : jednorazowyLimitDebetowy);

        if (kwota > dostepneSrodki)
            throw new InvalidOperationException("Nie masz wystarczającego salda na koncie.");

        if(kwota > base.Bilans){
            base.Wyplata(kwota);
            wykorzystanoLimit = true;
            BlokujKonto();
        }
        else{
            base.Wyplata(kwota);
        }
    }
        

    public override void Wplata(decimal kwota)
    {
        if (kwota < 0)
            throw new ArgumentException("Kwota wpłaty nie może być ujemna.");

        base.Wplata(kwota);

        if (base.Bilans >= 0)
        {
            OdblokujKonto();
            wykorzystanoLimit = false;
        }
    }
}