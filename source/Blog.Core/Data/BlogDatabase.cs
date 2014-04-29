using System;
using System.Data.Entity;
using Blog.Core.Data.Entities;
using System.Data.Entity.Validation;

namespace Blog.Core.Data
{
    public class BlogDatabase : DbContext
    {
        public IDbSet<BlogEntry> BlogEntries { get; set; }

        public BlogDatabase()
            : base("MDBlog")
        {
            Configuration.LazyLoadingEnabled = false;
        }


        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {

                var validationErrors = e.EntityValidationErrors;
                foreach (var valiationError in validationErrors)
                {
                    foreach (var red in valiationError.ValidationErrors)
                    {
                        var propName = red.PropertyName;
                        var erroMess = red.ErrorMessage;
                    }
                }

                throw;
            }
        }
    }
}