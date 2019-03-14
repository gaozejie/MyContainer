using MyContainer;
using MyInterface;
using MyService;
using System;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            Container container = new Container();
            container.RegisterType<IComputer, MateBook>();
            container.RegisterType<IKeyboard, Keyboard>();
            container.RegisterType<IMotherBoard, MotherBoard>();
            container.RegisterType<ICPU, CPU>();
            container.RegisterType<IAudioCard, AudioCard>();

            var computer = container.Resolve<IComputer>();

            Console.WriteLine("Hello World!");
        }
    }
}
