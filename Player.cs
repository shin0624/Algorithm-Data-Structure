using System;
using System.Collections.Generic;
using System.Text;


namespace ConsoleApp3
{
    class Pos
    {
        public Pos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }
     class Player
    {
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;

        enum Dir//방향정보(반시계방향 기준)
        {
            Up =0,
            Left = 1,
            Down = 2,
            Right = 3
        }

        int _dir = (int)Dir.Up;
        List<Pos> _points = new List<Pos>();
        public void Initialize(int posY, int posX, Board board)//초기좌표만 예외로 두고 ,player의 좌표는 player만 고칠 수 있도록 private
        {
            PosX = posX;
            PosY = posY;
            _board = board;

            // 현재 바라보고 있는 방향을 기준으로, 좌표 변화를 나타낸다. "앞으로 한 보 전진"을 수행하기 위해 구현
            int[] frontY = new int[] {-1, 0, 1, 0 };
            int[] frontX = new int[] { 0, -1, 0, 1 };
            int[] rightY = new int[] { 0, -1, 0, 1 };
            int[] rightX = new int[] { 1, 0, -1, 0 };

            _points.Add(new Pos(PosY, PosX));//-->좌표 변동 시(앞으로 한보 전진 시)넣는다.
            while (PosY != board.DestY || PosX!= board.DestX)//목적지에 도착하기 전까지 계속 실행하는 루프(오른손 법칙 이용)             
            {
                //1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if(_board.Tile[posY + rightY[_dir], posX + rightX[_dir]] == Board.TileType.Empty)//내가 바라보는 방향 기준으로의 x,y변화를 나타낼 수 있다.
                {
                    //오른쪽으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;
                    //앞으로 한 보 전진                   
                    posY = posY + frontY[_dir];//현재 Y좌표 = 현재 방향을 기반으로 변화하는 Y값+현재 Y값
                    posX = posX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                //2. 현재 바라보는 방향을 기준으로 전진할 수 있는지 확인
                else if(_board.Tile[posY + frontY[_dir], posX + frontX[_dir]]==Board.TileType.Empty)
                {
                    //앞으로 한 보 전진
                    posY = posY + frontY[_dir];
                    posX = posX + frontX[_dir];
                    _points.Add(new Pos(PosY, PosX));
                }
                else
                {
                    //왼쪽 방향으로 회전
                    _dir = (_dir + 1 + 4) % 4;
                }
            }
        }
        const int MOVE_TICK = 100;//100ms = 0.1초
        int _sumTick = 0;//시간을 세기 시작
        int _lastIndex = 0;//마지막으로 실행된 인덱스
        public void Update(int deltaTick)//deltaTick = 이전시간과 현재시간의 차이
        {
            if (_lastIndex >= _points.Count)
                return;

            _sumTick += deltaTick;
                if(_sumTick>=MOVE_TICK)//누적된 합이 100ms보다 커진다면 로직 실행
            {
                _sumTick = 0;//초기화

                PosY = _points[_lastIndex].Y;
                PosX = _points[_lastIndex].X;
                _lastIndex++;
                }

            }
        }
    }

