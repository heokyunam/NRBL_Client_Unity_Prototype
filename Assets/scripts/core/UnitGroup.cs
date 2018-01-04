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
        private LinkedList<Unit> units = new LinkedList<Unit>();

        public UnitGroup()
        {
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

        //모든 턴별로 모든 에너미가 접근해 턴을 실행하며, 충돌 체크를 한다.
        //플레이어도 한번, 에너미도 한번 실행되어야 한다
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

        //다음 턴 이동시 유닛들의 좌표가 바뀌기 때문에 이로 인해
        //생기는 문제들을 해결해주어야 한다
        public void CollisionCheck(UnitGroup enemy)
        {
            IEnumerator<Unit> players = units.GetEnumerator();
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
                        break;
                    }
                }
            }
        }

        //내가 이동한 후에 처리를 하기 때문에 상대방에 입힐 데미지를 구한다
        public int AttackCastleDamage()
        {
            int sum = 0;
            for(int i = 0; i < units.Count; i++)
            {
                Unit p = units.ElementAt(i);
                if(p.isAttackCastle())
                {
                    sum++;
                    p.Destroy();
                    units.Remove(p);
                }
            }

            return sum;
        }

        public IEnumerator<Unit> GetEnumerator()
        {
            return units.GetEnumerator();
        }
    }
}
