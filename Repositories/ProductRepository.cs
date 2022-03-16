using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface IProductRepository
{
    Task<Products> Create(Products Item);
    Task<bool> Update(Products Item);
    Task<bool> Delete(int Id);
    Task<List<Products>> GetList();
    Task<Products> GetById(int Id);
   // Task<List<Orders>> GetProductByOrderId(int CustomerId);
}

public class ProductRepository : BaseRepository, IProductRepository
{
    public ProductRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Products> Create(Products Item)
    {
        var query = $@"INSERT INTO {TableNames.products} 
        (product_name, price) 
        VALUES (@ProductName, @Price) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Products>(query, Item);
    }

    public async Task<bool> Delete(int ProductId)
    {
        var query = $@"DELETE FROM {TableNames.products} WHERE product_id = @ProductId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { ProductId }) > 0;
    }

    public async Task<Products> GetById(int ProductId)
    {
        var query = $@"SELECT * FROM {TableNames.products} WHERE product_id = @ProductId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Products>(query, new { ProductId });
    }

    public async Task<List<Products>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.products}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Products>(query)).AsList();
    }

    // public async Task<List<Products>> GetProductByOrderId(int OrderId)
    // {
    //        var query = $@"SELECT p.* FROM {TableNames.products} p 
    //     LEFT JOIN {TableNames.orders} o ON o.order_id = p.order_id 
    //     WHERE p.order_id = @OrderId";

    //     // LEFT JOIN {TableNames.guest} g ON g.id = s.guest_id 

    //     using (var con = NewConnection)
    //         return (await con.QueryAsync<Products>(query, new { OrderId })).AsList();
    // }

    public async Task<bool> Update(Products Item)
    {
        var query = $@"UPDATE {TableNames.products} 
        SET product_name = @ProductName WHERE product_id = @ProductId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}