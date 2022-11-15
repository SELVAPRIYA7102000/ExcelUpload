using SampleCore.Core.IRepositories;
using SampleCore.Core.Model;
using SampleCore.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SampleCore.Entity.Models;

namespace SampleCore.Repositories.Repositories
{
    public  class ApplicationRepository:IApplicationRepository
    {
        public JobApplicationContext entity;
        public ApplicationRepository(JobApplicationContext jobapplicationContext)
        {
            entity = jobapplicationContext;
        }

        #region Create
        public void CreateApplicationEntry(Core.Model.Application application)
        {
            if (application != null)
            {


                // Priya123Context entity = new Priya123Context();
                SampleCore.Entity.Models.JobApplication_Details person1 = new SampleCore.Entity.Models.JobApplication_Details();
                person1.Person_ID = application.PersonID;
                person1.Name = application.Name;
                person1.Applicant_Resume = application.ApplicantResume;
                person1.Gender = application.Gender;
                person1.Age = application.Age;
                person1.Email = application.Email;
                person1.Location = application.Location;
                person1.Full_Name = application.FullName;
                person1.SSLC_Marks = int.Parse(application.SSLCMarks);
                person1.HSC_Marks = int.Parse(application.HSCMarks);
                person1.Interests=application.Interests;
                person1.Skills=application.Skills;
                person1.Graduation_Percent = decimal.Parse(application.GraduationPercent);
                person1.Created_Time_Stamp = DateTime.Now;
                person1.Updated_Time_Stamp = DateTime.Now;
                entity.Add(person1);
                entity.SaveChanges();
            }
        }
        #endregion
        #region Read
        public List<Application> Reads()
        {
            List<Application> model = new List<Application>();
            var data = entity.JobApplication_Details.Where(x => x.Is_Deleted == false).ToList();
            foreach (var item in data)
            {
                Application person = new Application();
                person.PersonID = item.Person_ID;
                person.ApplicantResume = item.Applicant_Resume;
                person.Name = item.Name;              
                person.Gender = item.Gender;
                person.Email = item.Email;
                person.Location = item.Location;
                person.Age = item.Age;
                model.Add(person);
            }
            return model;
        }
        #endregion

        public Application Detail(int person_id)
        {


            Application pr = new Application();
            var data = entity.JobApplication_Details.Where(x => x.Person_ID== person_id).SingleOrDefault();

          

            pr.PersonID = data.Person_ID;
            pr.FullName = data.Full_Name;          
            pr.HSCMarks = Convert.ToString(data.HSC_Marks);
            pr.SSLCMarks = Convert.ToString(data.SSLC_Marks);
            pr.GraduationPercent = Convert.ToString(data.Graduation_Percent);
            pr.Interests = data.Interests;
            pr.Skills = data.Skills;
           
            return pr;

        }
    }
}
