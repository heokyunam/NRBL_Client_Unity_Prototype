using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.core
{
    public class Enemy : MonoBehaviour, CheckListener
    {
        private UnitGroup unitGroup = new UnitGroup();
        private int turn = 0, hp, coin;
        public GameObject[] pallette;

        private OrderParser orderParser;
        private Player player;

        private CheckDialog checkDialog;
        private GameObject goCheckDialog;
        private Transform tfEnemyHP;

        public int Turn { get { return this.turn; } }
        public int Hp {
            get { return this.hp; }
            set {
                this.hp = value;
                tfEnemyHP.GetComponent<Text>().text = value + "";
            }
        }
        public int Coin
        {
            get { return this.coin; }
            set {  this.coin = value; }
        }

        // Use this for initialization
        void Awake()
        {
            this.orderParser = this.GetComponent<OrderParser>();
            
            this.player = transform.root.Find("Player").GetComponent<Player>();
            this.goCheckDialog = GameObject.Find("OKDialog");
            this.checkDialog = this.goCheckDialog.GetComponent<CheckDialog>();
            this.tfEnemyHP = GameObject.Find("Canvas").transform.Find("EnemyHP").Find("Text");

            this.pallette = Resources.LoadAll<GameObject>("unit/red");
            for(int i = 0; i < this.pallette.Length; i++)
            {
                Unit unit = this.pallette[i].GetComponent<Unit>();
                if (unit != null)
                    unit.Init();
            }
            Hp = 5;

            AIMaker am = new AIMaker();
            am.write();
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
            unitGroup.UpdateEveryTurn(player.UnitGroup);
            int damage = unitGroup.AttackCastleDamage();
            player.Hp -= damage;

            turn++;
            this.goCheckDialog.SetActive(true);
            this.checkDialog.SetCheckListener(this);
            this.checkDialog.SetText("상대방이 턴을 당신에게로 넘겼습니다.");
            Debug.Log("NextTurn : " + this.goCheckDialog.activeInHierarchy);
        }

        public void OnCheck()
        {
            player.IsMyTurn = true;
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
