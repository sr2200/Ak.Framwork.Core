using System;
using System.Collections.Generic;
using System.Composition.Hosting;
using System.Reflection;
using System.Text;

namespace Ak.Framework.Core.Aspect
{
    public class AkProxy<T> : DispatchProxy
    {
        private T _instance;


        public List<IAkAspectParts> AkAspectPartsList { get; set; }


        public AkProxy()
        {
            AkAspectPartsList = new List<IAkAspectParts>();
            ComposeParts();
        }


        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            Guid id = Guid.NewGuid();
            try
            {
                AkAspectAttribute attribute =
                    targetMethod.GetCustomAttribute(typeof(AkAspectAttribute)) as AkAspectAttribute;

                object result;
                if (attribute != null && attribute.IsInjection)
                {
                    PreProcess(id, targetMethod, args);
                    result = targetMethod.Invoke(_instance, args);
                    PostProcess(id, targetMethod, args, result);
                }
                else if (attribute == null)
                {
                    PreProcess(id, targetMethod, args);
                    result = targetMethod.Invoke(_instance, args);
                    PostProcess(id, targetMethod, args, result);
                }
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


        public static T Create(T instance)
        {
            object proxy = Create<T, AkProxy<T>>();
            ((AkProxy<T>)proxy).SetParameters(instance);

            return (T)proxy;
        }


        private void SetParameters(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            _instance = instance;
        }


        private void ComposeParts()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            foreach (var asm in assemblies)
            {
                var configuration = new ContainerConfiguration().WithAssembly(asm);
                List<IAkAspectParts> tmpLst;
                using (var container = configuration.CreateContainer())
                {
                    tmpLst = new List<IAkAspectParts>(container.GetExports<IAkAspectParts>());
                    AkAspectPartsList.AddRange(tmpLst);
                }
            }
        }

        private void PreProcess(Guid id, MethodInfo targetMethod, object[] args)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.PreProcess(id, targetMethod, args);
            }
        }

        private void PostProcess(Guid id,
                                    MethodInfo targetMethod,
                                    object[] args,
                                    object result)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.PostProcess(id, targetMethod, args, result);
            }
        }

        private void ExceptionProcess(Guid id,
                                        MethodInfo targetMethod,
                                        object[] args,
                                        Exception ex)
        {
            foreach (var item in AkAspectPartsList)
            {
                item.ExceptionProcess(id, targetMethod, args, ex);
            }
        }

    }
}
