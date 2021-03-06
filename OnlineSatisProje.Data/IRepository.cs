﻿using System.Linq;

namespace OnlineSatisProje.Data
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> Table { get; }
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        int Save();
    }
}