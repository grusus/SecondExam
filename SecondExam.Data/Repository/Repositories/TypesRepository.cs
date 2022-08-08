using SecondExam.Data.Repository.Interfaces;

namespace SecondExam.Data.Repository.Repositories
{
    public class TypesRepository : ITypesRepository
    {
        private readonly ApiContext _context;
        public TypesRepository(ApiContext injectedContext)
        {
            _context = injectedContext;
        }
        public async Task<MaterialType?> CreateAsync(MaterialType entity)
        {
            EntityEntry<MaterialType> addedMaterialType = await _context.Types.AddAsync(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }

        public async Task<bool?> DeleteAsync(int id)
        {
            MaterialType? soughtMaterialType = await _context.Types.FindAsync(id);
            if (soughtMaterialType == null) return null;
            _context.Types.Remove(soughtMaterialType);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return true;
            return null;
        }

        public async Task<IEnumerable<MaterialType>> RetrieveAllAsync()
        {
            return await _context.Types.ToListAsync();
        }

        public async Task<MaterialType?> RetrieveAsync(int id)
        {
            return await _context.Types.FindAsync(id);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<MaterialType?> UpdateAsync(MaterialType entity)
        {
            _context.Types.Update(entity);
            int affectedRows = await SaveChangesAsync();
            if (affectedRows == 1) return entity;
            return null;
        }
    }
}
