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

        public void UpdateEveryTurn()
        {
            IEnumerator<Unit> it = units.GetEnumerator();
            while (it.MoveNext())
            {
                Unit unit = it.Current;
                unit.UpdateEveryTurn();
            }
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return units.GetEnumerator();
        }
    }
}
