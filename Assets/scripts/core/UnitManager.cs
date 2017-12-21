using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Assets.scripts.core
{
    public class UnitManager : MonoBehaviour
    {
        private XElement balanceData;
        public void Awake()
        {
            //밸런스 데이터를 불러온다.
            balanceData = XElement.Load("assets/datas/data.xml");

            //prototype은 일종의 디폴트값을 의미함. 
            XElement prototype = balanceData.Element("prototype");

            //모든 밸런스를 돌면서 prototype의 속성들중 유닛에 존재하지 않는 것을 찾아 대입을 시켜준다.
            foreach(var e in balanceData.Elements("unit"))
            {
                foreach(var p in prototype.Elements())
                {
                    XElement temp = e.Element(p.Name);
                    //prototype엔 존재하는데, unit 데이터엔 존재하지 않는 경우
                    if (e.Element(p.Name) == null)
                    {
                        e.Add(p);
                    }
                }
            } 
        }

        public XElement getElement(int id)
        {
            //Linq쿼리로 찾는다. 그냥 foreach로 찾는것보단 빠르지 않을까?
            var result = from xe in balanceData.Elements("unit")
                         where xe.Element("id").Value == id + ""
                         select xe;
            return result.First();
        }
    }
}
