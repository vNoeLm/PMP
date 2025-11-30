namespace Hazi2
{
    internal class Program
    {
        static int index = 0;
        static void Main(string[] args)
        {
            ReadInput();
        }

        private static void Evalinput(string[] lines)
        {
            if (lines[index] == "begin" && lines[index + 1] == "end")
            {
                WriteOutput(1);
                return;
            }
            index = 1;

            long totalPaths = CalculatePaths(lines, "end");
            WriteOutput(totalPaths);
        }

        private static long CalculatePaths(string[] lines, string stopAt)
        {
            long paths = 1;
            while (lines[index] != stopAt)
            {
                if (lines[index] == "if")
                {
                    index++;
                    long ifPaths = CalculatePaths(lines, "else");
                    long elsePaths = CalculatePaths(lines, "endif");

                    index++;

                    long nestedTotal = ifPaths + elsePaths;
                    paths *= nestedTotal;
                }
                else
                {
                    index++;
                }
            }
            return paths;
        }

        private static void WriteOutput(long possibilities)
        {
            StreamWriter sw = new StreamWriter("output.txt");
            sw.Write(possibilities);
            sw.Close();
        }

        private static void ReadInput()
        {
            FileStream fs = new FileStream("input.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            int firstLine = Convert.ToInt16(sr.ReadLine());
            string[] lines = new string[firstLine];

            for (int i = 0; i < firstLine; i++)
            {
                lines[i] = sr.ReadLine();
            }

            sr.Close();
            fs.Close();
            Evalinput(lines);
        }
    }
}
