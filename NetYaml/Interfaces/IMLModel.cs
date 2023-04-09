namespace NetYaml.Interfaces
{
    public interface IMLModel
    {
        ITrainedModel Fit(double[,] xTrain, double[,] yTrain);
    }
}
