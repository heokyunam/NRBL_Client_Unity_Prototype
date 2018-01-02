using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.scripts.core
{
    //여기에 UnitPallette를 병합하는게 낫겠음.
    //물론 에너미를 다 만들고 나서 나중에
    public class UnitGroup
    {
        private LinkedList<Unit> units;
        public UnitGroup()
        {
            units = new LinkedList<Unit>();
        }

        public void AddUnit(Unit unit)
        {
            if (unit == null)
            {
                throw new CannotFindComponentException(
                    "게임오브젝트에 유닛 스크립트가 할당되어 있지 않음");
            }

            units.AddLast(unit);
        }

        public void UpdateEveryTurn(UnitGroup enemy)
        {
            IEnumerator<Unit> it = units.GetEnumerator();
            while (it.MoveNext())
            {
                Unit unit = it.Current;
                unit.UpdateEveryTurn();
            }

            CollisionCheck(enemy);
        }

        public void CollisionCheck(UnitGroup enemy)
        {
            for(int i = 0; i < units.Count; i++)
            {
                for(int j = 0; j < enemy.units.Count; j++)
                {
                    Unit p = units.ElementAt(i);
                    Unit e = enemy.units.ElementAt(j);

                    if(p.isCollide(e))
                    {
                        //now we test this
                        p.Destroy();
                        e.Destroy();
                        units.Remove(p);
                        enemy.units.Remove(e);
                    }
                }
            }
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return units.GetEnumerator();
        }
    }
}
