using NUnit.Framework;

namespace NetYaml.UnitTests
{
    public class LinearRegressionUnitTests
    {
        [Test]
        public void Test1()
        {
            var x = new double[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            var y = new double[] { 2, 4, 6, 8, 10, 12, 14, 16, 18, 20, 22, 24, 26, 28, 30 };

            var linearRegressor = new LinearRegression();
            linearRegressor.Fit(x, y);

            var prediction = linearRegressor.Predict(16);

            Assert.AreEqual(32, prediction);
        }
    }
}