using SQLite;
using StayFitz.Models;

public class DatabaseService
{
//    private SQLiteAsyncConnection _db;

//    public async Task Init()
//    {
//        if (_db != null)
//            return;

//        var path = Path.Combine(FileSystem.AppDataDirectory, "StayFitz.db");

//        _db = new SQLiteAsyncConnection(path);

//        await _db.CreateTableAsync<MealModel>();
//    }

//    // ✅ GET ALL
//    public async Task<List<MealModel>> GetMealsAsync()
//    {
//        await Init();
//        return await _db.Table<MealModel>().ToListAsync();
//    }

//    // ✅ INSERT
//    public async Task AddMealAsync(MealModel meal)
//    {
//        await Init();
//        await _db.InsertAsync(meal);
//    }

//    // ✅ DELETE
//    public async Task DeleteMealAsync(MealModel meal)
//    {
//        await Init();
//        await _db.DeleteAsync(meal);
//    }
}