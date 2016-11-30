using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Infrastructure.CQRS.ReadStore.DocumentDb;

namespace Infrastructure.InMemory.ReadStore.DocumentDb
{
    public class InMemoryDocumentDb : IDocumentDb
    {
        public IDictionary<string, IDictionary<Guid, dynamic>> Documents { get; }

        public InMemoryDocumentDb()
        {
            Documents = new ConcurrentDictionary<string, IDictionary<Guid, dynamic>>();

            Documents.Add("Inventory", new ConcurrentDictionary<Guid, dynamic>());
            Documents.Add("InventoryItem", new ConcurrentDictionary<Guid, dynamic>());
        }
    }
}