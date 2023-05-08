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
                    arena.StartFight();
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

        public void StartFight()
        {
            List<Squad> hungarySquad = new List<Squad>
            {
                new Wrestler(nameof(Wrestler), 450, 20, 100),
                new Kickboxer(nameof(Kickboxer), 500, 15),
                new Boxer(nameof(Boxer), 375, 20),
                new SumoWrestler(nameof(SumoWrestler), 330, 15),
            };

            List<Squad> romaniaSquad = new List<Squad>
            {
                new Karateka(nameof(Karateka), 500, 22),
                new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 330, 30),
                new Сapoeirista(nameof(Сapoeirista), 510, 15),
                new Aikidoka(nameof(Aikidoka), 340, 25),
            };

            while (hungarySquad.Count > 0 && romaniaSquad.Count > 0)
            {
                ShowListSquad(hungarySquad);

                int numberHungarySquad = _random.Next(hungarySquad.Count);
                Squad hungary = hungarySquad[numberHungarySquad];

                ChooseFighters(hungarySquad, hungary);

                ShowListSquad(hungarySquad);

                int numberRomaniaSquad = _random.Next(romaniaSquad.Count);
                Squad romania = romaniaSquad[numberRomaniaSquad];

                ChooseFighters(romaniaSquad, romania);

                FightSquads(hungarySquad, romaniaSquad, romania, hungary);

                ShowWinningCountry(hungarySquad, romaniaSquad);
            }
        }

        private void ChooseFighters(List<Squad> countrySquad, Squad nameFighter)
        {
            Console.WriteLine("\nВы выбрали бойца - " + nameFighter.Name);
            Console.WriteLine("Для продолжения нажмите любую клавишу...");
            Console.ReadKey();
        }

        private void FightSquads(List<Squad> hungarySquad, List<Squad> romaniaSquad, Squad romania, Squad hungary)
        {
            Console.WriteLine("\nВОЙНА НАЧИНАЕТСЯ!");
            Console.WriteLine();

            while (hungary.Health > 0 && romania.Health > 0)
            {
                hungary.Attack(romania);
                romania.Attack(hungary);
                hungary.ShowInfoFighter();
                romania.ShowInfoFighter();

                Console.WriteLine("---------------------------------------------------");

                ShowWinningSquad(hungarySquad, romaniaSquad, romania, hungary);
            }
        }

        private void ShowWinningSquad(List<Squad> hungarySquad, List<Squad> romaniaSquad, Squad romania, Squad hungary)
        {
            if (hungary.Health <= 0)
            {
                if (romania.Health <= 0)
                {
                    hungarySquad.Remove(hungary);
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    hungarySquad.Remove(hungary);

                    Console.WriteLine("\nПобеда бойца - " + romania.Name);
                }
            }
            else if (romania.Health <= 0)
            {
                if (hungary.Health <= 0)
                {
                    hungarySquad.Remove(hungary);
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nПобеда бойца - " + hungary.Name);
                }
            }
        }

        private void ShowWinningCountry(List<Squad> hungarySquad, List<Squad> romaniaSquad)
        {
            if (hungarySquad.Count <= 0 & romaniaSquad.Count <= 0)
            {
                Console.WriteLine("\nНичья. Победила дружба.");
                return;
            }
            else if (hungarySquad.Count <= 0)
            {
                Console.WriteLine("\nСтрана Румыния побеждает!");
                return;
            }
            else if (romaniaSquad.Count <= 0)
            {
                Console.WriteLine("\nСтрана Венгрия побеждает!");
                return;
            }
        }

        private void ShowListSquad(List<Squad> fighters)
        {
            Console.WriteLine($"\nСписок бойцов:");

            for (int i = 0; i < fighters.Count; i++)
            {
                Console.Write(i + ". ");
                fighters[i].ShowInfoFighter();
            }
        }
    }

    abstract class Squad
    {
        public Squad(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            Name = nameCombatant;
            Health = healthCombatant;
            Damage = damageCombatant;
        }

        public int Health { get; protected set; }

        public int Damage { get; protected set; }

        public string Name { get; protected set; }

        public virtual void ShowInfoFighter()
        {
            Console.WriteLine("Name - " + Name + ", Health - " + Health + " хп; " + "Damage - " + Damage + ";");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }

        public abstract void Attack(Squad squad);
    }

    abstract class Fighter : Squad
    {
        public Fighter(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }
    }

    class Wrestler : Fighter
    {
        private int _samboWarriorEndurance = 100;
        private int _usingSkillDoubleAttack = 25;

        public Wrestler(string nameCombatant, int healthCombatant, int damageCombatant, int samboCombatantEndurance) : base(nameCombatant, healthCombatant, damageCombatant)
        {
            _samboWarriorEndurance = samboCombatantEndurance;
        }

        public override void ShowInfoFighter()
        {
            base.ShowInfoFighter();
            Console.WriteLine("Выносливость - " + _samboWarriorEndurance);
        }

        public override void Attack(Squad romania)
        {
            romania.TakeDamage(Damage);

            UseDoubleAttack(romania);
        }

        private void UseDoubleAttack(Squad romania)
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
                    romania.TakeDamage(Damage);
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

        public override void Attack(Squad romania)
        {
            romania.TakeDamage(Damage);

            RestoreHealth(romania);
        }

        private void RestoreHealth(Squad romania)
        {
            Random random = new Random();

            int halfRegenerationHealth = 2;
            int activatingRegenerationHealth = 40;
            int minimumActivatingRegenerationHealth = 1;
            int maximumActivatingRegenerationHealth = 100;

            if (activatingRegenerationHealth > random.Next(minimumActivatingRegenerationHealth, maximumActivatingRegenerationHealth))
            {
                int regenerationHealth = romania.Damage / halfRegenerationHealth;
                Console.WriteLine(Name + " получает - " + romania.Damage + " урона и восстанавливает " + regenerationHealth + " здоровья.");
                Health += regenerationHealth;
            }
        }
    }

    class Kickboxer : Fighter
    {
        private int _beginningDoubleDamageCountdown = 0;
        private int _endDoubleDamageCountdown = 3;

        public Kickboxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Squad romania)
        {
            romania.TakeDamage(Damage);

            UseDoubleDamage(romania);
        }

        private void UseDoubleDamage(Squad romania)
        {
            if (_endDoubleDamageCountdown <= _beginningDoubleDamageCountdown)
            {
                Console.WriteLine($"Двойной урон наносит {Name}");

                if (_endDoubleDamageCountdown == _beginningDoubleDamageCountdown)
                {
                    _endDoubleDamageCountdown += _endDoubleDamageCountdown;
                }

                romania.TakeDamage(Damage);
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

        public override void Attack(Squad hungary)
        {
            hungary.TakeDamage(Damage);

            UseBleedingDamage(hungary);
        }

        private void UseBleedingDamage(Squad hungary)
        {
            Random random = new Random();

            int activationBleedingDamage = 40;
            int minimumBleedingDamage = 1;
            int maximumBleedingDamage = 100;
            int bleedingDamage = random.Next(10, 30);

            if (activationBleedingDamage > random.Next(minimumBleedingDamage, maximumBleedingDamage))
            {
                Console.WriteLine(Name + ", выполняет режущий удар с логтя, у противника кровотечение на - " + bleedingDamage + " урона.");
                hungary.TakeDamage(bleedingDamage);
            }
        }
    }

    class SumoWrestler : Fighter
    {
        public SumoWrestler(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Squad romania)
        {
            romania.TakeDamage(Damage);

            UseDoublingDamage(romania);
        }

        private void UseDoublingDamage(Squad romania)
        {
            Random random = new Random();

            int activationDoublingAttack = 18;
            int minimumActivationDoublingAttack = 1;
            int maximumActivationDoublingAttack = 100;

            if (activationDoublingAttack > random.Next(minimumActivationDoublingAttack, maximumActivationDoublingAttack))
            {
                Console.WriteLine($"Урон {Name} удваивается.");

                Damage += Damage;

                romania.TakeDamage(Damage);
            }
        }
    }

    class TaekwondoPractitioner : Fighter
    {
        public TaekwondoPractitioner(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Squad hungary)
        {
            hungary.TakeDamage(Damage);

            UseReflectionDamage(hungary);
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

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void UseReflectionDamage(Squad hungary)
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                Console.WriteLine(Name + " отражает атаку противника на " + hungary.Damage + " урона.");
                hungary.TakeDamage(hungary.Damage);
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

        public override void Attack(Squad hungary)
        {
            UseCriticalDamage(hungary);
        }

        private void UseCriticalDamage(Squad hungary)
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

                hungary.TakeDamage(damage);
            }
            else
            {
                hungary.TakeDamage(Damage);
            }
        }
    }

    class Aikidoka : Fighter
    {
        public Aikidoka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Squad hungary)
        {
            hungary.TakeDamage(Damage);

            UseReverseDamage(hungary);
        }

        private void UseReverseDamage(Squad hungary)
        {
            Random random = new Random();

            int activationReverseDamage = 25;
            int minimumActivationReverseDamage = 1;
            int maximumActivationReverseDamage = 100;
            int damage = 2;

            if (activationReverseDamage > random.Next(minimumActivationReverseDamage, maximumActivationReverseDamage))
            {
                int reverseDamage = damage * Damage;

                hungary.TakeDamage(reverseDamage);

                TakeDamage(reverseDamage);

                Console.WriteLine($"{Name} выполняет запрещённый удар. Он наносит по противнику и себе {reverseDamage} урона.");
            }
        }
    }
}
