using MyInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyService
{
    public class Keyboard : IKeyboard
    {
        public Keyboard()
        {
            Console.WriteLine($"{this.GetType().Name}通过无参数构造函数被创建...");
        }
    }
}
