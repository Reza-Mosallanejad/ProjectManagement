using ProjectManagement.Data.Context;
using ProjectManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectManagement.xUnitTest
{
    public class SeedData
    {
        public static void Seed(PMDbContext context, params Type[] types)
        {
            if (types == null || context == null)
                return;

            //Project
            if (types.Contains(typeof(User)))
            {
                context.Users.Add(new User
                {
                    FullName = "Reza Mosalla",
                    Username = "Reza",
                    Password = "1234",
                    Email = "Reza@Mosalla.com",
                });
            }

            //Project
            if (types.Contains(typeof(Project)))
            {
                context.Projects.Add(new Project
                {
                    UserId = 1,
                    Caption = "Project1",
                    Employer = "Employer1",
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    Description = "Description Description",
                });
                context.Projects.Add(new Project
                {
                    UserId = 1,
                    Caption = "Project2",
                    Employer = "Employer2",
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    Description = "Description Description",
                });
                context.Projects.Add(new Project
                {
                    UserId = 1,
                    Caption = "Project3",
                    Employer = "Employer3",
                    FromDate = DateTime.Now,
                    ToDate = DateTime.Now,
                    Description = "Description Description",
                });
            }


            //save
            context.SaveChanges();
        }
    }
}
