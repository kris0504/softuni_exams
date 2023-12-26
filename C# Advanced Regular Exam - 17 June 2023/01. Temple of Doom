namespace temple_of_doom
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] t = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            int[] s = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            List<int> chal = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            Queue<int> tools = new Queue<int>(t);
            Stack<int> substances = new Stack<int>(s);
            while (chal.Count > 0 && tools.Count > 0&&substances.Count>0)
            {
                int currentTool = tools.Dequeue();
                int currentSubstance = substances.Pop();
                int mult = currentSubstance * currentTool;
                if (chal.Remove(mult)) ;
                else
                {
                    currentTool++;
                    tools.Enqueue(currentTool);
                    currentSubstance--;
                    if (currentSubstance > 0) substances.Push(currentSubstance);
                }

            }
            if (chal.Count>0)
            {
                Console.WriteLine("Harry is lost in the temple. Oblivion awaits him.");
                if (tools.Count > 0) Console.WriteLine($"Tools: {string.Join(", ", tools)}");
                if (substances.Count > 0) Console.WriteLine($"Substances: {string.Join(", ", substances)}");
                 Console.WriteLine($"Challenges: {string.Join(", ", chal)}");
            }
            else
            {
                Console.WriteLine("Harry found an ostracon, which is dated to the 6th century BCE.");
                if (tools.Count > 0) Console.WriteLine($"Tools: {string.Join(", ", tools)}");
                if (substances.Count > 0) Console.WriteLine($"Substances: {string.Join(", ", substances)}");
            }


        }
    }
}
