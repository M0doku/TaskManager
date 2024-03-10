using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Primitives;
using System.Diagnostics;
using System.Reflection;
using IImage = Microsoft.Maui.Graphics.IImage;

namespace TaskManager;

public partial class OpenImagePopup : Popup
{
	public static bool isLoaded = false;
	public OpenImagePopup()
	{
		InitializeComponent();
		ImagePopup.Source = TaskPage.img.Source;
		ImagePopup.SizeChanged += ImagePopup_SizeChanged;
		DeviceDisplay.MainDisplayInfoChanged += UpdateLayout;
	}

	private void UpdateLayout(object? sender, DisplayInfoChangedEventArgs e)
	{
		AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
		if (AppShell.Orientation == "Portrait")
		{
		    ImagePopup.VerticalOptions = LayoutOptions.Fill;
	    }
		else if (AppShell.Orientation == "Landscape")
		{
		    ImagePopup.VerticalOptions = LayoutOptions.FillAndExpand;
		}
	}

	private void ImagePopup_SizeChanged(object? sender, EventArgs e)
	{
		AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
		if(ImagePopup.Height > 0)
		{
			ImagePopup.HorizontalOptions = LayoutOptions.FillAndExpand;
			ImagePopup.VerticalOptions = LayoutOptions.FillAndExpand;
		}	
	}

	public void PopupPage_Closed(object sender, CommunityToolkit.Maui.Core.PopupClosedEventArgs e)
	{
		DeviceDisplay.MainDisplayInfoChanged -= UpdateLayout;
	}
}