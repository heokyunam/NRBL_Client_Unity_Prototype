using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.scripts.ui
{
    //tilemap cannot drawing completely
    //we must draw it precisely. so we do that in code.
    public class AutoMapper : MonoBehaviour
    {
        public GameObject[] tiles;
        public GameObject selected;
        private string map_file_name = "assets/datas/map.txt";
        
        void Awake()
        {
            this.tiles = Resources.LoadAll<GameObject>("tiles");
            for(int i = 0; i < tiles.Length; i++)
            {
                GameObject tile = this.tiles[i];
                Sprite sprite = tile.GetComponent<SpriteRenderer>().sprite;
                float width = sprite.bounds.size.x;
                float height = sprite.bounds.size.y;
                tile.transform.localScale = new Vector3(1.0f/width, 1.0f/height);
                //Debug.Log("AutoMapper Sprite Size Check : " + width + " : " + height);
            }
            this.selected.transform.localScale = new Vector3(1 / 1.25f, 1 / 1.25f);
        }

        void Start()
        {
            MapParser parser = new MapParser(map_file_name);/*
            Vector3 leftDown = Camera.main.ScreenToWorldPoint(new Vector3(0, 0));
            Vector3 rightUp = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
            float width = rightUp.x - leftDown.x;
            float height = rightUp.y - leftDown.y;*/

            for (int i = 0; i < MapParser.X_NUM; i++)
            {
                for (int j = 0; j < MapParser.Y_NUM; j++)
                {
                    int idx = parser.GetData(i, j);
                    GameObject obj = Instantiate(tiles[idx], this.transform);
                    obj.transform.Translate(new Vector3(i - 2f, j - MapParser.Y_NUM / 2));
                    obj.GetComponent<AutoTile>().move(i, j-2);
                }
            }
        }

        void Update()
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if(hit.collider != null)
                {
                    AutoTile tile = hit.collider.gameObject.GetComponent<AutoTile>();
                    if(tile != null)
                    {
                        tile.attach(selected);
                    }                     
                }
            }
        }
    }
}
