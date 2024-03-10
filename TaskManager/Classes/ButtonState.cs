using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TaskManager.Classes
{
	public class ButtonState : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler? PropertyChanged;
		private bool isVisible = false;
		public bool IsVisible { get { return isVisible; } set { isVisible = value; OnPropertyChanged(); } }
		protected void OnPropertyChanged([CallerMemberName] string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
