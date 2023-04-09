using NetYaml.Enums;
using NetYaml.Factories;
using NetYaml.Interfaces;
using NUnit.Framework;
using System;

namespace NetYaml.UnitTests
{
    public class DecisionTreeRegressionUnitTests
    {
        private IMLModelFactory _factory;

        [SetUp]
        public void Setup()
        {
            _factory = new MLModelFactory();
        }

        [Test]
        public void Predict_GivenManyToOneTrainingSet_ReturnsExpectedPrediction()
        {
            double[,] x = new double[,]
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 2 },
                { 2, 1, 1, 1 },
                { 3, 2, 1, 1 },
                { 3, 3, 2, 1 },
                { 3, 3, 2, 2 },
                { 2, 3, 2, 2 },
                { 1, 2, 1, 1 },
                { 1, 3, 2, 1 },
                { 3, 2, 2, 1 },
                { 1, 2, 2, 2 },
                { 2, 2, 1, 2 },
                { 2, 1, 1, 1 },
                { 3, 2, 2, 2 },
            };

            double[,] y = new double[,]
            {
                { 25 },
                { 30 },
                { 46 },
                { 45 },
                { 52 },
                { 23 },
                { 43 },
                { 35 },
                { 38 },
                { 46 },
                { 48 },
                { 52 },
                { 44 },
                { 30 },
            };

            var regressor = _factory.Create(ModelType.DecisionTreeRegression);
            var trainedModel = regressor.Fit(x, y);

            var prediction = Math.Round(trainedModel.Predict(new double[] { 5, 5 })[0]);
            Assert.AreEqual(10, prediction);
        }

        [Test]
        public void Predict_GivenManyToManyTrainingSet_ReturnsExpectedPrediction()
        {
            double[,] x = new double[,]
            {
                { 1, 1, 1, 1 },
                { 1, 1, 1, 2 },
                { 2, 1, 1, 1 },
                { 3, 2, 1, 1 },
                { 3, 3, 2, 1 },
                { 3, 3, 2, 2 },
                { 2, 3, 2, 2 },
                { 1, 2, 1, 1 },
                { 1, 3, 2, 1 },
                { 3, 2, 2, 1 },
                { 1, 2, 2, 2 },
                { 2, 2, 1, 2 },
                { 2, 1, 1, 1 },
                { 3, 2, 2, 2 },
            };

            double[,] y = new double[,]
            {
                { 26 },
                { 30 },
                { 48 },
                { 46 },
                { 62 },
                { 23 },
                { 43 },
                { 36 },
                { 38 },
                { 48 },
                { 48 },
                { 62 },
                { 44 },
                { 30 },
            };

            var regressor = _factory.Create(ModelType.DecisionTreeRegression);
            var trainedModel = regressor.Fit(x, y);


            var predictions = trainedModel.Predict(new double[] { 4, 5 });

            Assert.AreEqual(2, predictions.Length);
            Assert.AreEqual(9, Math.Round(predictions[0]));
            Assert.AreEqual(10, Math.Round(predictions[1]));
        }
    }
}
