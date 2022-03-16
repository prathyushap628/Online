using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface IOrderRepository
{
   
    Task<bool> Update(Orders Item);
    Task<bool> Delete(int Id);
    Task<List<Orders>> GetList();
    Task<Orders> GetById(int Id);
    Task<List<Orders>>GetListByCustomerId(int CustomerId);
}

public class OrderRepository : BaseRepository, IOrderRepository
{
    public OrderRepository(IConfiguration config) : base(config)
    {

    }

   public async Task<bool> Delete(int OrderId)
    {
        var query = $@"DELETE FROM {TableNames.orders} WHERE order_id = @OrderId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { OrderId }) > 0;
    }

    public async Task<Orders> GetById(int OrderId)
    {
        var query = $@"SELECT * FROM {TableNames.orders} WHERE order_id=@OrderId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Orders>(query, new { OrderId });
    }

    public async Task<List<Orders>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.orders}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Orders>(query)).AsList();
    }

    public async Task<List<Orders>> GetListByCustomerId(int CustomerId)
    {
          var query = $@"SELECT o.* FROM {TableNames.orders} o 
        LEFT JOIN {TableNames.customer} c ON c.customer_id = o.customer_id 
        WHERE o.customer_id = @CustomerId";

        // LEFT JOIN {TableNames.guest} g ON g.id = s.guest_id 

        using (var con = NewConnection)
            return (await con.QueryAsync<Orders>(query, new { CustomerId })).AsList();
    }

    public async Task<bool> Update(Orders Item)
    {
        var query = $@"UPDATE {TableNames.orders} 
        SET status = @Status  WHERE order_id = @OrderId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}