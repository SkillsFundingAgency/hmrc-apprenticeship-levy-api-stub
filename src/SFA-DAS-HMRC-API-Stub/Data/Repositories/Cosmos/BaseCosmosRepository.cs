using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public abstract class BaseCosmosRepository
    {
        protected readonly DocumentClient Client;
        protected readonly Uri CollectionUri;

        protected BaseCosmosRepository(
            DocumentClient client,
            Uri collectionUri
            )
        {
            Client = client ?? throw new ArgumentException("client cannot be null"); 
            CollectionUri = collectionUri ?? throw new ArgumentException("collectionUri cannot be null");
        }
    }
}
