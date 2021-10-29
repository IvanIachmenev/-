using DepartamentIMCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;


namespace DepartamentIMCS.Services
{
    public class DataPublications : IDataStore<Publications>
    {
        List<Publications> items;

        public DataPublications()
        {

            items = new List<Publications>()
            {
                new Publications { id = Guid.NewGuid().ToString(), title = "1", comment="2", Category = "3" }
            };

        }

        public async Task<bool> AddItemAsync(Publications item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Publications item)
        {
            var oldItem = items.Where((Publications arg) => arg.id == item.id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Publications arg) => arg.id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Publications> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.id == id));
        }

        public async Task<Publications> SetItemAsync(Publications item, Publications itemOld)
        {
            return await Task.FromResult(items[Convert.ToInt32(itemOld.id)] = item);
        }

        public async Task<IEnumerable<Publications>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
