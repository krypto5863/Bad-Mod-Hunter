using CM3D2.Toolkit.Arc;
using CM3D2.Toolkit.Arc.Entry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bad_Mod_Hunter
{
	class Arc
	{
		public static List<string> GetFiles(string arc)
		{

			var files = new List<String>();

			var afs = new ArcFileSystem();

			afs.LoadArc(arc);

			foreach (ArcFileEntry f in afs.Files)
			{
				files.Add(f.Name.ToLower());
			}

			afs.Clear();

			return files;
		}
	}
}
