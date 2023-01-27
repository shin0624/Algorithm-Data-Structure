using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {

            Board board = new Board();
            Player player = new Player();   
            board.Initialize(25, player);
            player.Initialize(1, 1, board);
            //프로그램이 시작하자마자 Initialize 실행-->board

            Console.CursorVisible = false;

            const int WAIT_TICK = 1000 / 30;//1초 = 1000ms이기 때문에 1000을 곱해준다.

            int lastTick = 0; //==>프레임 시간을 알아보기 위해 마지막 시간, 현재 시간을 출력(단위 = ms)
            while (true)
            {
                #region 프레임 관리
                //만약 경과한 시간이 1/30초보다 작다면 
                int currentTick = System.Environment.TickCount;
                if (currentTick - lastTick < WAIT_TICK)//경과한 시간 = 현재시간 - 마지막 시간
                                                       //1초 = 1000ms이기 때문에 1000을 곱해준다.
                    continue;
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                #endregion

                player.Update(deltaTick);

                Console.SetCursorPosition(0, 0);
                board.Render();


            }
        }
    }
}