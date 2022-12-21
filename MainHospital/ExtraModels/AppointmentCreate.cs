using Domain.Logic;
using Domain.Models;
using System.Xml.Linq;

namespace MainHospital.ExtraModels
{
    public class AppointmentCreate
    {
        public Doctor doctor { get; set; }
        public User patient { get; set; }

    }

}
