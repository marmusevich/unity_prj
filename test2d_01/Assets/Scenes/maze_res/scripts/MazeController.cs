using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

namespace Maze
{
    /// <summary>
    /// координата точьки в лабиринте
    /// </summary>
    using MazePoint = IntVector2D;
    using MazePointListType = List<IntVector2D>;
    using MazeType = UInt16;

    public class MazeController : MonoBehaviour
    {
        #region переменные

        /// <summary>
        /// префаб стены
        /// </summary>
        public GameObject PrefabWall;

        /// <summary>
        /// список всех стен
        /// </summary>
        public MazePointListType ListWall;

        /// <summary>
        /// список всех пустых ячеек
        /// </summary>
        public MazePointListType ListSpace;

        /// <summary>
        /// процент извилистости
        /// </summary>
        public float WindingPrecent = 0.5f;

        /// <summary>
        /// Размер лабиринта
        /// </summary>
        public uint xMazeSize = 20;
        public uint yMazeSize = 20;

        //для преобразователя координат
        Vector2 m_ofset;
        Vector2 topLeft;


        // хранит лабиринт, byte - 8 бит без знака
        private  MazeType[,] mMaze = null;
        // текущий генератор
        private IMazeGenerator mGenerator = null;

        #endregion

        #region функции

        /// <summary>
        /// Настройка преоброзователя координат
        /// </summary>
        /// <param name="xSize">размер по X</param>
        /// <param name="ySize">размер по Y</param>
        void CalculateScreenParametr(int xSize, int ySize)
        {
            MazeSpriteShiteAdapter mssa = PrefabWall.GetComponent<MazeSpriteShiteAdapter>();
            m_ofset = new Vector2(mssa.SizePerUnit, mssa.SizePerUnit);
            topLeft = new Vector2(-mssa.SizePerUnit * xSize / 2, mssa.SizePerUnit * ySize / 2);
        }

        /// <summary>
        /// Преоброзовать координаты матрицы в экранные
        /// </summary>
        /// <returns>Экранные коордионаты</returns>
        /// <param name="x">The x coordinate in maze.</param>
        /// <param name="y">The y coordinate in maze.</param>
        public Vector2 ConvertMazeCoordToScreen(int x, int y)
        {
            return new Vector2(topLeft.x + m_ofset.x / 2 + m_ofset.x * x, topLeft.y - m_ofset.y / 2 - m_ofset.y * y);
        }


        /// <summary>
        /// задать новые размеры, создает новый массив
        /// </summary>
        /// <param name="newXCount">New X count.</param>
        /// <param name="newYCount">New Y count.</param>
        /// <param name="reGenerateMaze">If set to <c>true</c> re generate maze.</param>
        public void SetNewSize(uint newXCount, uint newYCount, bool reGenerateMaze = false)
        {
            mMaze = new MazeType[newXCount, newYCount];
            if(reGenerateMaze)
                Generate();
        }

        /// <summary>
        /// задать новый генератор
        /// </summary>
        /// <param name="newGenerator">New generator.</param>
        /// <param name="reGenerateMaze">If set to <c>true</c> re generate maze.</param>
        public void SetNewGenerator(IMazeGenerator newGenerator, bool reGenerateMaze = false)
        {
            mGenerator = newGenerator;
            if(reGenerateMaze)
                Generate();
        }

        /// <summary>
        /// генерация лабиринта
        /// </summary>
        public void Generate()
        {
            if(mGenerator == null)
                return;
            if(mMaze == null)
                return;

            int xCount = mMaze.GetLength(0);
            int yCount = mMaze.GetLength(1);
            bool[,] tmp = new bool[xCount, yCount];
            mGenerator.Generate(ref tmp);

            //вычисление параметров стен
            KeyValuePair<MazePointListType,MazePointListType> lists = ConwertMazeToWall(tmp, ref mMaze);

            ListWall = lists.Key;
            ListSpace = lists.Value;
        }

        /// <summary>
        /// построение лабиринта со стенами на основе карты проходимости
        /// </summary>
        /// <returns>пара списков стен и пустых пространств (Key - список стен, Value - список пустых клеток)</returns>
        /// <param name="imput">входной лабиринт - карта проходимости</param>
        /// <param name="maze">выходной лабиринт со стенами</param>
        KeyValuePair<MazePointListType,MazePointListType> ConwertMazeToWall(bool[,] imput, ref MazeType[,] maze)
        {
            MazePointListType wall = new MazePointListType();
            MazePointListType space = new MazePointListType();

            int xCount = maze.GetLength(0);
            int yCount = maze.GetLength(1);

            int top, left, down, right;
            for(int i = 0; i < xCount; i++)
            {
                for(int j = 0; j < yCount; j++)
                {
                    if(!imput[i, j])
                    {
                        maze[i, j] = 0;
                        space.Add(new MazePoint(i, j));
                    } else
                    {
                        wall.Add(new MazePoint(i, j));

                        //значение левого
                        left = ( i - 1 < 0 ) ? 0 : ( imput[i - 1, j] ? 1 : 0 );
                        //значение правого
                        right = ( i + 1 > xCount - 1 ) ? 0 : ( imput[i + 1, j] ? 1 : 0 );
                        //элемент выше текущего
                        top = ( j - 1 < 0 ) ? 0 : ( imput[i, j - 1] ? 1 : 0 );
                        //элемент ниже текущего
                        down = ( j + 1 > yCount - 1 ) ? 0 : ( imput[i, j + 1] ? 1 : 0 );
                        //значение текущего с учет весов соседей
                        // значение не ноль, так как 0 это пустое пространство
                        maze[i, j] = (MazeType)( 1 + 1 * top + 2 * right + 4 * down + 8 * left );
                    }
                }
            }

            return new KeyValuePair<MazePointListType, MazePointListType>(wall, space);
        }

        /// <summary>
        /// вывод лаберинта
        /// </summary>
        public void Draw()
        {
            if(mMaze == null)
                return;

            int xCount = mMaze.GetLength(0);
            int yCount = mMaze.GetLength(1);
            /*
            #region в строке
            string str = "";
            for(int i = 0; i < rowCount; i++)
            {
                for(int j = 0; j < colCount; j++)
                {
                    str += string.Format("{0, 3}", mMaze[i, j]);
                }
                str += "\n";
            }
            Debug.Log(str);
            #endregion
            */


            CalculateScreenParametr(xCount, yCount);

            for(int i = 0; i < xCount; i++)
            {
                for(int j = 0; j < yCount; j++)
                {
                    if(mMaze[i, j] == 0)
                        continue;
                    GameObject obj = Instantiate(PrefabWall, ConvertMazeCoordToScreen(i, j), Quaternion.identity) as GameObject;
                    obj.name = string.Format("wool_{0}_{1}", i, j);

                    MazeSpriteShiteAdapter a = obj.GetComponent<MazeSpriteShiteAdapter>();
                    a.SetSprite(mMaze[i, j] - 1);
                }
            }
        }

        #endregion

        // разместить героя
        void PositPlaer()
        {
            GameObject hero = GameObject.FindGameObjectWithTag("Player");
            Vector2 vec = ConvertMazeCoordToScreen(ListSpace[ListSpace.Count - 1].x, ListSpace[ListSpace.Count - 1].y);
            ListSpace.RemoveAt(ListSpace.Count - 1);
            hero.transform.position = new Vector3(vec.x, vec.y, hero.transform.position.z);
        }

        public GameObject PrefabPrize;
        public int CountPrize = 10;

        //разместить призы
        void PositPrize()
        {
            if(PrefabPrize != null)
            {
                for(int i = 0; i < CountPrize; i++)
                {
                    if(ListSpace.Count < 1)
                        break;
                    int index = UnityEngine.Random.Range(0, ListSpace.Count);
                    Vector2 posPriz = ConvertMazeCoordToScreen(ListSpace[index].x, ListSpace[index].y);
                    GameObject obj = Instantiate(PrefabPrize, posPriz, Quaternion.identity) as GameObject;
                    obj.name = string.Format("Priz_{0}", i);
                    ListSpace.RemoveAt(index);
                }
            }
        }

        public void ClearMaze()
        {
            GameObject[] wall = GameObject.FindGameObjectsWithTag("Wall");
            foreach( GameObject w in wall )
                Destroy(w);

            GameObject[] prize = GameObject.FindGameObjectsWithTag("Prize");
            foreach( GameObject p in prize )
                Destroy(p);
        }




        public void ReStart()
        {
            ClearMaze();
            Generate();
            Draw();
            PositPlaer();
            PositPrize();
        }


        #region стандартные калбэки юнити

        // Use this for initialization
        void Start()
        {
            SetNewSize(xMazeSize, yMazeSize);
            SetNewGenerator(new GrowingTreeMezeGen(WindingPrecent));
            ReStart();
        }



        // Update is called once per frame
        void Update()
        {
	
        }

        #endregion
    }
}
