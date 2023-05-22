namespace OOP10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandСhooseFighters = "1";
            const string CommandExit = "2";

            Arena arena = new Arena();

            bool isWorking = true;

            Console.WriteLine($"{CommandСhooseFighters} - ВЫБРАТЬ БОЙЦОВ" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                if (CommandСhooseFighters == userInput)
                {
                    arena.Fight();
                }
                else if (userInput == CommandExit)
                {
                    isWorking = false;
                }
                else
                {
                    Console.WriteLine($"Ошибка. Введите {CommandСhooseFighters} или {CommandExit}");
                }
            }
        }
    }

    class Arena
    {
        private Squad _firstSquad;
        private Squad _secondSquad;
        private SquadCreator _creator = new SquadCreator();
        private Random _random = new Random();

        public Arena()
        {
            _firstSquad = _creator.CreateFirstSquad("Венгрия");
            _secondSquad = _creator.CreateSecondSquad("Румыния");
        }

        public void Fight()
        {
            while (_firstSquad.Count > 0 && _secondSquad.Count > 0)
            {
                ShowListSquad(_firstSquad);

                Fighter firstFighter = _firstSquad.GetFighterPlatoon(_random.Next(_secondSquad.Count));
                Console.WriteLine($"Вы выбрали бойца - {firstFighter.Name}");

                ShowListSquad(_secondSquad);

                Fighter secondFighter = _secondSquad.GetFighterPlatoon(_random.Next(_secondSquad.Count));
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

                ShowWinningSquad(firstFighter, secondFighter);
            }
        }

        private void ShowWinningSquad(Fighter firstFighter, Fighter secondFighter)
        {
            if (firstFighter.Health <= 0)
            {
                if (secondFighter.Health <= 0)
                {
                    RemoveCombatantSquad(firstFighter, secondFighter);
                }
                else
                {
                    _firstSquad.RemoveFighterPlatoon(firstFighter);

                    Console.WriteLine("\nПобеда бойца - " + secondFighter.Name);
                    Console.ReadKey();
                }
            }
            else if (secondFighter.Health <= 0)
            {
                if (firstFighter.Health <= 0)
                {
                    RemoveCombatantSquad(firstFighter, secondFighter);
                }
                else
                {
                    _secondSquad.RemoveFighterPlatoon(secondFighter);

                    Console.WriteLine("\nПобеда бойца - " + firstFighter.Name);
                    Console.ReadKey();
                }
            }
        }

        private void RemoveCombatantSquad(Fighter firstFighter, Fighter secondFighter)
        {
            _firstSquad.RemoveFighterPlatoon(firstFighter);
            _secondSquad.RemoveFighterPlatoon(secondFighter);

            Console.WriteLine("\nОба бойца погибли на поле боя.");
        }

        private void ShowWinningCountry()
        {
            if (_firstSquad.Count <= 0 & _secondSquad.Count <= 0)
            {
                Console.WriteLine("\nНичья. Победила дружба.");
                return;
            }
            else if (_firstSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {_firstSquad.Country} побеждает!");
                return;
            }
            else if (_secondSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {_secondSquad.Country} побеждает!");
                return;
            }
        }

        private void ShowListSquad(Squad squad)
        {
            Console.WriteLine($"\n{squad.Country}. Список бойцов:");

            squad.ShowInfoFighters();
        }
    }

    class SquadCreator
    {
        public Squad CreateFirstSquad(string country)
        {
            List<Fighter> firstSquad = new List<Fighter>
            {
                new Wrestler(nameof(Wrestler), 80, 20, 100),
                new Kickboxer(nameof(Kickboxer), 60, 15),
                new Boxer(nameof(Boxer), 50, 20),
                new SumoWrestler(nameof(SumoWrestler), 55, 15),
            };

            Squad squad = new Squad(firstSquad, country);

            return squad;
        }

        public Squad CreateSecondSquad(string country)
        {
            List<Fighter> secondSquad = new List<Fighter>
            {
                new Karateka(nameof(Karateka), 50, 22),
                new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 33, 30),
                new Сapoeirista(nameof(Сapoeirista), 100, 15),
                new Aikidoka(nameof(Aikidoka), 66, 25),
            };

            Squad squad = new Squad(secondSquad, country);

            return squad;
        }
    }

    class Squad
    {
        private List<Fighter> _fighters = new List<Fighter>();

        public Squad(List<Fighter> fighters, string country)
        {
            _fighters = fighters;
            Country = country;
        }

        public string Country { get; private set; }

        public int Count => _fighters.Count;

        public Fighter GetFighterPlatoon(int index)
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

        public void RemoveFighterPlatoon(Fighter fighter)
        {
            _fighters.Remove(fighter);
        }
    }

    abstract class Fighter
    {
        private List<Fighter> _fighters;

        public Fighter(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            Name = nameCombatant;
            Health = healthCombatant;
            Damage = damageCombatant;
        }

        public Fighter(List<Fighter> fighters, string name)
        {
            _fighters = fighters;
            Name = name;
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
            Random random = new Random();

            int activationDoubleAttack = 35;
            int minimumActivationDoubleAttack = 1;
            int maximumActivationDoubleAttack = 100;

            if (activationDoubleAttack > random.Next(minimumActivationDoubleAttack, maximumActivationDoubleAttack))
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
            Random random = new Random();

            int halfRegenerationHealth = 2;
            int activatingRegenerationHealth = 40;
            int minimumActivatingRegenerationHealth = 1;
            int maximumActivatingRegenerationHealth = 100;

            if (activatingRegenerationHealth > random.Next(minimumActivatingRegenerationHealth, maximumActivatingRegenerationHealth))
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
            Random random = new Random();

            int activationBleedingDamage = 40;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = random.Next(10, 30);

            if (activationBleedingDamage > random.Next(minimumBleedingDamage, maximumBleedingDamage))
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
            Random random = new Random();

            int activationDoublingAttack = 18;
            int minimumActivationDoublingAttack = 1;
            int maximumActivationDoublingAttack = 100;

            if (activationDoublingAttack > random.Next(minimumActivationDoublingAttack, maximumActivationDoublingAttack))
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
            Random random = new Random();

            int activationReflectionDamage = 25;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            return (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage));
        }

        private void UseReflectionDamage(Fighter enemy)
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
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
            Random random = new Random();

            int activationCriticalDamage = 10;
            int minimumActivationCriticalDamage = 1;
            int maximumActivationCriticalDamage = 100;
            int damage = 10;

            if (activationCriticalDamage > random.Next(minimumActivationCriticalDamage, maximumActivationCriticalDamage))
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
            Random random = new Random();

            int activationReverseDamage = 25;
            int minimumActivationReverseDamage = 1;
            int maximumActivationReverseDamage = 100;
            int damage = 2;

            if (activationReverseDamage > random.Next(minimumActivationReverseDamage, maximumActivationReverseDamage))
            {
                int reverseDamage = damage * Damage;

                enemy.TakeDamage(reverseDamage);

                TakeDamage(reverseDamage);

                Console.WriteLine($"{Name} выполняет запрещённый удар. Он наносит по противнику и себе {reverseDamage} урона.");
            }
        }
    }
}
