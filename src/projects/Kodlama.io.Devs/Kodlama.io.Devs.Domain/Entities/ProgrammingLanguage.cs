﻿using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
    public class ProgrammingLanguage : Entity
    {
        public ProgrammingLanguage()
        {
            Technologies = new HashSet<Technology>();
        }

        public ProgrammingLanguage(int id,string name):this()
        {
            Id = id;
            Name = name;
        }
        public string Name { get; set; }

        public virtual ICollection<Technology> Technologies { get; set; }
    }
}
