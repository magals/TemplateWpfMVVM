using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWpfMVVM.ViewModels.Base;

namespace TemplateWpfMVVM.ViewModels
{
	internal class MainViewModel : ViewModel
	{
		private readonly ILogger<MainViewModel> logger;

		public MainViewModel(ILogger<MainViewModel> logger,
												 ShowImageViewModel showImageViewModel)
		{
			this.logger = logger;
			ShowImageViewModel = showImageViewModel;
		}

		public ShowImageViewModel ShowImageViewModel { get; }
	}
}
