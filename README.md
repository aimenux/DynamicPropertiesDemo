# DynamicPropertiesDemo

>In this demo, i try to deserialize json files that share x known properties & may have y unknown properties.
>>So the goal is to propose a way to handle known & unknown properties (without loosing them during deserialization).

- `Solution 1` : based on inheriting model from dictionary ms type.
- `Solution 2` : based on some newtonsoft json specific attribute.
- `Solution 3` : adapted from [Rick Strahl expando library](https://github.com/RickStrahl/Expando).

**`Tools`** : `vs19, net core 3.1, newtonsoft json, nunit, fluent assertions`
