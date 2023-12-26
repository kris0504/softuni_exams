namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            int hazelnutcount = 0;
            string[] commands = Console.ReadLine().Split(", ", StringSplitOptions.RemoveEmptyEntries).ToArray();
            char[,] field = new char[n,n];
            int[] squirrel = new int[2];
            for (int i = 0; i < n; i++)
            {
                string line= Console.ReadLine();
                for (int j = 0; j < n; j++)
                {
                    if (line[j] == 's')
                    {
                        squirrel[0] = i;
                        squirrel[1] = j;
                    }
                    field[i, j] = line[j];
                }
            }
            for (int i = 0; i < commands.Length; i++)
            {
                string command=commands[i];
                if (command=="left"&&Verification(squirrel,0,-1,n))
                {
                    if (field[squirrel[0],squirrel[1]-1]=='h')
                    {
                        field[squirrel[0], squirrel[1] - 1] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[1] = squirrel[1] - 1;
                        hazelnutcount++;
                    }
                    else if (field[squirrel[0], squirrel[1] - 1] == 't')
                    {
                        Console.WriteLine("Unfortunately, the squirrel stepped on a trap...");
                        Console.WriteLine($"Hazelnuts collected: { hazelnutcount}");
                        Environment.Exit(0);
                    }
                    else
                    {
                        field[squirrel[0], squirrel[1] - 1] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[1] = squirrel[1] - 1;
                    }
                }
                else if (command == "right" && Verification(squirrel, 0, +1, n))
                {
                    if (field[squirrel[0], squirrel[1] + 1] == 'h')
                    {
                        field[squirrel[0], squirrel[1] + 1] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[1] = squirrel[1] + 1;
                        hazelnutcount++;
                    }
                    else if (field[squirrel[0], squirrel[1] +1] == 't')
                    {
                        Console.WriteLine("Unfortunately, the squirrel stepped on a trap...");
                        Console.WriteLine($"Hazelnuts collected: {hazelnutcount}");
                        Environment.Exit(0);
                    }
                    else
                    {
                        field[squirrel[0], squirrel[1] + 1] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[1] = squirrel[1] +1;
                    }
                }
                else if (command == "down" && Verification(squirrel, +1, 0, n))
                {
                    if (field[squirrel[0]+1, squirrel[1] ] == 'h')
                    {
                        field[squirrel[0]+1, squirrel[1] ] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        hazelnutcount++;
                        squirrel[0] = squirrel[0]+1 ;
                    }
                    else if (field[squirrel[0]+1, squirrel[1] ] == 't')
                    {
                        Console.WriteLine("Unfortunately, the squirrel stepped on a trap...");
                        Console.WriteLine($"Hazelnuts collected: {hazelnutcount}");
                        Environment.Exit(0);
                    }
                    else
                    {
                        field[squirrel[0]+1, squirrel[1]] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[0] = squirrel[0] + 1;
                    }
                }
                else if (command == "up" && Verification(squirrel, -1, 0, n))
                {
                    if (field[squirrel[0]-1, squirrel[1]] == 'h')
                    {
                        field[squirrel[0]-1, squirrel[1]] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        hazelnutcount++;
                        squirrel[0] = squirrel[0] - 1;
                    }
                    else if (field[squirrel[0]-1, squirrel[1]] == 't')
                    {
                        Console.WriteLine("Unfortunately, the squirrel stepped on a trap...");
                        Console.WriteLine($"Hazelnuts collected: {hazelnutcount}");
                        Environment.Exit(0);
                    }
                    else
                    {
                        field[squirrel[0]-1, squirrel[1]] = 's';
                        field[squirrel[0], squirrel[1]] = '*';
                        squirrel[0] = squirrel[0] - 1;
                    }
                }
                else
                {
                    Console.WriteLine("The squirrel is out of the field.");
                    Console.WriteLine($"Hazelnuts collected: { hazelnutcount}");
                    Environment.Exit(0);
                }
            }
            if (hazelnutcount==3)
            {
                Console.WriteLine("Good job! You have collected all hazelnuts!");
                Console.WriteLine($"Hazelnuts collected: {hazelnutcount}");
            }
            else
            {
                Console.WriteLine("There are more hazelnuts to collect.");
                Console.WriteLine($"Hazelnuts collected: {hazelnutcount}");
            }
        }
        static bool Verification( int[] squirrel, int i,int j, int n)
        {
            return squirrel[0] + i >= 0&&squirrel[1] + j >= 0 && squirrel[0] + i < n && squirrel[1]+j<n;
        }
    }
}