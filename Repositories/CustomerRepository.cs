using Dapper;
using Online.Models;
using Online.Utilities;

namespace Online.Repositories;

public interface ICustomerRepository
{
    Task<Customer> Create(Customer Item);
    Task<bool> Update(Customer Item);
    Task<bool> Delete(int Id);
    Task<List<Customer>> GetList();
    Task<Customer> GetById(int Id);
}

public class CustomerRepository : BaseRepository, ICustomerRepository
{
    public CustomerRepository(IConfiguration config) : base(config)
    {

    }

    public async Task<Customer> Create(Customer Item)
    {
        var query = $@"INSERT INTO {TableNames.customer} 
        (customer_name, gender, address,  mobile_number) 
        VALUES (@CustomerName, @Gender, @Address,@MobileNumber) 
        RETURNING *";

        using (var con = NewConnection)
            return await con.QuerySingleAsync<Customer>(query, Item);
    }

    public async Task<bool> Delete(int CustomerId)
    {
        var query = $@"DELETE FROM {TableNames.customer} WHERE customer_id = @CustomerId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, new { CustomerId }) > 0;
    }

    public async Task<Customer> GetById(int CustomerId)
    {
        var query = $@"SELECT * FROM {TableNames.customer} WHERE Customer_id = @CustomerId";

        using (var con = NewConnection)
            return await con.QuerySingleOrDefaultAsync<Customer>(query, new { CustomerId });
    }

    public async Task<List<Customer>> GetList()
    {
        var query = $@"SELECT * FROM {TableNames.customer}";

        using (var con = NewConnection)
            return (await con.QueryAsync<Customer>(query)).AsList();
    }

    public async Task<bool> Update(Customer Item)
    {
        var query = $@"UPDATE {TableNames.customer} 
        SET customer_name = @CustomerName, gender = @Gender, Address = @Address, 
        mobile_number = @MobileNumber WHERE Customer_id = @CustomerId";

        using (var con = NewConnection)
            return await con.ExecuteAsync(query, Item) > 0;
    }
}