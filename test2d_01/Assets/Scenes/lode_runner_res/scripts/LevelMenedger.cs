using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LodeRunner
{
    public class LevelMenedger : MonoBehaviour
    {
        // отладочный уровень
        void CreateDebagLevel()
        {
            if (Levels == null)
            {
                Levels = new List<OneLevel>();
            }

            OneLevel lvl = new OneLevel();
            Levels.Add(lvl);

            // по центру горизонталь веревка
            for (int i = 0; i < COUNT_BLOCK_ON_X; i++)
            {
                lvl.mMap[i, COUNT_BLOCK_ON_Y / 2] = MapObjectType.Ferriade;
            }

            //над веревкай кираичи
            for (int i = 0; i < COUNT_BLOCK_ON_X; i++)
            {
                lvl.mMap[i, COUNT_BLOCK_ON_Y * 3 / 4] = MapObjectType.Brick;
                lvl.mMap[i, COUNT_BLOCK_ON_Y * 1 / 4 - 1] = MapObjectType.Brick;
            }


            // по центру горизонталь лестница
            for (int i = 0; i < COUNT_BLOCK_ON_Y; i++)
            {
                lvl.mMap[COUNT_BLOCK_ON_X / 2, i] = MapObjectType.Stairs;
            }

            //воокруг кирпичи
            for (int i = 0; i < COUNT_BLOCK_ON_X; i++)
            {
                // без верха
                //lvl.mMap[i, 0] = MapObjectType.Brick;
                lvl.mMap[i, COUNT_BLOCK_ON_Y - 1] = MapObjectType.Brick;
            }

            for (int i = 0; i < COUNT_BLOCK_ON_Y; i++)
            {
                lvl.mMap[0, i] = MapObjectType.Brick;
                lvl.mMap[COUNT_BLOCK_ON_X - 1, i] = MapObjectType.Brick;
            }

            //выход на следующий уровень
            lvl.mMap[COUNT_BLOCK_ON_X / 2, 0] = MapObjectType.Stairs;

            //сокровища
            lvl.mMap[COUNT_BLOCK_ON_X * 1 / 4 - 1, COUNT_BLOCK_ON_Y - 2] = MapObjectType.Treasure;
            lvl.mMap[COUNT_BLOCK_ON_X * 1 / 4 - 1, COUNT_BLOCK_ON_Y * 3 / 4 - 1] = MapObjectType.Treasure;
            lvl.mMap[COUNT_BLOCK_ON_X * 3 / 4 - 1, COUNT_BLOCK_ON_Y - 2] = MapObjectType.Treasure;
            lvl.mMap[COUNT_BLOCK_ON_X * 3 / 4 - 1, COUNT_BLOCK_ON_Y * 3 / 4 - 1] = MapObjectType.Treasure;
        }

        // размер по умолчанию
        public const int COUNT_BLOCK_ON_X = 48;
        public const int COUNT_BLOCK_ON_Y = 36;

        public enum MapObjectType
        {
            // пустое место, ничего
            None = 0,
            // кирпичи
            Brick,
            // лестница
            Stairs,
            // переправа, веревка
            Ferriade,
            // награда, золото
            Treasure,
            // место перехода на следующий уровень
            Finish,
            // место старта героя
            Hero,
            // место старта врага
            Enemy
        }

        // описывает один уровень
        public class OneLevel
        {
            public MapObjectType[,] mMap;
            public OneLevel()
            {
                mMap = new MapObjectType[COUNT_BLOCK_ON_X, COUNT_BLOCK_ON_Y];
            }
        }

        List<OneLevel> Levels;

        // кирпичи
        public GameObject PrefabBrick;
        // лестница
        public GameObject PrefabStairs;
        // переправа, веревка
        public GameObject PrefabFerriade;
        // награда, золото
        public GameObject PrefabTreasure;
        //// место перехода на следующий уровень
        //Finish,
        //// место старта героя
        //Hero,
        //// место старта врага
        //Enemy

        public Vector2 defaultSize;
        public Vector2 topLeft, bottomRight;


        //проверки всякии добавить надо
        private void DraweMap(OneLevel lvl)
        {
            Camera camera = GetComponent<Camera>();

            //Vector2 topLeft, bottomRight;
            MyUtilsFunction.ReturnScreenSizeInCamera(camera, out topLeft, out bottomRight);

            Vector2 ofset = new Vector2(Mathf.Abs(topLeft.x - bottomRight.x) / COUNT_BLOCK_ON_X, Mathf.Abs(topLeft.y - bottomRight.y) / COUNT_BLOCK_ON_Y);
            //Vector2
            defaultSize = MyUtilsFunction.GetGameObjectSize(PrefabBrick);
            //Vector2 defaultSize = new Vector2(0.64f, 0.64f);
            Vector2 scale = new Vector2(ofset.x / defaultSize.x, ofset.y / defaultSize.y);

            GameObject block = null;

            for (int j = 0; j < COUNT_BLOCK_ON_Y; j++)
            {
                for (int i = 0; i < COUNT_BLOCK_ON_X; i++)
                {
                    block = null;
                    switch (lvl.mMap[i, j])
                    {
                        case MapObjectType.Brick:
                            block = PrefabBrick;
                            break;
                        case MapObjectType.Stairs:
                            block = PrefabStairs;
                            break;
                        case MapObjectType.Ferriade:
                            block = PrefabFerriade;
                            break;
                        case MapObjectType.Treasure:
                            block = PrefabTreasure;
                            break;
                        default:
                            continue;
                    }
                    GameObject obj = Instantiate(block, new Vector2(topLeft.x + ofset.x / 2 + ofset.x * i, topLeft.y - ofset.y / 2 - ofset.y * j), Quaternion.identity) as GameObject;
                    obj.name = lvl.mMap[i, j].ToString() + i + "_" + j;
                    //obj.transform.localScale = new Vector3(obj.transform.localScale.x * scale.x, obj.transform.localScale.y * scale.y, obj.transform.localScale.z);
                    obj.transform.localScale = new Vector3(scale.x, scale.y, obj.transform.localScale.z);
                }
            }
        }



        void Start()
        {
            Levels = new List<OneLevel>();

            // тест
            CreateDebagLevel();
            OneLevel lvl = Levels[0];
            DraweMap(lvl);
            // тест
        }

        void Update()
        {
            Camera camera = GetComponent<Camera>();
            MyUtilsFunction.ReturnScreenSizeInCamera(camera, out topLeft, out bottomRight);

        }
    }

}