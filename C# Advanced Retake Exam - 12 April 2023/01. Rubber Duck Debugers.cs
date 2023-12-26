namespace ConsoleApp12
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] times = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] tasks = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            Queue<int> time = new Queue<int>(times);
            Stack<int> task = new Stack<int>(tasks);
            Dictionary<string, int> count = new Dictionary<string, int>()
            { {"Darth Vader Ducky",0 },{"Thor Ducky",0 },{"Big Blue Rubber Ducky",0 },{"Small Yellow Rubber Ducky",0 }};
            while (time.Count != 0|| task.Count!=0)
            {
               int currenttime = time.Dequeue();
                int currenttask = task.Pop();
                int mtp = currenttask * currenttime;
                if (mtp>=0&&mtp<=60)
                {
                    count["Darth Vader Ducky"]++;
                }
                else if (mtp >= 61 && mtp <= 120)
                {
                    count["Thor Ducky"]++;
                }
                else if (mtp >= 121 && mtp <= 180)
                {
                    count["Big Blue Rubber Ducky"]++;
                }
                else if (mtp >= 181 && mtp <= 240)
                {
                    count["Small Yellow Rubber Ducky"]++;
                }
                else
                {
                    currenttask -= 2;
                    task.Push(currenttask);
                    time.Enqueue(currenttime);
                }
            }
            Console.WriteLine("Congratulations, all tasks have been completed! Rubber ducks rewarded:");
            Console.WriteLine($"Darth Vader Ducky: {count["Darth Vader Ducky"]}");
            Console.WriteLine($"Thor Ducky: {count["Thor Ducky"]}");
            Console.WriteLine($"Big Blue Rubber Ducky: {count["Big Blue Rubber Ducky"]}");
            Console.WriteLine($"Small Yellow Rubber Ducky: {count["Small Yellow Rubber Ducky"]}");
        }
    }
}