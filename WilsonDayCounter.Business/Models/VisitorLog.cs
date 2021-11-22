using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WilsonDayCounter.Business.Logic;
using WilsonDayCounter.Business.Logic.Helpers;
using WilsonDayCounter.Business.Logic.Validation;
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
            var validationResult = Validate();
            if(validationResult.IsValid)
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
            else
            {
                throw new InvalidArgumentException(validationResult.Message);
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

        private Logic.Validation.ValidationResult Validate()
        {
            var nameValidation = ValidateName();
            var dateOfBirthValidation = ValidateDateOfBirth();
            return new Logic.Validation.ValidationResult
            { 
                IsValid = nameValidation.IsValid && dateOfBirthValidation.IsValid,
                Message = string.Join("; ", nameValidation.Message, dateOfBirthValidation.Message)
            };
        }

        private Logic.Validation.ValidationResult ValidateName()
        {
            var messages = new List<string>();
            var nameValidationResult = new Logic.Validation.ValidationResult
            {
                IsValid = true
            };

            if(string.IsNullOrWhiteSpace(Name))
            {
                nameValidationResult.IsValid = false;
                messages.Add("Name must have a value");
            }

            var nameMaxLengthAttribute = AttributesHelper.GetStringLengthAttribute(GetType(), "Name");

            if (nameMaxLengthAttribute != null && Name != null && Name.Length > nameMaxLengthAttribute.MaximumLength)
            {
                nameValidationResult.IsValid = false;
                messages.Add(nameMaxLengthAttribute.ErrorMessage ?? $"Name must be less than {nameMaxLengthAttribute.MaximumLength} characters");
            }

            nameValidationResult.Message = string.Join("; ", messages);
            return nameValidationResult;
        }

        private Logic.Validation.ValidationResult ValidateDateOfBirth()
        {
            var messages = new List<string>();
            var dateOfBirthValidationResult = new Logic.Validation.ValidationResult
            {
                IsValid = true
            };

            if(DateOfBirth == null)
            {
                dateOfBirthValidationResult.IsValid = false;
                messages.Add("Date of Birth must have a value");
            }
            else if(DateOfBirth.Value.Date > DateTime.Now.Date)
            {
                dateOfBirthValidationResult.IsValid = false;
                messages.Add("Date of Birth must be before today");
            }
            else if(DateOfBirth.Value.Date < SqlDateTime.MinValue)
            {
                dateOfBirthValidationResult.IsValid = false;
                messages.Add($"Date of Birth myst be after {((DateTime)SqlDateTime.MinValue).ToShortDateString()}");
            }

            dateOfBirthValidationResult.Message = string.Join("; ", messages);
            return dateOfBirthValidationResult;
        }
    }
}
