using GladiatorsArena.ArenaModule;
using GladiatorsArena.Heroes;
using GladiatorsArena.Presentation;
using System.Drawing;

namespace GladiatorsArena.Input
{
    public class GameInput
    {
        private const int NumberOfHeroes = 2;

        private const string WarriorInfo = "Восстанавливает здроровье и получает дополнительный урон при первой смерти.";
        private const string MageInfo = "Увеличивает урон от атаки и поглощает урон с помощью ледяной формы.";
        private const string VampireInfo = "Излечивается от каждой атаки.";
        private const string ChaosKnightInfo = "Блокирует часть магического урона. Призывают иллюзию, которая наносит урон";
        private const string AncientGolemInfo = "Отражает часть входящего урона обратно во врага. Наносит случайный урон.";

        private readonly HeroType[] _availableHeroTypes = Enum.GetValues<HeroType>();

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
            foreach (HeroType type in _availableHeroTypes)
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
                if (currentHeroIndex > NumberOfHeroes)
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

                string? input = Console.ReadLine();

                if (int.TryParse(input, out int heroTypeNumber) && CheckHeroTypeNumberIsCorrect(heroTypeNumber))
                {
                    return (HeroType)(heroTypeNumber - 1);
                }
                else
                {
                    PrintHeroTypeInputError();
                    continue;
                }
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
            Console.WriteLine("Доступные герои:");

            for (int i = 0; i < _availableHeroTypes.Length; i++)
            {
                var heroType = _availableHeroTypes[i];
                Console.WriteLine("{0}. {1}", i + 1, _heroesInfo[heroType]);
            }
        }

        private bool CheckHeroTypeNumberIsCorrect(int heroTypeNumber)
        {
            return heroTypeNumber > 0 && heroTypeNumber <= _availableHeroTypes.Length;
        }

        private void PrintHeroTypeInputError()
        {
            Console.WriteLine("Неверно указан тип героя. Это должно быть число в диапазоне {0}-{1}", 1, _availableHeroTypes.Length);
        }

        private void PrintHeroNameInputError()
        {
            Console.WriteLine("Имя героя не может быть пустым.");
        }

        private void StartBattle()
        {
            int selectedHeroesCount = _selectedHeroes.Count;

            if (selectedHeroesCount != NumberOfHeroes)
            {
                string message = string.Format("Количество выбранных героев для участия на арене должно быть равно {0}, а было {1}", NumberOfHeroes, selectedHeroesCount);
                throw new ArgumentException(message);
            }

            Hero firstHero = CreateHero(0);
            Hero secondHero = CreateHero(1);

            var arena = new Arena(firstHero, secondHero);

            arena.BattleFinished += AwaitCommand;

            arena.StartBattle();
        }

        private Hero CreateHero(int heroNumber)
        {
            SelectedHero selectedHero = _selectedHeroes[heroNumber];

            return _heroFactory.CreateHeroByType(selectedHero.Type, selectedHero.Name);
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
                string? input = Console.ReadLine();

                if (int.TryParse(input, out int inputNumber) && CheckCommandNumberIsValid(inputNumber))
                {
                    ProcessCommand((InputCommand)inputNumber);
                    break;
                } 
                else
                {
                    PrintInvalidCommandNumber();
                    continue;
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
                    break;
            }
        }

        private enum InputCommand
        {
            Restart,
            Exit
        }

        private record SelectedHero(HeroType Type, string Name);
    }
}
