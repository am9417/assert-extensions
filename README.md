# assert-extensions
Extensions for VisualStudio UnitTest's Assert class to simplify e.g. testing of view models.

## What is this

This project provides "timed-out" assertions as C# extension methods for Visual Studio UnitTesting `Assert` class. The extension methods can be useful when testing e.g. `async void` methods. See examples for more.

In the future, there could be more extension capabilities for testing. Leave your suggestions in the issue tracker.

## Problem
Consider for example a `RelayCommand` inside a view model that has a string property `MyTextProperty`. It takes an `Action` as a parameter (here, as an async lambda expression):
```csharp
RelayCommand MyCommand = new RelayCommand(async () => 
{
    bool ok = await DoSomethingAsync(); // DoSomethingAsync returns Task<bool>
    MyTextProperty = ok ? "finished" : "failed";
});
```
How to test it? Maybe like this?
```csharp
viewModel.MyCommand.Execute(null);
Assert.AreEqual("finished", viewModel.MyTextProperty);
```
...but executing the command will just fire and forget. 

So maybe adding a delay helps:
```csharp
viewModel.MyCommand.Execute(null);
Task.Delay.Wait(1000);
Assert.AreEqual("finished", viewModel.MyTextProperty);
```
...but what should the delay be?

## Example

If you don't want to refactor your view model code completely just to test it, you can use these extensions:
```csharp
viewModel.MyCommand.Execute(null);
Assert.That.PropertyEquals("finished", () => viewModel.MyTextProperty);
```
This will poll for the value and return until `viewModel.MyTextProperty` equals `"finished"`, or it will time out in 15 seconds. You can give your own timeout as a parameter, if the timeout is too big, or too small.

There may be some other use cases for this simple extension, too.

## Installation

### Nuget package
The easiest way is to install the [_ViewModelAssertions_ NuGET package](https://www.nuget.org/packages/ViewModelAssertions/)

Or by Package Manager `Install-Package ViewModelAssertions -Version 1.1.0`

### From source
You can also download the source (`AssertionExtensions` project) and add it into your solution directly.

## License
```
Copyright 2020 Antti Konsta Kustaa

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
```
