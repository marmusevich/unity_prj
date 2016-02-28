using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Maze
{
    using MazePoint = IntVector2D;

    public class RecursiveBacktrackingMazeGen : BaseMazeGenerator
    {
        
        //MazePoint
        Stack<MazePoint> wave;
        bool[,] maze;

        #region private member

        int xCount;
        int yCount;
        //вернуть случайную координату X с учетом границ, границы не поподают
        private int getRandomX()
        {
            return (int)Random.Range(1.0F, (float)xCount);
        }

        //вернуть случайную координату Y с учетом границ, границы не поподают
        private int getRandomY()
        {
            return (int)Random.Range(1.0F, (float)yCount);
        }

        private bool PosibNextCell(MazePoint point)
        {
            return ( IsEmpty(point.x + 1, point.y) ||
            IsEmpty(point.x - 1, point.y) ||
            IsEmpty(point.x, point.y + 1) ||
            IsEmpty(point.x, point.y - 1) );
        }

        private MazePoint GetNextCell(MazePoint point)
        {
            MazePoint mp = new MazePoint(point.x, point.y);
            bool ok = false;
            while (!ok)
            {
                switch( (int)Random.Range(1.0F, 5.0F) )
                {
                    case 1:
                        mp.x = point.x + 1;
                        break;
                    case 2:
                        mp.x = point.x - 1;
                        break;
                    case 3:
                        mp.y = point.y + 1;
                        break;
                    case 4:
                        mp.y = point.y - 1;
                        break;
                }
                ok = IsEmpty(mp.x, mp.y);
            }
            return mp;
        }

        private bool IsEmpty(int x, int y)
        {
            if(x <= 0 || x >= xCount - 1 || y <= 0 || y >= yCount - 1)
                return false;

            if(maze[x, y])
            {
                return ( ( !maze[x + 1, y] ? 1 : 0 ) +
                ( !maze[x - 1, y] ? 1 : 0 ) +
                ( !maze[x, y + 1] ? 1 : 0 ) +
                ( !maze[x, y - 1] ? 1 : 0 ) ) <= 1; 
            }
            return false;
        }

        #endregion

        public override void Generate(ref bool[,] _maze)
        {
            maze = _maze;
            xCount = maze.GetLength(0);
            yCount = maze.GetLength(1);

            //нет генерации если слишком мал лабиринт
            if(xCount == 1 || yCount == 1)
                return;

            FullWall(ref maze);

            wave = new Stack<MazePoint>();

            MazePoint point = new MazePoint(getRandomX(), getRandomY());
            wave.Push(point);
            maze[point.x, point.y] = false;

            while (wave.Count < 1)
            {
                point = wave.Peek();

                if(PosibNextCell(point))
                {
                    point = GetNextCell(point);
                    wave.Push(point);
                    maze[point.x, point.y] = false;
                } else
                {
                    wave.Pop();
                }
            }
        }
    }
}

