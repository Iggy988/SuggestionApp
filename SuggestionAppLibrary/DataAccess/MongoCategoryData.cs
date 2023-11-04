using Microsoft.Extensions.Caching.Memory;

namespace SuggestionAppLibrary.DataAccess;
public class MongoCategoryData : ICategoryData
{
    private readonly IMongoCollection<CategoryModel> _categories;
    private readonly IMemoryCache _cache;
    private const string CacheName = "CategoryData";

    // koristimo memory cache zato sto se ova data nece mijenjati tako cesto
    public MongoCategoryData(IDbConnection db, IMemoryCache cache)
    {
        _cache = cache;
        _categories = db.CategoryCollection;
    }

    //koristimo cache
    public async Task<List<CategoryModel>> GetAllCategories()
    {
        var output = _cache.Get<List<CategoryModel>>(CacheName);
        //ako ne nadjemo data u cache
        if (output is null)
        {
            var results = await _categories.FindAsync(_ => true);
            output = results.ToList();

            //stavljamo data u cache (keep in cache for 1 day)
            _cache.Set(CacheName, output, TimeSpan.FromDays(1));

        }
        return output;
    }

    public Task CreateCategory(CategoryModel category)
    {
        return _categories.InsertOneAsync(category);
    }
}
