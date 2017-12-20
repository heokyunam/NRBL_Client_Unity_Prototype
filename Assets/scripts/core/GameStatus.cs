using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core.unit;

namespace Assets.scripts.core
{
    //1. 유닛 생성
    //1. 팔레트 내에서 원하는 유닛을 선택한다.
    //2. 원하는 위치를 선택하기 위해 타일 선택을 해야한다.
    //3. 유닛을 생성할만큼 자원이 충분한지 확인한다.(턴, 골드, 최대유닛, 위치)
    public class GameStatus
    {
        private UnitManager manager;
        private int selectedPalleteUnit;

        public GameStatus()
        {
            this.manager = new UnitManager();
        }

        public bool PalleteSelected
        {
            get
            {
                return selectedPalleteUnit >= 0;
            }
        }

        public int PalleteUnit
        {
            get
            {
                return this.selectedPalleteUnit;
            }
            set
            {
                this.selectedPalleteUnit = value;
            }
        }

        public Unit makeUnit(RegionKind kind, int x, int y)
        {
            //return new unit.Unit(kind, x, y);
        }

        public void build(RegionKind kind, int x, int y)
        {
            this.manager.build(makeUnit(kind, x, y));
        }
    }
}
