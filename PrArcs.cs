using CM3D2.Toolkit.Arc;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
	class PrArcs
	{

		public static List<String> GetArcFiles(string path)
		{

			int count = 0;
			int mcount = 0;

			String[] arcFiles = new String[0];

			ConcurrentBag<string> tempbag = new ConcurrentBag<string>();

			//Scans arc folders and lists down arc files within
			if (Directory.Exists(@path + @"\GameData"))
			{
				arcFiles = Directory.GetFiles(@path + @"\GameData", "*.arc", SearchOption.AllDirectories);

				//Arc file loading and file name saving
				Parallel.ForEach(arcFiles, a =>
				//foreach (String a in arcFiles)
				{
					++count;

					if (mcount >= 100 || mcount >= arcFiles.Count())
					{
						Console.Write("\r" + Program.rm.GetString("LArc") + " " + count + "/" + arcFiles.Count());
						mcount = 0;
					}
					else
					{
						++mcount;
					}

					foreach (string s in Arc.GetFiles(a))
					{
						tempbag.Add(s);
					}
				});
			}

			mcount = 0;
			count = 0;

			if (Directory.Exists(@path + @"\GameData_20"))
			{
				arcFiles = Directory.GetFiles(@path + @"\GameData_20", "*.arc", SearchOption.AllDirectories);

				//Loads arc files from GameData_20 and extracts the names of all the files within.
				Parallel.ForEach(arcFiles, a =>
				//foreach (String a in arcFiles2)
				{

					var afs = new ArcFileSystem();


					//progress bar function
					++count;

					if (mcount >= 100 || mcount >= arcFiles.Count())
					{
						Console.Write("\r" + Program.rm.GetString("LMArc") + " " + count + "/" + arcFiles.Count());
						mcount = 0;
					}
					else
					{
						++mcount;
					}

					foreach (string s in Arc.GetFiles(a))
					{
						tempbag.Add(s);
					}
				});
			}

			//Checks if the game is COM, and checks if CM is linked and tries to load those arc files if so and saves the names to a list.
			if (File.Exists(path + @"\COM3D2.exe"))
			{
				Console.Write("\r" + Program.rm.GetString("CMCheck") + "                              ");

				if (File.Exists(path + @"\update.cfg"))
				{
					StreamReader fs = new StreamReader(path + @"\update.cfg");

					while (!fs.EndOfStream)
					{
						string line = fs.ReadLine();
						if (line.Contains("m_strCM3D2Path="))
						{
							line = line.Replace("m_strCM3D2Path=", "");
							if (!line.Equals(""))
							{
								if (Directory.Exists(@line + @"\GameData"))
								{
									arcFiles = Directory.GetFiles(@line + @"\GameData", "*.arc", SearchOption.AllDirectories);

									Parallel.ForEach(arcFiles, a =>
									//foreach (string a in arcFiles3)
									{
										var afs = new ArcFileSystem();

										++count;

										if (mcount == 100 || mcount >= arcFiles.Count() || count == 1)
										{
											Console.Write("\r" + Program.rm.GetString("LCMArc") + " " + count + "/" + arcFiles.Count() + "        ");
											mcount = 0;
										}
										else if (mcount > 100)
										{
											mcount = 0;
										}
										else
										{
											++mcount;
										}

										foreach (string s in Arc.GetFiles(a))
										{
											tempbag.Add(s);
										}
									});
								}
							}
							break;
						}
					}
					fs.Close();
				}
			}
			return tempbag.ToList();
		}
	}
}
