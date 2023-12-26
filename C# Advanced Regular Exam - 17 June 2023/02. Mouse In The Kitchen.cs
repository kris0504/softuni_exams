namespace Mouse_in_the_kitchen
{
    internal class Program
    {
        static int cheesecounter;
        static int[] n;
        static char[,] area;
        static int[] mouse;
        static void Main(string[] args)
        {
             n= Console.ReadLine().Split(",", StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
             area = new char[n[0], n[1]];
             mouse = new int[2];
            cheesecounter = 0;
            for (int i = 0; i < n[0]; i++)
            {
                string row = Console.ReadLine();
                for (int j = 0; j < n[1]; j++)
                {
                    if (row[j] == 'M')
                    {
                        mouse[0] = i;
                        mouse[1] = j;
                    }
                    else if (row[j]=='C')cheesecounter++;
                    area[i, j] = row[j];
                }
            }
            string command = Console.ReadLine();
            while (command!="danger")
            {
                if (command=="left")
                {
                    Move(0, -1);
                }
                else if (command=="right")
                {
                    Move(0, 1);

                }
                else if (command=="up")
                {
                    Move(-1, 0);
                }
                else if (command == "down")
                {
                    Move(1, 0);
                }
                command = Console.ReadLine();
            }
            Console.WriteLine("Mouse will come back later!");
            MatrixPrint();

        }
        static void Move(int i, int j)
        {
            if (mouse[0] + i < 0 || mouse[0] + i >= n[0] || mouse[1] + j < 0 || mouse[1] + j >= n[1]) 
            {
                Console.WriteLine("No more cheese for tonight!");
                MatrixPrint();
                Environment.Exit(0);
            }
            if (area[mouse[0] + i, mouse[1] + j] == 'C')
            {
                area[mouse[0] + i, mouse[1] + j] = 'M';
                area[mouse[0], mouse[1]] = '*';
                mouse[0] += i;
                mouse[1] += j;
                cheesecounter--;
                if (cheesecounter == 0)
                {
                    Console.WriteLine("Happy mouse! All the cheese is eaten, good night!");
                    MatrixPrint();
                    Environment.Exit(0);
                }


            }
            else if (area[mouse[0] + i, mouse[1] + j] == 'T')
            {
                Console.WriteLine("Mouse is trapped!");
                area[mouse[0] + i, mouse[1] + j] = 'M';
                area[mouse[0], mouse[1]] = '*';
                mouse[0] += i;
                mouse[1] += j;
                MatrixPrint();
                Environment.Exit(0);
            }
            else if (area[mouse[0] + i, mouse[1] + j] == '*')
            {
                area[mouse[0] + i, mouse[1] + j] = 'M';
                area[mouse[0], mouse[1]] = '*';
                mouse[0] += i;
                mouse[1] += j;

            }
            else if (area[mouse[0] + i, mouse[1] + j] == '@')
            {

            }
            

        }
        static void MatrixPrint()
        {
            for (int i = 0; i < n[0]; i++)
            {
                for (int j = 0; j < n[1]; j++)
                {
                    Console.Write(area[i,j]);

                }
                Console.WriteLine();
            }
        }
        
    }
}
