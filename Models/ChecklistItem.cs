using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MauiTripPlanner.Models
{
	public class ChecklistItem : INotifyPropertyChanged
	{
		[PrimaryKey, AutoIncrement] 
		public int Id { get; set; }
		public string Name { get; set; }
		private bool isChecked { get; set; } = false;
		public bool IsChecked
		{
			get => isChecked;
			set
			{
				isChecked = value;
				OnPropertyChanged();
			}
		}

		#region INotifyPropertyChanged
		public event PropertyChangedEventHandler PropertyChanged;
		protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
		public void RaisePropertyChanged(params string[] properties)
		{
			foreach (var propertyName in properties)
			{
				PropertyChanged?.Invoke(this, new
				PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

	}
}
