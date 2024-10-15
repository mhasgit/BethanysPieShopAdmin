namespace BethanysPieShopAdmin.Models.Repositories
{
    public interface IPieRepository
    {
        public Task<IEnumerable<Pie>> GetAllPiesAsync();
        public Task<Pie?> GetPieByIdAsync(int pieId);
    }
}
