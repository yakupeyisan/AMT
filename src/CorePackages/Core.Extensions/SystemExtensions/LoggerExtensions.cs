/*using Core.Domain.Dtos;
using Newtonsoft.Json;
using Core.Utilities.Helpers;
using Core.Domain.Base;
using Core.Domain.Constants;

namespace Core.Extensions.SystemExtensions
{
    public static class LoggerExtensions
    {
        public static T AddLog<T>(this T record,
            string description,
            string table,
            string? recordId,
            T? oldRecord = default,
            LogType? logType=null,
            RecordType? recordType=null
            )
        {

            var log = new AddLogDto()
            {
                TableName=table,
                RecordId=recordId??"",
                RecordType = recordType?.Value ?? RecordType.Unknown.Value,
                Type = logType?.Value ?? LogType.Verbose.Value,
                Description = description,
                NewRecord = (record is string) ? "" : JsonConvert.SerializeObject(record),
                OldRecord = (oldRecord is null || oldRecord is string) ? "" : JsonConvert.SerializeObject(oldRecord)
            };
            _ =TransportHelper.Request<dynamic>(ServicePaths.LoggerService , "/AddLog", log, HttpMethod.Post, isThrowError: false);
            return record;
        }
    }
}
*/