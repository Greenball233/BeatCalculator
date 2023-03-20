using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("BPM: ");
            string bpmStr = Console.ReadLine();
            if (String.IsNullOrEmpty(bpmStr) || String.IsNullOrEmpty(bpmStr.Trim()))
            {
                Console.WriteLine("Illegal BPM");
                return;
            }

            if (!float.TryParse(bpmStr, out var bpm))
            {
                Console.WriteLine("Illegal BPM");
                return;
            }

            if (bpm <= 0)
            {
                Console.WriteLine("Illegal BPM");
                return;
            }
            
            Console.Write(">>");
            string qwq = Console.ReadLine();
            while (qwq != "exit")
            {
                switch (qwq)
                {
                    case "bpm":
                        Console.Write("New BPM: ");
                        bpmStr = Console.ReadLine();
                        if (String.IsNullOrEmpty(bpmStr) || String.IsNullOrEmpty(bpmStr.Trim()))
                        {
                            Console.WriteLine("Illegal BPM");
                            break;
                        }

                        if (!float.TryParse(bpmStr, out var tmp))
                        {
                            Console.WriteLine("Illegal BPM");
                            break;
                        }

                        if (tmp <= 0)
                        {
                            Console.WriteLine("Illegal BPM");
                            break;
                        }

                        bpm = tmp;
                        break;
                    case "a":
                        Console.Write("Beat: ");
                        string beatStr = Console.ReadLine();
                        if (String.IsNullOrEmpty(beatStr) || String.IsNullOrEmpty(beatStr.Trim()))
                        {
                            Console.WriteLine("Illegal Beat");
                            break;
                        }
                        if (!float.TryParse(beatStr, out float beat))
                        {
                            Console.WriteLine("Illegal Beat");
                        }
                        else if (beat <= 0)
                        {
                            Console.WriteLine("Illegal Beat");
                        }
                        else
                        {
                            float d;
                            Console.WriteLine("Time: " + (d = beat * 60 / bpm) + " (" + d.ToString("0.000") + ")");
                        }
                        break;
                    case "b":
                        Console.Write("Time: ");
                        string timeStr = Console.ReadLine();
                        if (String.IsNullOrEmpty(timeStr) || String.IsNullOrEmpty(timeStr.Trim()))
                        {
                            Console.WriteLine("Illegal Time");
                            break;
                        }
                        if (!TryParseTime(timeStr, out float time))
                        {
                            Console.WriteLine("Illegal Time");
                        }
                        else if (time <= 0)
                        {
                            Console.WriteLine("Illegal Time");
                        }
                        else
                        {
                            float a, b, c;
                            Console.WriteLine("Calculated Beat: " + (a = time * bpm / 60));
                            Console.WriteLine("Possible Beat: " + (b = MathF.Round(a)));
                            Console.WriteLine("Possible Real Time: " + (c = b * 60 / bpm) + " (" + c.ToString("0.000") + ")");
                        }
                        break;
                }

                Console.Write(">>");
                qwq = Console.ReadLine();
            }

            Console.WriteLine("bye");
        }

        private static bool TryParseTime(string ori, out float bpm)
        {
            try
            {
                bpm = float.Parse(ori);
                return true;
            }
            catch (FormatException f)
            {
                if (ori.Contains(':'))
                {
                    string[] strings = ori.Split(":");
                    if (strings.Length != 2)
                    {
                        bpm = -1;
                        return false;
                    }
                    try
                    {
                        int min = int.Parse(strings[0]);
                        float second = float.Parse(strings[1]);
                        bpm = min * 60 + second;
                        return true;
                    }
                    catch (FormatException fe)
                    {
                        bpm = -1;
                        return false;
                    }
                }
                bpm = -1;
                return false;
            }
        }
    }
}