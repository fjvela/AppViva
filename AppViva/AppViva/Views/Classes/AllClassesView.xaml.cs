﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AppViva.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AllClassesView : BaseCarouselPage
    {
		public AllClassesView ()
		{
			InitializeComponent();
		}
	}
}