using NetYaml.Enums;
using NetYaml.Interfaces;

namespace NetYaml.Factories
{
    public class MLModelFactory : IMLModelFactory
    {
        public IMLModel Create(ModelType modelType)
        {
            return modelType switch
            {
                ModelType.LinearRegression => new LinearRegression(),
                ModelType.DecisionTreeRegression => new DecisionTreeRegression()
            };
        }
    }
}
