using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using MauiTripPlanner.Models;

namespace MauiTripPlanner
{
	internal class Database
	{
		private readonly SQLiteAsyncConnection _connection;

		public Database()
		{
			var dataDir = FileSystem.AppDataDirectory;
			var databasePath = Path.Combine(dataDir, "MauiTripPlanner.db");

			string _dbEncryptionKey = SecureStorage.GetAsync("dbKey").Result;

			if (string.IsNullOrEmpty(_dbEncryptionKey))
			{
				Guid g = new Guid();
				_dbEncryptionKey = g.ToString();
				SecureStorage.SetAsync("dbKey", _dbEncryptionKey);
			}

			var dbOptions = new SQLiteConnectionString(databasePath, true, key: _dbEncryptionKey);

			_connection = new SQLiteAsyncConnection(dbOptions);

			_ = Initialise();
		}

		private async Task Initialise()
		{
			await _connection.CreateTableAsync<ChecklistItem>();
		}

		public async Task<List<ChecklistItem>> GetChecklistItems()
		{
			return await _connection.Table<ChecklistItem>().ToListAsync();
		}

		public async Task<ChecklistItem> GetChecklistItem(int id)
		{
			var query = _connection.Table<ChecklistItem>().Where(t => t.Id == id);

			return await query.FirstOrDefaultAsync();
		}

		public async Task<int> AddChecklistItem(ChecklistItem item)
		{
			return await _connection.InsertAsync(item);
		}

		public async Task<int> DeleteChecklistItem(ChecklistItem item)
		{
			return await _connection.DeleteAsync(item);
		}

		public async Task<int> UpdateChecklistItem(ChecklistItem item)
		{
			return await _connection.UpdateAsync(item);
		}
	}
}
