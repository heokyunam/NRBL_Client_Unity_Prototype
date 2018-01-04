using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using Assets.scripts.core;
using UnityEngine;

public class OrderParser : MonoBehaviour {
    public Enemy enemy;
    private AIMaker ai;
    void Awake()
    {
        this.enemy = GetComponent<Enemy>();
        this.ai = new AIMaker(enemy);
    }

    //return value : can move next
    public bool order(XElement element)
    {
        string type = element.Element("type").Value;
        //Debug.Log("turn" + enemy.Turn + ":" + type);
        if (type == "unit")
        {
            int x = int.Parse(element.Element("x").Value);
            int y = int.Parse(element.Element("y").Value);
            int unit_type = int.Parse(element.Element("unit").Value);

            enemy.AddUnit(unit_type, x, y);

            return true;
        }
        else if(type == "turn")
        {
            enemy.NextTurn();

            return false;
        }

        throw new System.Exception("찾을 수 없는 오더 타입 : " + type);
    }


    public void GiveAITurn()
    {
        /*
        XElement root = XElement.Load("assets/datas/ai2.xml");
        IEnumerable<XElement> result = from xe in root.Elements("step")
                                       where xe.Element("turn").Value == enemy.Turn + ""
                                       select xe;
        foreach (XElement xe in result)
        {
            if (order(xe) == false)
                break;
        }
        */
        XElement result = ai.Judge();
        foreach (XElement xe in result.Elements())
        {
            if (order(xe) == false)
                break;
        }
    }

    public void GiveTurn()
    {
        //현재 통신이 구현되지 않았기 때문에. xml 파일로 놓아둔 값에 대해
        //처리를 한다
        GiveAITurn();
    }
    /*
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}*/
    }
