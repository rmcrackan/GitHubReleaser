using System;
using System.Windows.Forms;

namespace GitHubReleaser
{
	static class Program
	{
		[STAThread]
		static void Main()
		{
			ApplicationConfiguration.Initialize();
			Application.Run(new Form1());
		}
	}
}
