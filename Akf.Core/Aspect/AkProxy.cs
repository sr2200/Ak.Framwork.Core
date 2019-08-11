using Akf.Core.Aspect.Parts;
using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.IO;
using System.Reflection;
using System.Text;

namespace Akf.Core.Aspect
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AkProxy<T> : DispatchProxy
    {
        /// <summary>
        /// 
        /// </summary>
        private T _instance;

        /// <summary>
        /// 
        /// </summary>
        private List<IAkAspectParts> AkAspectPartsList { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AkProxy{T}"/> class.
        /// </summary>
        public AkProxy()
        {
            if (File.Exists("appsettings.json"))
            {
                AkAspectPartsList = AkAspectUtility.GetComposePartsForSettingsFile("appsettings.json");
            }
            else
            {
                throw new ApplicationException("設定ファイルが存在しません。");
            }
        }

        /// <summary>
        /// メソッドを実行します。
        /// </summary>
        /// <param name="targetMethod">対象のメソッド情報</param>
        /// <param name="args">引数</param>
        /// <returns>呼び出されたメソッドの戻り値を格納している Object</returns>
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Guid id = Guid.NewGuid();
            try
            {
                AkAspectAttribute attribute =
                    targetMethod.GetCustomAttribute(typeof(AkAspectAttribute)) as AkAspectAttribute;

                object result;
                if ((attribute != null && attribute.IsInjection) || attribute == null)
                {
                    PreProcess(id, targetMethod, args);
                    result = targetMethod.Invoke(_instance, args);
                    PostProcess(id, targetMethod, args, result);
                }
                //else if (attribute == null)
                //{
                //    PreProcess(id, targetMethod, args);
                //    result = targetMethod.Invoke(_instance, args);
                //    PostProcess(id, targetMethod, args, result);
                //}
                else
                {
                    result = targetMethod.Invoke(_instance, args);
                }

                return result;
            }
            catch (Exception ex) when (ex is TargetInvocationException)
            {
                ExceptionProcess(id, targetMethod, args, ex);
                throw ex.InnerException ?? ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        internal static T Create(T instance)
        {
            object proxy = Create<T, AkProxy<T>>();
            ((AkProxy<T>)proxy).SetParameters(instance);

            return (T)proxy;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="instance"></param>
        private void SetParameters(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            _instance = instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        private void PreProcess(Guid id, MethodInfo targetMethod, object[] args)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.PreProcess<T>(id, targetMethod, _instance, args);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        /// <param name="result"></param>
        private void PostProcess(Guid id,
                                    MethodInfo targetMethod,
                                    object[] args,
                                    object result)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.PostProcess<T>(id, targetMethod, _instance, args, result);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="targetMethod"></param>
        /// <param name="args"></param>
        /// <param name="ex"></param>
        private void ExceptionProcess(Guid id,
                                        MethodInfo targetMethod,
                                        object[] args,
                                        Exception ex)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.ExceptionProcess<T>(id, targetMethod, _instance, args, ex);
            }
        }

    }
}
