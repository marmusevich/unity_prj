using UnityEngine;
using System.Collections;

public class MyUtilsFunction
{
    /// <summary>
    /// вернуть размеры в еденицах unity например префаба
    /// </summary>
    /// <returns>The game object size.</returns>
    /// <param name="obj">Object.</param>
    public static Vector2 GetGameObjectSize(GameObject obj)
    {
        float minX = float.MaxValue;
        float maxX = float.MinValue;
        float minY = float.MaxValue;
        float maxY = float.MinValue;
        SpriteRenderer[] sprites = obj.GetComponentsInChildren<SpriteRenderer>();
        foreach( SpriteRenderer sr in sprites )
        {
            float tmpMinX = sr.transform.position.x + sr.sprite.bounds.center.x - sr.sprite.bounds.size.x * sr.transform.localScale.x / 2f;
            float tmpMaxX = sr.transform.position.x + sr.sprite.bounds.center.x + sr.sprite.bounds.size.x * sr.transform.localScale.x / 2f;
            //if (tmpMinX < minX)
            //{
            //    minX = tmpMinX;
            //}
            //if (tmpMaxX > maxX)
            //{
            //    maxX = tmpMaxX;
            //}
            minX = Mathf.Min(tmpMinX, minX);
            maxX = Mathf.Max(tmpMaxX, maxX);

            float tmpMinY = sr.transform.position.x + sr.sprite.bounds.center.x - sr.sprite.bounds.size.x * sr.transform.localScale.x / 2f;
            float tmpMaxY = sr.transform.position.x + sr.sprite.bounds.center.x + sr.sprite.bounds.size.x * sr.transform.localScale.x / 2f;
            minY = Mathf.Min(tmpMinY, minY);
            maxY = Mathf.Max(tmpMaxY, maxY);
        }
        return new Vector2(maxX - minX, maxY - minY);
    }

    /// <summary>
    /// вернуть размер видимой области
    /// </summary>
    /// <param name="camera">Camera.</param>
    /// <param name="topLeft">Top left.</param>
    /// <param name="bottomRight">Bottom right.</param>
    public static void ReturnScreenSizeInCamera(Camera camera, out Vector2 topLeft, out Vector2 bottomRight)
    {
        float width = camera.pixelWidth;
        float height = camera.pixelHeight;
        topLeft = camera.ScreenToWorldPoint(new Vector2(0, height));
        bottomRight = camera.ScreenToWorldPoint(new Vector2(width, 0));
        //Vector2 topRight = camera.ScreenToWorldPoint(new Vector2(width, height));
        //Vector2 bottomLeft = camera.ScreenToWorldPoint(new Vector2(0, 0));
    }




    /* - вынести в общие класы
        //Перемешивание списка
        void Shuffle(int[] deck)
        {
            for(int i = 0; i < deck.Length; i++)
            {
                int temp = deck[i];
                int randomIndex = Random.Range(0, deck.Length);
                deck[i] = deck[randomIndex];
                deck[randomIndex] = temp;
            }
        }


        //Выбор элементов из набора без повторений
        Transform[] spawnPoints;

        Transform[] ChooseSet(int numRequired)
        {
            Transform[] result = new Transform[numRequired];
            int numToChoose = numRequired;
            for(int numLeft = spawnPoints.Length; numLeft > 0; numLeft--)
            {
                float prob = (float)numToChoose / (float)numLeft;
                if(Random.value <= prob)
                {
                    numToChoose--;
                    result[numToChoose] = spawnPoints[numLeft - 1];
                    if(numToChoose == 0)
                    {
                        break;
                    }
                }
            }
            return result;
        }
        */



}
