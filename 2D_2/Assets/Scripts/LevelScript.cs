using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;

public class LevelScript : MonoBehaviour
{
    //------------------------------------------------------------------------------------
    #region tipes, helper class

    //описание одного блока
    [XmlType("Block")] 
    public class OneBlock
    {
        [XmlElement("Name")]
        public string name;
        [XmlElement("Position")]
        public Vector2 position;

        public OneBlock()
        {
            name = "";
            position = new Vector2(0.0f, 0.0f);
        }

        public OneBlock(string _name, Vector2 _position)
        {
            name = _name;
            position = _position;
        }
    }

    //описание одного уровня
    [XmlType("Level")]
    [XmlInclude(typeof(OneBlock))]
    public class OneLevel
    {
        [XmlElement("LevelName")]
        public string levelName;

        [XmlArray("Bloks")]
        [XmlArrayItem("OneBlock")]
        public List<OneBlock> bloks = new List<OneBlock>();
    }

    //хранит все уровни
    [XmlRoot("LevelsInfa")]
    [XmlInclude(typeof(OneLevel))]
    public class Levels
    {
        [XmlArray("Levels")]
        [XmlArrayItem("Level")]
        public List<OneLevel> levels = new List<OneLevel>();
    }

    //XML
    public class Serializator
    {
        //to XML
        static public void SaveXml(Levels allLevel, string datapath)
        {

            Type[] extraTypes = { typeof(Levels), typeof(OneLevel), typeof(OneBlock) };
            XmlSerializer serializer = new XmlSerializer(typeof(Levels), extraTypes);
            FileStream fs = new FileStream(datapath, FileMode.Create);
            serializer.Serialize(fs, allLevel);
            fs.Close();


        }
        //from XML
        static public Levels DeXml(string datapath)
        {
            Type[] extraTypes = { typeof(Levels), typeof(OneLevel), typeof(OneBlock) };
            XmlSerializer serializer = new XmlSerializer(typeof(Levels), extraTypes);
            FileStream fs = new FileStream(datapath, FileMode.Open);
            Levels state = (Levels)serializer.Deserialize(fs);
            fs.Close();

            return state;
        }
    }

    #endregion
    //------------------------------------------------------------------------------------

    //------------------------------------------------------------------------------------
    #region vars

    public Levels allLevels = new Levels();
    
    #endregion
    //------------------------------------------------------------------------------------

    [ContextMenu("Save level")]
    private void SaveLevel()
    {
        string datapath;	// путь к файлу сохранения для этой локации
        //datapath = Application.dataPath + "/Saves/SavedData" + Application.loadedLevel + ".xml";
        datapath = Application.dataPath + "/SavedData.xml";


        allLevels.levels.Clear();

        if (File.Exists(datapath))	// если файл сохранения уже существует
        {
            allLevels = Serializator.DeXml(datapath);  // считываем state оттуда
        }

        OneLevel lev = GetLevelFromScrin();
        allLevels.levels.Add(lev);

        Serializator.SaveXml(allLevels, datapath);
        Debug.Log("Save to [ count: " + allLevels.levels.Count + " ]: " + datapath);
    }


    private OneLevel GetLevelFromScrin()
    {
        OneLevel lev = new OneLevel();
        GameObject[] allbloks = GameObject.FindGameObjectsWithTag("Bloks");
        for (int i = 0; i < allbloks.Length; i++)
        {
            lev.bloks.Add(new OneBlock(allbloks[i].name, allbloks[i].transform.position));
        }
        return lev;
    }

    private void SetLevelToScreen(OneLevel lev)
    {
        foreach (OneBlock block in lev.bloks)
        {  // для всех предметов в комнате
            Instantiate(Resources.Load(block.name), block.position, Quaternion.identity);
            //block.inst = Instantiate(Resources.Load(block.name), block.position, Quaternion.identity) as GameObject;
            // овеществляем их
            //felt.Estate(); // и задаём дополнительные параметры
        }

    }

    void Generate()
    {
    }


    ////------------------------------------------------------------------------------------
    //#region 

    //#endregion
    ////------------------------------------------------------------------------------------




    //---------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}


//создание из префаба
//	public GameObject asteroid;


//		for (int y = 0; y < 5; y++) {
//			for (int x = 0; x < 5; x++) {
//				Instantiate (asteroid, new Vector3 (x, y / 2 + 5, 0), Quaternion.identity);
//			}
//		}


//public GUISkin skinGUI;
//public GUIStyle styleGUI;
//void OnGUI()
//{
//    //GUI.skin = skinGUI;
//    //GUI.Label(new Rect(5.0f, 3.0f, 200.0f, 200.0f), "Live's: " + playerLives + " Score: " + playerPoints, styleGUI);

//    //if (GUI.Button(new Rect(500.0f, 3.0f, 200.0f, 20.0f), "Pause"))
//    //{
//    //    Debug.Log("Pause press");
//    //}
//}