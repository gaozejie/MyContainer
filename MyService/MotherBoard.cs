using MyInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyService
{
    public class MotherBoard : IMotherBoard
    {
        public MotherBoard()
        {
            Console.WriteLine($"{this.GetType().Name}通过无参数构造函数被创建...");
        }
        public MotherBoard(ICPU cPU, IAudioCard audioCard)
        {
            Console.WriteLine($"{this.GetType().Name}通过两个参数构造函数被创建...");
        }
    }
}
