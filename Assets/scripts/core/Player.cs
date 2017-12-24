using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Player : MonoBehaviour
    {
        public int coin;
        public bool isMyTurn;

        public int Coin { get { return coin; } }
        public bool IsMyTurn { get { return isMyTurn; } set { isMyTurn = value; } }
        
        // Use this for initialization
        void Start()
        {
            coin = 1;
            isMyTurn = true;
        }

        // Update is called once per frame
        void Update()
        {

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
    }
}
