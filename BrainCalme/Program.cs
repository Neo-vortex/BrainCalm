using System.Diagnostics;
using System.Text;

public class MainClass
{
    private static  int programm_index ;
    private static int _pointerIndex ;

    private static string _upper = @"
    #include <iostream>
    int main(){

    ";

    private static string _lower = @"
}";
    private static unsafe void Compile_BrainFuck(string input)
    {
        var _builder = new StringBuilder();
        _builder.AppendLine("char* input = " + @"""" +  string.Concat(input.Where(c => !char.IsWhiteSpace(c)))   + @"""" + ";");
        _builder.AppendLine("char array[30000] = {0};");
        _builder.AppendLine("char *ptr = array;");
        try
        {
                   var pointerPools = stackalloc byte[30000];
                  while (programm_index <= input.Length -1 )
                  {
                      var token = input[programm_index];
                      switch (token)
                      {
                          case '>':
                              _builder.AppendLine("++ptr;");
                              break;
                          case '<':
                              _builder.AppendLine("--ptr;");

                              break;
                          case '+':
                              _builder.AppendLine("++*ptr;");
                              break;
                          case '-':
                              _builder.AppendLine("--*ptr;");
                              break;
                          case '.' :
                              _builder.AppendLine(@"   if (*ptr == 10){
                                                              std::cout << std::endl;
                                                        }else{
                                                             putchar(*ptr);
                                                         }");
                              break;
                          case ',':
                              _builder.AppendLine(@"std::cout << " + @"""" + "Enter Input:" + @"""" + "<< std::endl; ");
                              _builder.AppendLine("*ptr = getchar();");
                              break;
                          case '[':
                              _builder.AppendLine("  while (*ptr) {");
                              
                              break;
                        
                          case ']' :
                              _builder.AppendLine("}");
                              break;
                      }
                      programm_index++;
                  }
                  System.IO.File.Delete("./Compiled");
                  System.IO.File.WriteAllText("brainfuck.cpp" ,_upper + _builder.ToString() + _lower );
                  Process.Start("g++", new[] { "brainfuck.cpp", "-O3" , "-oCompiled" , "-w" , "-fno-stack-protector" }).WaitForExit();
                  Process.Start("strip", new[] { "./Compiled" }).WaitForExit();
                  var proc = new Process 
                  {
                      StartInfo = new ProcessStartInfo
                      {
                          FileName = "./Compiled",
                          UseShellExecute = false,
                          RedirectStandardOutput = true,
                          CreateNoWindow = true
                      }
                  };
                  proc.Start();
                  while (!proc.StandardOutput.EndOfStream)
                  {
                      var line = Convert.ToChar(proc.StandardOutput.Read()) ;
                      Console.Write(line);
                  }
                  Console.WriteLine();
                  Console.WriteLine("----------");
                  Console.WriteLine("Brainfuck executed");
                  Console.ReadKey();
        }


        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    private static unsafe void Process_BrainFuck(string input)
    {
        try
        {
            var pointerPools = stackalloc byte[30000];
                  while (programm_index <= input.Length -1 )
                  {
                      var token = input[programm_index];
                      switch (token)
                      {
                          case '>':
                              while (input[programm_index] == '>')
                              {
                                  _pointerIndex++;
                                  programm_index++;
                              }
                              programm_index--;
                              break;
                          case '<':
                              while (input[programm_index] == '<')
                              {
                                  _pointerIndex--;
                                  programm_index++;
                              }
                              programm_index--;

                              break;
                          case '+':
                              while (input[programm_index] == '+')
                              {
                                  pointerPools[_pointerIndex]++;
                                  programm_index++;
                              }
                              programm_index--;
                              break;
                          case '-':
                              while (input[programm_index] == '-')
                              {
                                  pointerPools[_pointerIndex]--;
                                  programm_index++;
                              }
                              programm_index--;
                              break;
                          case '.' :
                              Console.Write(pointerPools[_pointerIndex] == 10 ? Environment.NewLine : Convert.ToChar(pointerPools[_pointerIndex])  );
                              break;
                          case ',':
                              Console.WriteLine("[Interpreter : Enter input :]");
                              pointerPools[_pointerIndex] = (byte)(int.Parse(Console.ReadLine() ?? throw new Exception("Bad input")));
                              break;
                          case '[':
                              if (pointerPools[_pointerIndex] == 0)
                              {
                                  var helper = 0;
                                  programm_index++;
                                  while (input[programm_index] != ']' || helper > 0 )
                                  {
                                      if (input[programm_index] == '[')
                                      {
                                          helper++;
                                      }
                                      if (input[programm_index] == ']')
                                      {
                                          helper--;
                                      }
                                      programm_index++;
                                  }  
                              }
                              break;
                        
                          case ']' :
                              if (pointerPools[_pointerIndex] != 0)
                              {
                                  var helper2 = 0;
                                  programm_index--;
                                  while (input[programm_index] != '[' || helper2 > 0 )
                                  {
                                      if (input[programm_index] == ']')
                                      {
                                          helper2++;
                                      }
                                      if (input[programm_index] == '[')
                                      {
                                          helper2--;
                                      }
                                      programm_index--;
                                  }  
                                  programm_index--;
                              }
                           
                              break;
                      }
                      programm_index++;
                  }
                  Console.WriteLine();
                  Console.WriteLine("----------");
                  Console.WriteLine("Brainfuck interpreted ");
                  Console.ReadKey();
        }
        catch (Exception e)
        {
            Console.WriteLine("Exception during brainfuck interpretation");
            Console.WriteLine(e);
        }


    }
    public  static  string ReadFile(string fileName)
    {
        try
        {
          return  File.ReadAllText(fileName) ;
        }
        catch (Exception e)
        {
            Console.WriteLine("Can not read file");
            Console.WriteLine(e.Message);
            return string.Empty;
        }
    }
    public static void PrintUsage()
    {
        Console.WriteLine("Usage: BrainCalme <inputfile> [-c] ");
        Console.WriteLine("\t[NO ARGS]\t\tPrint this help");
        Console.WriteLine("\t-c\t\tCompile instead of interpreting");
    }
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            PrintUsage();
            return;
        }
        var input = ReadFile(args[0]);
        if (args.Length == 2 && args[1] == "-c")
        {
            Compile_BrainFuck(input);
        }
        Process_BrainFuck(input);
    }
}