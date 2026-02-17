using Azure;
using Azure.Data.Tables;
using hanumachantsApi.Models;
using System.Text.Json;

public class TableService
{
    private readonly TableClient _table;

    public TableService(IConfiguration config)
    {
        var conn = config["ConnectionStrings:StorageConnection"];
        var tableName = config["ConnectionStrings:AzTableName"];

        _table = new TableClient(conn, tableName);
        _table.CreateIfNotExists();
    }

    public async Task<SessionEntity> CreateAsync()
    {
        //var id = Guid.NewGuid().ToString("HNMT")[..8].ToUpper();
        var id = "HNMT-" + Guid.NewGuid().ToString()[..6].ToUpper();

        var entity = new SessionEntity
        {
            PartitionKey = "SESSION",   // ADD THIS
            RowKey = id,
            Expiry = DateTimeOffset.UtcNow.AddDays(15),
            RangeStart = null,
            RangeEnd = null,
            CompletedDates = ""
        };
        try
        {
            await _table.AddEntityAsync(entity);
        }
        catch(Exception ex)
        {
            Console.WriteLine("Insert Failed: " + ex.Message);
            throw;
        }
        return entity;
    }

    public async Task<SessionEntity?> GetAsync(string id)
    {
        try
        {
            return await _table.GetEntityAsync<SessionEntity>("SESSION", id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<SessionEntity> UpdateAsync(string id, SessionUpdateDto dto)
    {
        try
        {
            var entity = await _table.GetEntityAsync<SessionEntity>("SESSION", id);

            if (dto.CompletedDates != null)
                entity.Value.CompletedDates = dto.CompletedDates;

            if (dto.RangeStart != null)
                entity.Value.RangeStart = dto.RangeStart;

            if (dto.RangeEnd != null)
                entity.Value.RangeEnd = dto.RangeEnd;

            await _table.UpsertEntityAsync(entity.Value, TableUpdateMode.Replace);
            return entity.Value;
        }
        catch
        {
            return null;
        }
    }
}

