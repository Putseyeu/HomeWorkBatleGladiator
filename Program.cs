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
                    arena.UseTalent(gladiatorOne, gladiatorTwo);
                }

                if (actionСhoice == three)
                {
                    Console.WriteLine($"{gladiatorTwo.Name} использует своё умениe!");
                    arena.UseTalent(gladiatorTwo, gladiatorOne);
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
        private int _zeroDamage = 0;
        Random random = new Random();      

        public Gladiator ChoiceTypeGladiator()
        {
            bool done = false;
            Console.WriteLine("Выбериите класс бойца." +
                "\n1 - Воин." +
                "\n2 - Боевой маг." +
                "\n3 - Паладин." +
                "\n4 - Шаман");

            while (done != true)
            {
                switch (_userInput = Console.ReadLine())
                {
                    case "1":
                        _gladiator = new Warrior();
                        done = true;
                        break;

                    case "2":
                        _gladiator = new Magician();
                        done = true;
                        break;

                    case "3":
                        _gladiator = new Paladin();
                        done = true;
                        break;

                    case "4":
                        _gladiator = new Shaman();
                        done = true;
                        break;

                        default:
                        Console.WriteLine("В сражение учавствует обычный боец");
                        _gladiator = new Gladiator();
                        done = true;
                        break;
                }
            }

            return _gladiator;
        }

        public void DalingDamage(Gladiator gladiatorOne, Gladiator gladiatorTwo)
        {
            gladiatorOne.ChangeHealth(gladiatorOne.Health, gladiatorTwo.Damage, _upHealth);
            gladiatorTwo.ChangeHealth(gladiatorTwo.Health, gladiatorOne.Damage, _upHealth);
            Console.WriteLine("Гладиаторы обмениваются ударами");                      
        }
        
        public void UseTalent(Gladiator gladiatorOne, Gladiator gladiatorTwo)
        {
            if (gladiatorOne is Warrior warrior)
            {
                int damage = warrior.UseForce();
                gladiatorOne.ChangeHealth(gladiatorOne.Health, gladiatorTwo.Damage, _upHealth);
                gladiatorTwo.ChangeHealth(gladiatorTwo.Health, damage, _upHealth);
            }

            if (gladiatorOne is Magician magician)
            {
                int damageMag = magician.UseMagic();
                gladiatorTwo.ChangeHealth(gladiatorTwo.Health, damageMag, _upHealth);
            }

            if (gladiatorOne is Paladin paladin)
            {
                _upHealth = paladin.UsePrayer();
                gladiatorOne.ChangeHealth(gladiatorOne.Health, _zeroDamage, _upHealth);
                _upHealth = 0;
            }

            if (gladiatorOne is Shaman shaman)
            {
                if (gladiatorOne.Health < gladiatorTwo.Health)
                {
                    _upHealth = shaman.UseExchange(gladiatorOne.Health, gladiatorTwo.Health);
                    gladiatorOne.ChangeHealth(gladiatorOne.Health, _zeroDamage, _upHealth);
                    _upHealth = 0;
                }
            }
        }

        public void DetermineWinner (Gladiator gladiatorOne, Gladiator gladiatorTwo)
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
        Console.WriteLine("Вы на арене ! Назовите своё имя!");
        Name = Console.ReadLine();
    }
}

class Magician : Gladiator
{
    public int UseMagic()
    {
        int magicDamage = 10;
        Console.WriteLine("Маг успел  произнести заклинание холода и противник не может ответить противника");
        return magicDamage;
    }
}

class Warrior : Gladiator
{
    public int UseForce()
    {
        int additionalDamage = 30;
        Console.WriteLine("Воин размашисто рубит противника. урон увеличен!");
        return additionalDamage;       
    }
}

class Paladin : Gladiator
{
    public int UsePrayer()
    {
        int upHealth = 5;
        Console.WriteLine("Воин паладин произносит молитву и залечивает раны.");
        return upHealth;        
    }
}

class Shaman : Gladiator
{
    public int UseExchange(int healtOne, int heltTwo)
    {
        Console.WriteLine("Шаман использует силу духа и выравнивает здоровье с противником!");
        int upHealt = heltTwo - healtOne;
        return upHealt;
    }
}

