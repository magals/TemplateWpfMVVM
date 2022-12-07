using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using TemplateWpfMVVM.Models;
using TemplateWpfMVVM.ViewModels;

namespace TemplateWpfMVVM
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : Application
	{
		public static bool IsDesignMode { get; private set; } = true;
		public static string CurrentDirectory => IsDesignMode ? Path.GetDirectoryName(GetSourceCodePath()) : Environment.CurrentDirectory;

		public static IHost Host => Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();
		public static IConfiguration Configuration { get; set; }
		protected override async void OnStartup(StartupEventArgs e)
		{
			Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
			var builder = new ConfigurationBuilder()
																					.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
			Configuration = builder.Build();
			IsDesignMode = false;
			base.OnStartup(e);
			await Host.StartAsync().ConfigureAwait(false);
		}

		protected override async void OnExit(ExitEventArgs e)
		{
			base.OnExit(e);
			await Host.StopAsync().ConfigureAwait(false);
			Host.Dispose();
		}

		public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
		{
			services.Configure<AppConfig>(Configuration?.GetSection("AppConfig"));
			services.AddSingleton(new JsonSerializerOptions
			{
				WriteIndented = true
			});
			services.AddSingleton<MainViewModel>();
			services.AddSingleton<ShowImageViewModel>();
		}

		private static string GetSourceCodePath([CallerFilePath] string Path = null)
		{
			if (string.IsNullOrWhiteSpace(Path))
				throw new ArgumentNullException(nameof(Path));
			return Path;
		}
	}
}
