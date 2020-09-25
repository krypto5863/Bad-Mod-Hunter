using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
    class Model
    {

        public static List<string> GetTex(string file)
        {
            var read = "";

            BinaryReader br = new BinaryReader(File.OpenRead(file), Encoding.UTF8);
            List<string> assets = new List<string>();
            try
            {
                read = br.ReadString();

                if (!read.Equals("CM3D2_MESH"))
                {
                    assets.Add("#NotModel");
                    br.Close();
                    return assets;
                }


                int Version = br.ReadInt32();
                //Console.WriteLine(Version);
                string Name = br.ReadString();
                string basebone = br.ReadString();

                //Console.WriteLine("Header read");

                float bonenum = br.ReadInt32();


                for (int i = 0; i < bonenum; i++)
                {
                    br.ReadString();
                    br.ReadByte();

                }
                for (int i = 0; i < bonenum; i++)
                {
                    for (int c = 0; c < 4; c++)
                        br.ReadByte();
                }
                for (int i = 0; i < bonenum; i++)
                {
                    for (int c = 0; c < 28; c++)
                        br.ReadByte();

                    if (Version >= 2001)
                    {
                        int use = br.ReadByte();
                        for (int c = 0; c < 12 && use == 1; c++)
                            br.ReadByte();
                    }
                }

                //Console.WriteLine("Bones read");

                int vertcount = br.ReadInt32();
                int facecount = br.ReadInt32();
                int locbonecount = br.ReadInt32();

                //Console.WriteLine("Counts read" + " : " + vertcount +" : " +facecount + " : " + locbonecount);

                for (int i = 0; i < locbonecount; i++)
                {
                    read = br.ReadString().ToString();
                }

                for (int i = 0; i < locbonecount; i++)
                {
                    for (int c = 0; c < 64; c++)
                        read = br.ReadByte().ToString();
                }

               // Console.WriteLine("Loc bones read");

                for (int i = 0; i < vertcount; i++)
                {
                    for (int c = 0; c < 32; c++)
                        read = br.ReadByte().ToString();
                }

                for (int i = 0; i < br.ReadInt32(); i++)
                {
                    for (int c = 0; c < 16; c++)
                        read = br.ReadByte().ToString();
                }

                for (int i = 0; i < vertcount; i++)
                {
                    for (int c = 0; c < 24; c++)
                        read = br.ReadByte().ToString();
                }

               // Console.WriteLine("verts read");

                for (int i = 0; i < facecount; i++)
                {
                    int facecount1 = br.ReadInt32();
                    //Console.Write("Face count is: " + facecount1);
                    for (int c = 0; c < (facecount1*2); c++)
                            read = br.ReadByte().ToString();
                }

                //Console.WriteLine("The rest was read");

            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to read the start of model file at " + file + " at around line: " + read + "\n\n" + e.ToString());
                assets.Add("#PosErr");
                br.Close();
                return assets;
            }
            int mate = br.ReadInt32();


            string name = "";
            string shader = "";
            string shader2 = "";

            try
            {
                for (int i = 0; i < mate; i++)
                {

                    name = br.ReadString();
                   shader =  br.ReadString();
                    shader2 =br.ReadString();
                    while (true)
                    {

                        read = br.ReadString();

                        if (read.Equals("col"))
                        {
                            read = br.ReadString();

                            br.ReadSingle();
                            br.ReadSingle();
                            br.ReadSingle();
                            br.ReadSingle();

                        }
                        else if (read.Equals("f"))
                        {
                            read = br.ReadString();
                            br.ReadSingle();
                        }
                        else if (read.Equals("tex"))
                        {
                            read = br.ReadString();
                            read = br.ReadString();

                            if (read.Equals("tex2d"))
                            {

                                read = br.ReadString();
                                assets.Add(read);
                                br.ReadString();
                                br.ReadSingle();
                                br.ReadSingle();
                                br.ReadSingle();
                                br.ReadSingle();
                            }
                        } else if (read.Equals("end"))
                        {
                            break;
                        }
                    }
                }
            } catch (Exception e)
            {
                assets.Add("#PosErr");
                br.Close();
                return assets;
            }
            br.Close();
            return assets;
        }
    }
}
