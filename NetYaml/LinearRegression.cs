using MathNet.Numerics.LinearAlgebra;
using NetYaml.Interfaces;

namespace NetYaml
{
    public class LinearRegression : IMLModel, ITrainedModel
    {
        private Vector<double>[] _weights;

        internal LinearRegression() { }

        public ITrainedModel Fit(double[,] xTrain, double[,] yTrain)
        {
            var row = Enumerable.Range(0, yTrain.GetLength(1))
                .Select(x => yTrain[0, x])
                .ToArray();

            _weights = new Vector<double>[row.Length];

            for (var i = 0; i < _weights.Length; i++)
            {
                var y = Enumerable.Range(0, yTrain.GetLength(0))
                    .Select(x => yTrain[x, i])
                    .ToArray();

                var xMatrix = Matrix<double>.Build.DenseOfArray(xTrain);
                var yMatrix = Vector<double>.Build.Dense(y);

                _weights[i] = xMatrix
                    .QR()
                    .Solve(yMatrix);
            }

            return this;
        }

        public double[] Predict(double[] x)
        {
            var prediction = new double[_weights.Length];

            for (var i = 0; i < _weights.Length; i++)
            {
                var xVector = Vector<double>
                    .Build
                    .DenseOfArray(x);
                
                prediction[i] = _weights[i].DotProduct(xVector);
            }

            return prediction;
        }
    }
}