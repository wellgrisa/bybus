using System;
using System.Collections.Generic;

using Xamarin.Forms;
using bybus.ViewModels;

namespace bybus
{
	public partial class RouteDetailsView : TabbedPage
	{
		public RouteDetailsView ()
		{
			InitializeComponent ();
		}

		public async void OnBackClicked(object sender, EventArgs e){
			await Navigation.PopAsync();
		}
	}
}

