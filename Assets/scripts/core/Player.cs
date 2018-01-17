using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.core
{
    public class Player : MonoBehaviour
    {
        private UnitGroup unitGroup = new UnitGroup();
        public static int REASON_COIN = 1, REASON_CAPACITY = 2;
        public Enemy enemy;
        public int coin, hp, capacity;
        private bool isMyTurn;
        private Transform tfPlayerHP, tfPlayerCoin;

        public int Coin {
            get { return coin; }
            set
            {
                this.coin = value;
                tfPlayerCoin.GetComponent<Text>().text = value + "";
            }
        }
        public bool IsMyTurn { get { return isMyTurn; } set { isMyTurn = value; } }
        public int Hp {
            get { return hp; }
            set {
                hp = value;
                tfPlayerHP.GetComponent<Text>().text = value + "";
            }
        }
        public int Capacity { get { return capacity; } set { capacity = value; } }
        
        // Use this for initialization
        void Awake()
        {
            isMyTurn = true;
            this.enemy = transform.root.Find("Enemy").GetComponent<Enemy>();
            this.tfPlayerHP = GameObject.Find("Canvas").transform.Find("PlayerHP").Find("Text");
            this.tfPlayerCoin = GameObject.Find("Canvas").transform.Find("PlayerGold").Find("Text");
            this.Hp = 5;
            this.Coin = 1;
            this.Capacity = 1;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void NextTurn()
        {
            UnitGroup.UpdateEveryTurn(enemy.UnitGroup);

            enemy.Hp -= UnitGroup.AttackCastleDamage();
            enemy.GiveTurn();
        }

        
        /**알아서 삭제되니 주의해야함
            코인 계산도 알아서 이루어짐
         */
        public bool checkEnough(GameObject palletteUnit)
        {
            Unit unit = palletteUnit.GetComponent<Unit>();
            if(unit == null)
            {
                throw new CannotFindComponentException(
                    "checkEnoughCoin 매개변수에서 Unit이 아닌 게임오브젝트 발견");
            }
            
            bool returnVal = this.Coin >= unit.Price;
           
            if (returnVal)
            {
                this.Coin -= unit.Price;
            }

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
