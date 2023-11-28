﻿using Shop.Domain.Entities;

namespace Shop.Domain.Interfaces
{
    public interface IItemRepository
    {
        Task<IEnumerable<Domain.Entities.Item>> GetAll();
        Task Create(Domain.Entities.Item item);
        Task<Domain.Entities.Item> GetByEncodedName(string encodedName);
        Task<Item> GetByName(string name);
        Task Commit();
        Task Delete(Item item);
    }
}
