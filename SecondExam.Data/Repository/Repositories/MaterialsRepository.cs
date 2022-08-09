using SecondExam.Data.Repository.Interfaces;

namespace SecondExam.Data.Repository.Repositories
{
    public class MaterialsRepository : IMaterialsRepository
    {
        private readonly ApiContext _context;
        public MaterialsRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<Material?> CreateAsync(Material entity)
        {
            EntityEntry<Material> addedMaterial = await _context.Materials.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            Material? soughtMaterial = await _context.Materials.FindAsync(id);
            if (soughtMaterial == null) return null;
            _context.Materials.Remove(soughtMaterial);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<Material>> RetrieveAllAsync()
        {
            return await _context.Materials.ToListAsync();
        }

        public async Task<Material?> RetrieveAsync(int id)
        {
            return await _context.Materials.FindAsync(id);
        }
        public async Task<Material?> RetrieveAsyncWithDetails(int id)
        {
            return await _context.Materials
                .Include(x => x.MatherialAuthor)
                .Include(x => x.MaterialType)
                .Include(x => x.MaterialReviews)
                .Where(x => x.MaterialId == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<Material?> UpdateAsync(Material entity)
        {
            _context.Materials.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}
