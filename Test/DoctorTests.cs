using Domain.Models;
using System.Xml.Linq;

namespace Test
{
    public class DoctorTests
    {
        private readonly DoctorService _doctorService;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public DoctorTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object);
        }

    }
}