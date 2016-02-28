using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Maze
{

    /*  T. D.
    // конвертор элементов лаберинта в индексы спрайтовой нарезки
    // объект, со своим спрайтом и функцией преобразования
    // а как по индексу обратится к спрайтовой нарезке?


    public GameObject PrefabWall;


    public Texture2D texture;
    //создать из текстуры
    Sprite spr = Sprite.Create(texture, new Rect(75.0f, 75.0f, 25.0f, 25.0f), new Vector2(0.5f, 0.5f));
    PrefabWall.GetComponent<SpriteRenderer>().sprite = spr;
    */


    public class MazeSpriteShiteAdapter : MonoBehaviour
    {

        public Sprite[] Sprites = new Sprite[16];

        public float SizePerUnit = 0.24f;


        //массив конвертации из расчетных в спрайтовые индексы
        int[] convertArr = { 12, 2, 3, 4, 0, 14, 5, 9, 1, 7, 15, 8, 6, 11, 10, 13 };


        // назначить спрайт
        public void SetSprite(int numer)
        {
            if(numer < 0 || numer > Sprites.Length)
                return;

            int tmp = convertArr[numer];
            //  Debug.Log(string.Format("{0} -> {1} = {2}", numer, tmp, Sprites[tmp].name));
            GetComponent<SpriteRenderer>().sprite = Sprites[tmp];
        }

        // преобразовать стену из расчетных в спрайтувою нарезку
        private int ConvertIndex(int numer)
        {
            return convertArr[numer];
        }



        // Use this for initialization
        void Start()
        {
	
        }
	
        // Update is called once per frame
        void Update()
        {
	
        }
    }
}