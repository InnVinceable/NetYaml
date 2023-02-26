# NetYaml
No, not a yaml library... Yet Another Machine Learning library.

Apologies if the misleading name lead you to click on this repository in the hopes of finding a reliable library to read/write yaml files. Double apologies if you arrived here during a serious time crunch to get that ticket out before sprint-end. If this was you, check out the YamlDotNet nuget package.

To express my sincere apologies, here's a snippet of how you'd typically read from a yaml file using YamlDotNet:
```
var deserializer = new YamlDotNet.Serialization.DeserializerBuilder()
    .WithNamingConvention(CamelCaseNamingConvention.Instance)
    .Build();

var myObjectThatIWantTheYamlDataToBeDeserializedTo = deserializer.Deserialize<YamlStuff>(File.ReadAllText("yamlstuff.yaml"));
```

Good luck!


## Down to business...
This is a distilled, simple and definitely NOT production-ready (and will never be) machine learning library written in .Net. The idea here is to create a simple, bells-and-whistles-free implementation of the various ML algorithms purely for educational purposes for myself and anyone wanting to gain an understanding of the basic algorithms behind regression, classification, reinforcement learning, neural networks, etc.

First thing you'll notice is that there are probably a lot of comments and no fancy design patterns, and when you run the tests, they will probably be embarassingly slow compared to other libraries.


## Contributing
Refactors, bug fixes, or any other suggestions for improvement are welcome.
