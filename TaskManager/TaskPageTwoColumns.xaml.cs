using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Layouts;
using CommunityToolkit.Maui.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TaskManager.Classes;
using TaskManager.Databases;

namespace TaskManager
{
	public partial class TaskPageTwoColumns : ContentPage
	{
		public static ObservableCollection<Classes.Item> ItemsToDo { get; set; } = new ObservableCollection<Classes.Item>();
		public static ObservableCollection<Classes.Item> ItemsDone { get; set; } = new ObservableCollection<Classes.Item>();
		private static Databases.ItemsDatabase? database;
		public static string ImageSourceString = "";
		public static string SelectedItemCollection { get; set; } = "";
		public static string? SelectedItemType { get; set; } = "Text";
		public static TaskPageTwoColumns TaskPageTwoColumnsInstance { get; set; } = new();
		public static Image img = new Image();
		public ButtonState ChangeLayoutButtonState = new ButtonState() { IsVisible = false };
		public bool QuickMoveIsOn = Preferences.Get("QuickMove", false);
		public TaskPageTwoColumns()
		{
			InitializeComponent();
			BindingContext = this;
			TaskPageTwoColumnsInstance = this;
			StartLayout();
			PickerLanguageSelector();
			SetButtonsBindings();
			DeviceDisplay.MainDisplayInfoChanged += UpdateLayout;
			Debug.WriteLine($"{Database?.GetDatabasePath()}");
		}
		void PickerLanguageSelector()
		{
			if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "�������")
			{
				ItemPicker.ItemsSource = new List<string>() { "�����", "�����������", "����������� + �����" };
				ItemPicker.SelectedItem = "�����";
			}
			else if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
			{
				ItemPicker.ItemsSource = new List<string>() { "Text", "Image", "Image+Text" };
				ItemPicker.SelectedItem = "Text";
			}
		}
		void StartLayout()
		{
			AppShell.Orientation = DeviceDisplay.MainDisplayInfo.Orientation.ToString();
			if (AppShell.Orientation == "Landscape")
			{
				Classes.LayoutUpdate.TaskPageTwoColumnsLandscapeLayout();
			}
			else if (AppShell.Orientation == "Portrait")
			{
				Classes.LayoutUpdate.TaskPageTwoColumnsPortraitLayout();
			}
		}
		void UpdateLayout(object? sender, DisplayInfoChangedEventArgs e)
		{
			AppShell.Orientation = e.DisplayInfo.Orientation.ToString();
			if(AppShell.Orientation == "Landscape")
			{
				Classes.LayoutUpdate.TaskPageTwoColumnsLandscapeLayout();			
			}
			else if(AppShell.Orientation == "Portrait")
			{
				Classes.LayoutUpdate.TaskPageTwoColumnsPortraitLayout();		
			}
		}
		public void SetButtonsBindings()
		{
			SetButtonBinding(DeleteButton);
			SetButtonBinding(MoveLeftButton);
			SetButtonBinding(MoveRightButton);
			SetButtonBinding(AddButton);
			SetButtonBinding(OpenButton);
			SetButtonBinding(EditButton);
			Binding binding = new Binding { Source = ChangeLayoutButtonState, Path = "IsVisible" };
			ItemPicker.SetBinding(IsVisibleProperty, binding);
		}
		public void SetButtonBinding(Button button)
		{
			Binding binding = new Binding { Source = ChangeLayoutButtonState, Path = "IsVisible" };
			button.SetBinding(Button.IsVisibleProperty, binding);	
		}
		PickOptions imageOptions = new()
		{
			PickerTitle = "Choose Image",
			FileTypes = FilePickerFileType.Images
		};
		public static Databases.ItemsDatabase? Database
		{
			get
			{
				if (database == null)
				{
					database = new Databases.ItemsDatabase(MainPage.TaskDbPath);
				}
				else if(MainPage.TaskDbPath != database.GetDatabasePath())
				{
					database = new Databases.ItemsDatabase(MainPage.TaskDbPath);
				}
				return database;
			}
		}
		public sealed class VideoItem : Classes.Item
		{

		}
		private async void AddItem_Button_Clicked(object sender, EventArgs e)
		{
			
			if (SelectedItemType == ItemPicker.Items[0].ToString())
			{
				this.ShowPopup(new CreateTextItemPopup());
			}
			else if (SelectedItemType == ItemPicker.Items[1].ToString())
			{
				
				var image = await FilePicker.Default.PickAsync(imageOptions);
				if(image != null)
				{
					ImageSourceString = image.FullPath.ToString();
					Classes.Item item = new Classes.Item() { ImageSource = ImageSourceString, isTextItem = false, isImageItem = true, Position = ItemsToDo.Count };
					await Database!.AddItem(item);
					ItemsToDo.Add(item);
				}			
			}
			else if(SelectedItemType == ItemPicker.Items[2].ToString())
			{
				var image = await FilePicker.Default.PickAsync(imageOptions);
				if(image != null)
				{
					ImageSourceString = image.FullPath.ToString();
					this.ShowPopup(new CreateTextItemPopup());
				}
			}	
		}

		private void ItemPicker_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedItemType = ItemPicker.SelectedItem?.ToString();
		}
		private void Edit_Button_Clicked(object sender, EventArgs e)
		{
			this.ShowPopup(new EditTextPopup());
			OpenButton.IsEnabled = false;
			EditButton.IsEnabled = false;
		}

		private async void MoveRight_Button_Clicked(object sender, EventArgs e)
		{
			var item = ItemMethods.DefineItem();
			if(item != null)
			{
				item.MoveRightTwoColumns();
				await Database!.UpdateItem(item);
				OpenButton.IsEnabled = false;
				EditButton.IsEnabled = false;
			}			
		}

		private async void ItemsToDo_CV_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ItemsToDo_CV.SelectedItem != null)
			{
				var item = ItemsToDo_CV.SelectedItem as Classes.Item;
				if(AppShell.AppShellInstance.EditIsOn == true || QuickMoveIsOn == false)
				{
					ItemsDone_CV.SelectedItem = null;
					SelectedItemCollection = "ItemsToDo";
					ItemMethods.CheckTextOrImage(item!);
					item!.CurrentCollection = "ItemsToDo";
					//Debug.WriteLine($"Item Text: {item.Text}, CurrentCollection: {item.CurrentCollection}, Type: {item.GetType()}, ID: {item.Id}, Editable: {item.IsNotEditable}, Position: {item.Position}");
				}
				else if (QuickMoveIsOn == true)
				{
					item.MoveRightTwoColumns();
					SelectedItemCollection = "ItemsToDo";
					ItemsDone_CV.SelectedItem = null;
					await Database!.UpdateItem(item);
				}
			}

		}

		private async void ItemsDone_CV_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (ItemsDone_CV.SelectedItem != null)
			{
				var item = ItemsDone_CV.SelectedItem as Classes.Item;
				if (AppShell.AppShellInstance.EditIsOn == true || QuickMoveIsOn == false)
				{
					ItemsToDo_CV.SelectedItem = null;
					SelectedItemCollection = "ItemsDone";
					ItemMethods.CheckTextOrImage(item!);
					item!.CurrentCollection = "ItemsDone";
					//Debug.WriteLine($"Item Text: {item.Text}, CurrentCollection: {item.CurrentCollection}, Type: {item.GetType()}, ID: {item.Id}, Editable: {item.IsNotEditable}, Position: {item.Position}");
				}
				else if(QuickMoveIsOn == true)
				{
					item.MoveLeftTwoColumns();
					SelectedItemCollection = "ItemsDone";
					ItemsToDo_CV.SelectedItem = null;
					await Database!.UpdateItem(item);
				}

			}

		}

		private async void MoveLeft_Button_Clicked(object sender, EventArgs e)
		{
			var item = ItemMethods.DefineItem();
			if (item != null)
			{
				item.MoveLeftTwoColumns();
				await Database!.UpdateItem(item);
				OpenButton.IsEnabled = false;
				EditButton.IsEnabled = false;
			}
		}
		public static async Task<bool> IsHaveItems()
		{
			var db = new ItemsDatabase(MainPage.TaskDbPath);
			List<Item> items = await db.GetItems();
			if(items.Count > 0)
			{
				return true;
			}
			else { return false; }
		}
		private async void ContentPage_Appearing(object sender, EventArgs e)
		{
			bool isHaveItems = await IsHaveItems();
			AppShell.AppShellInstance.AppShellTitle.Text = MainPage.TaskName;
			if (isHaveItems == true)
			{
				ItemsToDo_CV.ItemsSource = await ItemsAddingMethods.GetItemsToDoTwoRows();
				ItemsDone_CV.ItemsSource = await ItemsAddingMethods.GetItemsDoneTwoRows();
			}
			else if (isHaveItems == false)
			{
				ItemsToDo_CV.ItemsSource = ItemsToDo;
				ItemsDone_CV.ItemsSource = ItemsDone;
			}
			AppShell.AppShellInstance.AppShellButton.IsVisible = true;
			AppShell.AppShellInstance.EditIsOn = false;
		}

		private async void Delete_Button_Clicked(object sender, EventArgs e)
		{
			Classes.Item? item = ItemMethods.DefineItem();
			if(item != null)
			{
				await Database!.DeleteItem(item);
				item.DeleteItemTwoColumns();
				OpenButton.IsEnabled = false;
				EditButton.IsEnabled = false;
			}
			else
			{
				if (Settings.SettingsInstance!.PickerLanguages.SelectedItem.ToString() == "�������")
				{
					await DisplayAlert("Task Manager", "�������� �������", "��������");
				}
				else if (Settings.SettingsInstance.PickerLanguages.SelectedItem.ToString() == "English")
				{
					await DisplayAlert("Task Manager", "Select item", "Cancel");
				}
				
			}
		}

		private async void ContentPage_Disappearing(object sender, EventArgs e)
		{
			await Database.CloseAllConnections();
			DeviceDisplay.MainDisplayInfoChanged -= UpdateLayout;
			await Navigation.PopAsync();
			AppShell.AppShellInstance.AppShellButton.IsVisible = false;
		}

		private void Open_Button_Clicked(object sender, EventArgs e)
		{
			var item = ItemMethods.DefineItem();
			if(item != null)
			{
				img.Source = item.ImageSource;
				this.ShowPopup(new OpenImagePopup());
				OpenButton.IsEnabled = false;
				EditButton.IsEnabled = false;
			}
		}

		private async void ItemsToDo_CV_ChildRemoved(object sender, ElementEventArgs e)
		{
			if(ItemsToDo.Count > 0)
			{
				for (int i = 0; i < ItemsToDo.Count; i++)
				{
					var item = ItemsToDo[i];
					if (item != null)
					{
						item.Position = i + 1;
						await Database!.UpdateItem(item);
					}
				}
			}
		}


		private async void ItemsDone_CV_ChildRemoved(object sender, ElementEventArgs e)
		{
			if(ItemsDone.Count > 0)
			{
				for (int i = 0; i < ItemsDone.Count; i++)
				{
					var item = ItemsDone[i];
					if (item != null)
					{
						item.Position = i + 1;
						await Database!.UpdateItem(item);
					}
				}
			}
		}
		protected override bool OnBackButtonPressed()
		{
			var item = ItemMethods.DefineItem();
			if (item != null)
			{
				if(QuickMoveIsOn == true && AppShell.AppShellInstance.EditIsOn == false)
				{
					return base.OnBackButtonPressed();
				}
				if(item.CurrentCollection == "ItemsToDo")
				{
					ItemsToDo_CV.SelectedItem = null;
					OpenButton.IsEnabled = false;
					EditButton.IsEnabled = false;
				}
				if (item.CurrentCollection == "ItemsDone")
				{
					ItemsDone_CV.SelectedItem = null;
					OpenButton.IsEnabled = false;
					EditButton.IsEnabled = false;
				}
				return true;
			}
			else
			{
				return base.OnBackButtonPressed();
			}

		}
	}

}
