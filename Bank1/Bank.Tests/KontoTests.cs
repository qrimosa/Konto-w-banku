using Xunit;
using Bank;
using System;

namespace Bank.Tests
{
    public class KontoTests
    {
        [Fact]
        public void Konto_InicjalizacjaPoprawna()
        {
            // Arrange & Act
            var konto = new Konto("Marat Iskhakov", 1000);

            // Assert
            Assert.Equal("Marat Iskhakov", konto.Nazwa);
            Assert.Equal(1000, konto.Bilans);
            Assert.False(konto.CzyZablokowane);
        }

        [Fact]
        public void Wplata_DodajeSrodki()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 500);

            // Act
            konto.Wplata(200);

            // Assert
            Assert.Equal(700, konto.Bilans);
        }

        [Fact]
        public void Wplata_NegatywnaKwota_ThrowsException()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 500);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => konto.Wplata(-100));
        }

        [Fact]
        public void Wyplata_PrawidlowaKwota_OdejmowanieDziala()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 500);

            // Act
            konto.Wyplata(200);

            // Assert
            Assert.Equal(300, konto.Bilans);
        }

        [Fact]
        public void Wyplata_WiekszaNizBilans_ThrowsException()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 300);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(500));
        }

        [Fact]
        public void Wyplata_ZablokowaneKonto_ThrowsException()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 500);
            konto.BlokujKonto();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => konto.Wyplata(100));
        }

        [Fact]
        public void Blokuj_Odblokuj_KontoDziala()
        {
            // Arrange
            var konto = new Konto("Marat Iskhakov", 500);

            // Act
            konto.BlokujKonto();
            bool stanPoBlokadzie = konto.CzyZablokowane;

            konto.OdblokujKonto();
            bool stanPoOdblokowaniu = konto.CzyZablokowane;

            // Assert
            Assert.True(stanPoBlokadzie);
            Assert.False(stanPoOdblokowaniu);
        }
    }
}