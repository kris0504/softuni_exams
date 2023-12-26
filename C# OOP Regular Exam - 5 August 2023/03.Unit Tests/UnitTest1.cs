using NUnit.Framework;

namespace VendingRetail.Tests
{
    public class Tests
    {

        [SetUp]
        public void Setup()
        {


        }
        [Test]
        public void CtorTest()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            int exp = 8;
            Assert.AreEqual(8, coffemat.ButtonsCount);
        }



        [Test]
        public void FillWaterTankMessage()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);

            string res = coffemat.FillWaterTank();

            Assert.AreEqual($"Water tank is filled with {100}ml", res);
        }

        [Test]
        public void FillWaterTankTankIsFull()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.FillWaterTank();

            string res = coffemat.FillWaterTank();

            Assert.AreEqual($"Water tank is already full!", res);
            Assert.AreEqual(100, coffemat.WaterCapacity);
        }

        [Test]
        public void AddDrinkAddedSuccessfully()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);

            bool res = coffemat.AddDrink("Espresso", 1.5);

            Assert.IsTrue(res);
        }

        [Test]
        public void DrinkAlreadyExists()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.AddDrink("Espresso", 1.5);

            bool res = coffemat.AddDrink("Espresso", 2.0);

            Assert.IsFalse(res);
           
        }

        [Test]
        public void BuyDrinkShouldMessage()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.AddDrink("Latte", 2.5);
            coffemat.FillWaterTank();

            string res = coffemat.BuyDrink("Latte");

            Assert.AreEqual($"Your bill is 2.50$", res);
        }

        [Test]
        public void BuyDrinkNotAvailable()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.FillWaterTank();

            string result = coffemat.BuyDrink("Mocha");

            Assert.AreEqual("Mocha is not available!", result);
        }

        [Test]
        public void BuyDrinkTankLow()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.AddDrink("Espresso", 1.5);

            string res = coffemat.BuyDrink("Espresso");

            Assert.AreEqual("CoffeeMat is out of water!", res);
           Assert.AreEqual(0, coffemat.Income);
            
        }

        [Test]
        public void CollectIncome()
        {
            CoffeeMat coffemat = new CoffeeMat(100, 8);
            coffemat.AddDrink("Espresso", 1.5);
            coffemat.FillWaterTank();
            coffemat.BuyDrink("Espresso");

            double collected = coffemat.CollectIncome();

            Assert.AreEqual(1.5, collected);
            Assert.AreEqual(0, coffemat.Income);
        }
    }
}