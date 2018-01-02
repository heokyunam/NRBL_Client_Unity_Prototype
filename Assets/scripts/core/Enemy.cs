using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.core
{
    public class Enemy : MonoBehaviour, CheckListener
    {
        private UnitGroup unitGroup = new UnitGroup();
        private int turn = 0;
        public GameObject[] pallette;

        private OrderParser orderParser;
        private Player player;

        private CheckDialog checkDialog;
        private GameObject goCheckDialog;

        public int Turn { get { return this.turn; } }
        
        // Use this for initialization
        void Awake()
        {
            this.orderParser = this.GetComponent<OrderParser>();
            
            this.player = transform.root.Find("Player").GetComponent<Player>();
            this.goCheckDialog = GameObject.Find("OKDialog");
            this.checkDialog = this.goCheckDialog.GetComponent<CheckDialog>();

            this.pallette = Resources.LoadAll<GameObject>("unit/red");
            for(int i = 0; i < this.pallette.Length; i++)
            {
                Unit unit = this.pallette[i].GetComponent<Unit>();
                if (unit != null)
                    unit.Init();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void AddUnit(int unit_type, int x, int y)
        {
            GameObject obj = Instantiate<GameObject>(this.pallette[unit_type], this.transform);
            Unit unit = obj.GetComponent<Unit>();
            this.unitGroup.AddUnit(unit);

            unit.IsEnemy = true;
            unit.Init(unit_type, x, y);
            unit.attach();
        }

        public void GiveTurn()
        {
            orderParser.GiveTurn();
        }

        public void NextTurn()
        {
            unitGroup.UpdateEveryTurn();
            //Debug.Log("NextTurn : " + turn);
            turn++;
            player.IsMyTurn = true;
            this.checkDialog.SetCheckListener(this);
            this.goCheckDialog.SetActive(true);
            this.checkDialog.SetText("상대방이 턴을 당신에게로 넘겼습니다.");
        }

        public void OnCheck()
        {

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
