using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.core
{
    public class Player
    {
        private bool turn = false;
        private RegionKind region;
        public Player()
        {

        }

        public bool Turn = { get { return turn; }  }
        public RegionKind Region { get { return region; } }
    }
}
