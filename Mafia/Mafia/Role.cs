using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia
{
    public class Role
    {
        private String name;
        private Boolean killpower;
        private int targets;
        private Boolean goodness;
        private Boolean taken;

        public Role()
        {
            this.name = null;
            this.killpower = false;
            this.targets = 0;
            this.goodness = true;
            this.taken = false;
        }

        public void setName(String name) { this.name = name; }
        public void setKillpower(Boolean killpower) { this.killpower = killpower; }
        public void setTargets(int targets) { this.targets = targets; }
        public void setGoodness(Boolean goodness) { this.goodness = goodness; }
        public void setTaken(Boolean taken) { this.taken = taken; }

        public String getName() { return name; }
        public Boolean getKillpower() { return killpower; }
        public int getTargets() { return targets; }
        public Boolean getGoodness() { return goodness; }
        public Boolean getTaken() { return taken; }
    }
}
