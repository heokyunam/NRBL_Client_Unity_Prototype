using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Player : MonoBehaviour
    {
        private UnitGroup unitGroup = new UnitGroup();
        public Enemy enemy;
        public int coin;
        public bool isMyTurn;

        public int Coin { get { return coin; } set { coin = value; } }
        public bool IsMyTurn { get { return isMyTurn; } set { isMyTurn = value; } }
        
        // Use this for initialization
        void Awake()
        {
            coin = 1;
            isMyTurn = true;
            this.enemy = transform.root.Find("Enemy").GetComponent<Enemy>();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NextTurn()
        {
            //Debug.Log("Player NextTurn");
            enemy.GiveTurn();
        }

        //알아서 삭제되니 주의해야함
        public bool checkEnoughCoin(GameObject palletteUnit)
        {
            Unit unit = palletteUnit.GetComponent<Unit>();
            if(unit == null)
            {
                throw new CannotFindComponentException(
                    "checkEnoughCoin 매개변수에서 Unit이 아닌 게임오브젝트 발견");
            }
            
            bool returnVal = coin >= unit.Price;
            if(returnVal) coin -= unit.Price;
            return returnVal;
        }

        public IEnumerator<Unit> Units
        {
            get
            {
                return unitGroup.GetEnumerator();
            }
        }

        public UnitGroup UnitGroup
        {
            get
            {
                return unitGroup;
            }
        }
    }
}
