using Xunit;
using Bank;
using System;

namespace Bank.Tests
{
    public class KontoLimitTests
    {
        [Fact]
        public void KontoLimit_InicjalizacjaPoprawna()
        {
            // Arrange & Act
            var konto = new KontoLimit("Marat Iskhakov", 1000, 500);

            // Assert
            Assert.Equal("Marat Iskhakov", konto.Nazwa);
            Assert.Equal(1000, konto.Bilans);
            Assert.False(konto.CzyZablokowane);
            Assert.Equal(500, konto.LimitDebetowy);
        }

        [Fact]
        public void Wyplata_WRamachLimituDebetowego_PowodujeBlokade()
        {
            // Arrange
            var konto = new KontoLimit("Marat Iskhakov", 500, 300);

            // Act
            konto.Wyplata(700); // Wypłata 200 PLN poniżej zera (limit 300)

            // Assert
            Assert.Equal(500 - 700, konto.Bilans);
            Assert.True(konto.CzyZablokowane);
        }

        [Fact]
        public void Wyplata_PowyzejLimituDebetowego_ThrowsException()
        {
            // Arrange
            var konto = new KontoLimit("Marat Iskhakov", 200, 100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(400));
        }

        [Fact]
        public void Wplata_OdblokowujeKonto()
        {
            // Arrange
            var konto = new KontoLimit("Marat Iskhakov", 500, 300);
            konto.Wyplata(700); // Zejście na debet

            // Act
            konto.Wplata(300); // Powrót do zera

            // Assert
            Assert.False(konto.CzyZablokowane);
        }
    }
}