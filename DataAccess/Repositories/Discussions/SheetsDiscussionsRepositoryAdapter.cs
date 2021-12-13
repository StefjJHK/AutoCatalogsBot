using System;
using System.Linq;
using System.Collections.Generic;
using BusinessLogic.IRepositories;
using BusinessLogic.ParametrObjects;
using BusinessLogic.DTO;

namespace DataAccess.Repositories.Discussions
{
    public class SheetsDiscussionsRepositoryAdapter : IDiscussionsRepository
    {
        private readonly ISheetsDiscussionsRepository _sheetsDiscussionsRepository;
        private readonly SheetDiscussionsParams _parsingParams;

        public SheetsDiscussionsRepositoryAdapter(ISheetsDiscussionsRepository sheetsDiscussionsRepository, 
            SheetDiscussionsParams parsingParams)
        {
            _sheetsDiscussionsRepository = sheetsDiscussionsRepository ?? 
                throw new ArgumentNullException(nameof(sheetsDiscussionsRepository));
            _parsingParams = parsingParams ??
                throw new ArgumentNullException(nameof(parsingParams));
        }

        public IEnumerable<DiscussionDTO> GetAll()
        {
            var rows = _sheetsDiscussionsRepository.Get(_parsingParams.Sheet);

            return rows
                .Select(row => new DiscussionDTO(
                    (string)row[_parsingParams.Kind],
                    Convert.ToInt32((string)row[_parsingParams.Id])));
        }

        public DiscussionDTO GetByKind(string kind)
        {
            return GetAll()
                .FirstOrDefault(discussion => discussion.Kind == kind);
        }
    }
}
