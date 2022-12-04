using GladiatorsArena.ArenaModule;
using GladiatorsArena.Heroes;
using GladiatorsArena.Presentation;
using System.Drawing;

namespace GladiatorsArena.Input
{
    public class GameInput
    {
        private enum InputCommand
        {
            Restart,
            Exit
        }

        private record SelectedHero(HeroType Type,string Name);

        private const int InputNumberInvalid = -1;

        private const int HeroesCount = 2;

        private const string WarriorInfo = "Восстанавливает здроровье и получает дополнительный урон при первой смерти.";
        private const string MageInfo = "Увеличивает урон от атаки и поглощает урон с помощью ледяной формы.";
        private const string VampireInfo = "Излечивается от каждой атаки.";
        private const string ChaosKnightInfo = "Блокирует часть магического урона. Призывают иллюзию, которая наносит урон";
        private const string AncientGolemInfo = "Отражает часть входящего урона обратно во врага. Наносит случайный урон.";

        private readonly HeroType[] _availableHeroes = Enum.GetValues<HeroType>();

        private readonly InputCommand[] _availableInputCommands = Enum.GetValues<InputCommand>();

        private Dictionary<HeroType, string> _heroesInfo = new Dictionary<HeroType, string>();

        private HeroFactory _heroFactory = new HeroFactory();

        private List<SelectedHero> _selectedHeroes = new List<SelectedHero>();

        public GameInput()
        {
            InitHeroesInfoDictionary();
        }

        public void Start()
        {
            Reset();
        }

        private void InitHeroesInfoDictionary()
        {
            foreach (HeroType type in _availableHeroes)
            {
                _heroesInfo.Add(type, string.Format("{0}. {1}", GetColoredHeroTypeName(type), GetHeroTypeInfo(type)));
            }
        }

        private string GetColoredHeroTypeName(HeroType type)
        {
            string typeName = type switch
            {
                HeroType.Warrior => "Воин",
                HeroType.Mage => "Маг",
                HeroType.AncientGolem => "Древний Голем",
                HeroType.Vampire => "Вампир",
                HeroType.ChaosKnight => "Рыцарь Хаоса",
            };

            Color color = type.GetHeroTypeColor();
            return typeName.Colored(color);
        }

        private string GetHeroTypeInfo(HeroType type)
        {
            return type switch
            {
                HeroType.Warrior => WarriorInfo,
                HeroType.Mage => MageInfo,
                HeroType.AncientGolem => AncientGolemInfo,
                HeroType.Vampire => VampireInfo,
                HeroType.ChaosKnight => ChaosKnightInfo,
            };
        }

        private void Reset()
        {
            _selectedHeroes.Clear();

            Console.Clear();

            PrintHeroesInfo();

            ProcessHeroesSelection();
        }

        private void ProcessHeroesSelection()
        {
            int currentHeroIndex = 1;

            while (true)
            {
                if (currentHeroIndex > HeroesCount)
                {
                    StartBattle();
                    break;
                }

                Console.WriteLine("Выбираем героя №{0}", currentHeroIndex);

                HeroType type = ReadHeroType();
                string name = ReadHeroName();

                var selectedHero = new SelectedHero(type, name);

                _selectedHeroes.Add(selectedHero);
                currentHeroIndex++;
            }
        }

        private HeroType ReadHeroType()
        {
            while (true)
            {
                Console.WriteLine("Укажите тип героя:");

                int heroTypeNumber = ParseIntegerInput(Console.ReadLine());

                if (CheckHeroTypeNumberIsCorrect(heroTypeNumber) == false)
                {
                    PrintHeroTypeInputError();
                    continue;
                }
                return (HeroType) (heroTypeNumber - 1); 
            }
        }

        private string ReadHeroName()
        {
            while (true)
            {
                Console.WriteLine("Укажите имя героя:");

                string? heroName = Console.ReadLine();

                if (string.IsNullOrEmpty(heroName))
                {
                    PrintHeroNameInputError();
                    continue;
                }

                return heroName;
            }
        }

        private void PrintHeroesInfo()
        {
            Console.WriteLine("Достпуные герои:");

            for (int i = 0; i < _availableHeroes.Length; i++)
            {
                var heroType = _availableHeroes[i];
                Console.WriteLine("{0}. {1}", i + 1, _heroesInfo[heroType]);
            }
        }

        private int ParseIntegerInput(string input)
        {
            try
            {
                return int.Parse(input);
            }
            catch
            {
                return InputNumberInvalid;
            }
        }

        private bool CheckHeroTypeNumberIsCorrect(int heroTypeNumber)
        {
            return heroTypeNumber > 0 && heroTypeNumber <= _availableHeroes.Length;
        }

        private void PrintHeroTypeInputError()
        {
            Console.WriteLine("Неверно указан тип героя. Это должно быть число в диапазоне {0}-{1}", 1, _availableHeroes.Length);
        }

        private void PrintHeroNameInputError()
        {
            Console.WriteLine("Имя героя не может быть пустым.");
        }

        private void StartBattle()
        {
            SelectedHero firstSelectedHero = _selectedHeroes[0];
            SelectedHero secondSelectedHero = _selectedHeroes[1];

            Hero firstHero = _heroFactory.CreateHeroByType(firstSelectedHero.Type, firstSelectedHero.Name);
            Hero secondHero = _heroFactory.CreateHeroByType(secondSelectedHero.Type, secondSelectedHero.Name);

            var arena = new Arena(firstHero, secondHero);

            arena.BattleFinished += AwaitCommand;

            arena.StartBattle();
        }

        private void AwaitCommand()
        {
            Console.WriteLine("0 - Рестарт");
            Console.WriteLine("1 - Завершить");
            Console.WriteLine("Укажите команду:");

            AwaitCommandInput();
        }

        private void AwaitCommandInput()
        {
            while (true)
            {
                int inputNumber = ParseIntegerInput(Console.ReadLine());

                if (CheckCommandNumberIsValid(inputNumber) == false)
                {
                    PrintInvalidCommandNumber();
                    continue;
                } 
                else
                {
                    ProcessCommand((InputCommand)inputNumber);
                    break;
                }
            }
        }

        private bool CheckCommandNumberIsValid(int number)
        {
            InputCommand[] commands = Enum.GetValues<InputCommand>();

            return number >= 0 && number <= commands.Length;
        }

        private void PrintInvalidCommandNumber()
        {
            Console.WriteLine("Неверное значение команды. Это должгно быть число в диапазоне {0}-{1}", 0, _availableInputCommands.Length - 1);
        }

        private void ProcessCommand(InputCommand command)
        {
            switch (command)
            {
                case InputCommand.Restart:
                    Reset();
                    break;
                case InputCommand.Exit:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
