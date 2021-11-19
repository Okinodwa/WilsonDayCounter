using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilsonDayCounter.Business.Logic;
using WilsonDayCounter.Data;

namespace WilsonDayCounter.Business.Models
{
    public class VisitorLog
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "Please provide a Name."), StringLength(255, ErrorMessage = "Name must be less than 256 characters.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please provide a valid Date of Birth.")]
        public DateTime? DateOfBirth { get; set; }

        public DateTime DateEntered { get; set; }

        public VisitorLog() { }
        public VisitorLog(string name, DateTime dateOfBirth)
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public VisitorLog(WilsonDayCounter.Data.Entities.VisitorLog databaseLog)
        {
            if(databaseLog != null)
            {
                Name = databaseLog.Name;
                DateOfBirth = databaseLog.DateOfBirth;
                DateEntered = databaseLog.DateEntered;
            }
        }

        public async Task Create()
        {
            var databaseLog = new WilsonDayCounter.Data.Entities.VisitorLog
            {
                Name = Name,
                DateOfBirth = DateOfBirth ?? default
            };
            
            using (var context = new DatabaseContext())
            {
                context.VisitorLogs.Add(databaseLog);
                await context.SaveChangesAsync();
            }
        }

        public static IEnumerable<VisitorLog> Read()
        {
            using(var context = new DatabaseContext())
            {
                var allLogs = context.VisitorLogs
                    .AsEnumerable()
                    .Select(log => new VisitorLog(log));
                return allLogs.ToList();
            }
        }
        public static VisitorLog Read(int id)
        {
            if(id <= 0)
            {
                throw new InvalidArgumentException("ID parameter must be greater than 0");
            }
            using (var context = new DatabaseContext())
            {
                var databaseLog = context.VisitorLogs.FirstOrDefault(log => log.ID == id);
                return new VisitorLog(databaseLog);
            }
        }

        public void Update()
        {
            if (ID <= 0)
            {
                throw new InvalidArgumentException("ID parameter must be greater than 0");
            }
            using (var context = new DatabaseContext())
            {
                var existingLog = context.VisitorLogs.FirstOrDefault(log => log.ID == ID);
                if(existingLog != null)
                {
                    existingLog.Name = Name;
                    existingLog.DateOfBirth = DateOfBirth ?? default;
                    context.SaveChanges();
                }
            }
        }

        public void Delete()
        {
            if (ID <= 0)
            {
                throw new InvalidArgumentException("ID parameter must be greater than 0");
            }
            using (var context = new DatabaseContext())
            {
                var existingLog = context.VisitorLogs.FirstOrDefault(log => log.ID == ID);
                if (existingLog != null)
                {
                    context.VisitorLogs.Remove(existingLog);
                    context.SaveChanges();
                }
            }
        }
    }
}
