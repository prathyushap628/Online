using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface ITagsRepository
{
    Task<Tags> Create(Tags Item);
    Task<bool> Update(Tags Item);
    Task<bool> Delete(int Id);
    Task<List<Tags>> GetList();
    Task<Tags> GetById(int Id);
    Task<List<Tags>> GetProductByTagId(int CustomerId);


}

public class TagsRepository : BaseRepository, ITagsRepository
{
    public TagsRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Tags> Create(Tags Item)
    {
        var query = $@"INSERT INTO {TableNames.tags} ( tag_id, tag_name, price, order_id, product_id) 
        VALUES (@TagId, @TagName, @Price, @OrderId, @ProductId ) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Tags>(query, Item);
    }

    public async Task<bool> Delete(int TagId)
    {
        var query = $@"DELETE FROM {TableNames.tags} WHERE tag_id = @TagId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { TagId }) > 0;
    }

    public async Task<Tags> GetById(int TagId)
    {
        var query = $@"SELECT * FROM {TableNames.tags} WHERE tag_id = @TagId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Tags>(query, new { TagId });
    }

    public async Task<List<Tags>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.tags}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Tags>(query)).AsList();
    }

    public async Task<List<Tags>> GetProductByTagId(int ProductId)
    {
        var query = $@"SELECT t.* FROM {TableNames.tags} t 
        LEFT JOIN {TableNames.products} p ON p.product_id = t.product_id 
        WHERE t.product_id = @ProductId";

        // LEFT JOIN {TableNames.guest} g ON g.id = s.guest_id 

        using (var con = NewConnection)
            return (await con.QueryAsync<Tags>(query, new { ProductId })).AsList();
    }

    public async Task<bool> Update(Tags Item)
    {
        var query = $@"UPDATE {TableNames.tags} 
        SET tag_name = @TagName, price = @Price  WHERE tag_id = @TagId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}