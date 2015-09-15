using UnityEngine;
//using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using System;
using System.IO;
using UnityEngine.EventSystems;


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
        public string levelName = "default level";

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

    public GameObject prefabBlueBlock;
    public GameObject prefabGrinBlock;
    public GameObject prefabRedBlock;
    public GameObject prefabYellovBlock;

    private Dictionary<string, GameObject> allPrefab = new Dictionary<string, GameObject>();
    
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
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Bloks"))
        {
            lev.bloks.Add(new OneBlock(obj.name, obj.transform.position));
        }
        return lev;
    }

    private void SetLevelToScreen(OneLevel lev)
    {
        foreach (OneBlock block in lev.bloks)
        {  // для всех предметов в комнате
            GameObject obj = Instantiate(allPrefab[block.name], block.position, Quaternion.identity) as GameObject;
        }
    }




    // очистить текущий уровень
    //void Update()
    //{

    //}

    // сохранить в хранилища скриптов по номеру
    //void Update()
    //{

    //}

    //посторить из хранилища скриптов по номеру
    //void Update()
    //{

    //}

    // перезапуск = очистить текущий уровень + посторить из хранилища скриптов по номеру
    //void Update()
    //{

    //}

    // для редактора добавить в конец уровень
    //void Update()
    //{

    //}

    // дать обще количество уровней
    //void Update()
    //{

    //}




    // как состояния игры и игрока
    //  - сохранить текущее состояние
    //  - востоновить текущее состояние
    // для состояния свой серилизатор
    // хрень для простоты хранить последний пройденый уровень (номер), жизни, очки 
    //void Update()
    //{

    //}




    //---------------------------------------------------------------------------------------------
    // Use this for initialization
    void Start()
    {
        //иницилизировать словарь префабами
        allPrefab.Add(prefabBlueBlock.name, prefabBlueBlock);
        allPrefab.Add(prefabGrinBlock.name, prefabGrinBlock);
        allPrefab.Add(prefabRedBlock.name, prefabRedBlock);
        allPrefab.Add(prefabYellovBlock.name, prefabYellovBlock);

    }

    // Update is called once per frame
    void Update()
    {

    }
}


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

////------------------------------------------------------------------------------------
//#region 

//#endregion
////------------------------------------------------------------------------------------
