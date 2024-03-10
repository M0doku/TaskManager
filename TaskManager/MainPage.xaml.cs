using CommunityToolkit.Maui.Layouts;
using CommunityToolkit.Maui.Views;
using LocalizationResourceManager.Maui;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using TaskManager.Classes;
using Microsoft.Maui.ApplicationModel;
using TaskManager.Databases;

namespace TaskManager
{
	public partial class MainPage : ContentPage
	{
		private static Databases.TaskDatabase? database;
		public static ObservableCollection<Classes.Task> Tasks { get; set; } = new ObservableCollection<Classes.Task>();
		private readonly ILocalizationResourceManager resourceManager;
		public static ILocalizationResourceManager resourceManagerInstance;
		public static MainPage MainPageInstance = new(resourceManagerInstance);
		public static string TaskDbPath = "";
		public static string TaskName = "";
		public MainPage(ILocalizationResourceManager resourceManager)
		{
			InitializeComponent();			
			BindingContext = this;
			MainPageInstance = this;
			this.resourceManager = resourceManager;
			resourceManagerInstance = resourceManager;
			if(!Directory.Exists(FileSystem.AppDataDirectory + "/Tasks/"))
			{
				Directory.CreateDirectory(FileSystem.AppDataDirectory + "/Tasks");
			}
			StartLayout();
			DeviceDisplay.MainDisplayInfoChanged += UpdateLayout;
		}

		void StartLayout()
		{
			AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
			if (AppShell.Orientation == "Landscape")
			{
				Classes.LayoutUpdate.MainPageLandscapeLayout();
			}
			else if(AppShell.Orientation == "Portrait")
			{
				Classes.LayoutUpdate.MainPagePortraitLayout();
			}
		}

		void UpdateLayout(object? sender, DisplayInfoChangedEventArgs e)
		{
			AppShell.Orientation = e.DisplayInfo.Orientation.ToString();
			if (AppShell.Orientation == "Landscape")
			{
				Classes.LayoutUpdate.MainPageLandscapeLayout();
			}
			else if (AppShell.Orientation == "Portrait")
			{
				Classes.LayoutUpdate.MainPagePortraitLayout();
			}
		}

		public static Databases.TaskDatabase Database
		{
			get
			{
				if(database == null)
				{
					database = new Databases.TaskDatabase(Path.Combine(FileSystem.AppDataDirectory, "Tasks.db"));
				}
				return database;
			}
		}

		private void CreateTaskButton_Clicked(object sender, EventArgs e)
		{
			this.ShowPopup(new CreateTaskPopup());
		}

		private async void DeleteTaskButton_Clicked(object sender, EventArgs e)
		{
			if(Tasks_CV.SelectedItem != null)
			{
				var task = Tasks_CV.SelectedItem as Classes.Task;
				var database = new Databases.ItemsDatabase(task!.DatabasePath);
				await database.ClearDatabase();		
				Tasks.Remove(task);
				await Database.DeleteTask(task);
				if (File.Exists(task.DatabasePath))
				{
					File.Delete(task.DatabasePath);
				}
				Tasks_CV.SelectedItem = null;
				Tasks_CV.ItemsSource = await Database.GetTasks();
			}		
		}

		private void Tasks_CV_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(Tasks_CV.SelectedItem != null)
			{
				CreateTaskButton.IsVisible = false;
				CreateTaskButton.IsEnabled = false;
				OpenTaskButton.IsVisible = true;
				OpenTaskButton.IsEnabled = true;
				var task = Tasks_CV.SelectedItem as Classes.Task;
			}
			else if(Tasks_CV.SelectedItem == null)
			{
				CreateTaskButton.IsVisible = true;
				CreateTaskButton.IsEnabled = true;
				OpenTaskButton.IsVisible = false;
				OpenTaskButton.IsEnabled = false;
			}
		}

		private async void OpenTaskButton_Clicked(object sender, EventArgs e)
		{
			if(Tasks_CV.SelectedItem != null)
			{
				var task = Tasks_CV.SelectedItem as Classes.Task;
				TaskDbPath = task.DatabasePath;
				TaskName = task.TaskName;
				await Navigation.PushAsync(new TaskPage());

			}
		}

		protected override bool OnBackButtonPressed()
		{
			if(Tasks_CV.SelectedItem != null)
			{
				Tasks_CV.SelectedItem = null;
				return true;
			}
			else { return base.OnBackButtonPressed(); }

		}

		async void ContentPage_Appearing(object sender, EventArgs e)
		{
			base.OnAppearing();			
			Tasks.Clear();
			TaskDbPath = string.Empty;
		    Classes.TasksMethods.GetTasks();
			Tasks_CV.ItemsSource = await Database.GetTasks();
			Tasks_CV.SelectedItem = null;
			AppShell.AppShellInstance.AppShellTitle.Text = AppResources.ShellMainPageLabel;
			TaskPage.ItemsToDo.Clear();
			TaskPage.ItemsDoing.Clear();
			TaskPage.ItemsDone.Clear();
		}
	}
}
