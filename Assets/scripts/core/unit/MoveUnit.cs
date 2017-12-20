using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.core.unit
{
    public class MoveUnit : Unit
    {
        public MoveUnit(RegionKind kind, int x, int y) : base(kind, x, y) { }

        public override void Update()
        {
            if (Region == RegionKind.Top)
            {
                this.y--;
            }
            else if (Region == RegionKind.Bottom)
            {
                this.y++;
            }
        }

        //1. 이동유닛끼리 충돌해도 서로 사라짐
        //2. 건물이랑 충돌해도 사라짐.
        // 결국 둘중하나만 MoveUnit이면 무조건 상관없음
        /*여기서 반환을 true로 한다는 건 자신과 충돌대상 둘 다 제거해달라는 의미이다.*/
        public override bool Collide(Unit unit)
        {
            return this.X == unit.X && this.Y == unit.Y;
        }
    }
}
