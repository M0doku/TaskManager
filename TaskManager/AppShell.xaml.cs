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
		public bool EditIsOn = false;
		public AppShell()
		{
			InitializeComponent();
			AppShellInstance = this;
			this.BindingContext = this;
		}

		private void AppShellButton_Clicked(object sender, EventArgs e)
		{
			if(EditIsOn == true)
			{
				EditIsOn = false;
			}
			else { EditIsOn = true; }
			if(Preferences.Get("QuickMove", false) == true)
			{
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsToDo_CV.SelectedItem = null;
				TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemsDone_CV.SelectedItem = null;
			}
			if (Orientation == "Landscape")
			{
				Classes.LayoutUpdate.TaskPageLandscapeEditLayout();
				Classes.LayoutUpdate.TaskPageTwoColumnsLandscapeEditLayout();
			}
			else if(Orientation == "Portrait")
			{
				Classes.LayoutUpdate.TaskPagePortraitEditLayout();
				Classes.LayoutUpdate.TaskPageTwoColumnsPortraitEditLayout();
			}
		}
	}
}
