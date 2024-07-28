
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using TaskManager.Databases;

namespace TaskManager;

public partial class CreateTaskPopup : Popup
{
	private bool TwoColumnType = false;
	private bool ThreeColumnType = false;
	public CreateTaskPopup()
	{
		InitializeComponent();
		
	}

	async void CreateTaskButton_Clicked(object sender, EventArgs e)
	{
		string? TaskName = TaskEditor.Text;
		List<Classes.Task> tasks = await MainPage.Database.GetTasks();
		List<string> tasknames = new List<string>();
		foreach(Classes.Task task in tasks)
		{
			tasknames.Add(task.TaskName);
		}		
		if(tasknames.Contains(TaskName))
		{
			if(Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Задача с таким именем уже создана.", "Отменить");
			}
			else if(Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Task with this name is already created.", "Cancel");
			}
		}
		else if (TaskName?.Count() > 0)
		{
			Classes.Task task = new Classes.Task();
			if(TwoColumnType == true && ThreeColumnType == false)
			{
				task = new Classes.Task(TaskName, Path.Combine(FileSystem.AppDataDirectory + "/Tasks/" + TaskName + "_database.db"), "2C");
			}
			else if(TwoColumnType == false && ThreeColumnType == true)
			{
				task = new Classes.Task(TaskName, Path.Combine(FileSystem.AppDataDirectory + "/Tasks/" + TaskName + "_database.db"), "3C");
			}
			else if(TwoColumnType == false && ThreeColumnType == false)
			{
				if (Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
				{
					await AppShell.Current.DisplayAlert("Task Manager", "Выберите тип задачи", "Отменить");
				}
				else if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
				{
					await AppShell.Current.DisplayAlert("Task Manager", "Choose type of task", "Cancel");
				}
			}
			if(task.DatabasePath.Count() > 0)
			{
				if (!File.Exists(task.DatabasePath))
				{
					File.Create(task.DatabasePath);
					var db = new ItemsDatabase(task.DatabasePath);
					MainPage.Tasks.Add(task);
					await MainPage.Database.AddTask(task);
					MainPage.MainPageInstance!.Tasks_CV.ItemsSource = await MainPage.Database.GetTasks();
					this.Close();
				}
			}

		}
		else
		{
			if(Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Пожалуйста введите имя задачи.", "Отменить");
			}
			else if(Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Please enter task name.", "Cancel");
			}
		}		
	}

	private void CancelButton_Clicked(object sender, EventArgs e)
	{
		this.Close();
	}


	private void TwoColumnsButton_Clicked(object sender, EventArgs e)
	{
		if(TwoColumnType == false)
		{
			TwoColumnType = true;
			TwoColumnsButton.BackgroundColor = Colors.Lavender;
		}
		else if(TwoColumnType == true)
		{
			TwoColumnType = false;
			TwoColumnsButton.BackgroundColor = Colors.Transparent;
		}
		if(ThreeColumnType == true)
		{
			ThreeColumnType = false;
			ThreeColumnsButton.BackgroundColor = Colors.Transparent;
		}

	}

	private void ThreeColumnsButton_Clicked(object sender, EventArgs e)
	{
		if (ThreeColumnType == false)
		{
			ThreeColumnType = true;
			ThreeColumnsButton.BackgroundColor = Colors.Lavender;
		}
		else if (ThreeColumnType == true)
		{
			ThreeColumnsButton.BackgroundColor = Colors.Transparent;
		}
		if (TwoColumnType == true)
		{
			TwoColumnType = false;
			TwoColumnsButton.BackgroundColor = Colors.Transparent;
		}
	}
}