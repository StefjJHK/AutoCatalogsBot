using System;
using System.Linq;
using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using BusinessLogic.IRepositories;
using BusinessLogic.Statics;
using GoogleSheets.ParametrObjects;

namespace BusinessLogic.Implementations
{
    public class SheetsDiscussionsRepository : ISheetsDiscussionsRepository
    {
        private readonly SheetsService _sheetsService;
        private readonly string _connectionString;

        public SheetsDiscussionsRepository(SheetsService sheetsService, GoogleSheetsParams sheetsParams)
        {
            _sheetsService = sheetsService ?? 
                throw new ArgumentNullException(nameof(sheetsService));
            _connectionString = sheetsParams?.ConnectionString ??
                throw new ArgumentNullException(nameof(sheetsParams));
        }

        public List<IList<object>> Get(string sheet)
        {
            var values =  SheetsOperations.GetSheetValues(_sheetsService, _connectionString, sheet);

            return values
                .Skip(1)
                .ToList();
        }
    }
}