using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.scripts.core;

namespace Assets.scripts.core.unit
{
    public abstract class Unit
    {
        protected int x, y;
        private RegionKind kind;
        public Unit(RegionKind kind, int x, int y)
        {
            this.kind = kind;
            this.x = x;
            this.y = y;
        }
        public RegionKind Region { get { return kind; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        //충돌은 MoveUnit과 Unit사이에서만 확인하는데, 여기 메소드에서 확인하려면
        //instanceof를 체크하는 등 더 복잡하게 해야함.
        //MoveUnit에서 오버라이딩으로 쓸 수 있게 하기위해서 존재할 뿐임
        public virtual bool Collide(Unit unit)
        {
            if(unit.GetType() == typeof(MoveUnit))
            {
                return unit.Collide(this);
            }
            return false;
        }

        public abstract void Update();
    }
}
