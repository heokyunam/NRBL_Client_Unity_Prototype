using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Unit : MonoBehaviour
    {
        public GameObject unitPallette;
        public int id;
        public XElement balance;
        public int x, y;
        // Use this for initialization
        void Start()
        {
            this.unitPallette = GameObject.Find("UnitPallette");
            this.balance = unitPallette.GetComponent<UnitManager>().getElement(id);
        }

        //타일의 attach는 외부에서 SelectHelper를 통해 이루어진다
        public void SetPosition(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void UpdateEveryTurn()
        {


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
        {
            return int.Parse(this.balance.Element(key).Value);
        }
    }
}
