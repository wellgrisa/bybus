using System;
using Views.bybus;
using bybus.ViewModels;

namespace bybus
{
	public interface IViewModelVisitor
	{
		void Visit (MainPageViewModel mainPage);
		void Visit (RouteDetailsViewModel routeDetailsViewModel);
	}
}

