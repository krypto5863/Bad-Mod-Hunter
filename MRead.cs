using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
    class MRead
    {

        private const string MENU_TAG = "CM3D2_MENU";
        public static List<String> HReadMenu(string file)
        {

            var strList = new List<string>();

            try
            {
                var br = new BinaryReader(File.OpenRead(file), Encoding.UTF8);
                var tag = br.ReadString();
                if (tag != MENU_TAG)
                {
                    //Console.WriteLine("Provided file is not a valid menu file!");
                    strList.Add("NotMenu");
                    return strList;
                }

                int Version = br.ReadInt32();
                string Path = br.ReadString();
                string Name = br.ReadString();
                string Part = br.ReadString();
                string Description = br.ReadString();

                br.ReadInt32();

                while (true)
                {
                    var n1 = br.ReadByte();
                    if (n1 == 0)
                        break;

                    for (var i = 0; i < n1; i++)
                        strList.Add(br.ReadString());

                    if (strList.Count == 0)
                        continue;

                    if (strList[0] == "end")
                        break;
                }

            }
            catch (Exception e)
            {
                //Just skip the menu file
                Console.WriteLine("Error while trying to read menu file " + file + "\n\n" + e.ToString());
                strList.Add("!PosErr");
                return strList;
            }
            return strList;
        }
    }
}
