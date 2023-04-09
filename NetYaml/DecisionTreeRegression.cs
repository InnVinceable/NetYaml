using MathNet.Numerics.LinearAlgebra;
using NetYaml.Extensions;
using NetYaml.Interfaces;

namespace NetYaml
{
    public class DtNode
    {
        public DtNode(double? key = null)
        {
            Key = key;
        }

        public List<DtNode> Children { get; set; }

        public double? Key { get; set; }

        public double Value { get; set; }
    }

    public class DecisionTreeRegression : IMLModel, ITrainedModel
    {
        private const double cvThreshold = 10;

        private DtNode[] rootNodes;

        public ITrainedModel Fit(double[,] xTrain, double[,] yTrain)
        {
            rootNodes = new DtNode[yTrain.GetLength(1)];

            for (var i = 0; i < yTrain.GetLength(1); i++)
            {
                var y = Enumerable
                    .Range(0, yTrain.GetLength(0))
                    .Select(x => yTrain[x, i])
                    .ToArray();

                rootNodes[i] = new DtNode();

                Train(rootNodes[i], xTrain, y);
            }

            return this;
        }

        public double[] Predict(double[] x)
        {
            throw new NotImplementedException();
        }

        private void Train(DtNode node, double[,] xTrain, double[] yTrain)
        {
            var stdDev = StandardDeviation(yTrain);
            var average = yTrain.Average();
            var coeffeicientOfVariation = stdDev / average * 100;

            var highestSdr = 0d;
            var selectedXIndex = 0;
            IEnumerable<IGrouping<double, (double first, double second)>>? selectedGroupings = default;

            for (var i = 0; i < xTrain.GetLength(1); i++)
            {
                var xCol = Enumerable
                    .Range(0, xTrain.GetLength(0))
                    .Select(x => xTrain[x, i])
                    .ToArray();

                var group = xCol
                    .Zip(yTrain)
                    .GroupBy(c => c.First);

                var xStdDev = group
                    .Select(g => (g.Key, ((double)g.Count() / (double)xCol.Length) * StandardDeviation(g.Select(s => s.Second).ToArray())))
                    .Sum(s => s.Item2);

                var sdr = stdDev - xStdDev;
                if (sdr > highestSdr)
                {
                    selectedGroupings = group;
                    selectedXIndex = i;
                }
            }

            if (selectedGroupings == null)
            {
                return;
            }

            node.Children = new List<DtNode>();
            foreach (var group in selectedGroupings)
            {
                var childNode = new DtNode(selectedXIndex);


                Train(childNode, subset, );
                node.Children.Add();
            }
        }

        private double StandardDeviation(double[] x)
        {
            var n = x.Length;
            var avg = x.Sum() / n;
            var stdDev = Math.Sqrt(x.Sum(x => Math.Pow(x - avg, 2)) / n);

            return stdDev;
        }
    }
}
