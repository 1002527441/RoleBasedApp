﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.ProtectedBrowserStorage;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BlazorStrap;
using System;

namespace BlazorApp15.Data
{
    public class StorageServices
    {
        private readonly ProtectedSessionStorage sessionStore;
        private readonly ProtectedLocalStorage localStorage;
        private readonly HttpContext httpContext;       

        public StorageServices(
            ProtectedSessionStorage _sessionStore, 
            ProtectedLocalStorage _localStorage,
            IHttpContextAccessor _http)
        {
            sessionStore = _sessionStore;
            localStorage = _localStorage;
            httpContext = _http.HttpContext;

        }

        #region "Session"
        private async Task<string> GetSession(string key)
        {
            if (key == string.Empty) return string.Empty;
            return await sessionStore.GetAsync<string>(key);        
        }

        private async Task<bool> SetSession(string key, string value)
        {
            if (key == string.Empty) return false;
            await sessionStore.SetAsync(key, value);
            return true;
        }

        private async Task<bool> ClearSession(string key)
        {
            if (key == string.Empty) return false;
            await sessionStore.DeleteAsync(key);
            return true;
        }

        #endregion


        #region "LocalStorage"

        private async Task<string> GetLocalStorage(string key)
        {
            if (key == string.Empty) return string.Empty;
            return await localStorage.GetAsync<string>(key);
        }

        private async Task<bool> SetLocalStorage(string key, string value)
        {
            if (key == string.Empty) return false;
            await localStorage.SetAsync(key, value);
            return true;
        }

        private async Task<bool> ClearLocalStorage(string key)
        {
            if (key == string.Empty) return false;
            await localStorage.DeleteAsync(key);
            return true;
        }

        #endregion

        private async Task<string> GetCookie(string key)
        {          
            if (key == string.Empty) return string.Empty;
            string result = string.Empty;           
            var success= httpContext.Request.Cookies.TryGetValue(key, out result);
            if (string.IsNullOrEmpty(result))
            {
                result = string.Empty;
            }
      
            return await Task.FromResult(result);
        }

        
        public async Task<bool> SetCookie(string key, string value)
        {
            if (key == string.Empty) return false;
            var options = new CookieOptions { 
                    Expires= DateTime.Now.AddMilliseconds(1000),           
                    Path = "/",
                    HttpOnly = false,
                    IsEssential = true,
                };   

            httpContext.Response?.Cookies.Append(key,  value, options);
             return await Task.FromResult(true);
        }

        public async Task<bool> ClearCookie(string key)
        {
            if (key == string.Empty) return false;
            httpContext.Response.Cookies.Delete(key);
            return await Task.FromResult(true);
        }




        public async Task<bool> SetValueAsync(StorageType type, string key, string value)
        {
            bool result = false;
            switch (type)
            {
                case StorageType.Cookie:
                    result= await SetCookie(key, value);
                    break;
                case StorageType.SessionStorage:
                    result=await SetSession(key, value);
                    break;
                case StorageType.LocalStorage:
                    result = await SetLocalStorage(key, value);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }


        public async Task<string> GetValueAsync(StorageType type, string key)
        {
            string result = string.Empty;
            switch (type)
            {
                case StorageType.Cookie:
                    result= await GetCookie(key);
                    break;
                case StorageType.SessionStorage:
                    result= await GetSession(key);
                    break;
                case StorageType.LocalStorage:
                    result = await GetLocalStorage(key);
                    break;
                default:
                    result = string.Empty;
                    break;
            }

            return result;
        }


        public async Task<bool> ClearValueAsync(StorageType type, string key)
        {
            bool result = false;
            switch (type)
            {
                case StorageType.Cookie:
                    result = await ClearCookie(key);
                    break;
                case StorageType.SessionStorage:
                    result = await ClearSession(key);
                    break;
                case StorageType.LocalStorage:
                    result = await ClearLocalStorage(key);
                    break;
                default:
                    result = false;
                    break;
            }

            return result;
        }

    }


    public enum StorageType
    {
        Cookie=1,
        SessionStorage=2,
        LocalStorage=3
    }


    
}
