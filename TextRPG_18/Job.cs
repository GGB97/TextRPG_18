using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG_18
{
    internal class Job
    {
        public string skill;
        public int mp;
        public int atk;
        public string description;

        public Job(string skill, int mp, int atk, string description)
        {
            skill = skill;
            mp = mp;
            atk = atk;
            this.description = description;
        }

        
    }
}
