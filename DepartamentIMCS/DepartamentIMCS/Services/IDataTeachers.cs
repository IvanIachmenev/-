﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DepartamentIMCS.Services
{
    public interface IDataTeachers<T>
    {
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<T> SetItemAsync(T item, T itemOld);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);
    }
}
