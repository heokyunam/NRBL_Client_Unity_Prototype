using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.scripts.ui
{
    public class SelectHelper : MonoBehaviour
    {
        public GameObject selected, selected2;
        public SpriteRenderer renderer1, renderer2;
        void Start()
        {
            selected = GameObject.Find("selected");
            selected2 = GameObject.Find("selected2");
            renderer1 = selected.GetComponent<SpriteRenderer>();
            renderer2 = selected2.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null)
                {
                    AutoTile tile = hit.collider.gameObject.GetComponent<AutoTile>();
                    if (tile != null)
                    {
                        if(tile.isInArea())
                        {
                            tile.attach(selected);
                            renderer1.enabled = true;
                            renderer2.enabled = false;
                        }
                        else
                        {
                            tile.attach(selected2);
                            renderer1.enabled = false;
                            renderer2.enabled = true;
                        }
                    }
                }
            }
        }
    }
}
