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
        private Random _random = new Random();
        private Squad _creator = new Squad();
        private List<Fighter> _firstSquad;
        private List<Fighter> _secondSquad;
        
        public Arena()
        {
            _firstSquad = _creator.CreateFirstSquad();
            _secondSquad = _creator.CreateSecondSquad();
        }

        public void Fight()
        {
            while (_firstSquad.Count > 0 && _secondSquad.Count > 0)
            {
                ShowListSquad(_firstSquad);

                int numberFirstSquad = _random.Next(_firstSquad.Count);
                Fighter firstCombatantSquad = _firstSquad[numberFirstSquad];

                ChooseFighters(_firstSquad, firstCombatantSquad);

                ShowListSquad(_secondSquad);

                int numberSecondSquad = _random.Next(_secondSquad.Count);
                Fighter secondCombatantSquad = _secondSquad[numberSecondSquad];

                ChooseFighters(_secondSquad, secondCombatantSquad);

                FightSquads(_firstSquad, _secondSquad, secondCombatantSquad, firstCombatantSquad);

                ShowWinningCountry(_firstSquad, _secondSquad);
            }
        }

        private void ChooseFighters(List<Fighter> countrySquad, Fighter nameFighter)
        {
            Console.WriteLine("\nВы выбрали бойца - " + nameFighter.Name);
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void FightSquads(List<Fighter> firstCombatantSquad, List<Fighter> secondCombatantSquad, Fighter secondSquad, Fighter firstSquad)
        {
            Console.WriteLine("\nВОЙНА НАЧИНАЕТСЯ!");
            Console.WriteLine();

            while (firstSquad.Health > 0 && secondSquad.Health > 0)
            {
                firstSquad.Attack(secondSquad);
                secondSquad.Attack(firstSquad);
                firstSquad.ShowInfoSoldier();
                secondSquad.ShowInfoSoldier();

                Console.WriteLine("---------------------------------------------------");

                ShowWinningSquad(firstCombatantSquad, secondCombatantSquad, secondSquad, firstSquad);
            }
        }

        private void ShowWinningSquad(List<Fighter> firstCombatantSquad, List<Fighter> secondCombatantSquad, Fighter secondSquad, Fighter firstSquad)
        {
            if (firstSquad.Health <= 0)
            {
                if (secondSquad.Health <= 0)
                {
                    RemoveCombatantSquad(firstCombatantSquad, secondCombatantSquad, secondSquad, firstSquad);
                }
                else
                {
                    firstCombatantSquad.Remove(firstSquad);

                    Console.WriteLine("\nПобеда бойца - " + secondSquad.Name);
                }
            }
            else if (secondSquad.Health <= 0)
            {
                if (firstSquad.Health <= 0)
                {
                    RemoveCombatantSquad(firstCombatantSquad, secondCombatantSquad, secondSquad, firstSquad);
                }
                else
                {
                    secondCombatantSquad.Remove(secondSquad);

                    Console.WriteLine("\nПобеда бойца - " + firstSquad.Name);
                }
            }
        }

        private void RemoveCombatantSquad(List<Fighter> firstCombatantSquad, List<Fighter> secondCombatantSquad, Fighter secondSquad, Fighter firstSquad)
        {
            firstCombatantSquad.Remove(firstSquad);
            secondCombatantSquad.Remove(secondSquad);

            Console.WriteLine("\nОба бойца погибли на поле боя.");
        }

        private void ShowWinningCountry(List<Fighter> firstSquad, List<Fighter> secondSquad)
        {
            string firstCountry = "Венгрия";
            string secondCountry = "Румыния";
            
            if (firstSquad.Count <= 0 & secondSquad.Count <= 0)
            {
                Console.WriteLine("\nНичья. Победила дружба.");
                return;
            }
            else if (firstSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {firstCountry} побеждает!");
                return;
            }
            else if (secondSquad.Count <= 0)
            {
                Console.WriteLine($"\nСтрана {secondCountry} побеждает!");
                return;
            }
        }

        private void ShowListSquad(List<Fighter> fighters)
        {
            Console.WriteLine($"\nСписок бойцов:");

            for (int i = 0; i < _firstSquad.Count; i++)
            {
                Console.Write(i + ". ");
                _firstSquad[i].ShowInfoSoldier();
            }
        }
    }

    class Squad
    {
        public List<Fighter> CreateFirstSquad()
        {
            List<Fighter> firstSquad = new List<Fighter>
            {
                new Wrestler(nameof(Wrestler), 450, 20, 100),
                new Kickboxer(nameof(Kickboxer), 500, 15),
                new Boxer(nameof(Boxer), 375, 20),
                new SumoWrestler(nameof(SumoWrestler), 330, 15),
            };

            return firstSquad;
        }

        public List<Fighter> CreateSecondSquad()
        {
            List<Fighter> secondSquad = new List<Fighter>
            {
                new Karateka(nameof(Karateka), 500, 22),
                new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 330, 30),
                new Сapoeirista(nameof(Сapoeirista), 510, 15),
                new Aikidoka(nameof(Aikidoka), 340, 25),
            };

            return secondSquad;
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
