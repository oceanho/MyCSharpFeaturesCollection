using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace await_async.AwaitAsync
{
    internal static class AwaitAsyncHelper
    {
        static AwaitAsyncHelper()
        {
        }

        public static void Run()
        {
            Console.WriteLine("我是主线程，线程ID：{0}", Thread.CurrentThread.ManagedThreadId);

            Console.WriteLine("RunTask() Before");
            RunTask();
            Console.WriteLine("RunTask() After");
            Console.WriteLine("\n\n\n");

            Console.WriteLine("RunTaskWithAwaitAsync() Before,不加 await 关键字调用");
            RunTaskWithAwaitAsync();
            Console.WriteLine("RunTaskWithAwaitAsync() After,此处的代码不会等待 RunTaskWithAwaitAsync() 运行完成就执行了。");
        }

        #region RunTask/GetResult

        private static void RunTask()
        {
            Console.WriteLine($"执行GetResult()之前的,时间：{DateTime.Now.ToMyStr()},线程ID：{Thread.CurrentThread.ManagedThreadId}");
            var resultTasker = Task.Run(() => { return GetResult(); });
            Console.WriteLine($"执行GetResult()之后的,时间：{DateTime.Now.ToMyStr()},线程ID：{Thread.CurrentThread.ManagedThreadId}");

            // 获取 Result 属性,将会阻塞执行。Task中对应的方法。
            Console.WriteLine(resultTasker.Result);
            Console.WriteLine($"得到结果的时间是：{DateTime.Now.ToMyStr()}");
        }

        private static string GetResult()
        {
            Console.WriteLine("我是GetResult()方法,Thread.Sleep(2000).");
            Thread.Sleep(2000);
            return "我是GetResult()返回值.";
        }
        #endregion

        #region RunTaskWithAwaitAsync

        private static async Task RunTaskWithAwaitAsync()
        {
            Console.WriteLine("GetResultAsync() Before，线程ID：{0}。时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToMyStr());
            Console.WriteLine("Execute codes：var resultTask = GetResultAsync()");
            var resultTask = GetResultAsync();
            Console.WriteLine("GetResultAsync() After，线程ID：{0}。时间：{1}", Thread.CurrentThread.ManagedThreadId, DateTime.Now.ToMyStr());
            Console.WriteLine("await resultTask 结果：{0}。时间：{1}", await resultTask, DateTime.Now.ToMyStr());
        }

        private static async Task<string> GetResultAsync()
        {
            Console.WriteLine("我是GetResultAsync()方法，执行 await Task.Run(...), 线程ID：{0}", Thread.CurrentThread.ManagedThreadId);
            return await Task.Run(() =>
            {
                Console.WriteLine("GetResultAsync()  --> Task.Run(...)，线程ID。" + Thread.CurrentThread.ManagedThreadId);
                Console.WriteLine("Task.Run(...) --> Thread.Sleep(2000) Before，时间：" + Thread.CurrentThread.ManagedThreadId);
                Thread.Sleep(2000);
                Console.WriteLine("Task.Run(...) --> Thread.Sleep(2000) After，时间：" + DateTime.Now.ToMyStr());
                return "我是GetResultAsync()返回值";
            });
        }
        #endregion
    }

    internal static class Extensions
    {
        public static string ToMyStr(this DateTime srcDateTime)
        {
            return srcDateTime.ToString("yyyy年MM月dd日 hh:MM:ss");
        }
    }
}
