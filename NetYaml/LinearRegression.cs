namespace NetYaml
{
    public class LinearRegression
    {
        private double[] _weights;
        private double _yIntercept;

        public void Fit(double[][] xTrain, double[] yTrain)
        {
            var yMean = yTrain.Average();
            _weights = new double[xTrain.Length];

            var xMeans = new double[xTrain.Length];
            for (var i = 0; i < xTrain.Length; i++)
            {
                var xParameterSet = xTrain[i];
                var xMean = xParameterSet.Average();
                var numerator = 0d;
                var denominator = 0d;
                for (var j = 0; j < xTrain.Length; j++)
                {
                    numerator += (xParameterSet[j] - xMean) * (yTrain[j] - yMean);
                    denominator += Math.Pow(xParameterSet[j] - xMean, 2);
                }

                _weights[i] = numerator / denominator;
                _weights[i] /= xTrain.Length;
                xMeans[i] = xMean;
            }

            _yIntercept = yMean;
            for (var i = 0; i < xTrain.Length; i++)
            {
                _yIntercept -= (xMeans[i] * _weights[i]);
            }
        }

        public double Predict(double[] x)
        {
            var prediction = 0d;
            for (var i = 0; i < _weights.Length; i++)
            {
                prediction += _weights[i] * x[i];
            }

            prediction += _yIntercept;

            return prediction;
        }
    }
}