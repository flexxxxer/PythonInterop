# PythonInterop
Tool for cross-platform interacting C# and Python system interpreters

## Gettiong started
This library is made on .net standard 2.0, respectively, there is compatibility with .net framework 4.6.2+ and .net core 2.0+

## Features:
- working on Windows, macOS and Linux
- lazy getting all system interpreters with their versions and paths
- running a python script using the specified interpreter

## Usage
##### Getting all system interpreters
```csharp
IEnumerable<PythonInterpreter> interpreters = PythonEngine.PythonInterpreters;
```
##### Getting actual interpreter
```csharp
var actualInterpreter = PythonEngine.PythonInterpreters
                        .OrderByDescending(i => i.Version)
                        .First();
```
##### Getting the first interpreter with version 3 above
```csharp
var interpreter = PythonEngine.PythonInterpreters
                  .First(i => string.CompareOrdinal(i.Version, "3.0.0") > 1);
```
#### Getting an interpreter related only to the second version (i.e. between 2.0.0 and 2.99.99)
```csharp
var interpreter = PythonEngine.PythonInterpreters
                  .First(i => 
                      string.CompareOrdinal(i.Version, "3.0.0") < 0 && 
                      string.CompareOrdinal(i.Version, "2.0.0") > 1);
```

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

## License
[MIT](https://choosealicense.com/licenses/mit/)
