using System.Collections.Generic;

namespace BusinessLogic.ParamterObjects.Service.Requests
{
    public class GetByKindsRequest
    {
        public IEnumerable<string> Kinds { get; init; }

        public GetByKindsRequest(IEnumerable<string> kinds)
        {
            Kinds = kinds;
        }
    }
}
