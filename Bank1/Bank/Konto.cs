namespace Bank;

public class Konto
{
    private string klient;
    private decimal bilans;
    private bool zablokowane = false;

    public Konto(string klient, decimal bilansNaStart = 0){
        this.klient = klient;
        this.bilans = bilansNaStart;
    }

    public string Nazwa => klient;
    public virtual decimal Bilans => bilans;
    public bool CzyZablokowane => zablokowane;

    public virtual void Wplata(decimal kwota)
    {
        if (kwota < 0){
            throw new ArgumentException("Kwota wplaty nie może być ujemna.");
        }
        bilans += kwota;
    }

    public virtual void Wyplata(decimal kwota)
    {
        if (kwota < 0){
            throw new ArgumentException("Kwota wyplaty nie może być ujemna.");
        }
        if (zablokowane){
            throw new InvalidOperationException("Konto jest zablokowane.");
        }
        bilans -= kwota;
    }

    public void BlokujKonto()
    {
        zablokowane = true;
    }
    public void OdblokujKonto()
    {
        zablokowane = false;
    }
}