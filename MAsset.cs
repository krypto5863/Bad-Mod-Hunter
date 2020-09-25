using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
    class MAsset
    {
        private const string MENU_TAG = "CM3D2_MENU";
        public static List<String> NewGetAssets(string file)
        {

            var strList = new List<string>();

            try
            {
                var br = new BinaryReader(File.OpenRead(file), Encoding.UTF8);
                var tag = br.ReadString();
                if (tag != MENU_TAG)
                {
                    //Console.WriteLine("Provided file is not a valid menu file!");
                    strList.Add("!NotMenu");
                    br.Close();
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

                    for (var i = 0; i < n1;)
                    {

                        string read = br.ReadString();

                        ++i;

                        // Console.WriteLine(read);

                        if (read.Equals("icons") || read.Equals("icon"))
                        {
                            while (i != (n1 - 1))
                            {
                                br.ReadString();
                                ++i;
                            }

                            string res = br.ReadString().ToLower();

                            // Console.WriteLine("Sending this back: " + res);

                            strList.Add(res);
                            ++i;
                        }
                        else if (read.Equals("テクスチャ合成"))
                        {
                            while (i < 5)
                            {
                                br.ReadString();
                                ++i;
                            }
                            string res = br.ReadString().ToLower();

                            //Console.WriteLine("Sending back this: " + res);

                            strList.Add(res);
                            ++i;
                        }
                        else if (read.Equals("additem"))
                        {
                            strList.Add(br.ReadString().ToLower());
                            ++i;
                        }
                        else if (read.Equals("マテリアル変更"))
                        {
                            while (i < 3)
                            {
                                br.ReadString();
                                ++i;
                            }
                            string res = br.ReadString().ToLower();
                            ++i;

                            strList.Add(res);
                        }
                        else if (read.Equals("テクスチャ変更"))
                        {
                            while (i < 4)
                            {
                                br.ReadString();
                                ++i;
                            }
                            string res = br.ReadString().ToLower();

                            //Console.WriteLine("Sending back this: " + res);

                            strList.Add(res);
                            ++i;
                        }
                    }

                    if (strList.Count == 0)
                        continue;

                    if (strList[0] == "end")
                        break;
                }
                br.Close();
            }
            catch
            {
                //Just skip the menu file
                strList.Add("!PosErr");
                return strList;
            }

            return strList;
        }
    }
}
