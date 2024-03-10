
using CommunityToolkit.Maui.Views;
using System.Diagnostics;
using TaskManager.Classes;

namespace TaskManager;

public partial class EditTextPopup : Popup
{
	Classes.Item EditTextItem = ItemMethods.DefineItem();
	public EditTextPopup()
	{
		InitializeComponent();		
	}

	private void CancelButton_Clicked(object sender, EventArgs e)
	{
		this.Close();
	}

	private async void EditButton_Clicked(object sender, EventArgs e)
	{
		string EditTextItemEditor = TextItemEditor.Text;
		if(EditTextItemEditor.Length > 0)
		{
			Debug.WriteLine(EditTextItemEditor);
			EditTextItem.Text = EditTextItemEditor;
			await TaskPage.Database!.UpdateItem(EditTextItem);
			this.Close();
		}
		else
		{
			if (Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Пожалуйста, введите текст", "Отменить");
			}
			else if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
			{
				await AppShell.Current.DisplayAlert("Task Manager", "Please enter text.", "Cancel");
			}
		}
	}

	private void EditTextPopup_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
	{
		TextItemEditor.Text = EditTextItem.Text;
	}
}