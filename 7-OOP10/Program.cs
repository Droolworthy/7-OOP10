namespace OOP10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const string CommandСhooseFighters = "1";
            const string CommandExit = "2";

            War arena = new War();

            bool isWorking = true;

            Console.WriteLine($"{CommandСhooseFighters} - ВЫБРАТЬ БОЙЦОВ" + $"\n{CommandExit} - ВЫХОД");

            while (isWorking)
            {
                Console.Write("\nВведите команду: ");
                string userInput = Console.ReadLine();

                if (CommandСhooseFighters == userInput)
                {
                    arena.Warfare();
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

    class War
    {
        Random _random = new Random();

        public void Warfare()
        {
            List<Hungary> hungarySquad = new List<Hungary>
            {
                new Wrestler(nameof(Wrestler), 450, 20, 100),
                new Kickboxer(nameof(Kickboxer), 500, 15),
                new Boxer(nameof(Boxer), 375, 20),
                new SumoWrestler(nameof(SumoWrestler), 330, 15),
            };

            List<Romania> romaniaSquad = new List<Romania>
            {
                new Karateka(nameof(Karateka), 500, 22),
                new TaekwondoPractitioner(nameof(TaekwondoPractitioner), 330, 30),
                new Сapoeirista(nameof(Сapoeirista), 510, 15),
                new Aikidoka(nameof(Aikidoka), 340, 25),
            };

            ChooseFighters(hungarySquad, romaniaSquad);
        }

        private void ChooseFighters(List<Hungary> hungarySquad, List<Romania> romaniaSquad)
        {
            while (hungarySquad.Count > 0 && romaniaSquad.Count > 0)
            {
                Console.WriteLine();

                ShowListHungarySquad(hungarySquad);

                int numberHungarySquad = _random.Next(hungarySquad.Count);

                Hungary hungary = hungarySquad[numberHungarySquad];

                Console.WriteLine("\nВы выбрали бойца - " + hungary.NameState);
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();
                Console.WriteLine();

                ShowListRomaniaSquad(romaniaSquad);

                int numberRomaniaSquad = _random.Next(romaniaSquad.Count);

                Romania romania = romaniaSquad[numberRomaniaSquad];

                Console.WriteLine($"\nВы выбрали бойца - {romania.NameState}");
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey();

                FightSquads(hungarySquad, romaniaSquad, romania, hungary);

                ShowWinningCountry(hungarySquad, romaniaSquad);
            }
        }

        private void FightSquads(List<Hungary> hungarySquad, List<Romania> romaniaSquad, Romania romania, Hungary hungary)
        {
            Console.WriteLine("\nВОЙНА НАЧИНАЕТСЯ!");
            Console.WriteLine();

            while (hungary.HealthState > 0 && romania.HealthState > 0)
            {
                hungary.Attack(romania);
                romania.Attack(hungary);
                hungary.ShowInfoFighter();
                romania.ShowInfoFighter();

                Console.WriteLine("---------------------------------------------------");

                ShowWinningSquad(hungarySquad, romaniaSquad, romania, hungary);
            }
        }

        private void ShowWinningSquad(List<Hungary> hungarySquad, List<Romania> romaniaSquad, Romania romania, Hungary hungary)
        {
            if (hungary.HealthState <= 0)
            {
                if (romania.HealthState <= 0)
                {
                    hungarySquad.Remove(hungary);
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    hungarySquad.Remove(hungary);

                    Console.WriteLine("\nПобеда бойца - " + romania.NameState);
                }
            }
            else if (romania.HealthState <= 0)
            {
                if (hungary.HealthState <= 0)
                {
                    hungarySquad.Remove(hungary);
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nОба бойца погибли на поле боя.");
                }
                else
                {
                    romaniaSquad.Remove(romania);

                    Console.WriteLine("\nПобеда бойца - " + hungary.NameState);
                }
            }
        }

        private void ShowWinningCountry(List<Hungary> hungarySquad, List<Romania> romaniaSquad)
        {
            if (hungarySquad.Count <= 0 & romaniaSquad.Count <= 0)
            {
                Console.WriteLine("\nНичья. Победила дружба.");
                return;
            }
            else if (hungarySquad.Count <= 0)
            {
                Console.WriteLine("\nСтрана " + (nameof(Romania)) + " побеждает!");
                return;
            }
            else if (romaniaSquad.Count <= 0)
            {
                Console.WriteLine("\nСтрана " + (nameof(Hungary)) + " побеждает!");
                return;
            }
        }

        private void ShowListRomaniaSquad(List<Romania> romaniaSquad)
        {
            Console.WriteLine($"Список бойцов страны {nameof(Romania)}:");

            for (int i = 0; i < romaniaSquad.Count; i++)
            {
                Console.Write(i + ". ");
                romaniaSquad[i].ShowInfoFighter();
            }
        }

        private void ShowListHungarySquad(List<Hungary> hungarySquad)
        {
            Console.WriteLine($"Список бойцов страны {nameof(Hungary)}:");

            for (int i = 0; i < hungarySquad.Count; i++)
            {
                Console.Write(i + ". ");
                hungarySquad[i].ShowInfoFighter();
            }
        }
    }

    class Country
    {
        protected string Name;
        protected int Health;
        protected int Damage;

        public Country(string nameCombatant, int healthCombatant, int damageCombatant)
        {
            Name = nameCombatant;
            Health = healthCombatant;
            Damage = damageCombatant;
        }

        public string NameState
        {
            get
            {
                return Name;
            }
        }

        public int DamageState
        {
            get
            {
                return Damage;
            }
        }

        public int HealthState
        {
            get
            {
                return Health;
            }
        }

        public virtual void ShowInfoFighter()
        {
            Console.WriteLine("Name - " + Name + ", Health - " + Health + " хп; " + "Damage - " + Damage + ";");
        }

        public virtual void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }

    abstract class Romania : Country
    {
        public Romania(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public abstract void Attack(Hungary hungary);
    }

    abstract class Hungary : Country
    {
        public Hungary(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public abstract void Attack(Romania romania);
    }

    class Boxer : Hungary
    {
        public Boxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Romania romania)
        {
            romania.TakeDamage(Damage);

            RestoreHealth(romania);
        }

        private void RestoreHealth(Romania romania)
        {
            Random random = new Random();

            int halfRegenerationHealth = 2;
            int activatingRegenerationHealth = 40;
            int minimumActivatingRegenerationHealth = 1;
            int maximumActivatingRegenerationHealth = 100;

            if (activatingRegenerationHealth > random.Next(minimumActivatingRegenerationHealth, maximumActivatingRegenerationHealth))
            {
                int regenerationHealth = romania.DamageState / halfRegenerationHealth;
                Console.WriteLine(Name + " получает - " + romania.DamageState + " урона и восстанавливает " + regenerationHealth + " здоровья.");
                Health += regenerationHealth;
            }
        }
    }

    class Wrestler : Hungary
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

        public override void Attack(Romania romania)
        {
            romania.TakeDamage(Damage);

            UseDoubleAttack(romania);
        }

        private void UseDoubleAttack(Romania romania)
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

    class Kickboxer : Hungary
    {
        private int _beginningDoubleDamageCountdown = 0;
        private int _endDoubleDamageCountdown = 3;

        public Kickboxer(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Romania romania)
        {
            romania.TakeDamage(Damage);

            UseDoubleDamage(romania);
        }

        private void UseDoubleDamage(Romania romania)
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

    class SumoWrestler : Hungary
    {
        public SumoWrestler(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Romania romania)
        {
            romania.TakeDamage(Damage);

            UseDoublingDamage(romania);
        }

        private void UseDoublingDamage(Romania romania)
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

    class Karateka : Romania
    {
        public Karateka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Hungary hungary)
        {
            hungary.TakeDamage(Damage);

            UseBleedingDamage(hungary);
        }

        private void UseBleedingDamage(Hungary hungary)
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

    class TaekwondoPractitioner : Romania
    {
        public TaekwondoPractitioner(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Hungary hungary)
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

        private void UseReflectionDamage(Hungary hungary)
        {
            Random random = new Random();

            int activationReflectionDamage = 30;
            int minimumValueReflectionDamage = 1;
            int maximumValueReflectionDamage = 100;

            if (activationReflectionDamage > random.Next(minimumValueReflectionDamage, maximumValueReflectionDamage))
            {
                Console.WriteLine(Name + " отражает атаку противника на " + hungary.DamageState + " урона.");
                hungary.TakeDamage(hungary.DamageState);
            }
        }

        private void UseEvasionDamage()
        {
            int damage = 0;
            Console.WriteLine(Name + " уклоняется от атаки получив " + damage + " урона.");
            Health -= damage;
        }
    }

    class Сapoeirista : Romania
    {
        public Сapoeirista(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Hungary hungary)
        {
            UseCriticalDamage(hungary);
        }

        private void UseCriticalDamage(Hungary hungary)
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

    class Aikidoka : Romania
    {
        public Aikidoka(string nameCombatant, int healthCombatant, int damageCombatant) : base(nameCombatant, healthCombatant, damageCombatant) { }

        public override void Attack(Hungary hungary)
        {
            hungary.TakeDamage(Damage);

            UseReverseDamage(hungary);
        }

        private void UseReverseDamage(Hungary hungary)
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
