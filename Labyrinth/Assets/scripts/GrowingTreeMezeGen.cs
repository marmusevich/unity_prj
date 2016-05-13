using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using MazePoint = IntVector2D;
using Direction = IntVector2D;

public class GrowingTreeMezeGen : BaseMazeGenerator
{
	bool[,] maze;
	int xCount;
	int yCount;

	//процент извилистости
	float windingPrecent;

	#region private member

	/// <summary>
	/// вернуть случайную точьку с учетом границ, границы не поподают
	/// </summary>
	/// <returns>The random position.</returns>
	private MazePoint getRandomPos()
	{
		return new MazePoint( Random.Range( 1, xCount ), Random.Range( 1, yCount ) );
	}

	/// <summary>
	/// сделать проход в позиции (<c>pos</c>)
	/// </summary>
	/// <param name="pos">Position.</param>
	/// <param name="val">значение по умолчанию проход (<c>false</c>)</param>
	private void Carve( MazePoint pos, bool val = false )
	{
		if( CheckPosInMaze( pos ) )
			maze[ pos.x, pos.y ] = val;
	}

	/// <summary>
	/// вернуть значение в точьке
	/// </summary>
	/// <returns>value</returns>
	/// <param name="pos">Position.</param>
	private bool GetVal( MazePoint pos )
	{
		if( CheckPosInMaze( pos ) )
			return maze[ pos.x, pos.y ];
		return false;
	}

	/// <summary>
	/// проверка на возможность сделать проход в точке (<c>pos</c>) по направлению (<c>dir</c>)
	/// </summary>
	/// <returns><c>true</c>, проход может быть сделан</returns>
	/// <param name="pos">Position.</param>
	/// <param name="dir">Direction.</param>
	private bool CheckCanCarve( MazePoint pos, MazePoint dir )
	{
		//если через две клетки выход за границы или последущее за границами то не проверяем саму точьку
		if( CheckPosInMaze( pos + 3 * dir ) && CheckPosInMaze( pos + 2 * dir ) )
			return GetVal( pos + 2 * dir );
		return false;
	}

	/// <summary>
	/// проверка точьки (<c>pos</c>) на вхождение внутрь границ массива
	/// </summary>
	/// <returns><c>true</c>,если точька внутри границ масива</returns>
	/// <param name="pos">Position.</param>
	private bool CheckPosInMaze( MazePoint pos )
	{
		return pos.x >= 0 && pos.x < xCount && pos.y >= 0 && pos.y < yCount;
	}

	/// <summary>
	/// вернуть разрешонные направления
	/// </summary>
	/// <returns>The allowed dir.</returns>
	/// <param name="pos">Position.</param>
	private List<Direction> ReturnAllowedDir( MazePoint pos )
	{
		List<Direction> ret = new List<Direction>();
		if( CheckCanCarve( pos, Direction.Down ) )
			ret.Add( Direction.Down );
		if( CheckCanCarve( pos, Direction.Up ) )
			ret.Add( Direction.Up );
		if( CheckCanCarve( pos, Direction.Left ) )
			ret.Add( Direction.Left );
		if( CheckCanCarve( pos, Direction.Right ) )
			ret.Add( Direction.Right );

		return ret;
	}

	#endregion

	public GrowingTreeMezeGen( float _windingPrecent = 0.5f )
	{
		windingPrecent = _windingPrecent;
	}


	public override void Generate( ref bool[,] _maze )
	{
		maze = _maze;
		xCount = maze.GetLength( 0 );
		yCount = maze.GetLength( 1 );

		//нет генерации если слишком мал лабиринт
		if( xCount == 1 || yCount == 1 )
			return;

		FullWall( ref maze );

		Direction lastDir = Direction.Zero;
		Direction dir = Direction.Zero;
		Stack<MazePoint> cells = new Stack<MazePoint>();



		//MazePoint point = getRandomPos(); //MazePoint.One;
		MazePoint point = MazePoint.One;

		cells.Push( point );
		Carve( point );

		while( cells.Count > 0 )
		{
			point = cells.Peek();
			List<Direction> dirs = ReturnAllowedDir( point );
			if( dirs.Count > 0 )
			{
				if( dirs.Contains( lastDir ) && ( Random.value > windingPrecent ) )
					dir = lastDir;
				else
					dir = dirs[ Random.Range( 0, dirs.Count ) ];

				Carve( point + dir );
				Carve( point + 2 * dir );

				cells.Push( point + 2 * dir );
				lastDir = dir;
			} else
			{
				cells.Pop();
				lastDir = Direction.Zero;
			}
		}
	}
}
//  Debug.Log(string.Format("{0} -> {1} = {2}", numer, tmp, Sprites[tmp].name));




