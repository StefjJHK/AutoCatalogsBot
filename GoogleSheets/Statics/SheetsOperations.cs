using System.Collections.Generic;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using static Google.Apis.Sheets.v4.SpreadsheetsResource.ValuesResource;

namespace BusinessLogic.Statics
{
    static class SheetsOperations
    {
        public static IList<IList<object>> GetSheetValues(SheetsService service, string id, string range)
        {
            GetRequest request = service.Spreadsheets.Values.Get(id, range);
            ValueRange responce = request.Execute();

            return responce.Values;
        }
    }
}
