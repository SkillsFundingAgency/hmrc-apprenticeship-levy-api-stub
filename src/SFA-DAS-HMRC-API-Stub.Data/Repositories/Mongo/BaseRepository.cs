using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SFA.DAS.HMRC.API.Stub.Data.Repositories.Mongo
{
    public abstract class BaseRepository
    {
        protected readonly IMongoDatabase Database;

        protected BaseRepository(
            IMongoDatabase database
        )
        {
            Database = database ?? throw new ArgumentException("database cannot be null"); 
        }
    }
}
