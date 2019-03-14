using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyContainer
{
    public class Container
    {
        private static Dictionary<string, RegisterInfo> ContainerDictionary = new Dictionary<string, RegisterInfo>();

        // 单例对象容器
        private static Dictionary<string, object> SingletonContainerDictionary = new Dictionary<string, object>();

        private static readonly object container_Lock = new object();

        public void RegisterType<IT, T>(RegisterInfo.LifeTimeEnum lifeTimeType = RegisterInfo.LifeTimeEnum.Transient, string name = "")
        {
            ContainerDictionary.Add(typeof(IT).FullName, new RegisterInfo()
            {
                TargetType = typeof(T),
                LifeTimeType = lifeTimeType,
                Name = name
            });
        }

        public IT Resolve<IT>()
        {
            var typeFullName = typeof(IT).FullName;
            var registerInfo = ContainerDictionary[typeFullName];
            var tType = registerInfo.TargetType;
            object t = default(IT);
            switch (registerInfo.LifeTimeType)
            {
                // 瞬时生命周期
                case RegisterInfo.LifeTimeEnum.Transient:
                    t = this.CreateObj(tType);
                    break;
                // 线程单例
                case RegisterInfo.LifeTimeEnum.PerThread:
                    // https://blog.csdn.net/qq_33649351/article/details/80016359
                    // http://www.cazzulino.com/callcontext-netstandard-netcore.html
                    break;
                // 单例
                case RegisterInfo.LifeTimeEnum.Singleton:
                    if (SingletonContainerDictionary.ContainsKey(typeFullName))
                    {
                        t = SingletonContainerDictionary[typeFullName];
                    }
                    else
                    {
                        lock (container_Lock)
                        {
                            if (SingletonContainerDictionary.ContainsKey(typeFullName))
                            {
                                t = SingletonContainerDictionary[typeFullName];
                            }
                            else
                            {
                                t = this.CreateObj(tType);
                                SingletonContainerDictionary.Add(typeFullName, t);
                            }
                        }
                    }
                    break;
                default:
                    break;
            }


            return (IT)t;
        }

        private object CreateObj(Type tType)
        {
            var ctorArr = tType.GetConstructors();
            System.Reflection.ConstructorInfo ctor;
            if (ctorArr.Count(p => p.IsDefined(typeof(MyInjectionConstructorAttribute), true)) > 0)
                ctor = ctorArr.Where(p => p.IsDefined(typeof(MyInjectionConstructorAttribute), true)).FirstOrDefault();
            else
                ctor = ctorArr.OrderBy(p => p.GetParameters().Length).FirstOrDefault();

            var ctorParams = ctor.GetParameters();
            // 如果是无参数构造函数，则直接创建
            if (ctorParams.Length == 0)
                return Activator.CreateInstance(tType);

            List<object> listPara = new List<object>();
            foreach (var para in ctorParams)
            {
                var parameterTypeFullName = para.ParameterType.FullName;

                var paraType = (Type)ContainerDictionary[parameterTypeFullName].TargetType;
                var obj = this.CreateObj(paraType);
                listPara.Add(obj);

            }
            return Activator.CreateInstance(tType, listPara.ToArray());

        }
    }
}
