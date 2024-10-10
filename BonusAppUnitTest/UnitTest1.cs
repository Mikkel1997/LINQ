using BonusApp;

namespace BonusAppUnitTest
{

    [TestClass]
    public class UnitTest1
    {
        private Order order;

        [TestInitialize]
        public void InitializeTest()
        {
            order = new Order();
            order.AddProduct(new Product
            {
                Name = "Mælk",
                Value = 10.0,
                AvailableFrom = new DateTime(2024, 3, 1),
                AvailableTo = new DateTime(2024, 3, 5)
            });
            order.AddProduct(new Product
            {
                Name = "Smør",
                Value = 15.0,
                AvailableFrom = new DateTime(2024, 3, 3),
                AvailableTo = new DateTime(2024, 3, 4)
            });
            order.AddProduct(new Product
            {
                Name = "Pålæg",
                Value = 20.0,
                AvailableFrom = new DateTime(2024, 3, 4),
                AvailableTo = new DateTime(2024, 3, 7)
            });
        }

        [TestMethod]
        public void TenPercent_Test()
        {
            Assert.AreEqual(4.5, Bonuses.TenPercent(45.0));
            Assert.AreEqual(40.0, Bonuses.TenPercent(400.0));
        }

        [TestMethod]
        public void FlatTwoIfAMountMoreThanFive_Test()
        {
            Assert.AreEqual(2.0, Bonuses.FlatTwoIfAmountMoreThanFive(10.0));
            Assert.AreEqual(0.0, Bonuses.FlatTwoIfAmountMoreThanFive(4.0));
        }

        [TestMethod]
        public void GetValueOfProductsByDate_Test()
        {
            Assert.AreEqual(0.0, order.GetValueOfProducts(new DateTime(2024, 2, 28)));
            Assert.AreEqual(10.0, order.GetValueOfProducts(new DateTime(2024, 3, 2)));
            Assert.AreEqual(25.0, order.GetValueOfProducts(new DateTime(2024, 3, 3)));
            Assert.AreEqual(45.0, order.GetValueOfProducts(new DateTime(2024, 3, 4)));
        }

        [TestMethod]
        public void GetBonus_Test()
        {
            order.Bonus = Bonuses.TenPercent;
            Assert.AreEqual(4.5, order.GetBonus());

            order.Bonus = Bonuses.FlatTwoIfAmountMoreThanFive;
            Assert.AreEqual(2.0, order.GetBonus());
        }

        [TestMethod]
        public void GetTotalPrice_Test()
        {
            order.Bonus = Bonuses.TenPercent;
            Assert.AreEqual(40.5, order.GetTotalPrice());

            order.Bonus = Bonuses.FlatTwoIfAmountMoreThanFive;
            Assert.AreEqual(43.0, order.GetTotalPrice());
        }

        [TestMethod]
        public void SortProductOrderByName_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Name);
            string[] expectedOrder = { "Mælk", "Pålæg", "Smør" };

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].Name);
            }
        }

        [TestMethod]
        public void SortProductOrderByValue_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.Value);
            double[] expectedOrder = { 10.0, 15.0, 20.0 };

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].Value);
            }
        }

        [TestMethod]
        public void SortProductOrderByAvailableFrom_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.AvailableFrom);
            DateTime[] expectedOrder = {
                new DateTime(2024, 3, 1),
                new DateTime(2024, 3, 3),
                new DateTime(2024, 3, 4)
            };

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].AvailableFrom);
            }
        }

        [TestMethod]
        public void SortProductOrderByAvailableTo_Test()
        {
            List<Product> result = order.SortProductOrderBy(x => x.AvailableTo);
            DateTime[] expectedOrder = {
                new DateTime(2024, 3, 4),
                new DateTime(2024, 3, 5),
                new DateTime(2024, 3, 7)
            };

            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedOrder[i], result[i].AvailableTo);
            }
        }
    }
}