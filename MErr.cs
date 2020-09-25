using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
namespace Bad_Mod_Hunter
{
    class MErr
    {
        private const string MENU_TAG = "CM3D2_MENU";

        public static List<String> ErrCheck(string file)
        {
            var strList = new List<string>();
            string read = "";
            var br = new BinaryReader(File.OpenRead(file), Encoding.UTF8);

            try
            {
                var tag = br.ReadString();
                if (tag != MENU_TAG)
                {
                    //Console.WriteLine("Provided file is not a valid menu file!");

                    strList.Add("!" + Program.rm.GetString("NotMenu") + " " + file);
                    return strList;
                }

                int Version = br.ReadInt32();
                string Path = br.ReadString();
                string Name = br.ReadString();
                string Part = br.ReadString();
                string Description = br.ReadString();

                br.ReadInt32();
            } catch (Exception e)
            {
                br.Close();
                strList.Add("!" + Program.rm.GetString("MErrHead") + " " + file);
                return strList;
            }

            int line = 0;

            try
            {
                while (true)
                {
                    var i = 0;
                    var n1 = br.ReadByte();
                    if (n1 == 0)
                        break;

                    for (i = 0; i < n1;)
                    {

                        read = br.ReadString(); ++i;

                        //Console.WriteLine("Reading this branch " + read);

                        // Console.WriteLine(read);
                        switch (read)
                        {//Check for icon branches
                            case "icons":
                            case "icon":
                                if (n1 != 2)
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrL") + " icons");
                                }

                                read = br.ReadString().ToLower();
                                ++i;

                                if (!read.Contains(".tex"))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrType") + " " + read);
                                }
                                break;

                            //Check for texture loader branches
                            case "テクスチャ合成":
                                while (i < 5)
                                {
                                    br.ReadString();
                                    ++i;
                                }
                                read = br.ReadString().ToLower();
                                ++i;
                                if (!read.ToLower().Contains(".tex"))
                                {
                                    strList.Add("!"+ Program.rm.GetString("MErrType") + " " + read);
                                }
                                break;

                            case "setumei":

                                if (n1 == 2)
                                {
                                    read = br.ReadString();
                                    ++i;
                                } else if (n1 == 1)
                                {

                                }

                                break;

                            //Check for model loader branches
                            case "additem":

                                read = br.ReadString();
                                ++i;
                                if (!read.ToLower().Contains(".model"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }

                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " additem: " + read);
                                }
                                break;

                            //Check for material swap
                            case "マテリアル変更":
                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " マテリアル変更: " + read);
                                }

                                read = br.ReadString();
                                ++i;


                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " "+ read);
                                }


                                read = br.ReadString();
                                ++i;

                                if (!read.ToLower().Contains(".mate"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }
                                break;

                            //Texture replacers for stuff like skins
                            case "テクスチャ変更":
                                read = br.ReadString().ToLower();
                                ++i;
                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " テクスチャ変更: " + read);
                                }

                                read = br.ReadString().ToLower();
                                ++i;
                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }

                                read = br.ReadString().ToLower();
                                ++i;
                                if (!Lib.IsTexSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrTSlot") + " " + read);
                                }

                                read = br.ReadString().ToLower();
                                ++i;
                                if (!read.ToLower().Contains(".tex"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }
                                break;

                            case "メニューフォルダ":
                                read = br.ReadString();
                                ++i;
                                if (!Lib.IsMCat(read))
                                {
                                    strList.Add("#"+ Program.rm.GetString("MErrMCat") + " " + read);
                                }
                                break;
                            case "リソース参照":
                                read = br.ReadString();
                                ++i;
                                if (!Lib.IsClothState(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrMekure") + " " + read);
                                }
                                read = br.ReadString();
                                ++i;
                                if (!read.Contains(".menu"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }
                                break;
                            case "パーツnode消去":
                            case "パーツnode表示":
                                read = br.ReadString();
                                ++i;
                                if (!Lib.IsCat(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrCat") + " パーツnode: " + read);
                                }
                                break;
                            case "アタッチポイントの設定":
                                read = br.ReadString();
                                ++i;
/*                                if (!IsAttachPoint(read))
                                {
                                    strList.Add("#Unrecognized Attach Point: " + read);
                                }*/
                                while (i < n1)
                                {
                                    read = br.ReadString();
                                    ++i;

                                    if (!float.TryParse(read, NumberStyles.Float | NumberStyles.AllowThousands,
                             CultureInfo.InvariantCulture, out _))
                                    {
                                        strList.Add("#"+ Program.rm.GetString("MErrFloat") + " " + read);
                                    }
                                }
                                break;
                            case "アイテムパラメータ":
                                read = br.ReadString();
                                ++i;
                                if (!Lib.IsCat(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrCat") + " アイテムパラメータ: " + read);
                                }
                                br.ReadString();
                                ++i;
                                read = br.ReadString();
                                ++i;
                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }
                                break;
                            case "アイテム":

                                if (n1 > 1)
                                {
                                    read = br.ReadString();
                                    ++i;
                                    if (!Lib.IsDelItem(read)  && !read.ToLower().Contains(".menu"))
                                    {
                                        strList.Add("#" + Program.rm.GetString("MErrDel") + " " + read);
                                    }
                                }else
                                {
                                    strList.Add(Program.rm.GetString("MErrDelEmpty"));
                                }
                                break;

                            case "tex":

                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " tex: " + read);
                                }

                                read = br.ReadString();
                                ++i;

                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }

                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsTexSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrTSlot") + " " + read);
                                }

                                read = br.ReadString();
                                ++i;

                                if (!read.Contains(".tex"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }
                                if (i > n1)
                                {
                                    read = br.ReadString();
                                    ++i;

                                    if (!Lib.recoSlot.Contains(read.ToLower()))
                                    {
                                        strList.Add("#" + Program.rm.GetString("MErrRecoSlot") + " " + read);
                                    }
                                }

                                break;

                            case "shader":

                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " shader: " + read);
                                }

                                read = br.ReadString();
                                ++i;

                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }

                                break;
                            case "priority":

                                read = br.ReadString();
                                ++i;

                                int pri = 0;

                                if(!Int32.TryParse(read, out pri))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }

                                if (pri <= 0)
                                {
                                    strList.Add(Program.rm.GetString("MErrUPri") + " " + read);
                                }

                                break;
                            case "アイテム条件":
                                read = br.ReadString();
                                ++i;

                                if (!Lib.IsCat(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrCat") + " アイテム条件: " + read);
                                }

                                read = br.ReadString();
                                ++i;
                                read = br.ReadString();
                                ++i;
                                read = br.ReadString();
                                ++i;

                                read = br.ReadString();
                                ++i;

                                if (!read.Contains(".menu"))
                                {
                                    strList.Add("!" + Program.rm.GetString("MErrType") + " " + read);
                                }

                                break;

                            case "delitem":

                                read = br.ReadString(); ++i;
                                if (n1 <= 1)
                                {
                                    strList.Add(Program.rm.GetString("MErrDelEmpty"));
                                }
                                else if (!Lib.IsSlot (read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + " delitem: " + read);
                                }

                                break;

                            case "color_set":


                                read = br.ReadString(); ++i;
                                if (!Lib.IsCSetType(read))
                                {
                                    strList.Add(Program.rm.GetString("MErrColSlot")+ " " + read);
                                }

                                break;

                            case "color":

                                read = br.ReadString(); ++i;

                                if (!Lib.IsSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrSlot") + " " + "color: " + read);
                                }

                                read = br.ReadString(); ++i;

                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }

                                read = br.ReadString(); ++i;

                                if (!Lib.IsTexSlot(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrTSlot") + " " + read);
                                }

                                while (i > n1)
                                {
                                    read = br.ReadString(); ++i;

                                    if (!Int32.TryParse(read, out _))
                                    {
                                        strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                    }
                                }

                                break;

                            case "catno":
                                read = br.ReadString(); ++i;
                                if (!Int32.TryParse(read, out _))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrNum") + " " + read);
                                }
                                break;

                            case "category":

                                read = br.ReadString(); ++i;

                                if (!Lib.IsCat(read))
                                {
                                    strList.Add("#" + Program.rm.GetString("MErrCat") + " category: " + read);
                                }

                                break;

                            case "maskitem":
/*                                read = br.ReadString(); ++i;

                                if (!IsSlot(read)){
                                    strList.Add("#Unrecognized Masking Slot: " + read);
                                }*/

                                break;

                            default:

/*                                if (Program.IsMFile(read))
                                {
                                    strList.Add("#PossibleAsset: " + read);
                                }*/
/*                                if (i == 1 && !IsHandled(read) && !IsComment(read))
                                {
                                    strList.Add("#Unhandled Branch: " + read);
                                }*/
                                break;
                        }
                    }

                    line += i;

                    if (strList.Count == 0)
                        continue;

                    if (strList[0] == "end")
                        break;
                }
            }
            catch (Exception e)
            {
                //Just skip the menu file
                strList.Add("\n" + Program.rm.GetString("MErrReadErr") + " " + "@" + file +"\n\n" + e.ToString());
                // Console.Write("\n#ReadErr at " + read + " at line " + line + " @ " + file + "\n\n" + e.ToString());
                br.Close();
                return strList;
            }
            return strList;
        }
    }
}