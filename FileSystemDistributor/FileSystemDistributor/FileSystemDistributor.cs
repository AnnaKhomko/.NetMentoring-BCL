using FileSystemDistributor.Configuration;
using FileSystemDistributor.Utils.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using Strings = FileSystemDistributor.Resources.Strings;
using System.Text;
using System.Globalization;
using System.Threading;
using System.Text.RegularExpressions;
using FileSystemDistributor.Models;
using IoCContainer.Attributes;

namespace FileSystemDistributor
{
	/// <summary>
	/// Class contains logic
	/// </summary>
	public class FileSystemDistributor
    {
        private List<RuleModel> rules;
        private DirectoryInfo defaultDir;
        private ILogger log;
        private const int FileCheckTimoutMiliseconds = 2000;

        public FileSystemDistributor(List<RuleModel> rules, DirectoryInfo defaultDir, ILogger log)
        {
            this.rules = rules;
            this.defaultDir = defaultDir;
            this.log = log;
        }

		/// <summary>
		/// Moves the file according rules provided in config.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="sourcePath">The source path.</param>
		public void MoveFile(string name, string sourcePath)
        {
            int matchCount = 0;
            foreach (var rule in rules)
            {
                var template = new Regex(rule.FileNameTemplate);
                var isMatch = template.IsMatch(name);
                if (isMatch)
                {
                    matchCount++;
                    log.Log(String.Format(Strings.RuleFound, name));
                    string destinationPath = CreateDestinationPath(rule, name, matchCount);
                    Move(sourcePath, destinationPath);
                    log.Log(String.Format(Strings.FileMoved, name, destinationPath));
                    return;
                }
            }

            string defaultDestinationPath = Path.Combine(defaultDir.FullName, name);
            log.Log(String.Format(Strings.RuleNotFound, name));
            Move(sourcePath, defaultDestinationPath);
            log.Log(String.Format(Strings.FileMoved, name, defaultDestinationPath));
        }

		/// <summary>
		/// Moves the specified file from source path to destination path.
		/// </summary>
		/// <param name="sourcePath">The source path.</param>
		/// <param name="destinationPath">The destination path.</param>
		private void Move(string sourcePath, string destinationPath)
        {
            // Create directory if it is not exists yet
            string dir = Path.GetDirectoryName(destinationPath);

            Directory.CreateDirectory(dir);

            var ableToAccess = false;

            log.Log(String.Format(Strings.FileMoveStart, sourcePath, destinationPath));

            do
            {
                try
                {
                    if (File.Exists(destinationPath))
                    {
                        log.Log(String.Format(Strings.FileAlreadyExists, destinationPath));
                        File.Delete(destinationPath);
                        log.Log(String.Format(Strings.FileDeleted, destinationPath));
                    }
                    File.Move(sourcePath, destinationPath);
                    ableToAccess = true;
                }
                catch (FileNotFoundException)
                {
                    log.Log(String.Format(Strings.FileNotFound, sourcePath));
                    break;
                }
                catch (IOException)
                {
                    Thread.Sleep(FileCheckTimoutMiliseconds);
                }
            } while (!ableToAccess);
        }

		/// <summary>
		/// Creates the destination path.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <param name="name">The name.</param>
		/// <param name="matchCount">The match count.</param>
		/// <returns></returns>
		private string CreateDestinationPath(RuleModel rule, string name, int matchCount)
        {
            string ext = Path.GetExtension(name);
            string fileName = Path.GetFileNameWithoutExtension(name);
            var result = new StringBuilder().Append(Path.Combine(rule.DestinationDirectoryPath, fileName));

            if (rule.IsDateRequired)
            {
                var format = CultureInfo.CurrentCulture.DateTimeFormat;
                format.DateSeparator = ".";
                result.Append($"_{DateTime.Now.ToLocalTime().ToString(format.ShortDatePattern)}");
            }

            if (rule.IsOrderRequired)
            {
                result.Append($"_{matchCount}");
            }

            result.Append(ext);

            return result.ToString();
        }
    }
}
