namespace NetYaml.Interfaces
{
    public interface ITrainedModel
    {
        double[] Predict(double[] x);
    }
}
