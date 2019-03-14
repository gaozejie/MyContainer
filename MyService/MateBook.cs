using MyContainer;
using MyInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyService
{
    public class MateBook : IComputer
    {

        public MateBook()
        {
            Console.WriteLine($"{this.GetType().Name}通过无参数构造函数被创建...");
        }

        [MyInjectionConstructor]
        public MateBook(IKeyboard keyboard,IMotherBoard motherBoard)
        {
            Console.WriteLine($"{this.GetType().Name}通过两个参数构造函数被创建...");
        }
    }
}
