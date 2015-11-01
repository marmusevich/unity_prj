using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTreeDevelopment
{
    class Program
    {
        static void Main(string[] args)
        {
            string fn = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment.xml";
            string fn2 = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment2.xml";


            TreeDevelopment td = new TreeDevelopment();
            // Init
            td.Add("Base1", 0);
            td.Add("Base2", 0);
            td.Add("der1", 100);
            td.Add("der2", 150);
            td.Add("der3", 170);
            td.Add("der4", 180);

            //SetDepend
            td.SetDepend("Base1",   "der1");
            td.SetDepend("der1",    "der2");
            td.SetDepend("Base2",   "der2");
            td.SetDepend("der2",    "der3");
            td.SetDepend("der2",    "der4");

            //td.Print();

            //System.Console.WriteLine("\nSetStudie ->  Base1, Base2" );
            //td.SetStudie("Base1");
            //td.SetStudie("Base2");
            ////td.Print();

            //System.Console.WriteLine("\nSetStudie ->  der1" );
            //td.SetStudie("der1");
            ////td.Print();

            
            td.Save(fn);


            System.Console.WriteLine("\n\n\n -----  TD1  ------");
            TreeDevelopment td1 = new TreeDevelopment();
            td1.Load(fn);
            td1.Print();

            td1.Save(fn2);


            System.Console.ReadLine();
        }
    }
}
