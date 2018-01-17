using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.IO;
using UnityEngine;

namespace Assets.scripts.core
{
    //이거도 차라리 유닛그룹에 합치는게 나을까? 하지만 유닛 그룹은 두개다
    //다만 실제로 만들 때에는 클래스를 UnitBalance로 바꾸자
    public class UnitManager : MonoBehaviour
    {
        private XElement balanceData;
        private const string filename = "datas/units";
        private XElement BalanceData
        {
            get
            {
                if(balanceData == null)
                    MakeBalanceData();
                return balanceData;
            }
        }
        public void Awake()
        {
        }

        public void MakeBalanceData()
        {
            //밸런스 데이터를 불러온다.
            TextAsset txt = Resources.Load<TextAsset>(filename);
            //Debug.Log(txt);
            balanceData = XElement.Load(new StringReader(txt.text));//"assets/datas/data.xml");

            //prototype은 일종의 디폴트값을 의미함. 
            XElement prototype = balanceData.Element("prototype");

            //모든 밸런스를 돌면서 prototype의 속성들중 유닛에 존재하지 않는 것을 찾아 대입을 시켜준다.
            foreach (var e in balanceData.Elements("unit"))
            {
                foreach (var p in prototype.Elements())
                {
                    XElement temp = e.Element(p.Name);
                    //prototype엔 존재하는데, un it 데이터엔 존재하지 않는 경우
                    if (temp == null)
                    {
                        e.Add(p);
                    }
                }
            }
        }

        public XElement getElement(int id)
        {
            //Linq쿼리로 찾는다. 그냥 foreach로 찾는것보단 빠르지 않을까?
            IEnumerable<XElement> result = from xe in BalanceData.Elements("unit")
                         where xe.Element("id").Value == id + ""
                         select xe;
            return result.First();
        }
    }
}
