using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
	class Mate
	{

		public static List<string> getAssets(string file)
		{
			var read = "";

			BinaryReader br = new BinaryReader(File.OpenRead(file), Encoding.UTF8);
			List<string> assets = new List<string>();

			try
			{

				read = br.ReadString();

				if (!read.Equals("CM3D2_MATERIAL"))
				{
					assets.Add("!NotMaterial");
					br.Close();
					return assets;
				}

				//Console.WriteLine(read);

				int Version = br.ReadInt32();
				//Console.WriteLine(Version);
				string Name = br.ReadString();
				// Console.WriteLine(Name);
				string Name2 = br.ReadString();
				//Console.WriteLine(Name2);
				string shader1 = br.ReadString();
				//Console.WriteLine(shader1);
				string shader2 = br.ReadString();
				//Console.WriteLine(shader2);

				read = br.ReadString();

				//Console.WriteLine(read);

				var i = 0;


				while (!read.Equals("end"))
				{

					i++;

					//Console.WriteLine("Round: " + i);

					read = br.ReadString();
					//Console.WriteLine(read);

					if (read.ToLower().Equals("tex2d"))
					{
						read = br.ReadString();
						//Console.WriteLine(read);
						assets.Add(read);

						read = br.ReadString();
						//Console.WriteLine(read);

						float sing = 0;

						sing = br.ReadSingle();
						//Console.WriteLine(sing);
						sing = br.ReadSingle();
						//Console.WriteLine(sing);
						sing = br.ReadSingle();
						//Console.WriteLine(sing);
						sing = br.ReadSingle();
						//Console.WriteLine(sing);

					}
					else if (isProp(read))
					{
						break;
					}
					else if (isFloat(read))
					{
						break;
					}

				}
			}
			catch (Exception e)
			{
				assets.Add("!PosErr");
				br.Close();
				return assets;
			}
			br.Close();
			return assets;
		}


		public static bool isProp(string prop)
		{

			HashSet<string> props = new HashSet<string>()
			{
				"_color",
				"_shadowcolor",
				"_rimcolor",
				"_outlinecolor",
				"_shadowcolor",
			};

			if (props.Contains(prop.ToLower()))
			{
				return true;
			}

			return false;
		}

		public static bool isFloat(string prop)
		{

			HashSet<string> props = new HashSet<string>()
			{
				"_shininess",
				"_outlinewidth",
				"_rimpower",
				"_rimshift",
				"_hirate",
				"_hipow",
				"_cutoff"
			};

			if (props.Contains(prop.ToLower()))
			{
				return true;
			}

			return false;
		}

	}
}
