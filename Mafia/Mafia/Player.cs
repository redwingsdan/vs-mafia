using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mafia
{
    public class Player
    {
        private String name;
        private Role role;
        private Player[] chosen;

        public Player()
        {
            this.name = null;
            this.role = null;
            this.chosen = null;
        }

        public void setName(String name) { this.name = name; }
        public void setRole(Role role) { this.role = role; }
        public void setChosen(Player[] chosen) { this.chosen = chosen; }

        public String getName() { return name; }
        public Role getRole() { return role; }
        public Player[] getChosen() { return chosen; }
    }
}
