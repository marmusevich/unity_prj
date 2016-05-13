using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using MazePoint = IntVector2D;
using MazePointListType = System.Collections.Generic.List<IntVector2D>;


public class FindPath
{
	#region переменные

	// хранит лабиринт, byte - 8 бит без знака
	private  int[,] mMaze = null;
	private  uint xMazeSize;
	private  uint yMazeSize;

	#endregion

	#region функции

	// задать исходный лабиринт
	public void SetMaze( bool[,] imputMaze )
	{
		xMazeSize = ( uint )imputMaze.GetLength( 0 );
		yMazeSize = ( uint )imputMaze.GetLength( 1 );

		mMaze = new int[xMazeSize, yMazeSize];		

		for(int i = 0 ; i < xMazeSize ; i++)
		{
			for(int j = 0 ; j < yMazeSize ; j++)
			{
				if( !imputMaze[ i, j ] )
					mMaze[ i, j ] = 0;
				else
					mMaze[ i, j ] = -1;
			}
		}
	}

	//найти путь
	public MazePointListType GetPath( MazePoint startPos, MazePoint finishPos )
	{
		//копия масива
		int[,] tmpMaze = ( int[,] )mMaze.Clone();

		//		Распространение волны
		//     Пометить стартовую ячейку 0
		//    d := 0 
		//		ЦИКЛ
		//		ДЛЯ каждой ячейки loc, помеченной числом d
		//		пометить все соседние свободные не помеченные ячейки числом d + 1
		//		КЦ
		//		d := d + 1
		//			ПОКА (финишная ячейка не помечена) И (есть возможность распространения волны, шаг < количества ячеек)


		//		Восстановление пути
		//
		//		ЕСЛИ финишная ячейка помечена
		//		ТО
		//		перейти в финишную ячейку
		//		ЦИКЛ
		//		выбрать среди соседних ячейку, помеченную числом на 1 меньше числа в текущей ячейке
		//		перейти в выбранную ячейку и добавить её к пути
		//		ПОКА текущая ячейка — не стартовая
		//		ВОЗВРАТ путь найден
		//		ИНАЧЕ
		//		ВОЗВРАТ путь не найден
		MazePointListType path = new MazePointListType();



		return path;
	}

	#endregion

	public FindPath()
	{
	}
}


//
////Ищмем путь к врагу
////TargetX, TargetY - координаты ближайшего врага
//public int[,] findWave(int targetX, int targetY){
//	bool add = true; // условие выхода из цикла
//	// делаем копию карты локации, для дальнейшей ее разметки
//	int[,] cMap = new int[Battlefield.X, Battlefield.Y];
//	int x, y, step = 0; // значение шага равно 0
//	for (x = 0; x < Battlefield.x; x++) {
//		for (y = 0; y < Battlefield.y; y++) {
//			if(Battlefield.battlefield[x,y] == 1)
//				cMap[x,y] = -2; //если ячейка равна 1, то это стена (пишим -2)
//			else cMap[x,y] = -1; //иначе еще не ступали сюда
//		}
//	}
//
//	//начинаем отсчет с финиша, так будет удобней востанавливать путь
//	cMap[targetX,targetY] = 0;
//	while (add == true) {
//		add = false;
//		for (x = 0; x < Battlefield.x; x++) {
//			for (y = 0; y < Battlefield.y; y++) {
//				if(cMap[x,y] == step){
//					// если соседняя клетка не стена, и если она еще не помечена
//					// то помечаем ее значением шага + 1
//					if(y - 1 >= 0 && cMap[x, y - 1] != -2 && cMap[x, y - 1] == -1)
//						cMap[x, y - 1] = step + 1;
//					if(x - 1 >= 0 && cMap[x - 1, y] != -2 && cMap[x - 1, y] == -1)
//						cMap[x - 1, y] = step + 1;
//					if(y + 1 >= 0 && cMap[x, y + 1] != -2 && cMap[x, y + 1] == -1)
//						cMap[x, y + 1] = step + 1;
//					if(x + 1 >= 0 && cMap[x + 1, y] != -2 && cMap[x + 1, y] == -1)
//						cMap[x + 1, y] = step + 1;
//				}
//			}
//		}
//		step++;
//		add = true;
//		if(cMap[(int)transform.localPosition.x, (int)transform.localPosition.y] > 0) //решение найдено
//			add = false;
//		if(step > Battlefield.X * Battlefield.Y) //решение не найдено, если шагов больше чем клеток
//			add = false;
//	}
//	return cMap; // возвращаем помеченную матрицу, для востановления пути в методе move()
//}
//
//
///// <summary>РЕАЛИЗАЦИЯ ВОЛНОВОГО АЛГОРИТМА
/////	</summary>
///// <param name="cMap">Копия карты локации</param>
///// <param name="targetX">координата цели x</param>
///// <param name="targetY">координата цели y</param>
//private IEnumerator move(int[,] cMap, int targetX, int targetY){
//	ready = false;
//	int[] neighbors = new int[8]; //значение весов соседних клеток
//	// будем хранить в векторе координаты клетки в которую нужно переместиться
//	Vector3 moveTO = new Vector3(-1,0,10);
//
//	// да да да, можно было сделать через цикл for
//	neighbors[0] = cMap[(int)currentPosition.x+1, (int)currentPosition.y+1];
//	neighbors[1] = cMap[(int)currentPosition.x, (int)currentPosition.y+1];
//	neighbors[2] = cMap[(int)currentPosition.x-1, (int)currentPosition.y+1];
//	neighbors[3] = cMap[(int)currentPosition.x-1, (int)currentPosition.y];
//	neighbors[4] = cMap[(int)currentPosition.x-1,(int) currentPosition.y-1];
//	neighbors[5] = cMap[(int)currentPosition.x, (int)currentPosition.y-1];
//	neighbors[6] = cMap[(int)currentPosition.x+1,(int) currentPosition.y-1];
//	neighbors[7] = cMap[(int)currentPosition.x+1,(int) currentPosition.y];
//	for(int i = 0; i < 8; i++){
//		if(neighbors[i] == -2)
//			// если клетка является непроходимой, делаем так, чтобы на нее юнит точно не попал
//			// делаем этот костыль для того, чтобы потом было удобно брать первый элемент в
//			// отсортированом по возрастанию массиве
//			neighbors[i] = 99999;
//	}
//	Array.Sort(neighbors); //первый элемент массива будет вес клетки куда нужно двигаться
//
//	//ищем координаты клетки с минимальным весом.
//	for (int x = (int)currentPosition.x-1; x <= (int)currentPosition.x+1; x++) {
//		for (int y = (int)currentPosition.y+1; y >= (int)currentPosition.y-1; y--) {
//			if(cMap[x,y] == neighbors[0]){
//				// и указываем вектору координаты клетки, в которую переместим нашего юнита
//				moveTO = new Vector3(x,y,10);
//			}
//		}
//	}
//	//если мы не нашли куда перемещать юнита, то оставляем его на старой позиции.
//	// это случается, если вокруг юнита, во всех 8 клетках, уже размещены другие юниты
//	if(moveTO == new Vector3(-1,0,10))
//		moveTO = new Vector3(currentPosition.x, currentPosition.y, 10);
//
//	//и ура, наконец-то мы перемещаем нашего юнита
//	// теперь он на 1 клетку ближе к врагу
//	transform.localPosition = moveTO;
//
//	//устанавливаем задержку.
//	yield return new WaitForSeconds(waitMove);
//	ready = true;
//}
