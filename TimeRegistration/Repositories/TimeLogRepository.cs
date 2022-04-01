using TimeRegistration.Models;
using TimeRegistration.Data;
using Microsoft.EntityFrameworkCore;

namespace TimeRegistration.Repositories
{
    public interface ITimeLogRepository
    {
        public bool Create(TimeLog timeLog);
        public TimeLog[] ReadAll();
        public bool UpdateHourExit(int id);
        public bool Delete(TimeLog timeLog);
        public double Hours(TimeLog timeLog);
    }
    public class TimeLogRepository : ITimeLogRepository
    {
        private readonly TimeContext _context;

        public TimeLogRepository(TimeContext context)
        {
            _context = context;
        }

        public bool Create(TimeLog timeLog)
        {
            try
            {
                TimeNow(timeLog);
                _context.TimesLogs.Add(timeLog);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public TimeLog[] ReadAll()
        {
            IQueryable<TimeLog> query = _context.TimesLogs.AsNoTracking().OrderBy(h => h.Id);
            return query.ToArray();
        }

        public bool UpdateHourExit(int id)
        {
            try
            {
                var timeLogExit = _context.TimesLogs.FirstOrDefault(h => h.Id == id);
                HourExit(timeLogExit);
                _context.Update(timeLogExit);
                _context.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(TimeLog timeLog)
        {
            try
            {
                if (_context.TimesLogs.AsNoTracking().FirstOrDefault(h => h.Id == timeLog.Id) != null)
                {
                    _context.TimesLogs.Remove(timeLog);
                    _context.SaveChanges();
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public double Hours(TimeLog timeLog)
        {
            TimeSpan ts = timeLog.EndTime - timeLog.StartTime;
            return ts.TotalHours;
        }

        private void TimeNow(TimeLog timeLog)
        {
            timeLog.StartTime = DateTime.Now;
            timeLog.EndTime = DateTime.Now;
            timeLog.Hours = Math.Round(Hours(timeLog), 2);
        }

        private void HourExit(TimeLog timeLog)
        {
            timeLog.EndTime = DateTime.Now;
            timeLog.Hours = Math.Round(Hours(timeLog), 2);
        }
    }
}
