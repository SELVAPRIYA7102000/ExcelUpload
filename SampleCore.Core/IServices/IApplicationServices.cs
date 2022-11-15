﻿using SampleCore.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleCore.Core.IServices
{
    public interface IApplicationServices
    {
        public void CreateApplicationEntry(Application application);

        List<Application> Reads();
        public Application Detail(int person_id);
    }
}
