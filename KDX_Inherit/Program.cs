using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    继承
 */
namespace KDX_Inherit
{
    // 父类-动物类
    abstract public class Pet
    {
        public Pet(string name)
        {
            _name = name;
        }
        protected string _name;//受保护的
        public void PrintName()
        {
            Console.WriteLine("父类中---动物类---名字的方法---{0}", _name);
        }

        /*
        // 父类虚方法（供子类重写）
        virtual public void Speak(string speak)
        {
            Console.WriteLine("父类中---动物类---说话的方法---{0}", speak);
        }
        */

        // 将虚方法修改为 抽象方法
        abstract public void Speak(string speak);
    }

    // 子类-dog类，继承动物类
    public class Dog : Pet
    {
        // 构造函数
        public Dog(string name) : base(name)
        {
            //_name = name;
            // base(name) 调用基类的构造函数
        }

        // 重写父类方法
        override public void Speak(string speak)
        {
            Console.WriteLine("父类中---狗类---说话的方法---{0}", speak);
        }
    }

    // 子类-cat类，继承动物类
    // 隐藏父类方法 -- 类似重写
    public class Cat : Pet
    {
        // 构造函数
        public Cat(string name) : base(name)
        {
            //_name = name;
            // base(name) 调用基类的构造函数
        }

        // 隐藏原有的打印名字方法
        new public void PrintName()
        {
            Console.WriteLine("子类中---猫类---名字的方法-----{0}", _name);
        }

        // 重写父类方法
        override public void Speak(string speak)
        {
            Console.WriteLine("父类中---猫类---说话的方法---{0}", speak);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            /*  没有添加构造方法之前
             *  
             *  
            // 初始化dog类
            Dog aDog = new Dog();
            aDog.Name = "旺财";
            aDog.PrintName();

            //隐藏方法测试
            Cat aCat = new Cat();
            aCat.Name = "Alis";
            aCat.PrintName();// 打印子类cat中的PrintName方法

            Pet aCat01 = new Cat();
            aCat01.Name = "Alis";
            aCat01.PrintName();// 打印父类pet中的PrintName方法
            //Tip：狗和猫都继承Pet类，如果没有使用多态，子类初始化的时候引用是父类，那就会调用父类的方法


            //虚方法和多态测试-----目的：子类初始化的时候引用父类，但是调用的还是子类自己的方法
            Pet aDog01 = new Dog();
            aDog01.Speak("狗说你好！");// 打印子类Dog中的Speak方法

            Pet aCat02 = new Cat();
            aCat02.Speak("猫咪说你好！");// 打印子类Cat中的Speak方法
            */

        /*
            添加构造方法之后，可以直接一个数组来做上面的事
         */
        Pet[] pets = new Pet[] { new Dog("Jick"), new Cat("Tom")};
            for (int i = 0; i < pets.Length; i++)
            {
                pets[i].Speak("你好");
                /* 打印结果
                   父类中---狗类---说话的方法---你好
                   父类中---猫类---说话的方法---你好
                 */

                pets[i].PrintName(); // 没有重写PrintName方法，所以打印的是父类中的PrintName方法
            }


            Console.Read();
        }
    }
}
/*
 虚方法：
 声明为virtual的方法就是虚方法。基类的虚方法可以在派生类中使用override进行重写

 多态：
 通过指向派生类的基类引用，调用虚函数，会根据引用所指向派生类的实际类型，调用派生类中的同名重写函数，便是多态
*/