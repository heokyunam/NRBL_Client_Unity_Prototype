using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.scripts.core.unit;

namespace Assets.scripts.core
{
    public class UnitManager
    {
        private LinkedList<Unit> topUnits = new LinkedList<Unit>();
        private LinkedList<Unit> bottomUnits = new LinkedList<Unit>();
        //자원등에 대해선 이미 만족했다는 가정하에 일을 처리한다.
        //그러므로 이 함수를 호출하기전에 자원들을 확인할 필요가 있다
        public void build(Unit unit)
        {
            RegionKind region = unit.Region;
            if (region == RegionKind.Top)
            {
                topUnits.AddLast(unit);
            }
            else if(region == RegionKind.Bottom)
            {
                bottomUnits.AddLast(unit);
            }
        }


        public void Update()
        {
            //1. 이동유닛은 앞으로 전진을 한다
            //2. 이동유닛이 건물과 충돌할 시, 건물과 이동유닛은 사라진다
            //3. 이동유닛이 끝에 닿을 경우 상대 진영에 데미지를 입힌다 => RegionManager를 만들어 거기서 처리하는게 좋을 것 같다

            //1. 이동유닛은 앞으로 전진을 한다
            for (int i = 0; i < topUnits.Count; i++)
            {
                topUnits.ElementAt(i).Update();
            }
            for (int i = 0; i < bottomUnits.Count; i++)
            {
                bottomUnits.ElementAt(i).Update();
            }

            //2. 이동유닛이 건물과 충돌할 시, 건물과 이동유닛은 사라진다
            for (int i = 0; i < topUnits.Count; i++)
            {
                for(int j = 0; j < bottomUnits.Count; j++)
                {
                    Unit top = topUnits.ElementAt(i);
                    Unit bottom = bottomUnits.ElementAt(j);
                    if(top.Collide(bottom))
                    {
                        topUnits.Remove(top);
                        bottomUnits.Remove(bottom);
                    }
                }
            }
        }

    }
}
