using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuInfo : MonoBehaviour
{
    #region tipes

    public class MenuOneItem
    {
        public string Caption = "";
        public string SceneName = "";

        public MenuOneItem( string _Caption, string _SceneName )
        {
            Caption = _Caption;
            SceneName = _SceneName;
        }

        public override string ToString()
        {
            return string.Format( "Caption = '{0}', Scene name = '{1}'", ( Caption == null ) ? "null" : Caption, ( SceneName == null ) ? "null" : SceneName );
        }

    }

    #endregion

    public static List<MenuOneItem> MenuItems = new List<MenuOneItem>();

    #region стандартные калбеки unity

    // Use this for initialization
    void Start()
    {
        //получить элементы меню
        FillMenuItems();
    }
	
    // Update is called once per frame
    void Update()
    {
	
    }

    #endregion


    #region my

    public  static void FillMenuItems()
    {
        if( MenuItems.Count == 0 )
        {
            MenuItems = GetMenuItems();
        }
    }

    //тестовый, в дальнейшем получать из файла, также механизм записи в файл
    public  static List<MenuOneItem> GetMenuItems()
    {
        List<MenuOneItem> tmp = new List<MenuOneItem>();
        tmp.Add( new MenuOneItem( "Lode runner", "lode_runner" ) );
        tmp.Add( new MenuOneItem( "Habr: Character", "habr_test_1" ) );
        tmp.Add( new MenuOneItem( "Habr: Platformer", "habr_test_2" ) );
        tmp.Add( new MenuOneItem( "Arcanoid", "arcanoid" ) );
        tmp.Add( new MenuOneItem( "Space attack", "space_attac" ) );
        tmp.Add( new MenuOneItem( "Tree development", "tree_development" ) );

//		for (int i = 2; i<10; i++)
//			tmp.Add (new MenuOneItem (string.Format ("_scenes_{0}", i), null));

        return tmp;
    }

    #endregion

}
