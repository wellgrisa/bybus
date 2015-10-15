using System;
using Views.bybus;
using bybus.ViewModels;

namespace bybus.Shared.Visitor
{
	public class NavigateVisitor : IViewModelVisitor
	{
		#region INavigateVisitor implementation

		public void Visit (RouteDetailsViewModel routeDetailsViewModel)
		{
			App.Current.MainPage.Navigation.PushAsync
			(
				new RouteDetailsView 
				{
					BindingContext = routeDetailsViewModel
				}
			);
		}

		public void Visit (MainPageViewModel mainPage)
		{
			App.Current.MainPage.Navigation.PopAsync();
		}
		#endregion

	}
}

