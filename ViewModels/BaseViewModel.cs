using System;

using Xamarin.Forms;

namespace bybus
{
	public abstract class BaseViewModel
	{
		public abstract void Accept(IViewModelVisitor viewModelVisitor);
	}
}


