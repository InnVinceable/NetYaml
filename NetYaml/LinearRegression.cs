namespace NetYaml
{
    public class LinearRegression
    {
        private double _scale;
        private double _bias;

        public void Fit(double[] xTrain, double[] yTrain)
        {
            // Ordinary Least Mean Square Method
            // Given a list of independent x values and dependent y values,
            // the goal here is to form the linear equation mx + c of a line that
            // best fits these points such that the sum of distances between the line
            // is minimized.

            // For this, we need the gradient (m) and the y intercept (c) of the line.

            // In ML terms, gradient is referred to as "scale" and the y intercept
            // is referred to as the "bias"

            // Bias = SUM(X[i] - meanOfX) * (Y[i] - meanOfY) / SUM((X[i] - meanOfX) ^ 2)
            var xMean = xTrain.Average();
            var yMean = yTrain.Average();
            var numerator = 0d;
            var denominator = 0d;
            for (var i = 0; i < xTrain.Length; i++)
            {
                numerator += (xTrain[i] - xMean) * (yTrain[i] - yMean);
                denominator += Math.Pow(xTrain[i] - xMean, 2);
            }

            _scale = numerator / denominator;

            // Scale = meanOfY - bias * meanOfX
            _bias = yMean - _scale * xMean;
        }

        public double Predict(double x)
        {
            return _scale * x + _bias;
        }

        public double RootMeanSquareError(double[] xTest, double[] yTest)
        {
            var totalError = 0d;
            for (var i = 0; i < xTest.Length; i++)
            {
                var yPrediction = Predict(xTest[i]);
                totalError += Math.Pow(yPrediction - yTest[i], 2);
            }

            return Math.Sqrt(totalError / xTest.Length); 
        }
    }
}