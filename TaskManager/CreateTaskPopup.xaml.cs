
using CommunityToolkit.Maui.Core.Views;
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using TaskManager.Databases;

namespace TaskManager;

public partial class CreateTaskPopup : Popup
{
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
			Classes.Task task = new Classes.Task(TaskName, Path.Combine(FileSystem.AppDataDirectory + "/Tasks/" + TaskName + "_database.db"));
			if (!File.Exists(task.DatabasePath))
			{
				File.Create(task.DatabasePath);
				var db = new ItemsDatabase(task.DatabasePath);
			}
			MainPage.Tasks.Add(task);
			await MainPage.Database.AddTask(task);
			MainPage.MainPageInstance!.Tasks_CV.ItemsSource = await MainPage.Database.GetTasks();		
			this.Close();
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
}