using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Diagnostics;
using System.Windows.Input;
namespace TaskManager
{
	public partial class AppShell : Shell
	{
		public static AppShell AppShellInstance { get; set; } = new();

		public static string Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
		public static string AppDatabasePath = "";
		public AppShell()
		{
			InitializeComponent();
			AppShellInstance = this;
			this.BindingContext = this;
		}

		private void AppShellButton_Clicked(object sender, EventArgs e)
		{
			Debug.WriteLine($"ISVISIBLE: {TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible}");
			if (Orientation == "Landscape")
			{
				Classes.LayoutUpdate.TaskPageLandscapeEditLayout();
			}
			else if(Orientation == "Portrait")
			{
				Classes.LayoutUpdate.TaskPagePortraitEditLayout();
			}
			Debug.WriteLine($"ISVISIBLE: {TaskPage.TaskPageInstance.ChangeLayoutButtonState.IsVisible}");
		}
	}
}
