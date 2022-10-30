using System;
using System.Text;
using System.Threading;
using System.IO;

namespace Program
{
    class Program
    {
        static string[] symbols = new string[] { ":", " - ", " [", "] ", ", " };
        static int width;
        static int height;

        public static void Main()
        {
            string[] lines = File.ReadAllLines("file.txt");
            string[][] parameters = new string[lines.Length][];

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = GetParameters(lines[i]);
            }

            DrawWindow();

            for (int seconds = 0; ; seconds++)
            {
                CheckUpdateText(parameters, seconds);
                Thread.Sleep(1000);
            }
        }

        public static string[] GetParameters(string line)
        {
            foreach (var symbol in symbols)
            {
                line = line.Replace(symbol, "  ");
            }

            return line.Split("  ");
        }

        public static void CheckUpdateText(string[][] parameters, int seconds)
        {
            int secAppend;
            int secDelete;

            foreach (var line in parameters)
            {
                if (line.Length > 4)  // need fixed !!!
                {
                    secAppend = Int32.Parse(line[1]);
                    secDelete = Int32.Parse(line[3]);

                    if (secAppend == seconds)
                        EditWindowText(line[4], line[6], "Append");
                    if (secDelete == seconds)
                        EditWindowText(line[4], line[6], "Delete");
                }
            }
        }

        public static void EditWindowText(string side, string text, string param)
        {
            int x = 0;
            int y = 0;

            switch (side)
            {
                case "Top":
                    x = (width / 2) - (text.Length / 2);
                    y = 1;
                    break;
                case "Right":
                    x = width - text.Length - 1;
                    y = height / 2;
                    break;
                case "Left":
                    x = 1;
                    y = height / 2;
                    break;
                case "Bottom":
                    x = (width / 2) - (text.Length / 2);
                    y = height - 2;
                    break;
            }

            Console.SetCursorPosition(x, y);

            if (param == "Append") Console.Write(text);
            else if (param == "Delete") Console.Write(CreateEmptyString(text.Length));

            Console.SetCursorPosition(0, height);
        }

        public static string CreateEmptyString(int length)
        {
            StringBuilder result = new();

            for (int i = 0; i < length; i++)
                result.Append(' ');

            return result.ToString();
        }

        public static void DrawWindow()
        {
            string[] window = File.ReadAllLines("window.txt");

            foreach (string line in window)
            {
                Console.WriteLine(line);
            }

            width = window[0].Length;
            height = window.Length;
        }
    }
}