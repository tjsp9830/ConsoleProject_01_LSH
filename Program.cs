namespace Maze
{
    internal class Program
    {
        public struct GameData
        {
            public bool running; // 게임 진행여부, 기본 false

            public bool[,] map;
            public int[,] map2;
            public ConsoleKey inputKey;
            public Point playerPos;
            public Point goalPos;
        }

        public struct Point
        {
            public int x;
            public int y;
        }

        static GameData data; //함수들에서 data에 모두 접근 가능하게 하려고 main 밖에 놓음

        static void Main(string[] args)
        {
            // 준비단계: 
            Start();

            // 게임 루프
            while (data.running)
            {
                Render(); // 표현작업 (싹 지우고 다시 그리기)
                Input();  // 입력작업 (ReadKey로 입력받기)
                Update(); // 처리작업 (처리하기)
            }

            // 마무리단계
            End();
        }

        static void Start()
        {
            Console.CursorVisible = false; // 커서를 안보이게 (입력대기 반짝반짝 끄기)

            data = new GameData();

            data.running = true;
            //data.map = new bool[,]
            //{
            //    { false, false, false,  true, false, false, false },
            //    { false,  true, false,  true,  true,  true, false },
            //    { false,  true, false, false, false,  true, false },
            //    { false,  true,  true,  true,  true,  true, false },
            //    { false,  true, false,  true, false, false, false },
            //    { false,  true, false,  true,  true,  true, false },
            //    { false, false, false, false, false, false, false },
            //};
            data.map2 = new int[,]
            {
                // 0은 벽, 1은 길, 2는 몹, 3은 열쇠, 4는 문, 5는 골            
                { 0, 0, 0, 0, 0, 0, 0 },
                { 0, 1, 0, 2, 1, 1, 0 },
                { 0, 1, 0, 0, 0, 1, 0 },
                { 0, 1, 1, 1, 1, 1, 0 },
                { 0, 1, 0, 1, 0, 0, 0 },
                { 0, 2, 0, 1, 1, 1, 0 },
                { 0, 0, 0, 0, 0, 0, 0 },

            }
            ;
            data.playerPos = new Point() { x = 1, y = 1 };
            data.goalPos = new Point() { x = 5, y = 5 };

            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=        !수정 광산 탈출!          =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("    계속하려면 아무키나 누르세요    ");
            Console.ResetColor();
            Console.ReadKey();
        }

        static void End()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=            !탈출 성공!           =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void EndFail()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=            !탈출 실패!           =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.ResetColor();
            Console.WriteLine();
        }

        static void Render()
        {
            Console.Clear();

            PrintMap();
            PrintPlayer();
            PrintGoal();
        }

        static void Input()
        {
            data.inputKey = Console.ReadKey(true).Key;
        }

        static void Update()
        {
            Move();
            CheckGameClear();
        }

        static void PrintMap()
        {
            for (int y = 0; y < data.map2.GetLength(0); y++)
            {
                for (int x = 0; x < data.map2.GetLength(1); x++)
                {
                    if (data.map2[y, x] == 0 || data.map2[y, x] == 4) // 벽, 문
                    {
                        Console.Write("#");
                    }
                    else if (data.map2[y, x] == 1 || data.map2[y, x] == 5) // 길, 골
                    {
                        Console.Write(" ");
                    }
                    else if (data.map2[y, x] == 2 || data.map2[y, x] == 3) // 몹, 열쇠
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("#");
                        Console.ResetColor ();
                    }
                    else
                        Console.Write(" ");

                }
                Console.WriteLine();
            }
        }

        static void PrintPlayer()
        {
            Console.SetCursorPosition(data.playerPos.x, data.playerPos.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("P");
            Console.ResetColor();
        }

        static void PrintGoal()
        {
            Console.SetCursorPosition(data.goalPos.x, data.goalPos.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("G");
            Console.ResetColor();
        }

        static void Move()
        {
            switch (data.inputKey)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    MoveUp();
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    MoveDown();
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    MoveLeft();
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    MoveRight();
                    break;
            }
        }

        static void CheckGameClear()
        {
            if (data.playerPos.x == data.goalPos.x &&
                data.playerPos.y == data.goalPos.y)
            {
                data.running = false;
            }
        }

        static void MoveUp()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y - 1 };
            if (data.map2[next.y, next.x] != 0 && data.map2[next.y, next.x] != 4)
            {
                data.playerPos = next;
            }
        }

        static void MoveDown()
        {
            Point next = new Point() { x = data.playerPos.x, y = data.playerPos.y + 1 };
            if (data.map2[next.y, next.x] != 0 && data.map2[next.y, next.x] != 4)
            {
                data.playerPos = next;
            }
        }

        static void MoveLeft()
        {
            Point next = new Point() { x = data.playerPos.x - 1, y = data.playerPos.y };
            if (data.map2[next.y, next.x] != 0 && data.map2[next.y, next.x] != 4)
            {
                data.playerPos = next;
            }
        }

        static void MoveRight()
        {
            Point next = new Point() { x = data.playerPos.x + 1, y = data.playerPos.y };
            if (data.map2[next.y, next.x] != 0 && data.map2[next.y, next.x] != 4)
            {
                data.playerPos = next;
            }
        }
    }
}
