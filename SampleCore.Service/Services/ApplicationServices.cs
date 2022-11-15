using SampleCore.Core.IRepositories;
using SampleCore.Core.IServices;
using SampleCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Service.Services
{
    public class ApplicationServices:IApplicationServices
    {
        private readonly IApplicationRepository _applicationRepository;

        public ApplicationServices(IApplicationRepository applicationRepository)
        {
            _applicationRepository = applicationRepository;
        }
        public void CreateApplicationEntry(Application application)
        {
            //business condition
            if (application.Name != string.Empty && application.Name != string.Empty)
            {
                _applicationRepository.CreateApplicationEntry(application);
            }
        }
        public List<Application> Reads()
        {
            return _applicationRepository.Reads();
        }
        public Application Detail(int person_id)
        {
            return _applicationRepository.Detail(person_id);

        }
    }
}
