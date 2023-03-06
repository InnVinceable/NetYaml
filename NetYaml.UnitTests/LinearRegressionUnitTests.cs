using NUnit.Framework;

namespace NetYaml.UnitTests
{
    public class LinearRegressionUnitTests
    {
        [Test]
        public void Predict_GivenTrainingSet_ReturnsExpectedPrediction()
        {
            var x = new double[][] { 
                new double[] { 1, 2, 3, 4, 5 },
                new double[] { 2, 4, 6, 8, 10 }
            };

            var y = new double[] { 3, 6, 9, 12, 15 };

            var linearRegressor = new LinearRegression();
            linearRegressor.Fit(x, y);

            var prediction = linearRegressor.Predict(new double[] { 6, 12 });

            Assert.AreEqual(18, prediction);
        }
    }
}