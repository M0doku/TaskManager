using CommunityToolkit.Maui.Views;
using System.Runtime.InteropServices.Marshalling;

namespace TaskManager;

public partial class CreateTextItemPopup : Popup
{
	public static string CreateTextItemEditor = "";
	public bool QuickAdding = false;
	public int QuickAddingSymbolCount = 0;
	int SymbolStartCount = 0;
	int SymbolBetweenStartCount = 0;
	int SymbolBetweenEndCount = 0;
	int SymbolEndCount = 0;
	char SymbolBetween = ',';
	public CreateTextItemPopup()
	{
		InitializeComponent();
	}

	private async void CreateButton_Clicked(object sender, EventArgs e)
	{
		string? CreateTextItemEditor = TextItemEditor.Text;
		if (CreateTextItemEditor?.Length > 0)
		{
			if (QuickAdding == true && MainPage.TaskType == "2C")
			{
				AddMultiplePositions(CreateTextItemEditor);
				this.Close();
			}
			else if (QuickAdding == true && MainPage.TaskType == "3C" && await GetSymbolEntriesCount() > 0)
			{
				AddMultiplePositions(CreateTextItemEditor);
				this.Close();
			}
			else if (MainPage.TaskType == "2C")
			{
				if (TaskPageTwoColumns.SelectedItemType == TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemPicker.Items[0].ToString())
				{
					Classes.Item item = new Classes.Item(CreateTextItemEditor) { isTextItem = true, isImageItem = false, Position = TaskPageTwoColumns.ItemsToDo.Count };
					await TaskPageTwoColumns.Database!.AddItem(item);
					TaskPageTwoColumns.ItemsToDo.Add(item);
					this.Close();
				}
				else if (TaskPageTwoColumns.SelectedItemType == TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemPicker.Items[2].ToString())
				{
					Classes.Item item = new Classes.Item(CreateTextItemEditor) { ImageSource = TaskPageTwoColumns.ImageSourceString, isTextItem = true, isImageItem = true, Position = TaskPageTwoColumns.ItemsToDo.Count };
					await TaskPageTwoColumns.Database!.AddItem(item);
					TaskPageTwoColumns.ItemsToDo.Add(item);
					this.Close();
				}
			}
			else if (MainPage.TaskType == "3C")
			{
				if (TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[0].ToString())
				{
					Classes.Item item = new Classes.Item(CreateTextItemEditor) { isTextItem = true, isImageItem = false, Position = TaskPage.ItemsToDo.Count };
					await TaskPage.Database!.AddItem(item);
					TaskPage.ItemsToDo.Add(item);
					this.Close();
				}
				else if (TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[2].ToString())
				{
					Classes.Item item = new Classes.Item(CreateTextItemEditor) { ImageSource = TaskPage.ImageSourceString, isTextItem = true, isImageItem = true, Position = TaskPage.ItemsToDo.Count };
					await TaskPage.Database!.AddItem(item);
					TaskPage.ItemsToDo.Add(item);
					this.Close();
				}
			}

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

	async Task<int> GetSymbolEntriesCount()
	{
		int count = 0;
		await Task.Run(() =>
		{
			count = SymbolBetweenStartCountEntry.Text.Count() + SymbolStartCountEntry.Text.Count() + SymbolEndCountEntry.Text.Count() + SymbolEntry.Text.Count();
		});
		return count;
	}

	void GetSymbolEntriesValues(out int startcount, out int betweenstartcount, out int endcount, out int betweenendcount, out char symbol)
	{
		if (int.TryParse(SymbolStartCountEntry.Text, out int d))
			startcount = d;
		else
			startcount = 0;
		if (int.TryParse(SymbolBetweenStartCountEntry.Text, out int a))
			betweenstartcount = a;
		else
			betweenstartcount = 0;
		if (int.TryParse(SymbolEndCountEntry.Text, out int b))
			endcount = b;
		else
			endcount = 0;
		if (int.TryParse(SymbolBetweenEndCountEntry.Text, out int c))
			betweenendcount = c;
		else
			betweenendcount = 0;
		if (string.IsNullOrEmpty(SymbolEntry.Text))
			symbol = ',';
		else
			symbol = SymbolEntry.Text.First();	
	}

	private async void AddMultiplePositions(string positions)
	{
		int newlines = 0;
		GetSymbolEntriesValues(out SymbolStartCount, out SymbolBetweenStartCount, out SymbolEndCount, out SymbolBetweenEndCount, out SymbolBetween);
		string pos = "";
		if(TaskTypePicker.SelectedIndex == 0)
		{
			if(positions.Length >= SymbolStartCount)
			{
				positions = positions.Substring(SymbolStartCount);
				pos = positions;
			}
			else
			{
				positions = "";
			}
			for (int i = 0; i < positions.Length; i++)
			{
				if (positions[i] == '\n')
				{
					newlines++;
				}
			}
			if (newlines > 0)
			{
				for (int d = 0; d < newlines + 1; d++)
				{
					int index = positions.IndexOf('\n');
					int index2 = positions.IndexOf(' ');
					if (index > 0)
					{
						if(d == 0)
						{
							pos = positions.Substring(0, index - SymbolBetweenEndCount);
							positions = positions.Substring(index + 1);
						}
						else
						{
							pos = positions.Substring(SymbolBetweenStartCount, index - SymbolBetweenEndCount - SymbolBetweenStartCount);
							positions = positions.Substring(index + 1);
						}
					}
					else
					{
						pos = positions.Substring(SymbolBetweenStartCount);
						int count = pos.Count();
						pos = pos.Substring(0, count - SymbolEndCount);
					}
					if (MainPage.TaskType == "2C")
					{
						await AddItemTwoColumns(pos);
					}
					else if (MainPage.TaskType == "3C")
					{
						await AddItem(pos);
					}
				}
			}
			else
			{
				pos = positions.Substring(SymbolBetweenStartCount);
				int count = pos.Count();
				pos = pos.Substring(0, count - SymbolEndCount);
				if (MainPage.TaskType == "2C")
				{
					await AddItemTwoColumns(pos);
				}
				else if (MainPage.TaskType == "3C")
				{
					await AddItem(pos);
				}
			}
		}
		else if(TaskTypePicker.SelectedIndex == 1)
		{
			int count = 0;
			for(int i = 0; i < positions.Length; i++)
			{
				if (positions[i] == SymbolBetween)
				{
					count++;
				}
			}
			if (positions.Length >= SymbolStartCount)
			{
				positions = positions.Substring(SymbolStartCount);
				pos = positions;
			}
			else
			{
				positions = "";
			}
			if(count > 0)
			{
				for (int d = 0; d < count + 1; d++)
				{
					int index = positions.IndexOf(SymbolBetween);
					int spaceindex = positions.IndexOf(' ');
					int spacecount = 0;
					if (spaceindex > index)
						spaceindex = index;
					if (index > 0)
					{
						string temp = positions.Substring(index + 1);
						for(int a = 0; a < temp.Length; a++)
						{
							if (temp[a] == ' ')
								spacecount++;
							else
								break;
						}
						if (d == 0)
						{
							pos = positions.Substring(0, spaceindex);
							positions = positions.Substring(index + spacecount + 1);
						}
						else
						{
							pos = positions.Substring(0, spaceindex);
							positions = positions.Substring(index + spacecount + 1);
						}

					}
					else
					{
						pos = positions.Substring(0);
						int countd = pos.Count();
						pos = pos.Substring(0, countd - SymbolEndCount);
					}
					if (MainPage.TaskType == "2C")
					{
						await AddItemTwoColumns(pos);
					}
					else if (MainPage.TaskType == "3C")
					{
						await AddItem(pos);
					}
				}
			}
			else
			{
				pos = positions.Substring(0);
				int countd = pos.Count();
				pos = pos.Substring(0, countd - SymbolEndCount);
				if (MainPage.TaskType == "2C")
				{
					await AddItemTwoColumns(pos);
				}
				else if (MainPage.TaskType == "3C")
				{
					await AddItem(pos);
				}
			}
		}
	}

	private async Task AddItemTwoColumns(string pos)
	{
		if(TaskPageTwoColumns.SelectedItemType == TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemPicker.Items[0].ToString())
		{
			Classes.Item item = new Classes.Item(pos) { isTextItem = true, isImageItem = false, Position = TaskPageTwoColumns.ItemsToDo.Count };
			await TaskPageTwoColumns.Database!.AddItem(item);
			TaskPageTwoColumns.ItemsToDo.Add(item);
		}
		else if(TaskPageTwoColumns.SelectedItemType == TaskPageTwoColumns.TaskPageTwoColumnsInstance.ItemPicker.Items[2].ToString())
		{
			Classes.Item item = new Classes.Item(pos) { ImageSource = TaskPageTwoColumns.ImageSourceString, isTextItem = true, isImageItem = true, Position = TaskPageTwoColumns.ItemsToDo.Count };
			await TaskPageTwoColumns.Database!.AddItem(item);
			TaskPageTwoColumns.ItemsToDo.Add(item);
		}
	}
	private async Task AddItem(string pos)
	{
		if(TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[0].ToString())
		{
			Classes.Item item = new Classes.Item(pos) { isTextItem = true, isImageItem = false, Position = TaskPage.ItemsToDo.Count };
			await TaskPage.Database!.AddItem(item);
			TaskPage.ItemsToDo.Add(item);
		}
		else if(TaskPage.SelectedItemType == TaskPage.TaskPageInstance.ItemPicker.Items[2].ToString())
		{
			Classes.Item item = new Classes.Item(pos) { ImageSource = TaskPage.ImageSourceString, isTextItem = true, isImageItem = true, Position = TaskPage.ItemsToDo.Count };
			await TaskPage.Database!.AddItem(item);
			TaskPage.ItemsToDo.Add(item);
		}
	}
	private void CancelButton_Clicked(object sender, EventArgs e)
	{
		this.Close();
	}

	private void QuickAddingCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if(e.Value == true)
		{
			QuickAdding = true;
		}
		else if(e.Value == false)
		{
			QuickAdding = false;
		}
	}

	private void Popup_Opened(object sender, CommunityToolkit.Maui.Core.PopupOpenedEventArgs e)
	{
		if (Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "Русский")
		{
			TaskTypePicker.ItemsSource = new List<string> { "Список", "Перечисление" };
			TaskTypePicker.SelectedItem = "Список";
		}
		else if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
		{
			TaskTypePicker.ItemsSource = new List<string> { "List", "Enumeration" };
			TaskTypePicker.SelectedItem = "List";
		}
		SymbolEntry.IsEnabled = false;
	}

	private void TaskTypePicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if(TaskTypePicker.SelectedIndex == 1)
		{
			SymbolEntry.IsEnabled = true;
			SymbolBetweenStartCountEntry.IsEnabled = false;
			SymbolBetweenEndCountEntry.IsEnabled = false;
		}
		else
		{
			SymbolEntry.IsEnabled = false;
			SymbolBetweenStartCountEntry.IsEnabled = true;
			SymbolBetweenEndCountEntry.IsEnabled = true;
			
		}
	}
}