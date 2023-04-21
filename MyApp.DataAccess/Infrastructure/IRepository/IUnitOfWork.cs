﻿namespace MyApp.DataAccess.Infrastructure.IRepository
{
    public interface IUnitOfWork
    {

        ICategoryRepository Category { get; }
        IProductRepository Product { get; }


        void Save();
    }
}
