using System;
using System.Threading;
using System.Threading.Tasks;

namespace AwaitAsync
{
    class Program
    {
        static void Main(string[] args)
        {
            Operation operation = new Operation();

            //Task<string> result = operation.RunSlowOperationTask();
            Task<string> result = operation.RunSlowOperationTaskAsync();
            operation.RunTrivialOperation();

            Console.WriteLine("Return value of slow operation: {0}", result.Result);
            Console.WriteLine("The main thread has run complete on thread number {0}", Thread.CurrentThread.ManagedThreadId);
            Console.ReadLine();
        }
    }

    public class Operation
    {
        public Task<string> RunSlowOperationTask()
        {
            return Task.Factory.StartNew<string>(RunSlowOperation);
        }

        public async Task<string> RunSlowOperationTaskAsync()
        {
            Console.WriteLine("Slow operation running on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine("Slow operation about to finish on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            return "This is very slow...";
        }

        public string RunSlowOperation()
        {
            Console.WriteLine("Slow operation running on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            Thread.Sleep(2000);
            Console.WriteLine("Slow operation about to finish on thread id {0}", Thread.CurrentThread.ManagedThreadId);
            return "This is very slow...";
        }

        public void RunTrivialOperation()
        {
            string[] words = new string[] { "i", "love", "dot", "net", "four", "dot", "five" };
            foreach (string word in words)
            {
                Console.WriteLine(word);
            }
        }
    }
}
