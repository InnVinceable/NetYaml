using NetYaml.Enums;

namespace NetYaml.Interfaces
{
    public interface IMLModelFactory
    {
        IMLModel Create(ModelType modelType);
    }
}
