using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMenedger : MonoBehaviour
{
	// размер по умолчанию
	public const int DEFAULT_SIZE_X = 64;
	public const int DEFAULT_SIZE_Y = 32;


	public enum MapObjectType
	{
		None = 0,	// пустое место, ничего
		Brick,		// кирпичи
		Stairs,		// лестница
		Ferriade,	// переправа, веревка
		Treasure,	// награда, золото
		Finish,		// место перехода на следующий уровень
		Hero,		// место старта героя
		Enemy		// место старта врага
	}
	;


	// описывает один уровень
	public class OneLevel
	{
		[SerializeField]
		public readonly int
			mSIZE_X;
		[SerializeField]
		public readonly int
			mSIZE_Y;
		[SerializeField]
		public MapObjectType[,]
			mMap;

		public OneLevel( int sizeX = DEFAULT_SIZE_X, int sizeY = DEFAULT_SIZE_Y )
		{
			this.mSIZE_X = sizeX;
			this.mSIZE_Y = sizeY;
			
			mMap = new MapObjectType[mSIZE_X, mSIZE_Y];
		}
	}

	List<OneLevel> Levels;


	// Use this for initialization
	void Start()
	{
		Levels = new List<OneLevel>();

		CreateDebagLevel();

	}
	
	// Update is called once per frame
	void Update()
	{
	
	}

	void CreateDebagLevel()
	{
		if( Levels == null )
		{
			Levels = new List<OneLevel>();
		}

		OneLevel lvl = new OneLevel();
		Levels.Add( lvl );

		// по центру горизонталь веревка
		for( int i = 0; i < lvl.mSIZE_X; i++ )
		{
			lvl.mMap[ i, lvl.mSIZE_Y / 2 ] = MapObjectType.Ferriade;
		}

		// по центру горизонталь лестница
		for( int i = 0; i < lvl.mSIZE_Y; i++ )
		{
			lvl.mMap[ lvl.mSIZE_X / 2, i ] = MapObjectType.Stairs;
		}

		//воокруг кирпичи
		for( int i = 0; i < lvl.mSIZE_X; i++ )
		{
			lvl.mMap[ i, 0 ] = MapObjectType.Brick;
			lvl.mMap[ i, lvl.mSIZE_Y - 1 ] = MapObjectType.Brick;
		}

		for( int i = 0; i < lvl.mSIZE_Y; i++ )
		{
			lvl.mMap[ 0, i ] = MapObjectType.Brick;
			lvl.mMap[ lvl.mSIZE_X - 1, i ] = MapObjectType.Brick;
		}

	}


}
