﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeKeeper.API.Models;
using TimeKeeper.Domain;

namespace TimeKeeper.API.Factory
{
    public static class MasterFactory
    {
        public static string Monogram(this string str)
        {
            string[] S = str.Split();
            string M = "";
            foreach (string x in S) M += x.Substring(0, 1);
            return M.ToUpper();
        }

        public static MasterModel Master(this Role r)
        {
            return new MasterModel
            {
                Id = r.Id,
                Name = r.Name
            };
        }

        public static MasterModel Master(this Team t)
        {
            return new MasterModel
            {
                Id = t.Id,
                Name = t.Name
            };
        }

        public static MasterModel Master(this Member m, string what)
        {
            return new MasterModel
            {
                Id = (what == "team") ? m.Employee.Id : m.Team.Id,
                Name = ((what == "team") ? m.Employee.FullName : m.Team.Name) + ", " + m.Role.Name.Monogram()
            };
        }

        public static MasterModel Master(this Project p)
        {
            return new MasterModel
            {
                Id = p.Id,
                Name = p.Name + " [" + p.Name.Monogram() + "]"
            };
        }

        public static MasterModel Master(this Customer c)
        {
            return new MasterModel
            {
                Id = c.Id,
                Name = c.Name + " [" + c.Image + "]"
            };
        }

        public static MasterModel Master(this Employee e)
        {
            return new MasterModel
            {
                Id = e.Id,
                Name = $"{e.FullName}, {e.Position}"
            };
        }
    }
}
