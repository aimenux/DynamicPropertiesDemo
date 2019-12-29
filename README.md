# DynamicPropertiesDemo

>In this demo, i try to deserialize json files that share x known properties & may have y unknown properties.
>>So the goal is to propose a model that handle known & unknown properties (without loosing them during deserialization).

- `Solution 1` : based on inheriting model for dictionary ms type.
- `Solution 2` : based on some newtonsoft json specific attribute.
- `Solution 3` : adapted from Rick Strahl expando library.

Tools used : vs19, net core 3.1, newtonsoft json, nunit, fluent assertions
