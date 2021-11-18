using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilsonDayCounter.Business.Logic;
using WilsonDayCounter.Data;

namespace WilsonDayCounter.Business.DTO
{
    public class VisitorLog
    {
        public int ID { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }

        public DateTime DateEntered { get; set; }

        public VisitorLog(string name, DateTime dateOfBirth) 
        {
            Name = name;
            DateOfBirth = dateOfBirth;
        }

        public VisitorLog(WilsonDayCounter.Data.Models.VisitorLog databaseLog)
        {
            if(databaseLog != null)
            {
                Name = databaseLog.Name;
                DateOfBirth = databaseLog.DateOfBirth;
                DateEntered = databaseLog.DateEntered;
            }
        }

        public void Create()
        {
            var databaseLog = new WilsonDayCounter.Data.Models.VisitorLog();
            databaseLog.Name = Name;
            databaseLog.DateOfBirth = DateOfBirth;
            using (var context = new DatabaseContext())
            {
                context.VisitorLogs.Add(databaseLog);
                context.SaveChanges();
            }
        }

        public static IEnumerable<VisitorLog> Read()
        {
            using(var context = new DatabaseContext())
            {
                var allLogs = context.VisitorLogs
                    .AsEnumerable()
                    .Select(log => new VisitorLog(log));
                return allLogs;
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
                    existingLog.DateOfBirth = DateOfBirth;
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
