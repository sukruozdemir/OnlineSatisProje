using System;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;

namespace OnlineSatisProje.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IDbContext _context;
        private IDbSet<T> _entities;

        public Repository(IDbContext context)
        {
            _context = context;
        }

        private IDbSet<T> Entities => _entities ?? (_entities = _context.Set<T>());

        public T GetById(object id)
        {
            return Entities.Find(id);
        }

        public void Insert(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                Entities.Add(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                var msg = e.EntityValidationErrors.Aggregate(string.Empty,
                    (current1, validationErrors) => validationErrors.ValidationErrors
                        .Aggregate(current1,
                            (current, validationError) =>
                                current +
                                $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"));

                var fail = new Exception(msg, e);
                throw fail;
            }
        }

        public void Update(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty,
                    (current1, validationErrors) =>
                        validationErrors.ValidationErrors.Aggregate(current1,
                            (current, validationError) =>
                                current + Environment.NewLine +
                                $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"));
                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public void Delete(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity));
                Entities.Remove(entity);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException dbEx)
            {
                var msg = dbEx.EntityValidationErrors.Aggregate(string.Empty,
                    (current1, validationErrors) =>
                        validationErrors.ValidationErrors.Aggregate(current1,
                            (current, validationError) =>
                                current + Environment.NewLine +
                                $"Property: {validationError.PropertyName} Error: {validationError.ErrorMessage}"));

                var fail = new Exception(msg, dbEx);
                throw fail;
            }
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public IQueryable<T> Table => Entities;
    }
}