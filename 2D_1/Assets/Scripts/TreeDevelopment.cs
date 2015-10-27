using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

// добавить проверки


namespace TestTreeDevelopment
{
    public class TreeDevelopment
    {
        // внутреннее представление
        public class TreeDevelopmentElement
        {
            //зависит
            public List<string> mDepends = null;
            //влияет
            public List<string> mEffectOn = null;
            //стоимость изучения
            public int mTrainingPrice = 0;
            //изучено
            public bool mIsStudied = false;
            //может быть изучено
            public bool mIsCanStudied = false;

            public TreeDevelopmentElement()
            {
                mDepends = new List<string>();
                mEffectOn = new List<string>();
            }

            public void SetDepend(string key)
            {
                mDepends.Remove(key);
                if (mDepends.Count() == 0)
                    mIsCanStudied = true;
            }
        }
        //хранилище
        private Dictionary<string, TreeDevelopmentElement> mData = null;

        public TreeDevelopment()
        {
            mData = new Dictionary<string, TreeDevelopmentElement>();
        }

        //добавить
        public void Add(string Key, int TrainingPrice)
        {
            TreeDevelopmentElement tmp = new TreeDevelopmentElement();
            tmp.mTrainingPrice = TrainingPrice;
            mData.Add(Key, tmp);
        }

        // установить зависемость, Key - этот ключь влияет на KeyEffectOn
        public void SetDepend(string Key, string KeyEffectOn)
        {
            TreeDevelopmentElement tmp;
            tmp = mData[KeyEffectOn];
            tmp.mDepends.Add(Key);

            tmp = mData[Key];
            tmp.mEffectOn.Add(KeyEffectOn);
        }

        //выучить
        public void SetStudie(string key)
        {
            TreeDevelopmentElement tmp = mData[key];
            tmp.mIsStudied = true;

            foreach (string effectOnKey in tmp.mEffectOn)
            {
                TreeDevelopmentElement effect = mData[effectOnKey];
                effect.SetDepend(key);
            }
        }

        
        //печать
        public void Print()
        {
            foreach (KeyValuePair<string, TreeDevelopmentElement> tmp in mData)
            {
                string str = string.Format("{0}\t Is Can Studied={1},\t Is Studied={2},\t Training Price={3}", tmp.Key, tmp.Value.mIsCanStudied, tmp.Value.mIsStudied, tmp.Value.mTrainingPrice);
                System.Console.WriteLine(str);
                str = "";
                foreach (string s in tmp.Value.mDepends)
                    str += s + ", ";
                System.Console.WriteLine("\t Depends - " + str);
                str = "";
                foreach (string s in tmp.Value.mEffectOn)
                    str += s + ", ";
                System.Console.WriteLine("\t EffectOn - " + str);
            }
        }

        public void Save(string fn)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Indent = true;
            settings.IndentChars = "\t";
            using (XmlWriter writer = XmlTextWriter.Create(fn, settings))
            {
                writer.WriteStartElement("root");
                foreach (KeyValuePair<string, TreeDevelopmentElement> tmp in mData)
                {
                        writer.WriteStartElement("Key");
                        writer.WriteAttributeString("IsCanStudied", tmp.Value.mIsCanStudied.ToString());
                        writer.WriteAttributeString("IsStudied", tmp.Value.mIsStudied.ToString());
                        writer.WriteAttributeString("TrainingPrice", tmp.Value.mTrainingPrice.ToString());
                            writer.WriteString(tmp.Key);
                            foreach (string s in tmp.Value.mDepends)
                            {
                                writer.WriteStartElement("Depend");
                                    writer.WriteString(s);
                                writer.WriteEndElement();
                            }

                            foreach (string s in tmp.Value.mEffectOn)
                            {
                                writer.WriteStartElement("EffectOn");
                                writer.WriteString(s);
                                writer.WriteEndElement();
                            }

                        writer.WriteEndElement();
                }
                writer.WriteEndElement();
            }
        }

        public void Load(string fn) 
        {
            try
            {
                if (File.Exists(fn))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(fn);

                    XmlNodeList elemList = doc.GetElementsByTagName("Key");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNode node = elemList[i];

                        TreeDevelopmentElement tmp = new TreeDevelopmentElement();
                        tmp.mIsCanStudied = Boolean.Parse( node.Attributes["IsCanStudied"].Value ); 
                        tmp.mIsStudied = Boolean.Parse( node.Attributes["IsStudied"].Value ); 
                        tmp.mTrainingPrice = int.Parse( node.Attributes["TrainingPrice"].Value);

                        mData.Add(node.FirstChild.InnerText, tmp);

                    }

                    elemList = doc.GetElementsByTagName("Key");
                    for (int i = 0; i < elemList.Count; i++)
                    {
                        XmlNode node = elemList[i];
                        XmlNodeList innerElemList = node.ChildNodes;
                        for (int j = 0; j < innerElemList.Count; j++)
                        {
                            XmlNode innNode = innerElemList[j];
                            if (innNode.FirstChild != null)
                            {
                                if (innNode.Name == "Depend")
                                    mData[node.FirstChild.InnerText].mDepends.Add(innNode.FirstChild.InnerText);
                                if (innNode.Name == "EffectOn")
                                    mData[node.FirstChild.InnerText].mEffectOn.Add(innNode.FirstChild.InnerText);
                            }
                        }
                    }  
                }
            }
            catch (Exception ex) // обработка ошибок
            {
                System.Console.WriteLine(string.Format("{0}.{1}()] {2}\r\n", ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message));
            }
        }
    }
}





//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestTreeDevelopment
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            string fn = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment.xml";
//            string fn2 = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment2.xml";


//            TreeDevelopment td = new TreeDevelopment();
//            // Init
//            td.Add("Base1", 0);
//            td.Add("Base2", 0);
//            td.Add("der1", 100);
//            td.Add("der2", 150);
//            td.Add("der3", 170);
//            td.Add("der4", 180);

//            //SetDepend
//            td.SetDepend("Base1",   "der1");
//            td.SetDepend("der1",    "der2");
//            td.SetDepend("Base2",   "der2");
//            td.SetDepend("der2",    "der3");
//            td.SetDepend("der2",    "der4");

//            //td.Print();

//            //System.Console.WriteLine("\nSetStudie ->  Base1, Base2" );
//            //td.SetStudie("Base1");
//            //td.SetStudie("Base2");
//            ////td.Print();

//            //System.Console.WriteLine("\nSetStudie ->  der1" );
//            //td.SetStudie("der1");
//            ////td.Print();

            
//            td.Save(fn);


//            System.Console.WriteLine("\n\n\n -----  TD1  ------");
//            TreeDevelopment td1 = new TreeDevelopment();
//            td1.Load(fn);
//            td1.Print();

//            td1.Save(fn2);


//            System.Console.ReadLine();
//        }
//    }
//}