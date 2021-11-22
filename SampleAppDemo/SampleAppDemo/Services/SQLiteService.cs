using SampleAppDemo.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleAppDemo.Services
{
    public class SQLiteService
    {
        private readonly SQLiteAsyncConnection sQLiteAsyncConnection;

        public string StatusMessage { get; set; }

        public SQLiteService(string dbPath)
        {
            sQLiteAsyncConnection = new SQLiteAsyncConnection(dbPath);
            sQLiteAsyncConnection.CreateTableAsync<ScanModel>().Wait();
        }

        public async Task CreateItem(ScanModel item)
        {
            try
            {
                // Basic validation to ensure we have an item name.
                if (string.IsNullOrWhiteSpace(item.Name))
                    throw new Exception("Name is required");

                // Insert/update items.
                var result = await sQLiteAsyncConnection.InsertOrReplaceAsync(item).ConfigureAwait(continueOnCapturedContext: false);
                StatusMessage = $"{result} record(s) added [Item Name: {item.Name}])";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to create item: {item.Name}. Error: {ex.Message}";
            }
        }

        public Task<List<ScanModel>> GetAllItems()
        {
            return sQLiteAsyncConnection.Table<ScanModel>().ToListAsync();
        }
    }
}
