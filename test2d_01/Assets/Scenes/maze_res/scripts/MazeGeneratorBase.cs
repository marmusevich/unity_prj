using UnityEngine;
using System.Collections;

namespace Maze
{
    /// <summary>
    /// интерфейс генераторов лабиринтов
    /// </summary>
    public interface IMazeGenerator
    {
        /// <summary>
        /// генерирует лаберинт
        /// </summary>
        /// <param name="maze"> иницилизированный массив для генерации лабиринта</param>
        void Generate(ref bool[,] maze);
    }


    /// <summary>
    /// Базавая функциональность, генератор ничего не делеает
    /// </summary>
    public class BaseMazeGenerator : IMazeGenerator
    {
        /// <summary>
        /// ничего не делающий генератор
        /// </summary>
        /// <param name="maze">иницилизированный массив для генерации лабиринта</param>
        public virtual void Generate(ref bool[,] maze)
        {
            //ничего не делает
        }

        /// <summary>
        /// Заполнить все стенами
        /// </summary>
        /// <param name="maze">иницилизированный массив для заполнения лабиринта</param>
        public void FullWall(ref bool[,] maze)
        {
            Full(ref maze, true);
        }

        /// <summary>
        /// очистить все от стен
        /// </summary>
        /// <param name="maze">иницилизированный массив для заполнения лабиринта</param>
        public void ClearWall(ref bool[,] maze)
        {
            Full(ref maze, false);
        }

        /// <summary>
        /// заполнить произвольным символом
        /// </summary>
        /// <param name="maze">иницилизированный массив для заполнения лабиринта</param>
        /// <param name="fullerUnit">символ заполнения.</param>
        public void Full(ref bool[,] maze, bool fullerUnit)
        {
            if(maze == null)
                return;

            int xCount = maze.GetLength(0);
            int yCount = maze.GetLength(1);

            for(int i = 0; i < xCount; i++)
            {
                for(int j = 0; j < yCount; j++)
                {
                    maze[i, j] = fullerUnit;
                }
            }
        }
    }



    // для тестирования рисования
    public class ForTestDraw : BaseMazeGenerator
    {
        public override void Generate(ref bool[,] maze)
        {
            if(maze == null)
                return;

            ClearWall(ref maze);

            int xCount = maze.GetLength(0);
            int yCount = maze.GetLength(1);

            //Debug.Log(string.Format("Maze[{0}, {1}]", rowCount, colCount));

            for(int i = 0; i < xCount; i++)
            {
                maze[i, 0] = true;
                maze[i, yCount - 1] = true;
            }
            for(int j = 0; j < yCount; j++)
            {
                maze[0, j] = true;
                maze[xCount - 1, j] = true;
            }

            for(int j = 0; j < yCount; j++)
            {
                maze[xCount / 2 - 1, j] = true;
            }

            for(int i = 0; i < xCount; i++)
            {
                maze[i, yCount / 2 - 1] = true;
            }


            for(int i = xCount / 4; i < xCount / 2 - 1; i++)
            {
                maze[i, yCount / 4 - 1] = true;
            }
            for(int i = xCount / 2; i < xCount * 3 / 4 - 1; i++)
            {
                maze[i, yCount * 3 / 4 - 1] = true;
            }

            for(int j = yCount / 4; j < yCount / 2 - 1; j++)
            {
                maze[xCount / 4 - 1, j] = true;
            }

            for(int j = yCount / 2; j < yCount * 3 / 4 - 1; j++)
            {
                maze[xCount * 3 / 4 - 1, j] = true;
            }


        }
    }
}
