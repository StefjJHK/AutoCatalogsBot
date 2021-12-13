using BusinessLogic.DTO;
using BusinessLogic.IRepositories;
using BusinessLogic.ParamterObjects.Service.Requests;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services.Requests
{
    class GetByKindsService : IRequestService<GetByKindsRequest, IEnumerable<CatalogDTO>>
    {
        private readonly ICatalogsRepository _catalogsRepository;

        public GetByKindsService(ICatalogsRepository catalogsRepository)
        {
            _catalogsRepository = catalogsRepository ??
                throw new ArgumentNullException(nameof(catalogsRepository));

        }

        public IEnumerable<CatalogDTO> Execute(GetByKindsRequest request)
        {
            return request.Kinds
                .Select(kind => _catalogsRepository.GetByKind(kind));
        }
    }
}
