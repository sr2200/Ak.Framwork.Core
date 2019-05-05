using Akf.Core.Aspect;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Composition;
using System.Reflection;
using System.Text;
using System.Threading;

namespace Akf.Core.Test.Aspect
{
    [Export(typeof(IAkAspectParts))]
    public class AkTestAspect : IAkAspectParts
    {
        public void PreProcess(Guid id, MethodInfo targetMethod, object[] args)
        {
            Console.WriteLine($"{targetMethod.Name} を実行します。");

            List<string> logList = GetLogList();
            logList.Add($"{targetMethod.Name} を実行します。");
        }

        public void PostProcess(Guid id, MethodInfo targetMethod, object[] args, object result)
        {
            Console.WriteLine($"{targetMethod.Name} の実行が終了しました。");

            List<string> logList = GetLogList();
            logList.Add($"{targetMethod.Name} の実行が終了しました。");
        }

        public void ExceptionProcess(Guid id, MethodInfo targetMethod, object[] args, Exception ex)
        {
            Console.WriteLine("例外が発生しました。");

            List<string> logList = GetLogList();
            logList.Add("例外が発生しました。");
        }

        private static List<string> GetLogList()
        {
            List<string> logList = LocalContext.GetData("AkLog") as List<string>;
            if (logList == null)
            {
                logList = new List<string>();
                LocalContext.SetData("AkLog", logList);
            }

            return logList;
        }
    }

    public static class LocalContext
    {
        static ConcurrentDictionary<string, AsyncLocal<object>> state =
            new ConcurrentDictionary<string, AsyncLocal<object>>();

        public static void SetData(string name, object data) =>
            state.GetOrAdd(name, _ => new AsyncLocal<object>()).Value = data;

        public static object GetData(string name) =>
            state.TryGetValue(name, out AsyncLocal<object> data) ? data.Value : null;
    }
}
