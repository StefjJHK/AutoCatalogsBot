using System;
using System.Linq;
using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using BusinessLogic.IRepositories;
using BusinessLogic.Statics;
using GoogleSheets.ParametrObjects;

namespace BusinessLogic.Implementations
{
    public class SheetsCatalogsRepository : ISheetsCatalogsRepository
    {
        private readonly SheetsService _sheetsService;
        private readonly string _connectionString;

        public SheetsCatalogsRepository(SheetsService sheetsService, GoogleSheetsParams sheetsParams)
        {
            _sheetsService = sheetsService ?? 
                throw new ArgumentNullException(nameof(sheetsService));
            _connectionString = sheetsParams?.ConnectionString ?? 
                throw new ArgumentNullException(nameof(sheetsParams));
        }

        public Dictionary<string, List<IList<object>>> Get(IEnumerable<string> sheets)
        {
            return sheets.AsParallel()
                .Select(sheet => new 
                {
                    Key = sheet,
                    Values = GetBySheet(sheet) 
                })
                .ToDictionary(group => 
                    group.Key,
                    group => group.Values);
        }

        public List<IList<object>> GetBySheet(string sheet)
        {
            var values = SheetsOperations.GetSheetValues(_sheetsService, _connectionString, sheet);

            return values
                .Skip(1)
                .ToList();
        }

        public void Refresh(string sheet, List<IList<object>> catalog)
        {
            ValueRange valueRange = new ValueRange();
            valueRange.MajorDimension = "ROWS";
            valueRange.Range = $"{sheet}!A2:D";
            valueRange.Values = catalog;

            string range = $"{sheet}!A2:D";

            var updateRequest = _sheetsService.Spreadsheets.Values.Update(valueRange, _connectionString, range);
            var clearRequest = _sheetsService.Spreadsheets.Values.Clear(new ClearValuesRequest(), _connectionString, range);

            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.RAW;

            clearRequest.Execute();
            updateRequest.Execute();
        }
    }
}
