using MathNet.Numerics.LinearAlgebra;
using NetYaml.Extensions;
using NetYaml.Interfaces;

namespace NetYaml
{
    public class DtNode
    {
        public DtNode() { }

        public DtNode(int xIndex, double key)
        {
            XIndex = xIndex;
            Key = key;
        }

        public List<DtNode> Children { get; set; }

        public int XIndex { get; set; }

        public double Key { get; set; }

        public double Value { get; set; }

        public double GetLeafValue(double[] xValues)
        {
            return TraverseTree(xValues);
        }

        private double TraverseTree(double[] xValues)
        {
            if (Children == null || !Children.Any())
            {
                return Value;
            }

            foreach (var child in Children)
            {
                if (xValues[child.XIndex] == child.Key)
                {
                    return child.TraverseTree(xValues);
                }
            }

            return 0;
        }
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
            return rootNodes
                .Select(node => node.GetLeafValue(x))
                .ToArray();
        }

        private void Train(DtNode node, double[,] xTrain, double[] yTrain)
        {
            var stdDev = StandardDeviation(yTrain);
            var average = yTrain.Average();
            var coeffeicientOfVariation = stdDev / average * 100;
            node.Value = average;

            if (coeffeicientOfVariation < cvThreshold)
            {
                return;
            }

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
                    highestSdr = sdr;
                }
            }

            if (selectedGroupings == null)
            {
                return;
            }

            var subsets = SubSetTrainingSets(xTrain, yTrain, selectedXIndex, selectedGroupings.Select(g => g.Key).ToArray());

            node.Children = new List<DtNode>();
            foreach (var subset in subsets)
            {
                var childNode = new DtNode(selectedXIndex, subset.xTrain[0, selectedXIndex]);
                Train(childNode, subset.xTrain, subset.yTrain);
                node.Children.Add(childNode);
            }
        }

        private IEnumerable<(double[,] xTrain, double[] yTrain)> SubSetTrainingSets(double[,] xTrain, double[] yTrain, int selectedIndex, double[] keys)
        {
            var subsetDict = new Dictionary<double, (List<double[]> xTrain, List<double> yTrain)>();
            for (var i = 0; i < xTrain.GetLength(0); i++)
            {
                var row = xTrain.GetRow(i);
                
                if (!subsetDict.ContainsKey(row[selectedIndex]))
                {
                    subsetDict.Add(row[selectedIndex], (new List<double[]>(), new List<double>()));
                }

                subsetDict[row[selectedIndex]].xTrain.Add(row);
                subsetDict[row[selectedIndex]].yTrain.Add(yTrain[i]);
            }

            return subsetDict
                .Select(d => (d.Value.xTrain.ToArray().To2DArray(), d.Value.yTrain.ToArray()));
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
