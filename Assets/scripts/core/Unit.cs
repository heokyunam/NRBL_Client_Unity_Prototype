using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using Assets.scripts.ui;

namespace Assets.scripts.core
{
    public class Unit : MonoBehaviour
    {
        public GameObject unitPallette, autoMapper;
        public int id;
        public XElement balance;
        public string balanceForTest;
        public int x, y;
        public bool isEnemy = false;
        
        public bool IsEnemy { set { isEnemy = value; } }
        // Use this for initialization
        void Start()
        {
            this.Init();
        }

        public void Init()
        {
            Init(this.id);
        }

        public void Init(int id)
        {
            this.unitPallette = GameObject.Find("UnitPallette");
            this.autoMapper = GameObject.Find("AutoMapper");
            this.balance = unitPallette.GetComponent<UnitManager>().getElement(id);
            if (this.balance == null)
                throw new CannotFindComponentException("Unit 생성시 Balance 데이터에 접근이 안됨");
        }

        public void Init(int id, int x, int y)
        {
            this.Init(id);
            SetPosition(x, y);
        }

        //타일의 attach는 외부에서 SelectHelper를 통해 이루어진다
        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void UpdateEveryTurn()
        {
            if(Move > 0)
            {
                if(isEnemy)
                {
                    this.y -= Move;
                }
                else
                {
                    this.y += Move;
                }
                attach();    
            }
            if(Coin > 0)
            {
                Player player = GameObject.Find("Player").GetComponent<Player>();
                player.Coin += Coin;
                //Debug.Log(player.Coin);
            }
        }

        public void attach()
        {
            string name = "autotile(" + this.x + "," + this.y + ")";
            Transform tfAutoTile = this.autoMapper.transform.Find(name);
            GameObject goAutoTile = tfAutoTile.gameObject;
            if(goAutoTile == null)
            {
                throw new CannotFindComponentException(name + "을 찾지못함");
            }
            AutoTile tile = goAutoTile.GetComponent<AutoTile>();
            if (tile == null)
            {
                throw new CannotFindComponentException(name + " AutoTile 을 찾지못함");
            }
            tile.attach(this.gameObject);
        }

        public void attach(GameObject selected)
        {
            Vector3 temp = this.transform.localPosition;
            temp.z = -2;
            selected.transform.localPosition = temp;
        }

        public int Price { get { return getIntValue("price");  } }
        public int Power { get { return getIntValue("power");  } }
        public int Move { get { return getIntValue("move"); } }
        public int Coin { get { return getIntValue("coin"); } }

        public int getIntValue(string key)
        {;
            if (this.balance == null)
                throw new CannotFindComponentException("balance " + id + " is null");
            XElement xe = this.balance.Element(key);
            return int.Parse(xe.Value);
        }
    }
}
