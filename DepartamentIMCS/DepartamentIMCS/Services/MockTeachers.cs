using DepartamentIMCS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;

using Newtonsoft;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace DepartamentIMCS.Services
{
    public class MockTeachers : IDataTeachers<Teacher>
    {
        List<Teacher> items;
        public MockTeachers() 
        {
            items = new List<Teacher>();
        }

        public async Task<bool> AddItemAsync(Teacher item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Teacher item)
        {
            var oldItem = items.Where((Teacher arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Teacher arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Teacher> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<Teacher> SetItemAsync(Teacher item, Teacher itemOld)
        {
            return await Task.FromResult(items[Convert.ToInt32(itemOld.Id)] = item);
        }

        public async Task<IEnumerable<Teacher>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}
