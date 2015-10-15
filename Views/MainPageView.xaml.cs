using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Views.bybus;

namespace bybus
{
	public partial class MainPageView : ContentPage
	{
		public MainPageView ()
		{
			InitializeComponent ();

			BindingContext = new MainPageViewModel ();
		}
	}
}

