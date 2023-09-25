This is a Brainfuck interpreter and compiler implemented in C# with the ability to both interpret and compile Brainfuck code into a standalone C++ executable using the g++ compiler.

## Usage
To use this Brainfuck interpreter and compiler, follow the instructions below:

## Prerequisites
* .NET Core SDK for compiling the C# code.
* g++ (GNU Compiler Collection) for compiling the generated C++ code.
* strip (optional) for optimizing the generated binary.
  
##Compilation
Compile the C# code using the following command:

```
dotnet build
```


## Execution
You can choose between two modes of operation: interpretation and compilation.

## Interpretation
To interpret a Brainfuck program, use the following command:

```
dotnet run <inputfile>
<inputfile>: The Brainfuck source code file to be interpreted.
```

## Compilation
To compile the Brainfuck code into a standalone executable, use the -c flag:

```
dotnet run <inputfile> -c
<inputfile>: The Brainfuck source code file to be compiled.
```

## Example
Here's an example of running the interpreter:

```
dotnet run sample.bf
```

And here's an example of compiling the Brainfuck code into an executable:

```
dotnet run sample.bf -c
```


The interpreted output will be displayed in the console.
The compiled Brainfuck code will generate a standalone executable named Compiled in the same directory as the source code.
For optimized binary generation, the strip command is used to remove unnecessary symbols.
Brainfuck Language Features
This Brainfuck interpreter and compiler support the following Brainfuck language features:
```
>: Increment the data pointer.
<: Decrement the data pointer.
+: Increment the byte at the data pointer.
-: Decrement the byte at the data pointer.
.: Output the byte at the data pointer (newline converted to '\n').
,: Input a byte and store it at the data pointer.
[: Jump past the corresponding ] if the byte at the data pointer is 0.
]: Jump back to the corresponding [ if the byte at the data pointer is nonzero.
```
## Implementation Details
The Brainfuck source code is translated into equivalent C++ code, which is then compiled into an executable.
The C++ code is stored in a file named brainfuck.cpp.
The compiled executable is named Compiled.
## License
This project is open-source and available under the MIT License.
