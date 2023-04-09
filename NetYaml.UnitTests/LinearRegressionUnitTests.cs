using NetYaml.Enums;
using NetYaml.Factories;
using NetYaml.Interfaces;
using NUnit.Framework;
using System;

namespace NetYaml.UnitTests
{
    public class LinearRegressionUnitTests
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
                { 1.0, 2.0 },
                { 2.0, 3.0 },
                { 3.0, 4.0 }
            };

            double[,] y = new double[,]
            {
                { 3.0 },
                { 5.0 },
                { 7.0 }
            };

            var regressor = _factory.Create(ModelType.LinearRegression);
            var trainedModel = regressor.Fit(x, y);

            var prediction = Math.Round(trainedModel.Predict(new double[] { 5, 5 })[0]);
            Assert.AreEqual(10, prediction);
        }

        [Test]
        public void Predict_GivenManyToManyTrainingSet_ReturnsExpectedPrediction()
        {
            double[,] x = new double[,]
            {
                { 1.0, 2.0 },
                { 2.0, 3.0 },
                { 3.0, 4.0 }
            };

            double[,] y = new double[,]
            {
                { 3.0, 4.0 },
                { 5.0, 6.0 },
                { 7.0, 8.0 }
            };

            var regressor = _factory.Create(ModelType.LinearRegression);
            var trainedModel = regressor.Fit(x, y);

            var predictions = trainedModel.Predict(new double[] { 4, 5 });

            Assert.AreEqual(2, predictions.Length);
            Assert.AreEqual(9, Math.Round(predictions[0]));
            Assert.AreEqual(10, Math.Round(predictions[1]));
        }
    }
}