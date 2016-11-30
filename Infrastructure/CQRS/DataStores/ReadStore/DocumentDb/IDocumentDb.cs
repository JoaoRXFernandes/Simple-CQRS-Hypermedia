using System;
using System.Collections.Generic;

namespace Infrastructure.CQRS.ReadStore.DocumentDb
{
    public interface IDocumentDb
    {
        IDictionary<string, IDictionary<Guid, dynamic>> Documents { get; }
    }
}