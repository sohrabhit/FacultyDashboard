using Data.Repositories;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.DataInitializer
{
    public class RoleDataInitializer : IDataInitializer
    {
        private readonly IRepository<Role> repository;

        public RoleDataInitializer(IRepository<Role> repository)
        {
            this.repository = repository;
        }

        public void InitializeData()
        {
            if (!repository.TableNoTracking.Any(p => p.Name == "admin"))
            {
                repository.Add(new Role
                {
                    Name = "admin",
                    Description = "admin"
                });
            }
            //if (!repository.TableNoTracking.Any(p => p.Name == "دسته بندی اولیه 2"))
            //{
            //    repository.Add(new Category
            //    {
            //        Name = "دسته بندی اولیه 2"
            //    });
            //}
            //if (!repository.TableNoTracking.Any(p => p.Name == "دسته بندی اولیه 3"))
            //{
            //    repository.Add(new Category
            //    {
            //        Name = "دسته بندی اولیه 3"
            //    });
            //}
        }
    }
}
