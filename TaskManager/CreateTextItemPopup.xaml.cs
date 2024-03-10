using CommunityToolkit.Maui.Views;

namespace TaskManager;

public partial class CreateTextItemPopup : Popup
{
	public static string CreateTextItemEditor = "";
	public CreateTextItemPopup()
	{
		InitializeComponent();
	}

	private async void CreateButton_Clicked(object sender, EventArgs e)
	{
		string? CreateTextItemEditor = TextItemEditor.Text;
		if (CreateTextItemEditor?.Length > 0)
		{
			if (TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[0].ToString())
			{
				Classes.Item item = new Classes.Item(CreateTextItemEditor) { isTextItem = true, isImageItem = false, Position = TaskPage.ItemsToDo.Count };
				await TaskPage.Database!.AddItem(item);
				TaskPage.ItemsToDo.Add(item);
				this.Close();
			}
			else if(TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[2].ToString())
			{
				Classes.Item item = new Classes.Item(CreateTextItemEditor) { ImageSource = TaskPage.ImageSourceString, isTextItem = true, isImageItem = true, Position = TaskPage.ItemsToDo.Count };
				await TaskPage.Database!.AddItem(item);
				TaskPage.ItemsToDo.Add(item);
				this.Close();
			}			
		}
		else 
		{
			if(Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Пожалуйста, введите текст", "Отменить");
			}
			else if(Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Please enter text.", "Cancel");
			}
		}
	}

	private void CancelButton_Clicked(object sender, EventArgs e)
	{
		this.Close();
	}
}