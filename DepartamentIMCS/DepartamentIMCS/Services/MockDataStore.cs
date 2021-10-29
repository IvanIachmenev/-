using DepartamentIMCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace DepartamentIMCS.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        List<Item> items;

        public MockDataStore()
        {

            items = new List<Item>()
            {
                new Item { Id = Guid.NewGuid().ToString(), Text = "01.03.02 Прикладная математика и информатика", Description="Здесь должно быть описание направления  ", Category = "Бакалавриат" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "02.03.01 Математика и компьютерные науки", Description="Здесь должно быть описание направления ", Category = "Бакалавриат" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "01.03.02 Прикладная математика и информатика", Description="Здесь должно быть описание направления ", Category = "Магистратура" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "01.06.01 Математика и механика", Description="Здесь должно быть описание направления ", Category = "Аспирантура" },
                new Item { Id = Guid.NewGuid().ToString(), Text = "02.06.01 Компьютерные и информационные технологии", Description="Здесь должно быть описание направления ", Category = "Аспирантура" },
            };

        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Item> SetItemAsync(Item item, Item itemOld)
        {
            return await Task.FromResult(items[Convert.ToInt32(itemOld.Id)] = item);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}