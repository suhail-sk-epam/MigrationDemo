Feature: TestCafe to SpecFlow Conversion

Scenario: Validate converted tests
Given the original TestCafe test file 'test/test.js'
And the converted C# file 'test/test_converted.cs'
When the conversion process is completed
Then a feature file 'test/generated_feature.feature' should be generated
And it should contain the corresponding SpecFlow steps