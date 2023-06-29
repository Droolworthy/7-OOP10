//Есть 2 взвода. 1 взвод страны один, 2 взвод страны два.
//Каждый взвод внутри имеет солдат.
//Нужно написать программу, которая будет моделировать бой этих взводов.
//Каждый боец - это уникальная единица, он может иметь уникальные способности или же уникальные характеристики, такие как повышенная сила.
//Побеждает та страна, во взводе которой остались выжившие бойцы.
//Не важно, какой будет бой, рукопашный, стрелковый.
namespace OOP10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandFightFighters = "1";
            const string CommandExit = "2";

            List<Fighter> firstSquad = new List<Fighter>();
            List<Fighter> secondSquad = new List<Fighter>();

            Squad firstPlatoon = new(firstSquad);
            Squad secondPlatoon = new(secondSquad);
            Arena arena = new Arena(firstPlatoon, secondPlatoon);

            bool isWorking = true;

            Console.WriteLine($"{CommandFightFighters} - БИТВА БОЙЦОВ" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                if (CommandFightFighters == userInput)
                {
                    arena.Fight();
                }
                else if (userInput == CommandExit)
                {
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine($"Ошибка. Введите {CommandFightFighters} или {CommandExit}");
                }
            }
        }
    }

    class UserUtils
    {
        private static Random _random = new Random();

        public static int GenerateRandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }

    class Arena
    {
        private Squad _firstSquad;
        private Squad _secondSquad;

        public Arena(Squad firstSquad, Squad secondSquad)
        {
            _firstSquad = firstSquad;
            _secondSquad = secondSquad;
        }

        public void Fight()
        {
            while (_firstSquad.Count > 0 && _secondSquad.Count > 0)
            {
                ShowListSquad(_firstSquad);

                Fighter firstFighter = _firstSquad.GetFighter(UserUtils.GenerateRandomNumber(0, _firstSquad.Count));
                Console.WriteLine($"Вы выбрали бойца - {firstFighter.Name}");

                ShowListSquad(_secondSquad);

                Fighter secondFighter = _secondSquad.GetFighter(UserUtils.GenerateRandomNumber(0, _secondSquad.Count));
                Console.WriteLine($"Вы выбрали бойца - {secondFighter.Name}");

                FightSquads(firstFighter, secondFighter);

                ShowWinningCountry();
            }
        }

        private void FightSquads(Fighter firstFighter, Fighter secondFighter)
        {
            Console.WriteLine("\nВОЙНА НАЧИНАЕТСЯ!");
            Console.WriteLine();

            while (firstFighter.Health > 0 && secondFighter.Health > 0)
            {
                firstFighter.Attack(secondFighter);
                secondFighter.Attack(firstFighter);
                firstFighter.ShowInfoSoldier();
                secondFighter.ShowInfoSoldier();

                Console.WriteLine("---------------------------------------------------");
                Console.ReadKey();

                ShowWinningFighter(firstFighter, secondFighter);

                RemoveCombatantSquad(firstFighter, secondFighter);
            }
        }

        private void ShowWinningFighter(Fighter firstFighter, Fighter secondFighter)
        {
            if (firstFighter.Health <= 0)
            {
                if (secondFighter.Health <= 0)
                {
                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    Console.WriteLine("\nПобеда бойца - " + secondFighter.Name);
                    Console.ReadKey();
                }
            }
            else if (secondFighter.Health <= 0)
            {
                if (firstFighter.Health <= 0)
                {
                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    Console.WriteLine("\nПобеда бойца - " + firstFighter.Name);
                    Console.ReadKey();
                }
            }
        }

        private void RemoveCombatantSquad(Fighter firstFighter, Fighter secondFighter)
        {
            if (firstFighter.Health <= 0)
            {
                if (secondFighter.Health <= 0)
                {
                    _firstSquad.RemoveFighter(firstFighter);
                    _secondSquad.RemoveFighter(secondFighter);
                }
                else
                {
                    _firstSquad.RemoveFighter(firstFighter);
                }
            }
            else if (secondFighter.Health <= 0)
            {
                if (firstFighter.Health <= 0)
                {
                    _firstSquad.RemoveFighter(firstFighter);
                    _secondSquad.RemoveFighter(secondFighter);
                }
                else
                {
                    _secondSquad.RemoveFighter(secondFighter);
                }
            }
        }

        private void ShowWinningCountry()
        {
            string firstCountry = "Венгрия";
            string secondCountry = "Румыния";

            if (_firstSquad.Count <= 0 & _secondSquad.Count <= 0)
            {
                Console.WriteLine("\nНичья. Победила дружба.");
                return;
            }
            else if (_firstSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {firstCountry} побеждает!");
                return;
            }
            else if (_secondSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {secondCountry} побеждает!");
                return;
            }
        }

        private void ShowListSquad(Squad squad)
        {
            Console.WriteLine($"\nСписок бойцов:");

            squad.ShowInfoFighters();
        }
    }

    class Squad
    {
        private List<Fighter> _fighters = new List<Fighter>();

        public Squad(List<Fighter> fighters)
        {
            _fighters = fighters;

            Fill(fighters);
        }

        public int Count => _fighters.Count;

        public Fighter GetFighter(int index)
        {
            if (index >= 0 && index < _fighters.Count)
            {
                return _fighters[index];
            }

            return _fighters[_fighters.Count - 1];
        }

        public void ShowInfoFighters()
        {
            for (int i = 0; i < _fighters.Count; i++)
            {
                Console.Write(i + ". ");
                _fighters[i].ShowInfoSoldier();
            }
        }

        public void RemoveFighter(Fighter fighter)
        {
            _fighters.Remove(fighter);
        }

        private void Fill(List<Fighter> fighters)
        {
            fighters.Add(new Wrestler(nameof(Wrestler), 100, 20, 100));
            fighters.Add(new Kickboxer(nameof(Kickboxer), 80, 15));
            fighters.Add(new Boxer(nameof(Boxer), 70, 20));
            fighters.Add(new SumoWrestler(nameof(SumoWrestler), 90, 15));
            fighters.Add(new Karateka(nameof(Karateka), 75, 22));
            fighters.Add(new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 90, 30));
            fighters.Add(new Сapoeirista(nameof(Сapoeirista), 100, 15));
            fighters.Add(new Aikidoka(nameof(Aikidoka), 80, 25));
        }
    }

    abstract class Fighter
    {
        public Fighter(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            Name = nameCombatant;
            Health = healthCombatant;
            Damage = damageCombatant;
        }

        public int Health { get; protected set; }

        public int Damage { get; protected set; }

        public string Name { get; protected set; }

        public virtual void ShowInfoSoldier()
        {
            Console.WriteLine("Name - " + Name + ", Health - " + Health + " хп; " + "Damage - " + Damage + ";");
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage > 0)
            {
                Health -= damage;
            }
        }

        public abstract void Attack(Fighter enemy);
    }

    class Wrestler : Fighter
    {
        private int _samboWarriorEndurance = 100;
        private int _usingSkillDoubleAttack = 25;

        public Wrestler(string nameCombatant, int healthCombatant, int damageCombatant, int samboCombatantEndurance) : base(nameCombatant, healthCombatant, damageCombatant)
        {
            _samboWarriorEndurance = samboCombatantEndurance;
        }

        public override void ShowInfoSoldier()
        {
            base.ShowInfoSoldier();
            Console.WriteLine("Выносливость - " + _samboWarriorEndurance);
        }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseDoubleAttack(enemy);
        }

        private void UseDoubleAttack(Fighter enemy)
        {
            int activationDoubleAttack = 35;
            int minimumActivationDoubleAttack = 1;
            int maximumActivationDoubleAttack = 100;

            if (activationDoubleAttack > UserUtils.GenerateRandomNumber(minimumActivationDoubleAttack, maximumActivationDoubleAttack))
            {
                _samboWarriorEndurance -= _usingSkillDoubleAttack;

                if (TryDoubleAttack())
                {
                    enemy.TakeDamage(Damage);
                }
                else
                {
                    _samboWarriorEndurance = 0;
                }
            }
        }

        private bool TryDoubleAttack()
        {
            if (_samboWarriorEndurance >= 0)
            {
                Console.WriteLine("Боец " + Name + " применяет способность двойная атака, используя " + _usingSkillDoubleAttack + "% выносливости.");
                return true;
            }
            else
            {
                _samboWarriorEndurance = 0;
                return false;
            }
        }
    }

    class Boxer : Fighter
    {
        public Boxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            RestoreHealth(enemy);
        }

        private void RestoreHealth(Fighter enemy)
        {
            int halfRegenerationHealth = 2;
            int activatingRegenerationHealth = 40;
            int minimumActivatingRegenerationHealth = 1;
            int maximumActivatingRegenerationHealth = 100;

            if (activatingRegenerationHealth > UserUtils.GenerateRandomNumber(minimumActivatingRegenerationHealth, maximumActivatingRegenerationHealth))
            {
                int regenerationHealth = enemy.Damage / halfRegenerationHealth;
                Console.WriteLine(Name + " получает - " + enemy.Damage + " урона и восстанавливает " + regenerationHealth + " здоровья.");
                Health += regenerationHealth;
            }
        }
    }

    class Kickboxer : Fighter
    {
        private int _beginningDoubleDamageCountdown = 0;
        private int _endDoubleDamageCountdown = 3;

        public Kickboxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseDoubleDamage(enemy);
        }

        private void UseDoubleDamage(Fighter enemy)
        {
            if (_endDoubleDamageCountdown <= _beginningDoubleDamageCountdown)
            {
                Console.WriteLine($"Двойной урон наносит {Name}");

                if (_endDoubleDamageCountdown == _beginningDoubleDamageCountdown)
                {
                    _endDoubleDamageCountdown += _endDoubleDamageCountdown;
                }

                enemy.TakeDamage(Damage);
            }
            else
            {
                this._beginningDoubleDamageCountdown++;
            }
        }
    }

    class Karateka : Fighter
    {
        public Karateka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseBleedingDamage(enemy);
        }

        private void UseBleedingDamage(Fighter enemy)
        {
            int activationBleedingDamage = 40;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = UserUtils.GenerateRandomNumber(10, 30);

            if (activationBleedingDamage > UserUtils.GenerateRandomNumber(minimumBleedingDamage, maximumBleedingDamage))
            {
                Console.WriteLine(Name + ", выполняет режущий удар с логтя, у противника кровотечение на - " + bleedingDamage + " урона.");
                enemy.TakeDamage(bleedingDamage);
            }
        }
    }

    class SumoWrestler : Fighter
    {
        public SumoWrestler(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseDoublingDamage(enemy);
        }

        private void UseDoublingDamage(Fighter enemy)
        {
            int activationDoublingAttack = 18;
            int minimumActivationDoublingAttack = 1;
            int maximumActivationDoublingAttack = 100;

            if (activationDoublingAttack > UserUtils.GenerateRandomNumber(minimumActivationDoublingAttack, maximumActivationDoublingAttack))
            {
                Console.WriteLine($"Урон {Name} удваивается.");

                Damage += Damage;

                enemy.TakeDamage(Damage);
            }
        }
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseReflectionDamage(enemy);
        }

        public override void TakeDamage(int damage)
        {
            if (TryReflectionDamageFighter())
            {
                UseEvasionDamage();
            }
            else
            {
                base.TakeDamage(damage);
            }
        }

        private bool TryReflectionDamageFighter()
        {
            int activationReflectionDamage = 25;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            return (activationReflectionDamage > UserUtils.GenerateRandomNumber(minimumValueReflectionDamage, maximumValueReflectionDamage));
        }

        private void UseReflectionDamage(Fighter enemy)
        {
            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > UserUtils.GenerateRandomNumber(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                Console.WriteLine(Name + " отражает атаку противника на " + enemy.Damage + " урона.");
                enemy.TakeDamage(enemy.Damage);
            }
        }

        private void UseEvasionDamage()
        {
            int damage = 0;
            Console.WriteLine(Name + " уклоняется от атаки получив " + damage + " урона.");
            Health -= damage;
        }
    }

    class Сapoeirista : Fighter
    {
        public Сapoeirista(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            UseCriticalDamage(enemy);
        }

        private void UseCriticalDamage(Fighter enemy)
        {
            int activationCriticalDamage = 10;
            int minimumActivationCriticalDamage = 1;
            int maximumActivationCriticalDamage = 100;
            int damage = 10;

            if (activationCriticalDamage > UserUtils.GenerateRandomNumber(minimumActivationCriticalDamage, maximumActivationCriticalDamage))
            {
                int criticalDamage = damage * Damage;

                Console.WriteLine($"У {Name} критический урон. Он наносит по противнику {criticalDamage} урона.");

                enemy.TakeDamage(damage);
            }
            else
            {
                enemy.TakeDamage(Damage);
            }
        }
    }

    class Aikidoka : Fighter
    {
        public Aikidoka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Fighter enemy)
        {
            enemy.TakeDamage(Damage);

            UseReverseDamage(enemy);
        }

        private void UseReverseDamage(Fighter enemy)
        {
            int activationReverseDamage = 25;
            int minimumActivationReverseDamage = 1;
            int maximumActivationReverseDamage = 100;
            int damage = 2;

            if (activationReverseDamage > UserUtils.GenerateRandomNumber(minimumActivationReverseDamage, maximumActivationReverseDamage))
            {
                int reverseDamage = damage * Damage;

                enemy.TakeDamage(reverseDamage);

                TakeDamage(reverseDamage);

                Console.WriteLine($"{Name} выполняет запрещённый удар. Он наносит по противнику и себе {reverseDamage} урона.");
            }
        }
    }
}
