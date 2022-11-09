using DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Converters.FromDomain
{
    public static class DomainToSheduleModelConverter
    {
        public static SheduleModel? ToSheduleModel(this Shedule model)
        {
            return new SheduleModel
            {
                DoctorId = model.DoctorId,
                StartWorking = model.StartWorking,
                EndWorking = model.EndWorking
            };
        }
    }
}
