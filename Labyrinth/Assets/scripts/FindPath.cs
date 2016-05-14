using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

using MazePoint = IntVector2D;
using Direction = IntVector2D;
using MazePointListType = System.Collections.Generic.List<IntVector2D>;
using MazeType = System.UInt16;


public class FindPath
{
	#region переменные

	// хранит лабиринт, byte - 8 бит без знака
	private  int[,] mMaze = null;

	private int xCount;
	private int yCount;


	#endregion

	#region функции

	private void SetVal(ref int[,] tmpMaze, MazePoint pos, int val  )
	{
		if( CheckPosInMaze( pos ) )
			tmpMaze[ pos.x, pos.y ] = val;
	}

	private int GetVal( ref int[,] tmpMaze, MazePoint pos )
	{
		if( CheckPosInMaze( pos ) )
			return tmpMaze[ pos.x, pos.y ];
		return -1;
	}

	private bool CheckPosInMaze( MazePoint pos )
	{
		return pos.x >= 0 && pos.x < xCount && pos.y >= 0 && pos.y < yCount;
	}

	// задать исходный лабиринт
	public void SetMaze( MazeType[,] imputMaze )
	{
		xCount = imputMaze.GetLength( 0 );
		yCount = imputMaze.GetLength( 1 );

		mMaze = new int[xCount, yCount];		

		for(int i = 0 ; i < xCount; i++)
		{
			for(int j = 0 ; j < yCount; j++)
			{
				if( imputMaze[ i, j ] == 0 )
					mMaze[ i, j ] = 0;
				else
					mMaze[ i, j ] = -1;
			}
		}
	}

	//найти путь
	public MazePointListType GetPath( MazePoint startPos, MazePoint finishPos )
	{
		if( GetVal( ref mMaze, finishPos ) != 0 )
			return null;

		#region вывод строке
		{
			string str = "";
			for(int i = 0 ; i < xCount ; i++)
			{
				for(int j = 0 ; j < yCount ; j++)
				{
					str += string.Format( "{0, 3}\t", GetVal( ref mMaze, new MazePoint(i,j)) );
					//str += string.Format( "{0, 3}\t", tmpMaze[ i, j ] );

				}
				str += "\n";
			}
			Debug.Log( str );
		}		
		#endregion


		//копия масива
		int[,] tmpMaze = ( int[,] )mMaze.Clone();

		//		Распространение волны
		Stack<MazePoint> cells = new Stack<MazePoint>();
		MazePoint point = startPos;
		SetVal( ref tmpMaze, point, 1 );
		cells.Push( point );

		MazePoint tmpPoint;

		while( cells.Count > 0 )
		{
			point = cells.Pop();
			if( point == finishPos)
			{
				break;
			}
			//up
			tmpPoint = point + Direction.Up;
			if( GetVal( ref tmpMaze, tmpPoint) == 0 )
			{
				SetVal( ref tmpMaze, tmpPoint, GetVal( ref tmpMaze, point)+1 );
				cells.Push( tmpPoint );
			}
			//left
			tmpPoint = point + Direction.Left;
			if( GetVal( ref tmpMaze, tmpPoint) == 0 )
			{
				SetVal( ref tmpMaze, tmpPoint, GetVal( ref tmpMaze, point)+1 );
				cells.Push( tmpPoint );
			}
			//down
			tmpPoint = point + Direction.Down;
			if( GetVal( ref tmpMaze, tmpPoint) == 0 )
			{
				SetVal( ref tmpMaze, tmpPoint, GetVal( ref tmpMaze, point)+1 );
				cells.Push( tmpPoint );
			}
			//right
			tmpPoint = point + Direction.Right;
			if( GetVal( ref tmpMaze, tmpPoint) == 0 )
			{
				SetVal( ref tmpMaze, tmpPoint, GetVal( ref tmpMaze, point)+1 );
				cells.Push( tmpPoint );
			}

		}



//		#region вывод строке
//		{
//			string str = "";
//			for(int i = 0 ; i < xCount ; i++)
//			{
//				for(int j = 0 ; j < yCount ; j++)
//				{
//					str += string.Format( "{0, 3}\t", GetVal( ref tmpMaze, new MazePoint(i,j)) );
//					//str += string.Format( "{0, 3}\t", tmpMaze[ i, j ] );
//
//				}
//				str += "\n";
//			}
//			Debug.Log( str );
//		}		
//		#endregion





		//		Восстановление пути
		cells = new Stack<MazePoint>();
		cells.Push( finishPos );

		while( point != startPos)
		{
			//Debug.Log(point.ToString() + " ---> " + GetVal( ref tmpMaze, point) );

			//up
			tmpPoint = point + Direction.Up;
			if( GetVal( ref tmpMaze, tmpPoint) == GetVal( ref tmpMaze, point)-1 )
			{
				point = tmpPoint;
				cells.Push( point );
				continue;
			}
			//left
			tmpPoint = point + Direction.Left;
			if( GetVal( ref tmpMaze, tmpPoint) == GetVal( ref tmpMaze, point)-1 )
			{
				point = tmpPoint;
				cells.Push( point );
				continue;
			}
			//down
			tmpPoint = point + Direction.Down;
			if( GetVal( ref tmpMaze, tmpPoint) == GetVal( ref tmpMaze, point)-1 )
			{
				point = tmpPoint;
				cells.Push( point );
				continue;
			}
			//right
			tmpPoint = point + Direction.Right;
			if( GetVal( ref tmpMaze, tmpPoint) == GetVal( ref tmpMaze, point)-1 )
			{
				point = tmpPoint;
				cells.Push( point );
				continue;
			}
		}
		cells.Push( startPos );


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

		Debug.Log( "cells.Count: "+ cells.Count.ToString() );

		MazePointListType path = new MazePointListType();
		while( cells.Count > 0 )
			path.Add( cells.Pop());

		return path;
	}

	#endregion

	public FindPath()
	{
	}
}

