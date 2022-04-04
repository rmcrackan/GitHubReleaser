using System;
using System.IO;
using Newtonsoft.Json.Linq;

namespace GitHubReleaser
{
	public class SECRETS
	{
		public static string VS2022ProjectsDirectory { get; }
		public static string GithubApiToken { get; }

		static SECRETS()
		{
			//
			// local __SECRETS folder **MUST** be stored where it can never end up in git.
			// descend recursively until we find it
			//

			const string __SECRETS = "__SECRETS";

			var dirInfo = new DirectoryInfo(AppContext.BaseDirectory);
			while (true)
			{
				if (Directory.Exists(Path.Combine(dirInfo.FullName, __SECRETS)))
					break;
				dirInfo = dirInfo.Parent;
			}

			var json = File.ReadAllText(Path.Combine(dirInfo.FullName, __SECRETS, "secrets.json"));
			VS2022ProjectsDirectory = JObject.Parse(json)["vs_2022_projects_directory"].Value<string>();
			GithubApiToken = JObject.Parse(json)["github_api_token"].Value<string>();
		}
	}
}
