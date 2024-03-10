using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace TaskManager.Classes
{
	[Table("Items")]
	public class Item : INotifyPropertyChanged
	{
		[PrimaryKey, AutoIncrement]
		[Column("id")]
	    public int Id { get; set; }
		public event PropertyChangedEventHandler? PropertyChanged;
		[Column("currentcollection")]	
		public string CurrentCollection { get; set; } = "ItemsToDo";
		private string text = "";
		[Column("text")]
		public string Text { get { return text; } set { text = value; OnPropertyChanged(); } }
		[Column("imagesource")]
		public string ImageSource { get; set; } = string.Empty;
		[Column("istextitem")]
		public bool isTextItem { get; set; }
		[Column("isimageitem")]
		public bool isImageItem { get; set; }
		private bool isNotEditable = true;
		private int position = 0;
		[Column("isnoteditable")]
		public bool IsNotEditable { get { return isNotEditable; } set { isNotEditable = value; OnPropertyChanged(); } }
		[Column("position")]
		public int Position { get { return position; } set { position = value; OnPropertyChanged(); } }
		protected void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
		public Item(string text)
		{
			Text = text;	
		}
		public Item() { }
		public void MoveRight()
		{
			switch (CurrentCollection)
			{
				case "ItemsToDo":
				TaskPage.ItemsToDo.Remove(this);
				TaskPage.ItemsDoing.Add(this);
				this.Position = TaskPage.ItemsDoing.Count;
				this.CurrentCollection = "ItemsDoing";
				break;
				case "ItemsDoing":
				TaskPage.ItemsDoing.Remove(this);
				TaskPage.ItemsDone.Add(this);
				this.Position = TaskPage.ItemsDone.Count;
				this.CurrentCollection = "ItemsDone";
				break;
			}
		}
		public void DeleteItem()
		{
			switch(CurrentCollection)
			{
				case "ItemsToDo":
				TaskPage.ItemsToDo.Remove(this);
				break;
				case "ItemsDoing":
				TaskPage.ItemsDoing.Remove(this);
				break;
				case "ItemsDone":
				TaskPage.ItemsDone.Remove(this);
				break;
			}
		}
		public void MoveLeft()
		{
			switch (CurrentCollection)
			{
				case "ItemsDone":
				TaskPage.ItemsDone.Remove(this);
				TaskPage.ItemsDoing.Add(this);
				this.Position = TaskPage.ItemsDoing.Count;
				this.CurrentCollection = "ItemsDoing";
				break;
				case "ItemsDoing":
				TaskPage.ItemsDoing.Remove(this);
				TaskPage.ItemsToDo.Add(this);
				this.Position = TaskPage.ItemsToDo.Count;
				this.CurrentCollection = "ItemsToDo";
				break;
			}
		}
	}
}
