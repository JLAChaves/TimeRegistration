using TimeRegistration.Models;
using TimeRegistration.Data;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.Repositories
{
    public interface IContractRepository
    {
        public bool Create(Contract contract);
        public Contract[] ReadAll();
        public Contract ReadId(int id);
        public Contract[] ReadNames(string name);
        public bool Update(Contract contract);
        public bool UpdateTotalHours(int? id);
        public bool Delete(Contract contract);
    }

    public class ContractRepository : IContractRepository
    {
        private readonly TimeContext _context;

        public ContractRepository(TimeContext context)
        {
            _context = context;
        }

        public bool Create(Contract contract)
        {
            try
            {
                _context.Contracts.Add(contract);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public Contract[] ReadAll()
        {
            IQueryable<Contract> query = _context.Contracts.Include(h => h.TimeLogs);
            query = query.AsNoTracking().OrderBy(h => h.Id);
            return query.ToArray();
        }

        public Contract ReadId(int id)
        {
            IQueryable<Contract> query = _context.Contracts.Include(h => h.TimeLogs);
            query = query.AsNoTracking().OrderBy(h => h.Id);
            return query.FirstOrDefault(p => p.Id == id);
        }

        public Contract[] ReadNames(string name)
        {
            IQueryable<Contract> query = _context.Contracts.Include(h => h.TimeLogs);
            query = query.AsNoTracking().
                Where(h => h.Name.Contains(name)).
                OrderBy(h => h.Id);

            return query.ToArray();
        }

        public bool Update(Contract contract)
        {
            try
            {
                if (_context.Contracts.AsNoTracking().FirstOrDefault(p => p.Id == contract.Id) != null)
                {
                    _context.Contracts.Update(contract);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(Contract contract)
        {
            try
            {
                if (_context.Contracts.AsNoTracking().FirstOrDefault(p => p.Id == contract.Id) != null)
                {
                    _context.Contracts.Remove(contract);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateTotalHours(int? id)
        {
            try
            {
                var contract = _context.Contracts.FirstOrDefault(p => p.Id == id);
                contract.TotalHours = Math.Round(TotalHours(id), 2);
                contract.TotalValue = TotalValue(contract);
                _context.Contracts.Update(contract);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public double TotalHours(int? id)
        {
            return _context.TimesLogs.Where(p => p.ContractId == id).Sum(p => p.Hours);
        }

        public double TotalValue(Contract contract)
        {
            return contract.TotalHours * contract.ValuePerHour;
        }
    }
}
