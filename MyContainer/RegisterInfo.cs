using System;
using System.Collections.Generic;
using System.Text;

namespace MyContainer
{
    public class RegisterInfo
    {
        public RegisterInfo()
        {

        }
        public RegisterInfo(LifeTimeEnum lifeTimeType)
        {
            this.LifeTimeType = lifeTimeType;
        }
        public RegisterInfo(string name)
        {
            this.Name = name;
        }
        public RegisterInfo(string name, LifeTimeEnum lifeTimeType)
        {
            this.Name = name;
            this.LifeTimeType = lifeTimeType;
        }
        public Type TargetType { get; set; }

        public LifeTimeEnum LifeTimeType { get; set; }

        public string Name { get; set; }

        public enum LifeTimeEnum
        {
            Transient,
            PerThread,
            Singleton
        }
    }
}
