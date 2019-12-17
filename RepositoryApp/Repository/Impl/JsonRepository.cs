using Newtonsoft.Json;
using RepositoryApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RepositoryApp.Repository.Impl
{
    public class JsonRepository<T> : IRepository<T> where T : User
    {
        T[] db;
        T[] temp;
        int count=0;
        public JsonRepository()
        {
            db = JsonConvert.DeserializeObject<T[]>(File.ReadAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/MOCK_DATA.json")));
        }

        private void Expansion()
        {
            Clear();
            db = new T[count];
            for (int i = 0; i < temp.Count(); i++) 
                db[i] = temp[i];
        }
        private void Clear()
        {
            temp = new T[db.Count()];
            for (int i = 0; i < db.Count(); i++)
                temp[i] = db[i];
            count = db.Count()+1;
            db = null;
        }

        public void Create(T item)
        {
            Expansion();
            if (item.Id==0)
                item.Id = db.Length;
            db[db.Length - 1] = item;
            Save();
        }
       
        private void Save()
        {
            File.WriteAllText(System.Web.Hosting.HostingEnvironment.MapPath("~/App_Data/MOCK_DATA.json"), JsonConvert.SerializeObject(db));
        }
        public void Delete(T item)
        {
            if (temp!=null)
                temp = null;
            temp = new T[db.Count()-1];
            int j = 0;
            for (int i = 0; i < db.Length; i++)
                if (db[i].Id != item.Id)
                {
                    temp[j] = db[i];
                    ++j;
                }
            
            db = null;
            db = new T[temp.Length];
            for (int i = 0; i < temp.Length; i++)
                db[i] = temp[i];
            Save();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                {
                    if (db!=null)
                         db = null;
                    if (temp!=null)
                        temp = null;
                    if (count!=0)
                        count = 0;
                }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Edit(T item)
        {
            for (int i = 0; i < db.Length; i++)
                if (db[i].Id==item.Id)
                {
                    db[i].FirstName = item.FirstName;
                    db[i].LastName = item.LastName;
                    db[i].Email = item.Email;
                    db[i].Gender = item.Gender;
                    break;
                }
            Save();
        }

        public T GetEntity(int id)
        {
            for (int i = 0; i < db.Length; i++)
                if ((id)==db[i].Id)
                    return db[i];
            return null;
        }

        public IEnumerable<T> GetEntitysList()
        {
            return db;
        }
    }
}