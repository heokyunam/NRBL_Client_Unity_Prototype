using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.scripts.core;
using UnityEngine;

namespace Assets.scripts.ui
{
    public class UnitPallette : MonoBehaviour
    {
        public GameObject[] units;
        void Awake()
        {
            units = Resources.LoadAll<GameObject>("unit/blue");
            for(int i = 0; i < units.Length; i++)
            {
                Sprite sprite = units[i].GetComponent<SpriteRenderer>().sprite;
                float width = sprite.bounds.size.x;
                float height = sprite.bounds.size.y;
                units[i].transform.localScale = new Vector3(1 / width, 1 / height);
            }
        }

        void Start()
        {
            Init(GameObject.Find("autotile(0,0)"));
        }

        void Init(GameObject left)
        {
            float x = left.transform.localPosition.x;
            float y = left.transform.localPosition.y;
            for(int i = 0; i < units.Length; i++)
            {
                GameObject obj = Instantiate(units[i], this.transform);
                obj.transform.Translate(new Vector3(x + i, y - 1));
                obj.name = "pallette" + i;

                Unit unit = obj.GetComponent<Unit>();
                if(unit != null)
                    unit.Id = i;
            }
        }
    }
}
