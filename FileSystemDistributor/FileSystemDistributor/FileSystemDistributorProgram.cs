using System;
using FileSystemDistributorSection = FileSystemDistributor.Configuration.FileSystemDistributorSection;
using RuleElement = FileSystemDistributor.Configuration.RuleElement;
using DirectoryElement = FileSystemDistributor.Configuration.DirectoryElement;
using Strings = FileSystemDistributor.Resources.Strings;
using System.Configuration;
using FileSystemDistributor.Utils;
using FileSystemDistributor.EventArgs;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using FileSystemDistributor.Models;
using System.Linq;
using IoCContainer;
using System.Reflection;
using FileSystemDistributor.Utils.Interfaces;

namespace FileSystemDistributor
{
	class FileSystemDistributorProgram
	{
		private static FileSystemDistributor distributor;
		private static bool isclosing = false;
		private static Container container;

		static void Main(string[] args)
		{
			try
			{
				RegisterTypes();

				ConfigModel config = ReadConfiguration();

				SetConsoleCtrlHandler(new HandlerRoutine(ConsoleCtrlCheck), true);

				var log = container.CreateInstance<ILogger>();
				log.Log(Strings.CurrentCulture);

				distributor = new FileSystemDistributor(config.Rules, new DirectoryInfo(config.DefaulFolder), log);
				InitWatcher(config, log);

				log.Log(Strings.ExitMessage);

				while (!isclosing);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex);
			}
		}

		/// <summary>
		/// Initializes the watcher.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <param name="log">The log.</param>
		private static void InitWatcher(ConfigModel config, ILogger log)
		{
			var watcher = container.CreateInstance<FileSystemWatcherService>();

			var directories = config.Directories.ToList().Select(_ => _.Path).ToList();

			var context = container.CreateInstance<FileSystemWatcherConfigurationModel>();

			{
				context.Directories = directories;
				context.EnableRaisingEvents = true;
				context.IncludeSubdirectories = true;
				context.NotifyFilter = NotifyFilters.FileName;
			}

			watcher.Init(context);
			watcher.OnFileCreated += OnFileCreated;
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

		#region Read config data 

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
		/// Reads the configuration.
		/// </summary>
		/// <param name="config">The configuration.</param>
		/// <returns>Configuration model object</returns>
		private static ConfigModel ReadConfig(FileSystemDistributorSection config)
		{
			CultureInfo.DefaultThreadCurrentCulture = config.Culture;
			CultureInfo.DefaultThreadCurrentUICulture = config.Culture;
			CultureInfo.CurrentUICulture = config.Culture;
			CultureInfo.CurrentCulture = config.Culture;

			var configModel = container.CreateInstance<ConfigModel>();

			foreach (DirectoryElement dir in config.Directories)
			{
				configModel.Directories.Add(ReadDirectoryConfigData(dir));
			}

			foreach (RuleElement rule in config.Rules)
			{
				configModel.Rules.Add(ReadRuleConfigData(rule));
			}

			configModel.DefaulFolder = config.Rules.DefaultFolder;

			return configModel;
		}

		/// <summary>
		/// Reads the directory configuration data.
		/// </summary>
		/// <param name="directory">The directory.</param>
		/// <returns>The directory model oject.</returns>
		private static DirectoryModel ReadDirectoryConfigData(DirectoryElement directory)
		{
			var directoryModel = container.CreateInstance<DirectoryModel>();

			directoryModel.Path = directory.DirectoryPath;
			return directoryModel;
		}

		/// <summary>
		/// Reads the rule configuration data.
		/// </summary>
		/// <param name="rule">The rule.</param>
		/// <returns>Tre rule model object.</returns>
		private static RuleModel ReadRuleConfigData(RuleElement rule)
		{
			var ruleModel = container.CreateInstance<RuleModel>();

			ruleModel.DestinationDirectoryPath = rule.DestinationDirectoryPath;
			ruleModel.FileNameTemplate = rule.FileNameTemplate;
			ruleModel.IsDateRequired = rule.IsDateRequired;
			ruleModel.IsOrderRequired = rule.IsOrderRequired;

			return ruleModel;
		}

		#endregion

		#region ConsoleCtrlHandler

		
		[DllImport("Kernel32")]
		public static extern bool SetConsoleCtrlHandler(HandlerRoutine Handler, bool Add);
	
		/// <summary>
		/// A delegate type to be used as the handler routine for SetConsoleCtrlHandler
		/// </summary>
		/// <param name="CtrlType">Type of the control.</param>
		public delegate bool HandlerRoutine(CtrlTypes CtrlType);

		/// <summary>
		/// An enumerated type for the control messages sent to the handler routine.	
		/// </summary>
		public enum CtrlTypes
		{
			CTRL_C_EVENT = 0,
			CTRL_BREAK_EVENT
		}

		/// <summary>
		/// Consoles the control check.
		/// </summary>
		/// <param name="ctrlType">Type of the control.</param>
		/// <returns>Returns true if ctrl type recieved otherwise false</returns>
		private static bool ConsoleCtrlCheck(CtrlTypes ctrlType)
		{
			switch (ctrlType)
			{
				case CtrlTypes.CTRL_C_EVENT:
					isclosing = true;
					Console.WriteLine(Strings.CtrlCType);
					break;
				case CtrlTypes.CTRL_BREAK_EVENT:
					isclosing = true;
					Console.WriteLine(Strings.CtrlBreakType);
					break;
			}
			return true;
		}

		#endregion

		private static void RegisterTypes()
		{
			container = new Container();

			container.AddType(typeof(ConfigModel));
			container.AddType(typeof(DirectoryModel));
			container.AddType(typeof(RuleModel));
			container.AddType(typeof(ILogger), typeof(Logger));
			container.AddType(typeof(FileSystemDistributor));
			container.AddType(typeof(FileSystemWatcherConfigurationModel));
			container.AddType(typeof(FileSystemWatcherService));
		}
	}
}
