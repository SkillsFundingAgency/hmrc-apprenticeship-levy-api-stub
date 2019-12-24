using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories
{
    public abstract class BaseCosmosRepository
    {
        protected readonly IDocumentClient Client;
        protected readonly Uri CollectionUri;

        protected BaseCosmosRepository(
            IDocumentClient client,
            Uri collectionUri
            )
        {
            Client = client ?? throw new ArgumentException("client cannot be null"); 
            CollectionUri = collectionUri ?? throw new ArgumentException("collectionUri cannot be null");
        }
    }
}
