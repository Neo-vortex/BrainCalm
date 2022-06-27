public class MainClass
{
    private static  int programm_index ;
    private static int _pointerIndex ;

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
        Console.WriteLine("Usage: BrainCalme <inputfile> [-s] ");
        Console.WriteLine("\t[NO ARGS]\t\tPrint this help");
    }
    public static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            PrintUsage();
            return;
        }
        var input = ReadFile(args[0]);
        Process_BrainFuck(input);
    }
}