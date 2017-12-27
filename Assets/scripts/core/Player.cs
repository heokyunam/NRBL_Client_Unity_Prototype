using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Player : MonoBehaviour
    {
        private LinkedList<Unit> units;
        public int coin;
        public bool isMyTurn;

        public int Coin { get { return coin; } set { coin = value; } }
        public bool IsMyTurn { get { return isMyTurn; } set { isMyTurn = value; } }
        
        // Use this for initialization
        void Start()
        {
            coin = 1;
            isMyTurn = true;
            units = new LinkedList<Unit>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UpdateEveryTurn()
        {
            IEnumerator<Unit> it = units.GetEnumerator();
            while(it.MoveNext())
            {
                Unit unit = it.Current;
                unit.UpdateEveryTurn();
                Debug.Log(unit.name);
            }
        }

        //알아서 삭제되니 주의해야함
        public bool checkEnoughCoin(GameObject palletteUnit)
        {
            Unit unit = palletteUnit.GetComponent<Unit>();
            if(unit == null)
            {
                throw new CannotFindComponentException("checkEnoughCoin 매개변수에서 Unit이 아닌 게임오브젝트 발견");
            }
            
            bool returnVal = coin >= unit.Price;
            if(returnVal) coin -= unit.Price;
            return returnVal;
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
    }
}
