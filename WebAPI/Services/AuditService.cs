using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Setia.Data;
using Setia.Models;
using Setia.Services.Interfaces;

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

        public async Task<string> Add(AuditModel model)
        {
            try
            {
                model.Id = 0;
                model.LastUpdateDate = DateTime.Now;
                model.Id_LastUpdateBy = await _auth.GetCurrentUser();
                await _context.Audit.AddAsync(_mapper.Map<AuditModel>(model));
                await _context.SaveChangesAsync();

                return "Added";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public async Task<IEnumerable<AuditModel>> GetAll()
        {
            try
            {
                return await _context.Audit.ToListAsync();
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        public async Task<IEnumerable<AuditModel>> GetAllWithFilter(AuditModel filter)
        {
            try
            {
                if (filter == null)
                {
                    return [];
                }

                var query = _context.Audit.AsQueryable();
                var result = await AddFilter(query, filter);
                return result.ToList();
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        public async Task<ActionResult<bool>> Update(AuditModel model)
        {
            try
            {
                model.LastUpdateDate = DateTime.Now;
                _context.Audit.Update(_mapper.Map<AuditModel>(model));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<ActionResult<bool>> Delete(int id)
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
                return false;
            }
        }

        private static async Task<IQueryable<AuditModel>> AddFilter(IQueryable<AuditModel> query, AuditModel filter)
        {
            if (filter == null || query == null)
            {
                return query;
            }

            foreach (var property in typeof(AuditModel).GetProperties())
            {
                var value = property.GetValue(filter);

                if (value != null && value.GetType() != typeof(string) && !value.Equals(Activator.CreateInstance(property.PropertyType)))
                {
                    if (property.PropertyType == typeof(string))
                    {
                        query = query.Where(item => (string)property.GetValue(item) == (string)value);
                    }
                    else
                    {
                        query = query.Where(item => property.GetValue(item).Equals(value));
                    }
                }
            }

            return query;
        }
    }
}