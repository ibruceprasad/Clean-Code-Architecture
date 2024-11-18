using library.Services.Domain.Dtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    public interface IDataValidaion
    {
        public string ValidateData<T>(T dto) where T : class;
    }
    public class DataValidaion : IDataValidaion
    {
        public DataValidaion() { }
        public string ValidateData<T>(T dto) where T : class
        {
            var validationCOntext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dto, validationCOntext, validationResults, true);
            if (!isValid)
            {
              
                var message = string.Join(", ", validationResults.Select(x=>x.ErrorMessage??"").ToList());
                /*var message = new StringBuilder();
                foreach (var result in validationResults)
                {
                    message = message.Append(result.ErrorMessage ?? "Error Unknown");
                }*/
                return message.ToString();
            }
            return string.Empty;
        }
    }
}
