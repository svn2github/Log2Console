using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Serilog;
using Serilog.Formatting.Json;
using Serilog.Sinks.Network;

namespace SerilogTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .WriteTo.RollingFile(new JsonFormatter(), "log-{Date}.txt", retainedFileCountLimit: 2, buffered: true, flushToDiskInterval:TimeSpan.FromSeconds(5))
                .WriteTo.LiterateConsole()
                //.WriteTo.Udp(IPAddress.Loopback, 7071)
                .WriteTo.UDPSink(IPAddress.Loopback, 7071)
                .CreateLogger();

            Log.Verbose("Verbose");
            Log.Debug("Debug");
            Log.Information("Information");
            Log.Warning("Warning");
            Log.Error("Error");
            Log.Fatal("Fatal");

            var me = new Person("Duane", "123", "?");
            Log.Information("I just created {@Someone} at {Now}", me, DateTime.Now);
            int count = 1;
            Log.Information("Create {Count} people", count);
            var fruit = new string[] {"Apple", "Pear", "Orange"};
            Log.Information("In my bowl I have {Fruit}", fruit);

            //Console.WriteLine("Enter a simple math statement.  Operators supported are (+,-,/,*)");
            //var line = "";
            //while ((line = Console.ReadLine()).ToLower() != "q")
            //{
            //    Domath(line);
            //}
            Domath("10/5");
            Domath("8%2");
            Domath("99/0");
        }

        static void Domath(string math)
        {
            var match = Regex.Match(math, @"(\d+)\s*(\D)\s*(\d+)");
            if (match.Groups.Count == 4)
            {
                Log.Information("Attempting to process {Operand1}{Operator}{Operand2}", match.Groups[1], match.Groups[2],
                    match.Groups[3]);
                try
                {
                    var operand1 = int.Parse(match.Groups[1].Value);
                    var operand2 = int.Parse(match.Groups[3].Value);
                    double answer = 0;
                    switch (match.Groups[2].Value)
                    {
                        case "+":
                            answer = operand1 + operand2;
                            break;
                        case "-":
                            answer = operand1 - operand2;
                            break;
                        case "/":
                            answer = operand1 / operand2;
                            break;
                        case "*":
                            answer = operand1 * operand2;
                            break;
                        default:
                            throw new InvalidOperationException("Unknown operator");
                            break;
                    }
                    Log.Information("{Operand1}{Operator}{Operand2}={Answer}", operand1, match.Groups[2], operand2, answer);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Error does not compute");
                }
            }
        }
    }
}
