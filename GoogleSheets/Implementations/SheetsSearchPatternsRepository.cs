using System;
using System.Linq;
using System.Collections.Generic;
using BusinessLogic.Statics;
using Google.Apis.Sheets.v4;
using BusinessLogic.IRepositories;
using GoogleSheets.ParametrObjects;

namespace BusinessLogic.Implementations
{
    public class SheetsSearchPatternsRepository : ISheetsSearchPatternsRepository
    {
        private readonly SheetsService _sheetsService;
        private readonly string _connectionString;

        public SheetsSearchPatternsRepository(SheetsService sheetsService, GoogleSheetsParams sheetsParams)
        {
            _sheetsService = sheetsService ?? 
                throw new ArgumentNullException(nameof(sheetsService));
            _connectionString = sheetsParams?.ConnectionString ?? 
                throw new ArgumentNullException(nameof(sheetsParams));
        }

        public List<IList<object>> Get(string sheet)
        {
            var values = SheetsOperations.GetSheetValues(_sheetsService, _connectionString, sheet);

            return values
                .Skip(1)
                .ToList();
        }
    }
}
