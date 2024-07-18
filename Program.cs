using System.ComponentModel.Design;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Day07_consoleProject
{

    internal class Program
    {

        // 게임 클리어 여부
        struct GameClear
        {
            public bool isRunning;          // 게임 실행중 여부 
            public bool isPlayerDie;          // 플레이어가 죽었는지 여부
            public bool isClearStage1;        // 스테이지 클리어 여부  
            public bool isClearStage2;        // 스테이지 클리어 여부  
            public bool isClearStage3;        // 스테이지 클리어 여부  
            public bool isDoorUnLock;           // 열쇠 습득 여부
            public bool isGetMap;               // 지도 습득 여부

        }


        // 게임 데이터
        struct GameData
        {
            public string[,] myMap;         // 맵 배열
            public ConsoleKey ConsoleKey;   // 키 입력 받아올 변수
            public InGamePos posPlayer;     // 플레이어 위치
            public InGamePos posGoal;       // 골인지점 위치
            public int StageData;             // 스테이지 정보
            public int playerHP;              // 플레이어 체력

        }
                

        // 위치값
        struct InGamePos
        {
            public int x;
            public int y;
        }


        // 맵 배열에 쓸 열거형
        enum dData { 벽=0, 길=1, 몹=2, 키=3, 문=4, 골=5 }
        //enum dData { Wall=0, Road=1, Monster=2, Key=3, Door=4, Goal=5 }


        // 구조체와 열거형을 모든 함수에서 사용할 수 있도록 전역변수로 선언
        static GameData gameData;
        static GameClear gameClear;
        static dData stageElement;


        /// <summary>
        /// 프로그램 실행 흐름이 담긴 메인함수
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {

            // 게임 타이틀
            Title();

            // 게임 준비단계
            // 이 함수들은 Main함수 흐름에 따라 호출되는게 아니라,
            // Title()함수와 NextStage() 함수에서 직접 호출했습니다.
            // 이런식으로 굴리면 나중에 스파게티 코드가 될 위험이 있나요?
            //ReadyStage1();
            //ReadyStage2();
            //ReadyStage3();

            // 게임 루프과정
            while (gameClear.isRunning && !gameClear.isPlayerDie)
            {

                // 나타내기
                Render();
                //ㄴPrintMap();
                ////ㄴPrintScript(); --> 플레이어 체력, 접촉한 아이템 정보 등 출력 함수
                //ㄴPrintPlayer();
                //ㄴPrintGoal();

                // 입력받기
                Input();
                //ㄴgameData.ConsoleKey = Console.ReadKey(true).Key;                

                // 처리하기
                Update();
                //ㄴPlayerMove();
                ////ㄴMoveUp(); MoveDown(); MoveLeft(); MoveRight();
                //ㄴPlayerHit();
                ////ㄴGetDamage();
                //ㄴIsTheEnd();
                ////ㄴNextStage();
                ////ㄴTheFail(); ----> if(isPlayerDie) 콘솔 정지
                ////ㄴTheEnd(); -----> if(isRunning)   콘솔 정지

            }


        }



        static void Title()
        {

            // 게임 진행여부 -> 실행
            gameClear.isRunning = true;

            // 플레이어 부활
            gameClear.isPlayerDie = false;

            // 스테이지 클리어 여부 -> 비활성화
            gameClear.isClearStage1 = false;
            gameClear.isClearStage2 = false;
            gameClear.isClearStage3 = false;


            // 커서 깜빡거림 중지
            Console.CursorVisible = false;


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

            // 이전내용 삭제
            Console.Clear();

            // 스테이지 1 준비
            ReadyStage1();

        }


        static void ReadyStage1()
        {   

            // 스테이지 1 진입 여부
            gameClear.isClearStage1 = true;

            // 다시 문단속
            gameClear.isDoorUnLock = false;

            // 지도 보유여부 -> 지도기믹 없는 스테이지
            gameClear.isGetMap = true;


            // 스테이지 정보 초기화
            gameData.StageData = 1;

            // 맵구성 초기화
            gameData.myMap = new string[,]
            {
                { "벽", "벽", "벽", "벽", "벽", "벽", "벽" },
                { "벽", "길", "벽", "몹", "길", "길", "벽" },
                { "벽", "길", "벽", "벽", "벽", "길", "벽" },
                { "벽", "길", "길", "길", "길", "길", "벽" },
                { "벽", "길", "벽", "길", "벽", "벽", "벽" },
                { "벽", "몹", "벽", "길", "길", "길", "벽" },
                { "벽", "벽", "벽", "벽", "벽", "벽", "벽" },
            };

            // 플레이어 위치, 골인지점 초기화
            gameData.posPlayer = new InGamePos() { x = 1, y = 1 };
            gameData.posGoal = new InGamePos() { x = 5, y = 5 };

            // 플레이어 체력 부여
            gameData.playerHP = 3;


            // stage 1 표시멘트
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================");
            Console.WriteLine("=           ! Stage 1 !           =");
            Console.WriteLine("====================================");
            Console.ResetColor();

            // 키 입력받기
            Console.ReadKey();

        }



        static void ReadyStage2()
        {

            // 스테이지 1 클리어 했을때만 실행되게 하기
            if (gameClear.isClearStage1 = false)
                return;

            // 스테이지 2 진입 여부
            gameClear.isClearStage2 = true;

            // 다시 문단속
            gameClear.isDoorUnLock = false;

            // 지도 보유여부 -> 지도기믹 없는 스테이지
            gameClear.isGetMap = true;


            // 스테이지 정보 초기화
            gameData.StageData = 2;

            // 맵구성 초기화
            gameData.myMap = new string[,]
            {
                // 벽이랑 문은 #, 길은 공란, 골 자리도 공란으로 표기 후 골과 플레이어는 따로 출력
                // 몹이랑 열쇠(키->노랑), 지도(맵->초록)은 빨간색 #, 텔포(텔)는 파란색 #으로 표기 
                { "벽", "벽", "벽", "벽", "벽",   "벽", "벽", "벽", "벽", "벽",   "벽", "벽", "벽", "벽", "벽" },
                { "벽", "벽", "길", "길", "길",   "길", "길", "길", "길", "길",   "길", "길", "길", "몹", "벽" },
                { "벽", "키", "벽", "벽", "벽",   "골", "벽", "길", "벽", "벽",   "벽", "벽", "길", "벽", "벽" },
                { "벽", "길", "벽", "길", "벽",   "벽", "벽", "벽", "벽", "벽",   "벽", "길", "길", "길", "벽" },
                { "벽", "길", "벽", "길", "길",   "길", "길", "길", "길", "벽",   "벽", "문", "벽", "벽", "벽" },
                
                { "벽", "길", "벽", "몹", "벽",   "길", "벽", "벽", "길", "길",   "길", "길", "벽", "벽", "벽" },
                { "벽", "길", "길", "길", "벽",   "길", "길", "길", "몹", "벽",   "벽", "길", "길", "길", "벽" },
                { "벽", "벽", "길", "길", "벽",   "길", "벽", "길", "벽", "벽",   "벽", "길", "벽", "벽", "벽" },
                { "벽", "벽", "벽", "길", "길",   "길", "길", "길", "길", "벽",   "벽", "길", "길", "몹", "벽" },
                { "벽", "벽", "길", "길", "벽",   "벽", "벽", "길", "벽", "벽",   "벽", "길", "벽", "길", "벽" },
                
                { "벽", "벽", "길", "길", "길",   "벽", "길", "길", "길", "길",   "길", "길", "벽", "길", "벽" },
                { "벽", "길", "길", "벽", "길",   "벽", "길", "벽", "벽", "벽",   "벽", "벽", "벽", "길", "벽" },
                { "벽", "길", "길", "길", "길",   "길", "길", "길", "길", "길",   "길", "길", "벽", "길", "벽" },
                { "벽", "몹", "벽", "벽", "벽",   "벽", "벽", "벽", "벽", "길",   "벽", "길", "길", "길", "벽" },
                { "벽", "벽", "벽", "벽", "벽",   "벽", "벽", "벽", "벽", "벽",   "벽", "벽", "벽", "벽", "벽" }
            };

            // 플레이어 위치, 골인지점 초기화
            gameData.posPlayer = new InGamePos() { x = 13, y = 13 };
            gameData.posGoal = new InGamePos() { x = 5, y = 2 };

            // 플레이어 체력 부여
            gameData.playerHP = 3;


            // stage 2 표시멘트
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================");
            Console.WriteLine("=           ! Stage 2 !           =");
            Console.WriteLine("====================================");
            Console.ResetColor();

            // 키 입력받기
            Console.ReadKey();

        }



        static void ReadyStage3()
        {

            // 스테이지 2 클리어 했을때만 실행되게 하기
            if (gameClear.isClearStage2 = false)
                return;

            // 스테이지 3 진입 여부
            gameClear.isClearStage3 = true;

            // 다시 문단속
            gameClear.isDoorUnLock = false;

            // 지도 보유여부 -> 지도기믹 있는 스테이지
            gameClear.isGetMap = false;


            // 스테이지 정보 초기화
            gameData.StageData = 3;

            // 맵 구성 초기화
            gameData.myMap = new string[,]
            {

                // 벽이랑 문은 #, 길은 공란, 골 자리도 공란으로 표기 후 골과 플레이어는 따로 출력
                // 몹이랑 열쇠(키->노랑), 지도(맵->초록)은 빨간색 #, 텔포(텔)는 파란색 #으로 표기 
                { "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽" },   // y = 0
                { "벽", "길", "길", "몹", "길",  "길", "길", "길", "길", "길",    "길", "길", "길", "길", "길",  "길", "길", "길", "길", "벽",    "벽" },   // y = 1
                { "벽", "벽", "벽", "길", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "벽", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "벽",    "벽" },   // y = 2
                { "벽", "길", "길", "길", "길",  "길", "길", "길", "길", "길",    "길", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "맵",    "벽" },   // y = 3
                { "벽", "벽", "벽", "길", "벽",  "벽", "벽", "길", "벽", "벽",    "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 4
                                                                                                                                                        
                { "벽", "몹", "벽", "길", "벽",  "벽", "벽", "길", "벽", "벽",    "벽", "길", "벽", "길", "길",  "길", "길", "길", "길", "길",    "벽" },   // y = 5
                { "벽", "길", "벽", "벽", "벽",  "벽", "벽", "길", "벽", "벽",    "벽", "길", "벽", "길", "벽",  "벽", "벽", "길", "벽", "벽",    "벽" },   // y = 6
                { "벽", "길", "벽", "벽", "몹",  "길", "길", "길", "벽", "길",    "길", "길", "벽", "길", "길",  "길", "벽", "길", "벽", "벽",    "벽" },   // y = 7
                { "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "길", "벽", "벽", "벽",  "길", "벽", "길", "벽", "벽",    "벽" },   // y = 8
                { "벽", "길", "벽", "벽", "벽",  "길", "길", "길", "길", "벽",    "벽", "길", "벽", "벽", "벽",  "몹", "벽", "길", "길", "길",    "벽" },   // y = 9
                                                                                                                                                        
                                                                                                                                                        
                { "벽", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "벽",    "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 10
                { "벽", "길", "길", "문", "길",  "길", "길", "길", "길", "벽",    "벽", "길", "길", "길", "길",  "길", "길", "몹", "벽", "길",    "벽" },   // y = 11
                { "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "길", "길",    "길", "길", "벽", "길", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 12
                { "벽", "길", "길", "길", "길",  "길", "벽", "벽", "벽", "길",    "벽", "벽", "벽", "길", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 13
                { "벽", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "길",    "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 14
                                                                                                                                                        
                { "벽", "길", "벽", "벽", "벽",  "몹", "벽", "벽", "벽", "길",    "벽", "길", "길", "길", "길",  "몹", "벽", "벽", "벽", "길",    "벽" },   // y = 15
                { "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "길",    "벽" },   // y = 16
                { "벽", "길", "벽", "벽", "벽",  "길", "길", "길", "길", "길",    "길", "길", "벽", "벽", "벽",  "길", "길", "길", "길", "길",    "벽" },   // y = 17
                { "벽", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "길",    "벽", "벽", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "길",    "벽" },   // y = 18
                { "벽", "길", "길", "길", "길",  "길", "벽", "벽", "벽", "길",    "벽", "길", "벽", "길", "벽",  "길", "벽", "벽", "벽", "길",    "벽" },   // y = 19
                                                                                                                                                        
                                                                                                                                                        
                { "벽", "벽", "벽", "길", "벽",  "벽", "벽", "벽", "벽", "길",    "벽", "길", "벽", "길", "벽",  "길", "벽", "벽", "벽", "길",    "벽" },   // y = 20
                { "벽", "벽", "벽", "길", "벽",  "몹", "길", "길", "길", "길",    "길", "길", "길", "길", "벽",  "길", "벽", "길", "길", "길",    "벽" },   // y = 21
                { "벽", "길", "길", "길", "벽",  "벽", "벽", "벽", "벽", "길",    "벽", "벽", "벽", "길", "벽",  "길", "벽", "벽", "벽", "길",    "벽" },   // y = 22
                { "벽", "길", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "길",    "벽", "벽", "벽", "몹", "벽",  "길", "길", "길", "벽", "길",    "벽" },   // y = 23
                { "벽", "몹", "벽", "벽", "벽",  "길", "벽", "벽", "벽", "벽",    "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "길", "벽", "길",    "벽" },   // y = 24
                                                                                                                                                        
                { "벽", "벽", "벽", "길", "길",  "길", "길", "길", "길", "길",    "길", "길", "길", "길", "길",  "길", "길", "길", "벽", "길",    "벽" },   // y = 25
                { "벽", "벽", "벽", "길", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "길", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 26
                { "벽", "길", "길", "길", "길",  "길", "길", "길", "벽", "벽",    "벽", "길", "길", "길", "키",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 27
                { "벽", "길", "벽", "벽", "벽",  "벽", "벽", "길", "벽", "벽",    "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "길",    "벽" },   // y = 28
                { "벽", "길", "벽", "벽", "벽",  "길", "길", "길", "길", "길",    "길", "길", "길", "길", "길",  "길", "길", "길", "길", "몹",    "벽" },   // y = 29
                                                                                                                                                        
                                                                                                                                                        
                { "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽", "벽", "벽", "벽", "벽",  "벽", "벽", "벽", "벽", "벽",    "벽" },   // y = 30
                

            };

            // 플레이어, 골인지점 위치 초기화
            gameData.posPlayer = new InGamePos() { x = 1, y = 29 };
            gameData.posGoal = new InGamePos() { x = 18, y = 1 };

            // 플레이어 체력 부여
            gameData.playerHP = 3;


            // stage 3 표시멘트
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("====================================");
            Console.WriteLine("=           ! Stage 3 !           =");
            Console.WriteLine("====================================");
            Console.ResetColor();

            // 키 입력받기
            Console.ReadKey();



        }



        // 나타내기
        static void Render()
        {

            PrintMap();
            //ㄴPrintScript();
            //PrintScript();
            PrintPlayer();
            PrintGoal();


        }



        // 맵 출력
        static void PrintMap()
        {

            // 이전내용 삭제
            Console.Clear();

            // 맵 배열의 0번째 인자, 즉 y축 길이 알아오기
            for (int i = 0; i < gameData.myMap.GetLength(0); i++)
            {

                // 맵 배열의 1번째 인자, 즉 x축 길이 알아오기    
                for (int j = 0; j < gameData.myMap.GetLength(1); j++)
                {

                    // 벽이랑 문은 #, 길은 공란, 열쇠(키)랑 몹은 빨간색 #, 텔포(텔)는 하늘색 #, 골 자리도 공란으로 표기
                    if (gameData.myMap[i, j] == "길" || gameData.myMap[i, j] == "골")
                    {

                        Console.Write(" ");

                    }

                    else if (gameData.myMap[i, j] == "벽")
                    {
                        Console.Write("#");

                    }
                    else if (gameData.myMap[i, j] == "문")
                    {

                        if (gameClear.isDoorUnLock == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;
                            Console.Write("#");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.Write("#");
                        }


                    }

                    else if (gameData.myMap[i, j] == "몹")
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("#");
                        Console.ResetColor();
                    }
                    else if (gameData.myMap[i, j] == "키")
                    {

                        if (gameClear.isDoorUnLock == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("#");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("#");
                            Console.ResetColor();
                        }


                    }
                    else if (gameData.myMap[i, j] == "맵")
                    {

                        if (gameClear.isGetMap == true)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.Write("#");
                            Console.ResetColor();
                        }
                        else 
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("#");
                            Console.ResetColor();
                        }
                        
                    }
                    //else if (gameData.myMap[i, j] == "텔")
                    //{
                    //    Console.ForegroundColor = ConsoleColor.Cyan;
                    //    Console.Write("#");
                    //    Console.ResetColor();
                    //}


                }

                Console.WriteLine("");

            }


            // 맵 아래에 스크립트 출력
            PrintScript();
            

        }




        // 맵 아래에 출력될 메시지 
        static void PrintScript()
        {

            // ******************** (키입력시에도 지워지지 않는 스크립트)

            // 플레이어 위치와 게이트, 충돌이벤트의 설명 표시            
            Console.Write("\n도움말 - ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("P");
            Console.ResetColor();
            Console.Write(": 플레이어, ");
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.Write("G");
            Console.ResetColor();
            Console.Write(": 게이트, ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("#");
            Console.ResetColor();
            Console.Write(": 아이템or함정");

            // 현재 스테이지 정보와 플레이어의 체력 표시 
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write($"\n~~ 스테이지 {gameData.StageData}, ");
            Console.WriteLine($"플레이어 체력: {gameData.playerHP} ~~");
            Console.ResetColor();
                        
            // 열쇠 획득시에만 열쇠보유 스크립트 출력
            if (gameClear.isDoorUnLock == true)
            { 
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("열쇠를 찾았습니다!! 숨겨진 문을 열 수 있습니다.");
                Console.ResetColor();
            }

            // 지도 기믹이 있는 스테이지 에서만 지도 습득시 지도보유 스크립트 출력
            if (gameData.StageData == 3 && gameClear.isGetMap == true)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("지도를 획득했습니다!! 게이트가 나타납니다.");
                Console.ResetColor();
            }



            // ******************** (키입력시 맵을 새로 출력하는 과정에서 지워짐)

            // 플레이어 충돌이벤트 처리 및 스크립트 출력

            // 플레이어가 위치한 y, x값을 맵 배열의 인자로 넣어서 봤을때, 그 요소가 "몹" 등이면 충돌이벤트 처리
            if (gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x] == "몹")
            {                

                // 플레이어 피격처리
                PlayerHit();

                // 스크립트 출력
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("몬스터와 부딪혔습니다! 체력이 감소합니다.");
                Console.ResetColor();


            }
            else if (gameData.myMap[gameData.posPlayer.y - 1, gameData.posPlayer.x] == "문" ||
                     gameData.myMap[gameData.posPlayer.y + 1, gameData.posPlayer.x] == "문" ||
                     gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x - 1] == "문" ||
                     gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x + 1] == "문")
            {

                if (gameClear.isDoorUnLock == false)
                {
                    // 닫힌 문과 충돌했다는 스크립트 출력 (숨겨진 문)
                    Console.WriteLine("숨겨진 문을 발견했습니다! 진입하려면 열쇠가 필요합니다.");
                    
                }                
                

            }
            else if (gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x] == "문")
            {

                if (gameClear.isDoorUnLock == true)
                {
                    // 열린 문과 충돌했다는 스크립트 출력
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("열쇠를 얻어 문을 열고 지나갑니다.");

                }


            }
            else if (gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x] == "키")
            {

                // 열쇠 획득처리
                gameClear.isDoorUnLock = true;

                // 열쇠 획득 스크립트 출력
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("열쇠를 찾았습니다!! 숨겨진 문을 열 수 있습니다.");
                Console.ResetColor();


            }
            else if (gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x] == "맵")
            {

                // 지도 획득처리
                gameClear.isGetMap = true;

                // 지도 획득 스크립트 출력
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("지도를 획득했습니다!! 게이트가 나타납니다.");
                Console.ResetColor();


            }
            //else if (gameData.myMap[gameData.posPlayer.y, gameData.posPlayer.x] == "텔")
            //{

            //    // 텔포 닿은거 처리하기


            //}
            else
            {

                // 나머지는 다 길임, 벽에는 위치할수 없고 쓸 필요도 없음
                //Console.WriteLine("테스트 케이스1 길");


            }



        }



        // 플레이어 출력
        static void PrintPlayer()
        {
            // Console.SetCursorPosition(x,y) --> 커서를 사용자가 설정한 위치값에 위치시키는 함수
            Console.SetCursorPosition(gameData.posPlayer.x, gameData.posPlayer.y);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("P");
            Console.ResetColor();

        }


        // 골인지점 출력
        static void PrintGoal()
        {
            if (gameClear.isGetMap == false)
            {
                // 지도기믹이 있는 스테이지의 경우, 지도를 먹지 않으면 보유를 False로 해둠
                return;

            }
            else
            {
                // 지도 보유 True
                Console.SetCursorPosition(gameData.posGoal.x, gameData.posGoal.y);
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("G");
                Console.ResetColor();
            }

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

            switch (gameData.ConsoleKey)
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

            // 맵 배열의 y, x축에 next의 xy값을 대입해보고,
            // 그 맵 배열의 string값이 "문"이나 "벽"이 아니면 실행
            if (gameData.myMap[next.y, next.x] == "문") 
            {

                if (gameClear.isDoorUnLock == true)
                    gameData.posPlayer = next;

            }
            else if (gameData.myMap[next.y, next.x] != "벽")
            {
                // 플레이어의 위치값에 next를 넣음
                gameData.posPlayer = next;
            }


        }

        static void MoveDown()
        {
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x, y = gameData.posPlayer.y + 1 };

            if (gameData.myMap[next.y, next.x] == "문")
            {

                if (gameClear.isDoorUnLock == true)
                    gameData.posPlayer = next;

            }
            else if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }

        static void MoveLeft()
        {
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x - 1, y = gameData.posPlayer.y };

            if (gameData.myMap[next.y, next.x] == "문")
            {

                if (gameClear.isDoorUnLock == true)
                    gameData.posPlayer = next;

            }
            else if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }

        static void MoveRight()
        {
            InGamePos next = new InGamePos() { x = gameData.posPlayer.x + 1, y = gameData.posPlayer.y };

            if (gameData.myMap[next.y, next.x] == "문")
            {

                if (gameClear.isDoorUnLock == true)
                    gameData.posPlayer = next;

            }
            else if (gameData.myMap[next.y, next.x] != "벽")
            {
                gameData.posPlayer = next;
            }

        }



        // 플레이어 피격 이벤트
        static void PlayerHit()
        {
            gameData.playerHP -= 1;


            if(gameData.playerHP < 1)
            {
                gameClear.isPlayerDie = true;
                IsTheEnd();
            }

        }
        ////ㄴGetDamage();





        // 스테이지를 넘길지, 엔딩을 띄울지 판단
        static void IsTheEnd()
        {

            // 플레이어 체력이 다 닳으면 실패엔딩
            if(gameClear.isPlayerDie)
            {
                TheFail();
            }

            // 게임이 끝나면 성공엔딩
            if(!gameClear.isRunning)
            {
                TheEnd();
            }

            // 플레이어가 골인지점에 닿으면 다음 스테이지로
            if (gameData.posGoal.y == gameData.posPlayer.y &&
               gameData.posGoal.x == gameData.posPlayer.x)
            {

                if (gameClear.isGetMap == false)
                {
                    return;
                }
                else
                {
                    NextStage();
                }
                
            }

        }


        static void NextStage()
        {

            // 이전내용 삭제
            Console.Clear();

            // 스테이지 클리어 표시멘트
            Console.WriteLine("====================================");
            Console.WriteLine("=            ! Clear !            =");
            Console.WriteLine("====================================");

            // 키 입력받기
            Console.ReadKey();

            if (gameClear.isClearStage1)
            {
                // 스테이지1 클리어시, 스테이지 2 진입 준비
                ReadyStage2();
            }
            else if(gameClear.isClearStage2)
            {
                // 스테이지2 클리어시, 스테이지 3 진입 준비
                ReadyStage3();
            }
            else if(gameClear.isClearStage3)
            {
                // 스테이지3 클리어시, 성공엔딩으로 가기위한 조건
                gameClear.isRunning = false;
                TheEnd();
            }


        }



        static void TheFail()
        {

            // 이전내용 삭제
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=        !광산 탈출 실패!          =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();
            
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine(" 몬스터에게 잡아먹힌 당신은 ");
            Console.WriteLine(" 영혼만 남아 광산속을 영원히 떠돌게됩니다... ");
            Console.ResetColor();

            // while문이 한번 더 돌아가면 출력이 반복되니 여기서 그냥 콘솔을 끝내버림
            Environment.Exit(0);

        }


        static void TheEnd()
        {

            // 이전내용 삭제
            Console.Clear();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("====================================");
            Console.WriteLine("=                                  =");
            Console.WriteLine("=        !광산 탈출 성공!          =");
            Console.WriteLine("=                                  =");
            Console.WriteLine("====================================");
            Console.WriteLine();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("     또 다른곳을 탐험해보세요!    ");
            Console.ResetColor();

        }



    }




}