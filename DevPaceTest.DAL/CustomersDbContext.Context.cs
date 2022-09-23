﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DevPaceTest.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class DevPaceTestEntities : DbContext
    {
        public DevPaceTestEntities()
            : base("name=DevPaceTestEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Customer> Customers { get; set; }
    
        public virtual ObjectResult<Customer> FindAllCustomers()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer>("FindAllCustomers");
        }
    
        public virtual ObjectResult<Customer> FindAllCustomers(MergeOption mergeOption)
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer>("FindAllCustomers", mergeOption);
        }
    
        public virtual ObjectResult<Customer> FindCustomer(string filter, string orderBy, Nullable<int> pageNumber, Nullable<int> rowsOnPage)
        {
            var filterParameter = filter != null ?
                new ObjectParameter("Filter", filter) :
                new ObjectParameter("Filter", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var rowsOnPageParameter = rowsOnPage.HasValue ?
                new ObjectParameter("RowsOnPage", rowsOnPage) :
                new ObjectParameter("RowsOnPage", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer>("FindCustomer", filterParameter, orderByParameter, pageNumberParameter, rowsOnPageParameter);
        }
    
        public virtual ObjectResult<Customer> FindCustomer(string filter, string orderBy, Nullable<int> pageNumber, Nullable<int> rowsOnPage, MergeOption mergeOption)
        {
            var filterParameter = filter != null ?
                new ObjectParameter("Filter", filter) :
                new ObjectParameter("Filter", typeof(string));
    
            var orderByParameter = orderBy != null ?
                new ObjectParameter("OrderBy", orderBy) :
                new ObjectParameter("OrderBy", typeof(string));
    
            var pageNumberParameter = pageNumber.HasValue ?
                new ObjectParameter("PageNumber", pageNumber) :
                new ObjectParameter("PageNumber", typeof(int));
    
            var rowsOnPageParameter = rowsOnPage.HasValue ?
                new ObjectParameter("RowsOnPage", rowsOnPage) :
                new ObjectParameter("RowsOnPage", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Customer>("FindCustomer", mergeOption, filterParameter, orderByParameter, pageNumberParameter, rowsOnPageParameter);
        }
    }
}
