using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Bad_Mod_Hunter;

namespace Bad_Mod_Hunter
{
	class Program
	{

		public static System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Bad Mod Hunter.string", Assembly.GetExecutingAssembly());
		static void Main(string[] args)
		{
			try
			{

				CultureInfo ci = CultureInfo.CurrentUICulture;

				/*				if (ci.TwoLetterISOLanguageName.ToString().Equals("zh")){
									LangManager.ChangeCulture("zh_HANS");
									Console.Title = "BadModHunter CM3D2&COM3D2 壞插件壞MOD獵人 輪胎KU39漢化版 #原作者:Doctor#";
								} else
								{

								}*/

				LangManager.ChangeCulture("en");

				Console.OutputEncoding = System.Text.Encoding.UTF8;

				Console.WriteLine(rm.GetString("DirAsk"));

				string path = Console.ReadLine();

				String[] allfiles;

				List<FileInfo> fileI = new List<FileInfo>();
				HashSet<string> fileNames = new HashSet<string>();
				HashSet<string> aFileNames = new HashSet<String>();

				ConcurrentBag<String> tempbag = new ConcurrentBag<String>();
				ConcurrentBag<FileInfo> tempbagFI = new ConcurrentBag<FileInfo>();

				int mcount = 0;
				int count = 0;
				int nmod = 0;
				int modfile = 0;
				double modsize = 0;
				double nmodsize = 0;

				Stopwatch watch = new Stopwatch();
				Stopwatch arcwatch = new Stopwatch();
				Stopwatch filewatch = new Stopwatch();
				Stopwatch dupewatch = new Stopwatch();
				Stopwatch adupewatch = new Stopwatch();
				Stopwatch moderrwatch = new Stopwatch();

				ConcurrentBag<String> sbad = new ConcurrentBag<String>();
				ConcurrentBag<String> err = new ConcurrentBag<String>();
				ConcurrentBag<String> bad = new ConcurrentBag<String>();
				ConcurrentBag<String> adupe = new ConcurrentBag<String>();
				ConcurrentBag<String> mmfile = new ConcurrentBag<String>();
				ConcurrentBag<String> usedfile = new ConcurrentBag<String>();

				Dictionary<string, int> errCount = new Dictionary<string, int>();
				List<string> errIgnore = new List<string>();

				Console.WriteLine("\n"+ rm.GetString("ArcAsk"));

				int rArc = Int32.Parse(Console.ReadLine());

				Console.WriteLine(rm.GetString("FolderAsk"));

				//Scans mod folders and lists down files within
				if (Console.ReadLine().Equals("2"))
				{

					Console.Clear();

					Console.WriteLine("\n"+ rm.GetString("Work") + "\n");

					Console.Write("\r" + rm.GetString("Scanning"));

					@allfiles = Directory.GetFiles(@path + @"\Mod", "*", SearchOption.AllDirectories);
				}
				else
				{

					Console.Clear();

					Console.WriteLine("\n" + rm.GetString("Work") + "\n");

					Console.Write("\r" + rm.GetString("Scanning"));

					@allfiles = Directory.GetFiles(@path + @"\Sybaris\GameData", "*", SearchOption.AllDirectories);
				}

				watch.Start();

				if (rArc == 1)
				{

					arcwatch.Start();

					aFileNames = PrArcs.GetArcFiles(path).ToHashSet();

					arcwatch.Stop();

				}

				filewatch.Start();

				Console.Write("\r"+ rm.GetString("Loading") + "                                 ");

				//We make sure the files returned are accessible by the application. Otherwise, we don't note them down.
				Parallel.ForEach(allfiles, f =>
				//foreach (string f in allfiles)
				{
					if (File.Exists(f))
					{
						tempbagFI.Add(new FileInfo(f));
					}
				});

				fileI.AddRange(tempbagFI.ToList());

				tempbagFI = new ConcurrentBag<FileInfo>();


				//This saves file names to a list for easy retrieval. Due to the nature of threadsafe collections, they are not the correct solution. So here we use a single thread foreach loop to add to our standard list. We also filter by file type.
				foreach (FileInfo f in fileI)
				{
					if (IsMFile(f.Name.ToLower()))
					{
						fileNames.Add(f.Name.ToLower());
					}
					else
					{
						//Omit the entry entirely
						fileNames.Add("");
					}
				}

				//Dupes searching was converted to this function. Should be light years faster in testing. Spoiler alert, it definitely is.
				dupewatch.Start();

				List<FileInfo> dupe = fileI.GroupBy(x => x.Name.ToLower())
												.Where(g => g.Count() > 1)
												.SelectMany(x => x).ToList();

				dupe = dupe.OrderBy(x => x.FullName).ToList();

				dupewatch.Stop();

				filewatch.Stop();

				count = 0;
				mcount = 0;

				//Main functions
				foreach (FileInfo f in fileI)
				{

					++count;
					//The reporting function, it gives the user a basic progress tracker.
					if (mcount >= 100 || count == fileI.Count || count == 1)
					{
						Console.Write("\r"+ rm.GetString("Count") + " " + count + "/" + fileI.Count + "                                           ");
						mcount = 0;
					}
					else
					{
						++mcount;
					}


					//Checks if the file is a mod file before it even begins processing.
					if (IsMFile(f.Name))
					{

						//Adds to the modfile count and to the total modfile size to be reported at the end.
						++modfile;
						modsize += f.Length;

						try
						{
							//Checks if the file is zero length. These are bad files.
							if (f.Length <= 0)
							{

								//File is a mod file and is zero length, bad file.
								if (IsMFile(f.Name))
								{
									bad.Add(f.FullName);
								}
								else//File is zero length but is not a mod file, a minor bad file.
								{
									sbad.Add(f.FullName);
								}
							}
						}
						catch
						{//Sometimes the file cannot be read, these files are added as error files.
							err.Add(f.FullName);
						}

						if (rArc == 1)
						{
							adupewatch.Start();

							if (aFileNames.Contains(f.Name.ToLower()))
							{
								adupe.Add(f.FullName);
							}

							adupewatch.Stop();
						}

						moderrwatch.Start();

						//This function checks if the current file is a menu, if so, it sends it to parser to retrieve assets and then checks if they exist in the mod folder or in the arc files
						if (rArc == 1 && f.Extension.ToLower().Equals(".menu"))
						{
							//Console.WriteLine("Attempting to read " + f.Name);

							foreach (string s in MErr.ErrCheck(f.FullName))
							{
								if (s.Contains("#"))
									err.Add(s + " @ " + f.FullName);
								else if (s.Contains("!"))
								{
									bad.Add(s + " @ " + f.FullName);
								}
								else
								{
									sbad.Add(s + " @ " + f.FullName);
								}

								if (f.Name.Any(Char.IsWhiteSpace))
								{
									sbad.Add(rm.GetString("MFileSpace") + " @ " + f.FullName);
								}
							}

							var assets = new HashSet<string>(MAsset.NewGetAssets(f.FullName));

							if (assets.Contains("NotMenu"))
							{
								bad.Add(rm.GetString("NotMenu") + " " + f.FullName);
							}
							else if (assets.Contains("PosErr"))
							{
								err.Add(rm.GetString("NoRead") + " " + f.FullName);
							}
							else
							{

								Parallel.ForEach(assets, a =>
								//foreach (string a in assets)
								{

									if (!a.Contains("*") && !fileNames.Contains(a.ToLower()) && !aFileNames.Contains(a.ToLower()))
									{
										mmfile.Add(rm.GetString("MFile") + " " + f.FullName + " "+ rm.GetString("MFile") + " " + a);
									}
								});
							}
						}
						else if (rArc == 1 && f.Extension.ToLower().Equals(".mate"))
						{
							var assets = new HashSet<string>(Mate.getAssets(f.FullName));


							if (assets.Contains("NotMaterial"))
							{
								bad.Add(rm.GetString("NotMate") + " " + f.FullName);
							}
							else if (assets.Contains("PosErr"))
							{
								bad.Add(rm.GetString("NoRead") + " " + f.FullName);
							}
							else
							{

								Parallel.ForEach(assets, a =>
								//foreach (string a in assets)
								{

									if (!a.Contains("*") && !fileNames.Contains(a.ToLower() + ".tex") && !aFileNames.Contains(a.ToLower() + ".tex"))
									{
										mmfile.Add("Material file " + f.FullName + " is missing " + a + ".tex");
									}
								});
							}
						}
						else if (rArc == 1 && f.Extension.ToLower().Equals(".model"))
						{
							var assets = new HashSet<string>(Model.GetTex(f.FullName));


							if (assets.Contains("NotModel"))
							{
								bad.Add(rm.GetString("NotModel") + " " + f.FullName);
							}
							else if (assets.Contains("PosErr"))
							{
								bad.Add(rm.GetString("NoRead") + " " + f.FullName);
							}
							else
							{

								Parallel.ForEach(assets, a =>
								//foreach (string a in assets)
								{

									if (!a.Contains("*") && !fileNames.Contains(a.ToLower() + ".tex") && !aFileNames.Contains(a.ToLower() + ".tex"))
									{
										mmfile.Add(rm.GetString("MoFile") + " " + f.FullName + " "+ rm.GetString("Missing") + " " + a + ".tex");
									}
								});
							}
						}

						moderrwatch.Stop();
					}
					else
					{
						//file is not a mod file, noting down it's size and adding it to the none mod file count.
						++nmod;
						nmodsize += f.Length;
					}
				}

				watch.Stop();
				StreamWriter wr = new StreamWriter("log.txt");

				{
					List<string> sorted = sbad.ToList();

					sorted.Sort();

					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine("\n\n"+ rm.GetString("Warn"));

					wr.WriteLine(rm.GetString("Warn"));
					foreach (string f in sorted)
					{
						Console.WriteLine(f);
						wr.WriteLine(f);
					}

					sorted = err.ToList();

					sorted.Sort();

					wr.WriteLine("\n\n"+ rm.GetString("Err"));
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("\n\n" + rm.GetString("Err"));
					foreach (string f in sorted)
					{
						if (!f.Equals(""))
						{
							Console.WriteLine(f);
							wr.WriteLine(f);
						}
					}

					sorted = bad.ToList();

					sorted.Sort();

					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine("\n\n" + rm.GetString("Crit"));
					wr.WriteLine("\n\n" + rm.GetString("Crit"));
					foreach (string f in sorted)
					{
						Console.WriteLine(f);
						wr.WriteLine(f);
					}

					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.WriteLine("\n\n" + rm.GetString("Dupe"));
					wr.WriteLine("\n\n" + rm.GetString("Dupe"));
					foreach (FileInfo f in dupe)
					{

						if (IsMFile(f.Extension))
						{
							Console.WriteLine(f.FullName);
							wr.WriteLine(f);
						}

					}

					sorted = adupe.ToList();

					sorted.Sort();

					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine("\n\n" + rm.GetString("ADupe"));
					wr.WriteLine("\n\n" + rm.GetString("ADupe"));
					foreach (string f in sorted)
					{
						Console.WriteLine(f);
						wr.WriteLine(f);
					}

					sorted = mmfile.ToList();

					sorted.Sort();

					Console.ForegroundColor = ConsoleColor.DarkMagenta;
					Console.WriteLine("\n\n" + rm.GetString("MMiss"));
					wr.WriteLine("\n\n" + rm.GetString("MMiss"));
					foreach (string f in sorted)
					{
						Console.WriteLine(f);
						wr.WriteLine(f);
					}

					wr.Close();

					Console.ForegroundColor = ConsoleColor.White;

					Console.WriteLine("\n\n" + rm.GetString("Proc1") +" "+ fileI.Count + " "+ rm.GetString("Proc2") + " " + aFileNames.Count + " " + rm.GetString("Proc3") + " " + watch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(rm.GetString("ARead") + " " + arcwatch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(rm.GetString("FilePrep") + " " + filewatch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(rm.GetString("DupeFind") + " " + dupewatch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(rm.GetString("ADupeFind") + " " + adupewatch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(rm.GetString("MErr") + " " + moderrwatch.Elapsed.TotalSeconds.ToString() + " " + rm.GetString("sec") + "\n");
					Console.WriteLine(modfile + " " + rm.GetString("ModCount") + " " + (modsize / 1073741824) + " " + rm.GetString("GiBs") + "\n");
					Console.WriteLine(nmod + " "+ rm.GetString("NoModCount") + " " + (nmodsize / 1073741824) + " " + rm.GetString("GiBs") + "\n");

					Console.WriteLine("\n" + rm.GetString("CCode") + "\n"+ rm.GetString("CInfo"));
					Console.ForegroundColor = ConsoleColor.Yellow;
					Console.WriteLine(rm.GetString("CWarn"));
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine(rm.GetString("CErr"));
					Console.ForegroundColor = ConsoleColor.DarkRed;
					Console.WriteLine(rm.GetString("CBad"));
					Console.ForegroundColor = ConsoleColor.DarkGreen;
					Console.WriteLine(rm.GetString("CDupe"));
					Console.ForegroundColor = ConsoleColor.Blue;
					Console.WriteLine(rm.GetString("CArc"));
					Console.ForegroundColor = ConsoleColor.DarkMagenta;
					Console.WriteLine(rm.GetString("CMiss") + "\n");

					Console.ForegroundColor = ConsoleColor.White;


					Console.WriteLine(rm.GetString("Done"));

					Console.WriteLine(rm.GetString("Quit"));
				}

				while (Console.ReadKey().Key != ConsoleKey.Escape)
				{
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(rm.GetString("Exception") + "\n\n" + e.ToString() + "\n\n" + rm.GetString("Quit"));
				while (Console.ReadKey().Key != ConsoleKey.Escape)
				{
				}
			}

		}

		//A janked little function to quickly surmise if a file is a mod file or not.
		public static bool IsMFile(string file)
		{
			HashSet<String> modext = new HashSet<string>();

			modext.Add(".mod");
			modext.Add(".menu");
			modext.Add(".model");
			modext.Add(".tex");
			modext.Add(".mate");
			modext.Add(".pmat");
			modext.Add(".asset_bg");
			modext.Add(".csv");
			modext.Add(".anm");
			modext.Add(".nei");
			modext.Add(".ks");
			modext.Add(".json");
			modext.Add(".phy");
			modext.Add(".psk");
			modext.Add(".col");
			modext.Add(".room");
			modext.Add(".arc");


			foreach (var ext in modext)
			{
				if (file.ToLower().Contains(ext))
				{
					return true;
				}
			}
			return false;
		}
	}
}
