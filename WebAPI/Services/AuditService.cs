using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;
using System.Reflection;

namespace Setia.Services
{
    public class AuditService : IAudit
    {
        private readonly SetiaContext _context;
        private readonly ILogger<AuditService> _logger;
        private readonly IMapper _mapper;
        private readonly IAuth _auth;

        public AuditService
        (
            SetiaContext context,
            ILogger<AuditService> logger,
            IMapper mapper,
            IAuth auth
        )
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;
            _auth = auth;
        }

        public async Task<IEnumerable<AuditModel>> GetAll()
        {
            try
            {
                return await _context.Audit.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return [];
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public IEnumerable<AuditModel> GetAllWithFilter(AuditModel filter)
        {
            try
            {
                var query = _context.Audit.AsQueryable();
                var result = AddFilter(query, filter);
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return [];
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Add(AuditModel model)
        {
            try
            {
                await _context.Audit.AddAsync(_mapper.Map<AuditModel>(model));
                await _context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Update(AuditModel model)
        {
            try
            {
                _context.Audit.Update(_mapper.Map<AuditModel>(model));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var auditToDelete = await _context.Audit.FindAsync(id);
                if (auditToDelete != null)
                {
                    _context.Audit.Remove(auditToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, this.GetType().FullName);
                return false;
            }
            finally
            {
                _logger.LogInformation(this.GetType().FullName);
            }
        }

        public string CompareObjects<T>(T obj1, T obj2)
        {
            Type type = typeof(T);
            PropertyInfo[] properties = type.GetProperties();

            var differences = new Dictionary<string, string>();

            foreach (PropertyInfo property in properties)
            {
                string value1 = property.GetValue(obj1)?.ToString();
                string value2 = property.GetValue(obj2)?.ToString();

                if (value1 != value2)
                {
                    differences.Add(property.Name, $"Object 1 value: {value1}, Object 2 value: {value2}");
                }
            }

            return differences.ToString();
        }

        private static IQueryable<AuditModel> AddFilter(IQueryable<AuditModel> query, AuditModel filter)
        {
            if (filter != null)
            {
                foreach (var property in typeof(AuditModel).GetProperties())
                {
                    query = query.Where(item => property.GetValue(item).Equals(property.GetValue(filter)));
                }
            }
            return query;
        }
    }
}
