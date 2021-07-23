using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Szkolenie.Testy
{

    public class Wynik
    {
        public int Dane { get; set; }
        public string Blad { get; internal set; }
    }

    public interface IOperacje
    {
        int Dodaj(int p1, int p2);
        Wynik Odejmij(int p1, int p2);
        float Podziel(int p1, int p2);
    }

    public interface IOdejmowanie
    {
        public int Stala { get; set; }
        Wynik Odejmij(int p1, int p2);
    }

    public class Dodawanie : IOperacje
    {
        private IOdejmowanie _odejmowanie;

        public Dodawanie(IOdejmowanie odejmowanie)
        {
            _odejmowanie = odejmowanie;
        }

        public int Dodaj(int p1, int p2)
        {
            return p1 + p2;
        }

        public Wynik Odejmij(int p1, int p2)
        {
            try
            {
                return _odejmowanie.Odejmij(p1 + _odejmowanie.Stala, p2);
            }
            catch (Exception e)
            {
                return new Wynik() { Dane = 0, Blad = e.Message };
            }
        }

        public float Podziel(int p1, int p2)
        {
            return (float)p1 / p2;
        }
    }

    [TestClass]
    public class MyTestClass
    {
        private Mock<IOdejmowanie> _odejmowanie;
        private Dodawanie _dodawanie;

        [TestInitialize]
        public void Init()
        {
            _odejmowanie = new Mock<IOdejmowanie>();
            _dodawanie = new Dodawanie(_odejmowanie.Object);
        }

        [TestMethod]
        public void Test1()
        {
            Assert.AreEqual(4, _dodawanie.Dodaj(1, 3));
            Assert.AreEqual(4, _dodawanie.Dodaj(2, 2));
            Assert.AreEqual(4, _dodawanie.Dodaj(3, 1));
            Assert.AreEqual(4, _dodawanie.Dodaj(4, 0));
            Assert.AreEqual(4, _dodawanie.Dodaj(-1, 5));
        }

        [TestMethod]
        public void UzywaOdejmowania()
        {
            var dto = new Wynik();
            var wyniki = new List<int>();
            _odejmowanie.Setup(m => m.Odejmij(It.Is<int>(x => x > 0), It.IsAny<int>())).
                Callback<int, int>((p1, p2) =>
                {
                    wyniki.Add(p1);
                    wyniki.Add(p2);
                });

            _dodawanie.Odejmij(3, 2);
            _dodawanie.Odejmij(5, 6);
            _dodawanie.Odejmij(-1, 6);
            Assert.AreEqual(4, wyniki.Count);
            Assert.AreEqual(3, wyniki[0]);
            Assert.AreEqual(2, wyniki[1]);
            Assert.AreEqual(5, wyniki[2]);
            Assert.AreEqual(6, wyniki[3]);
        }

        [TestMethod]
        public void ZwracaWłaściwość()
        {
            _odejmowanie.SetupGet(p => p.Stala).Returns(3);
            var dto = new Wynik();
            var wyniki = new List<int>();
            _dodawanie.Odejmij(0, 0);
            _odejmowanie.Verify(m => m.Odejmij(3, 0));
        }

        [TestMethod]
        public void ObsługujeWyjątek()
        {
            _odejmowanie.Setup(m => m.Odejmij(3, 5)).Throws(new Exception("Błąd"));
            var wynik = _dodawanie.Odejmij(3, 5);
            Assert.AreEqual(0, wynik.Dane);
            Assert.AreEqual("Błąd", wynik.Blad);
        }

        [TestMethod]
        public void Dzielenie()
        {
            var wynik = _dodawanie.Podziel(3, 5);
            Assert.AreEqual(3.0 / 5.0, wynik);
        }

        [TestMethod]
        //[ExpectedException(typeof(ArithmeticException), AllowDerivedTypes = true)]
        public void DzieleniePrzezZero()
        {
            try
            {
                var wynik = _dodawanie.Podziel(3, 0);
                Assert.Fail($"Jest wynik:{wynik}");
            }
            catch (Exception e)
            {
                Assert.AreEqual("sdsd", e.Message);
            }
        }

        private void SpodziewanyWyjatek(Action action, string Komunikat)
        {
            try
            {
                action();
                Assert.Fail("Nie było wyjątku....");
            }
            catch(Exception e)
            {
                Assert.AreEqual(Komunikat, e.Message);
            }
        }

        [TestMethod]
        public void MyTestMethod()
        {
            SpodziewanyWyjatek(
                () => _dodawanie.Podziel(0, 0), 
                "Dzielenie przez zero.");
        }
    }
}
