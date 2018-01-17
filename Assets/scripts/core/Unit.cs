﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;
using Assets.scripts.ui;

namespace Assets.scripts.core
{
    public class Unit : MonoBehaviour
    {
        private GameObject unitPallette, autoMapper;
        private int id;
        private XElement balance;
        private string balanceForTest;
        private int x, y;
        private bool isEnemy = false;
        
        public bool IsEnemy { get { return isEnemy; } set { isEnemy = value; } }
        public int Id { get { return id; } set { this.id = value; } }
        public int X { get { return x; } }
        public int Y { get { return y; } }
        // Use this for initialization
        void Awake()
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
            this.id = id;
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
            if (Move > 0)
            {
                if(IsEnemy)
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
                if(this.IsEnemy == false)
                {
                    Player player = GameObject.Find("Player").GetComponent<Player>();
                    player.Coin += this.Coin;//like a plant, for giving coins
                }
                else
                {
                    Enemy enemy = GameObject.Find("Enemy").GetComponent<Enemy>();
                    enemy.Coin += this.Coin;
                }
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

        public bool isCollide(Unit unit)
        {
            return (this.x == unit.x && this.y == unit.y);
        }

        public bool isAttackCastle()
        {
            if (isEnemy)
            {
                return this.y == 0;
            }
            else
            {
                return this.y == 5;
            }
        }

        public void Destroy()
        {            
            Destroy(this.gameObject);
        }

        public int Price { get { return getIntValue("price");  } }
        public int Power { get { return getIntValue("power");  } }
        public int Move { get { return getIntValue("move"); } }
        public int Coin { get { return getIntValue("coin"); } }
        public int Food { get { return getIntValue("food"); } }
        public int Capacity { get { return getIntValue("capacity"); } }

        public int getIntValue(string key)
        {;
            if (this.balance == null)
                throw new CannotFindComponentException("balance " + id + " is null");
            XElement xe = this.balance.Element(key);
            if (xe == null)
                Debug.Log(this.balance);
            return int.Parse(xe.Value);
        }
    }
}
