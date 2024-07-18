using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day07_consoleProject
{

    internal class Program
    {

        // 게임 데이터
        struct GameData
        {
            public bool isRunning;          // 게임 실행중 여부 
            public string[,] myMap;         // 맵 배열
            public ConsoleKey ConsoleKey;   // 키 입력 받아올 변수
            public InGamePos posPlayer;     // 플레이어 위치
            public InGamePos posGoal;       // 골인지점 위치

        }

        // 위치값
        struct InGamePos
        {
            public int x;
            public int y;
        }



        static GameData gameData;

        static void Main(string[] args)
        {

            // 게임 준비단계
            Ready();

            // 게임 루프과정
            while (gameData.isRunning)
            {

                Render();   // 나타내기                                
                Input();    // 입력받기               
                Update();   // 처리하기

            }

            // 게임 엔딩단계
            TheEnd();


        }



        static void Ready()
        {

            // 게임 진행여부 -> 실행
            gameData.isRunning = true;

            // 커서 깜빡거림 중지
            Console.CursorVisible = false;

            // 맵 구성 초기화
            gameData.myMap = new string[,]
            {

                { "벽", "벽", "벽", "벽", "벽" },
                { "벽", "길", "길", "길", "벽" },
                { "벽", "길", "길", "길", "벽" },
                { "벽", "길", "길", "길", "벽" },
                { "벽", "벽", "벽", "벽", "벽" },

            };

            // 플레이어, 골인지점 위치 초기화
            gameData.posPlayer = new InGamePos() { x = 1, y = 1 };
            gameData.posGoal = new InGamePos() { x = 3, y = 3 };


            // 타이틀 멘트
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

            // 키 입력받기
            Console.ReadKey();


        }



        // 나타내기
        static void Render()
        {

            PrintMap();
            PrintPlayer();
            PrintGoal();


        }



        // 맵 출력
        static void PrintMap()
        {

            // 이전내용 삭제
            Console.Clear();
            
            // 맵 배열의 0번째 인자, 즉 y축 길이 알아오기
            for(int i = 0; i < gameData.myMap.GetLength(0); i++)
            {

                // 맵 배열의 1번째 인자, 즉 x축 길이 알아오기    
                for (int j = 0; j < gameData.myMap.GetLength(1); j++)
                {

                    if (gameData.myMap[i, j] == "벽")
                        Console.Write("#");
                    else if (gameData.myMap[i, j] == "길")
                        Console.Write(" ");
                    

                }

                Console.WriteLine("");

            }
            
            

        }



        // 플레이어 출력
        static void PrintPlayer()
        {
            // Console.SetCursorPosition(x,y) --> 커서를 사용자가 설정한 위치값에 위치시키는 함수
            Console.SetCursorPosition(gameData.posPlayer.x, gameData.posPlayer.y);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("P");
            Console.ResetColor();
                        
        }


        // 골인지점 출력
        static void PrintGoal()
        {

            Console.SetCursorPosition(gameData.posGoal.x, gameData.posGoal.y);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("G");
            Console.ResetColor();
                        
        }



        // 입력받기
        static void Input()
        {

            gameData.ConsoleKey = Console.ReadKey(true).Key;
            
        }



        // 처리하기
        static void Update()
        {

            // 플레이어 움직이기
            PlayerMove();

            // 게임 실행이 끝났는지 여부 판단하기
            IsTheEnd();

        }



        // 입력받은 값에 따라 플레이어 움직이기
        static void PlayerMove()
        {

            switch(gameData.ConsoleKey)
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



        static void MoveUp()
        {// 윗키 입력을 받으면, 

            // 이 다음에 위치할 예정인 포지션을 받아와서 next라는 InGamePos구조체 변수에 넣음
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x, y = gameData.posPlayer.y - 1 };
            
            // 맵 배열의 y, x축에 next의 xy값을 대입해보고, 그 맵 배열의 string값이 벽이 아니면
            if(gameData.myMap[next.y,next.x] != "벽")
            {
                // 플레이어의 위치값에 next를 넣음
                gameData.posPlayer = next;
            }


        }

        static void MoveDown()
        {            
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x, y = gameData.posPlayer.y + 1 };

            if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }

        static void MoveLeft()
        {
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x - 1, y = gameData.posPlayer.y };

            if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }

        static void MoveRight()
        {
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x + 1, y = gameData.posPlayer.y };

            if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }



        static void IsTheEnd()
        {

            if(gameData.posGoal.y == gameData.posPlayer.y &&
               gameData.posGoal.x == gameData.posPlayer.x)
            {
                gameData.isRunning = false;

            }


        }



        static void TheEnd()
        {

            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=        !광산 탈출 성공!          =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            Console.ResetColor();

        }



    }




}

//    internal class Program
//    {

//        // 구조체 게임데이터 만듦
//        // 게임 진행여부, 맵 배열, 키 입력, 플레이어 위치, 골인지점 위치
//        struct GameData
//        {

//            public bool isRunning;             // 게임 진행여부 (T=진행중, F=끝남)
//            public string[,] stMap;            // 맵 배열
//            public ConsoleKey ConsoleKey;      // 키 입력
//            public InGamePos inGamePosPlayer;  // 플레이어 위치
//            public InGamePos inGamePosGoal;    // 골인지점 위치

//        }

//        // 구조체 위치정보 만듦
//        // 위 게임데이터 속 플레이어와 골인지점의 위치가 이 구조체임
//        struct InGamePos
//        {

//            public int X;
//            public int Y;


//        }


//        // 모든 함수에서 접근 할 수 있도록 전역변수로 선언
//        static GameData gameData;


//        // Main함수 속 흐름
//        // 준비단계 --> 게임 루프 --> 마무리단계
//        static void Main(string[] args)
//        {

//            // 준비단계
//            Ready();

//            // 게임 루프단계
//            while (gameData.isRunning)
//            {

//                Rendering();    //표현작업
//                Input();        //입력작업
//                Update();       //처리작업

//            }

//            // 마무리단계
//            TheEnd();


//        }


//        // 준비단계:
//        // 커서 안보이게하기, 게임 진행여부 F, 게임데이터 구조체 속 정보 초기화, 게임 시작멘트 출력
//        static void Ready()
//        {

//            // 커서를 안보이게하기 (입력대기 반짝반짝 끄기)
//            Console.CursorVisible = false;


//            // 게임 진행여부 T
//            gameData.isRunning = true;


//            // 게임데이터 구조체 속 맵 정보 초기화

//            gameData.stMap = new string[,]
//            {

//                { "벽", "벽", "벽", "벽", "벽" },
//                { "벽", "길", "길", "길", "벽" },
//                { "벽", "길", "길", "길", "벽" },
//                { "벽", "길", "길", "길", "벽" },
//                { "벽", "벽", "벽", "벽", "벽" },

//            };


//            // 게임데이터 구조체 속 위치정보 초기화
//            gameData.inGamePosPlayer = new InGamePos() { X = 1, Y = 1 };
//            gameData.inGamePosGoal = new InGamePos() { X = 3, Y = 3 };


//            // 게임 시작멘트 출력

//            Console.ForegroundColor = ConsoleColor.White;
//            Console.WriteLine("*********************************");
//            Console.WriteLine("*                               *");
//            Console.WriteLine("*       수정동굴 탐험하기       *");
//            Console.WriteLine("*                               *");
//            Console.WriteLine("*********************************");

//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.WriteLine("\n* 시작하려면 아무 키나 눌러주세요");
//            Console.ResetColor();

//            Console.ReadKey();


//        } // Ready함수 끝부분



//        // 루프단계: 표현작업
//        // 콘솔내용 다 지우고 다시 출력하기, 맵 출력하기, 플레이어 위치 출력, 골인지점 출력
//        static void Rendering()
//        {

//            // 콘솔내용 다 지우고 다시 출력하기
//            Console.Clear();


//            // 맵 프린트            
//            PrintMap();

//            // 플레이어 프린트
//            PrintPlayer();

//            // 골인지점 프린트
//            PrintGoal();



//        }


//        static void PrintMap()
//        {

//            for (int i = 0; i < gameData.stMap.GetLength(0); i++)
//            {

//                for (int j = 0; j < gameData.stMap.GetLength(1); j++)
//                {

//                    if (gameData.stMap[i, j] == "벽")
//                    {
//                        Console.Write("#");
//                    }
//                    else if (gameData.stMap[i, j] == "길")
//                    {
//                        Console.Write(" ");
//                    }
//                    //else if (gameData.stMap[j, i] == "플")
//                    //{
//                    //    Console.ForegroundColor = ConsoleColor.Yellow;
//                    //    Console.Write("P");
//                    //    Console.ResetColor();
//                    //}
//                    //else if (gameData.stMap[j, i] == "골")
//                    //{
//                    //    Console.ForegroundColor = ConsoleColor.Green;
//                    //    Console.Write("G");
//                    //    Console.ResetColor();
//                    //}


//                }

//                Console.WriteLine("");

//            }

//        }


//        // 플레이어 위치에 P 프린트
//        static void PrintPlayer()
//        {
//            Console.SetCursorPosition(gameData.inGamePosPlayer.X, gameData.inGamePosPlayer.Y);
//            // Console.SetCursorPosition(0,0) --> 커서를 사용자가 설정한 위치값에 위치시키는 함수
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.Write("P");
//            Console.ResetColor();

//        }


//        // 골인지점 위치에 G 프린트
//        static void PrintGoal()
//        {
//            Console.SetCursorPosition(gameData.inGamePosGoal.X, gameData.inGamePosGoal.Y);
//            Console.ForegroundColor = ConsoleColor.Green;
//            Console.Write("G");
//            Console.ResetColor();

//        }



//        // 루프단계: 입력작업
//        // (ReadKey로 입력받기)
//        static void Input()
//        {

//            // ReadKey로 입력받기            
//            gameData.ConsoleKey = Console.ReadKey(true).Key;

//        }


//        // 루프단계: 처리작업
//        // (움직임 연산하기, 게임 클리어 여부 판단하기)
//        static void Update()
//        {

//            // 움직임 연산하기
//            PlayerMove();

//            // 게임 클리어 여부 판단하기
//            CheckClear();

//        }


//        // 움직임 연산하기        
//        static void PlayerMove()
//        {

//            // 입력과정에서 들어온 값 switch로 선택 제어하기

//            switch (gameData.ConsoleKey)
//            {

//                case ConsoleKey.W : 
//                case ConsoleKey.UpArrow :
//                    MoveUp();
//                    break;

//                case ConsoleKey.A:
//                case ConsoleKey.LeftArrow:
//                    MoveLeft();
//                    break;

//                case ConsoleKey.S:
//                case ConsoleKey.DownArrow:
//                    MoveDown();
//                    break;

//                case ConsoleKey.D:
//                case ConsoleKey.RightArrow:
//                    MoveRight();
//                    break;

//            }

//        }


//        // 들어온 값에 맞는 이동함수 실행시키기

//        static void MoveUp()
//        {

//            InGamePos next = new InGamePos() { X= gameData.inGamePosPlayer.X, Y= gameData.inGamePosPlayer.Y-1 };
//            if (gameData.stMap[next.Y, next.X] != "벽")
//            {
//                gameData.inGamePosPlayer = next;
//            }


//        }

//        static void MoveLeft()
//        {

//            InGamePos next = new InGamePos() { X = gameData.inGamePosPlayer.X-1, Y = gameData.inGamePosPlayer.Y };
//            if (gameData.stMap[next.Y, next.X] != "벽")
//            {
//                gameData.inGamePosPlayer = next;
//            }

//        }

//        static void MoveDown()
//        {

//            InGamePos next = new InGamePos() { X = gameData.inGamePosPlayer.X, Y = gameData.inGamePosPlayer.Y + 1 };
//            if (gameData.stMap[next.Y, next.X] != "벽")
//            {
//                gameData.inGamePosPlayer = next;
//            }

//        }

//        static void MoveRight()
//        {

//            InGamePos next = new InGamePos() { X = gameData.inGamePosPlayer.X+1, Y = gameData.inGamePosPlayer.Y };
//            if (gameData.stMap[next.Y, next.X] != "벽")
//            {
//                gameData.inGamePosPlayer = next;
//            }

//        }



//        // 게임 클리어 여부 판단하기
//        static void CheckClear()
//        {

//            if(gameData.inGamePosPlayer.X == gameData.inGamePosGoal.X && 
//                gameData.inGamePosPlayer.Y == gameData.inGamePosGoal.Y)
//            {
//                gameData.isRunning = false;
//            }


//        }



//        // 마무리단계: 게임 클리어 멘트 출력
//        static void TheEnd()
//        {

//            Console.Clear();

//            Console.ForegroundColor = ConsoleColor.Cyan;
//            Console.WriteLine("*********************************");
//            Console.WriteLine("*                               *");
//            Console.WriteLine("*       수정동굴 탐험성공       *");
//            Console.WriteLine("*                               *");
//            Console.WriteLine("*********************************");
//            Console.ResetColor();

//        }




//    }



//}
