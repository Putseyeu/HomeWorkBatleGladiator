using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWorkGladiator2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Gladiator gladiatorOne;
            Gladiator gladiatorTwo;
            Arena arena = new Arena();
            Random random = new Random();
            int one = 1;
            int two = 2;
            int three = 3;
            int four = 4;
            int actionСhoice;

            Console.WriteLine("Выбирете первого бойца на арене");
            gladiatorOne = arena.ChoiceTypeGladiator();
            Console.WriteLine("Выбирете второго бойца на арене");
            gladiatorTwo = arena.ChoiceTypeGladiator();

            while (gladiatorOne.Health > 0 && gladiatorTwo.Health > 0)
            {
                actionСhoice = random.Next(one, four);
                if (actionСhoice == one)
                {
                    arena.DalingDamage(gladiatorOne, gladiatorTwo);
                }

                if (actionСhoice == two)
                {
                    Console.WriteLine($"{gladiatorOne.Name} использует навык !");
                    gladiatorOne.UseTalent(gladiatorOne, gladiatorTwo);
                }

                if (actionСhoice == three)
                {
                    Console.WriteLine($"{gladiatorTwo.Name} использует своё умениe!");
                    gladiatorTwo.UseTalent(gladiatorTwo, gladiatorOne);
                }

                arena.ShowHealth(gladiatorOne.Health, gladiatorOne.Name, gladiatorTwo.Health, gladiatorTwo.Name);
            }

            arena.DetermineWinner(gladiatorOne, gladiatorTwo);
            Console.ReadLine();
        }
    }

    class Arena
    {
        private Gladiator _gladiator;
        private string _userInput = "";
        private int _upHealth = 0;

        public Gladiator ChoiceTypeGladiator()
        {
            int numberSelect = 1;
            Console.WriteLine("Выбериите класс бойца." +
                "\n1 - Воин." +
                "\n2 - Боевой маг." +
                "\n3 - Паладин." +
                "\n4 - Шаман");

            while (numberSelect != 0)
            {
                switch (_userInput = Console.ReadLine())
                {
                    case "1":
                        _gladiator = new Warrior();
                        break;

                    case "2":
                        _gladiator = new Magician();
                        break;

                    case "3":
                        _gladiator = new Paladin();
                        break;

                    case "4":
                        _gladiator = new Shaman();
                        break;

                    default:
                        _gladiator = new Gladiator();
                        break;
                }

                numberSelect--;
            }

            return _gladiator;
        }

        public void DalingDamage(Gladiator gladiatorOne, Gladiator gladiatorTwo)
        {
            gladiatorOne.ChangeHealth(gladiatorOne.Health, gladiatorTwo.Damage, _upHealth);
            gladiatorTwo.ChangeHealth(gladiatorTwo.Health, gladiatorOne.Damage, _upHealth);
            Console.WriteLine("Гладиаторы обмениваются ударами");
        }

        public void DetermineWinner(Gladiator gladiatorOne, Gladiator gladiatorTwo)
        {
            if (gladiatorOne.Health > 0)
            {
                Console.WriteLine($"победил {gladiatorOne.Name}");
            }

            else if (gladiatorTwo.Health > 0)
            {
                Console.WriteLine($"победил {gladiatorTwo.Name}");
            }

            else if (gladiatorOne.Health < 0 && gladiatorTwo.Health < 0)
            {
                Console.WriteLine("Оба гладиатора погибли!");
            }
        }

        public void ShowHealth(int healthOne, string nameOne, int healthTwo, string nameTwo)
        {
            Console.WriteLine($"У {nameOne} Осталось здоровья {healthOne}\nУ {nameTwo} Осталось здоровья {healthTwo}");
            Console.WriteLine();
        }
    }
}

class Gladiator
{
    private int _health = 100;
    protected int _damage = 15;
    private Random random = new Random();

    public string Name { get; private set; }
    public int Health => _health;
    public int Damage => _damage;

    public Gladiator()
    {
        SetName();
    }

    public virtual void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
    {

    }

    public int ChangeHealth(int health, int damage, int upHealth)
    {
        int minDamage = 0;
        int maxDamage = damage;

        health -= random.Next(minDamage, maxDamage);
        health += upHealth;
        _health = health;
        return Health;
    }

    public void SetName()
    {
        Console.WriteLine("Боец на арене ! Назовите своё имя!");
        Name = Console.ReadLine();
    }
}

class Magician : Gladiator
{
    public override void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
    {
        int upHealth = 0;
        int magicDamage = 10;
        Console.WriteLine("Маг успел  произнести заклинание холода и противник не может ответить противника");
        gladiatorTwo.ChangeHealth(gladiatorTwo.Health, magicDamage, upHealth);
    }
}

class Warrior : Gladiator
{
    public override void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
    {

        int upHealth = 0;
        int additionalDamage = 30;
        Console.WriteLine("Воин размашисто рубит противника. урон увеличен!");
        gladiatorOne.ChangeHealth(gladiatorOne.Health, gladiatorTwo.Damage, upHealth);
        gladiatorTwo.ChangeHealth(gladiatorTwo.Health, additionalDamage, upHealth);
    }
}

class Paladin : Gladiator
{
    public override void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
    {
        int upHealth = 5;
        int damage = 0;
        Console.WriteLine("Воин паладин произносит молитву и залечивает раны.");
        gladiatorOne.ChangeHealth(gladiatorOne.Health, damage, upHealth);
    }
}

class Shaman : Gladiator
{
    public override void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
    {
        if (gladiatorOne.Health < gladiatorTwo.Health)
        {
            int damage = 0;
            Console.WriteLine("Шаман использует силу духа и выравнивает здоровье с противником!");
            int upHealt = gladiatorTwo.Health - gladiatorOne.Health;
            gladiatorOne.ChangeHealth(gladiatorOne.Health, damage, upHealt);
        }
    }
}

