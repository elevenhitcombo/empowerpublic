using Empower.Domain.Client.Models;
using Empower.Domain.Client.Requests;
using Empower.Domain.Client.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Empower.Services
{
    public interface IActorService
    {
        ActorSearchResponse Search(ActorSearchRequest request);
        ActorCreateResponse Create(ActorCreateRequest request);
        ActorUpdateResponse Update(ActorUpdateRequest request);
        ActorDeleteResponse Delete(ActorDeleteRequest request);
        Actor Get(int id);

    }
}
