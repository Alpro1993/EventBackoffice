using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EventBackofficeBackend.Controllers
{
    public class Parameters
    {
        public int ID {get; set;} = default!;
        public Dictionary<string, int> IDs {get; set;} = default!; 
        public string StartDate {get; set;} = default!;
        public string EndDate {get; set;} = default!;

        public void ValidateParameters()
        {
            if (this.ID < 0) 
            {
                throw new ArgumentOutOfRangeException("Id", "Id must be greater than zero");
            }

            foreach (string name in IDs.Keys)
            {
                if (IDs[name] < 0)
                {
                    throw new ArgumentOutOfRangeException(name, name + " ID must be greater than zero");
                }
            }

            if (this.StartDate is not null)
            {
                validateDate(this.StartDate);
            }

            if (this.EndDate is not null)
            {
                validateDate(this.EndDate);
            }

            void validateDate(string _date)
            {
                try
                {
                    DateTime.ParseExact(_date, "dd/MM/yyyy", new CultureInfo("pt-PT"));
                }
                catch (FormatException e)
                {
                    throw new ArgumentException(e.GetBaseException().ToString());
                }
            }
        }
    }  
}