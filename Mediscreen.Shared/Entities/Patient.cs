using AspNetCore.Identity.MongoDbCore.Models;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mediscreen.Shared.Entities
{
    [CollectionName("Patients")]
    public class Patient : MongoIdentityUser<int>
    {
    }
}
