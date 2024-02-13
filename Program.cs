namespace ConsoleApp3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Message message = new Message();
            string[] qatorlar = { "GetAll", "Insertion", "ShowMessages" };
            int cursor = 0;
            ConsoleKeyInfo key;
            
            while (true)
            {
                for (int i = 0; i < qatorlar.Length; i++)
                {
                    if (i == cursor)
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\n\t[{qatorlar[i]}]");
                        Console.ForegroundColor = ConsoleColor.Cyan;
                    }
                    else
                        Console.WriteLine($" {qatorlar[i]}");
                }
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.UpArrow && cursor > 0)
                    cursor--;
                else if (key.Key == ConsoleKey.DownArrow && cursor < qatorlar.Length - 1)
                    cursor++;
                if (key.Key == ConsoleKey.Enter)
                {
                    if (cursor == 0)
                    {
                        Console.Clear();
                        message.GetAll();
                        Console.ReadKey();
                    }
                    if (cursor == 1)
                    {
                        Console.Clear();
                        message.Insertion();
                        message.Insertion2();
                        Console.ReadKey();
                    }
                    if (cursor == 2)
                    {
                        Console.Clear();
                        message.GetMsg();
                        Console.ReadKey();
                    }
                }
                Console.Clear();
            }

        }
    }
}