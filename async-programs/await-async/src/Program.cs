using await_async.AwaitAsync;
using System;

namespace await_async
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("The await async Features.");
            AwaitAsyncHelper.Run();
            Console.ReadLine();
        }
    }
}
