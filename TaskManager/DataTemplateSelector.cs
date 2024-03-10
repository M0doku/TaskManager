using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskManager
{
	public class ItemDataTemplateSelector : DataTemplateSelector
	{
		public DataTemplate? HybridTemplate { get; set; }	
		protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
		{
			return HybridTemplate!;
		}
	}
}