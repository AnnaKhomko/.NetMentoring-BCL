using System;
using System.Collections.Generic;
using FileSystemDistributorSection = FileSystemDistributor.Configuration.FileSystemDistributorSection;
using RuleElement = FileSystemDistributor.Configuration.RuleElement;
using DirectoryElement = FileSystemDistributor.Configuration.DirectoryElement;
using Strings = FileSystemDistributor.Resources.Strings;
using System.Configuration;
using FileSystemDistributor.Utils.Interfaces;
using FileSystemDistributor.Utils;
using FileSystemDistributor.EventArgs;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using FileSystemDistributor.Models;
using System.Linq;

namespace FileSystemDistributor
{
	class FileSystemDistributorProgram
	{
		private static FileSystemDistributor distributor;
		private static bool isclosing = false;
		static void Main(string[] args)
		{
			try
			{
				ConfigModel config = ReadConfiguration();

				SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

				var log = new Logger();
				log.Log(Strings.CurrentCulture);

				distributor = new FileSystemDistributor(config.Rules, new DirectoryInfo(config.DefaulFolder), log);
				InitWather(config, log);

				log.Log(Strings.ExitMessage);

				while (!isclosing) ;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		/// <summary>
		/// Initializes the wather.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <param name="log">The log.</param>
		private static void InitWather(ConfigModel config, Logger log)
		{
			var watcher = new FileSystemWatcherService(log);

			var directories = config.Directories.ToList().Select(_ => _.DirectoryPath).ToList();

			var context = new FileSystemWatcherConfigurationModel
			{
				Directories = directories,
				EnableRaisingEvents = true,
				IncludeSubdirectories = true,
				NotifyFilter = NotifyFilters.FileName
			};

			watcher.Init(context);
			watcher.OnFileCreated += OnFileCreated;
		}

		/// <summary>
		/// Validates the configuration.
		/// </summary>
		/// <returns></returns>
		/// <exception cref="Exception"></exception>
		private static ConfigModel ReadConfiguration()
		{
			var config = ConfigurationManager.GetSection("fileSystemDistributorSection") as FileSystemDistributorSection;

			if (config == null)
			{
				throw new Exception(Strings.ConfigNotFound);
			}

			var configModel = ReadConfig(config);

			return configModel;
		}

		/// <summary>
		/// Called when [file created].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="args">The <see cref="FileCreatedEventArgs"/> instance containing the event data.</param>
		private static void OnFileCreated(object sender, FileCreatedEventArgs args)
		{
			distributor.MoveFile(args.Name, args.FilePath);
		}

		/// <summary>
		/// Reads the configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <returns></returns>
		private static ConfigModel ReadConfig(FileSystemDistributorSection config)
		{
			CultureInfo.DefaultThreadCurrentCulture = config.Culture;
			CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
			CultureInfo.CurrentUICulture = config.Culture;
			CultureInfo.CurrentCulture = config.Culture;

			var configModel = new ConfigModel();

			foreach (DirectoryElement dir in config.Directories)
			{
				configModel.Directories.Add(dir);
			}

			foreach (RuleElement rule in config.Rules)
			{
				configModel.Rules.Add(rule);
			}

			configModel.DefaulFolder = config.Rules.DefaultFolder;

			return configModel;
		}

		/// <summary>
		/// Consoles the control check.
		/// </summary>
		/// <param name="ctrlType">Type of the control.</param>
		/// <returns></returns>
		private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
		{
			switch (ctrlType)
			{
				case CtrlTypes.CTRL_C_EVENT:
					isclosing = true;
					Console.WriteLine("CTRL+C received!");
					break;
				case CtrlTypes.CTRL_BREAK_EVENT:
					isclosing = true;
					Console.WriteLine("CTRL+BREAK received!");
					break;
			}
			return true;
		}

		#region unmanaged

		// Declare the SetConsoleCtrlHandler function
		// as external and receiving a delegate.
		[DllImport("Kernel32")]
		public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);

		// A delegate type to be used as the handler routine
		// for SetConsoleCtrlHandler.
		public delegate bool HandlerRoutine(CtrlTypes CtrlType);

		// An enumerated type for the control messages
		// sent to the handler routine.
		public enum CtrlTypes
		{
			CTRL_C_EVENT = 0,
			CTRL_BREAK_EVENT
		}

		#endregion
	}
}
