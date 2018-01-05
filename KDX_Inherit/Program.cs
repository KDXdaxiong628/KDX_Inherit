using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*
    继承demo
 */
namespace KDX_Inherit
{
    // 定义一个结构 - 鱼  结构存储在栈中,这里定义了一个食物鱼的结构，不需要长期存贮，吃了就没有了
    struct fish
    {
        int weight;
        int size;
        int type;
    }

    // 定义一个接口 - 抓住一只老虎苏
    interface ICatchMice // 接口方法名首字母一般都是I 寓意inerface
    {
        void CatchMice();// 默认是pubulic，但是不能加任何访问修饰符。不能有实现
    }

    // 定义一个 爬树接口
    interface IClimbTree 
    {
        void ClimbTree();
    }

    // 父类-动物类
    abstract public class Pet
    {
        public Pet(string name)
        {
            _name = name;
            _age = 0;// 测试重载运算符
        }
        protected string _name;//受保护的
        protected int _age;
        public void PrintName()
        {
            Console.WriteLine("父类中---动物类---名字的方法---{0}", _name);
        }
        public void ShowAge()
        {
            Console.WriteLine(_name + "`s Age = " + _age);
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

        // 重载运算符，让年龄自增
        public static Pet operator ++(Pet pet)
        {
            ++pet._age;
            return pet;
        }
    }

    // 子类-dog类，继承动物类
    public class Dog : Pet
    {
        // 添加静态成员
        static int Num;
        static Dog()
        {
            Num = 0;
        }
        // 构造函数
        public Dog(string name) : base(name)
        {
            //_name = name;
            // base(name) 调用基类的构造函数
            ++Num;
        }

        // 重写父类方法
        override public void Speak(string speak)
        {
            Console.WriteLine("父类中---狗类---说话的方法---{0}", speak);
        }

        static public void ShowDogNum()
        {
            Console.WriteLine("狗的数量 = " + Num);
        }

        // 自定义转换 - 隐式转换   把狗转换成猫
        public static implicit operator Cat(Dog aDog)
        {
            return new Cat(aDog._name);
        }

        // 泛型方法
        public void IsHappy<T>(T target)
        {
            Console.WriteLine("IsHappy: " + target.ToString());
        }

        // 为泛型方法添加约束  必须传入一个class
        public void IsHappy01<T>(T target) where T : class
        {
            Console.WriteLine("IsHappy: " + target.ToString());
        }
    }

    /*
     * 使用静态类来扩展方法
     */
    static class PetGuide
    {
        static public void HowToFeed(this Dog dog)
        {
            Console.WriteLine("Play a video about how to feed dogs");
        }
    }

    // 子类-cat类，继承动物类
    // 隐藏父类方法 -- 类似重写
    public class Cat : Pet, ICatchMice, IClimbTree
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
        /*
        override public void Speak(string speak)
        {
            Console.WriteLine("父类中---猫类---说话的方法---{0}", speak);
        }*/

        /*
         密闭类 - 有些类不希望被其他人通过继承来修改
         密闭方法 - 不希望某个方法被重写
         */
        // 添加sealed 使Speak方法变成 密封类 方法，不被子类重写等。（解释：比如猫类，它的叫声都是“miao miao”,不可能有别的，所以可以把speak方法变成密封类，猫下面的子类就不能改动）
        sealed override public void Speak(string speak)
        {
            Console.WriteLine(_name + "父类中---猫类---说话的方法---{0}", speak);
        }

        // 因为Cat类添加了接口，所以必须要实现，而且加上public
        public void CatchMice()
        {
            Console.WriteLine("Catch Mice");
        }

        public void ClimbTree()
        {
            Console.WriteLine("Climb Treee");
        }

        // 猫 显示 的转换成 狗
        public static explicit operator Dog(Cat aCat)
        {
            return new Dog(aCat._name);
        }


    }

    // Cat的子类 - 波斯猫
    public class PersianCat : Cat
    {
        public PersianCat(string name) : base(name)
        {

        }

        // 如果在波斯猫这个类里面重写speak方法，编译器会报错
        /*
        override public void Speak(string speak)
        {
            Console.WriteLine(_name + "父类中---猫类---说话的方法---{0}", speak);
        }*/

    }

    // 泛型测试 - 笼子 泛型类
    public class Cage<T>
    {
        T[] array;// 存放东西的数组
        readonly int Size;
        int num;
        public Cage(int n)
        {
            Size = n;
            num = 0;
            array = new T[Size];
        }

        public void Putin(T pet)
        {
            if (num < Size)
            {
                array[num++] = pet;
            }
            else
            {
                Console.WriteLine("cage is full");
            }
        }

        public T Takeout()
        {
            if (num > 0)
            {
                return array[--num];// 移除数组最后一个
            }
            else
            {
                Console.WriteLine("cage is empty");
                return default(T);
            }
        }

    }

    public class Person
    {

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

            Cat cat01 = new Cat("Tom02");
            ICatchMice catchMice = (ICatchMice)cat01;// 强制转换
            cat01.CatchMice();
            catchMice.CatchMice();

            // 调用静态函数 - 直接用类名调用
            Dog.ShowDogNum();

            // 调用静态类  Dog类本身没有这个方法，是使用静态类来添加的
            Dog dog = new Dog("huahua");
            dog.HowToFeed();

            Dog dog02 = new Dog("wangcai");
            dog02.Speak("wangwang");
            // 开始转换
            Cat cat02 = dog02;
            cat02.PrintName();// 打印出名字还是叫wangcai,只是现在是一只猫

            Dog dog03 = (Dog)cat02; // 显示转换 ，要添加转换类型
            dog03.PrintName();


            // 测试重载运算符
            {
                Pet[] pets01 = new Pet[] { new Dog("Jick012"), new Cat("Tom023") };
                for (int i = 0; i < pets01.Length; i++)
                {
                    pets01[i]++;
                    pets01[i].ShowAge();

                }
            }

            // 泛型测试
            var dogCage = new Cage<Dog>(1);
            dogCage.Putin(new Dog("A"));
            dogCage.Putin(new Dog("B"));

            var dogTemp = dogCage.Takeout();
            dogTemp.PrintName();

            // 调用泛型方法
            var dog04 = new Dog("C");
            dog04.IsHappy<int>(3);

            // 调用有约束的泛型方法 - 只能传入class
            var dog05 = new Dog("D");
            dog05.IsHappy01<Person>(new Person());


            // 集合
            List<Dog> list = new List<Dog>();
            list.Add(new Dog("S"));
            list.Add(new Dog("SS"));
            list.Add(new Dog("SSS"));
            for (int i = 0; i < list.Count; i++)
            {
                list[i].PrintName();
            }

            Dictionary<string, Dog> dic = new Dictionary<string, Dog>();
            dic.Add("One", new Dog("B"));
            dic.Add("Two", new Dog("BB"));
            dic.Add("Three", new Dog("BBB"));
            dic["Two"].PrintName();

            // 栈 - 先进后出 ----  集合
            Stack<Pet> stack = new Stack<Pet>();
            stack.Push(new Dog("A"));
            stack.Push(new Cat("B"));

            stack.Peek().PrintName();// 打印最顶部的数据
            stack.Pop();
            stack.Peek().PrintName();

            // 队列  先进先出
            Queue<Pet> queue = new Queue<Pet>();
            queue.Enqueue(new Dog("Aaaaaaaa"));
            queue.Enqueue(new Dog("Bbbbbbbb"));
            queue.Enqueue(new Dog("Cccccccc"));

            Pet pet = null;
            pet = queue.Dequeue();// 出去
            pet.PrintName();
            pet = queue.Dequeue();// 出去
            pet.PrintName();
            pet = queue.Dequeue();// 出去
            pet.PrintName();

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