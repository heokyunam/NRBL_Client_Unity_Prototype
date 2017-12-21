using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.scripts.core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.ui
{
    public class SelectHelper : MonoBehaviour, DialogListener
    {
        public GameObject selected, selected2;
        public SpriteRenderer renderer1, renderer2;
        public GameObject selectedUnit;
        public GameObject okCancelDialog;

        private AutoTile selectedTile;


        void Start()
        {
            selected = GameObject.Find("selected");
            selected2 = GameObject.Find("selected2");
            renderer1 = selected.GetComponent<SpriteRenderer>();
            renderer2 = selected2.GetComponent<SpriteRenderer>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && okCancelDialog.activeInHierarchy == false)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.collider != null)
                {
                    //1. 유닛 팔레트에선 Unit이 있는 오브젝트일 것임
                    //2. 선택타일에선 AutoTile이 나타날 것이다
                    GameObject gameObject = hit.collider.gameObject;
                    Unit unit = gameObject.GetComponent<Unit>();
                    AutoTile tile = gameObject.GetComponent<AutoTile>();

                    if(unit != null)
                    {
                        SelectPallette(unit);
                        selectedUnit = gameObject;
                    }
                    else
                    {
                        SelectAutoTile(tile);
                    }
                }
            }
        }

        public void SelectPallette(Unit unit)
        {
            renderer2.enabled = true;
            unit.attach(selected2);
        }

        public void SelectAutoTile(AutoTile tile)
        {
            if(renderer2.enabled)
            {
                renderer1.enabled = true;
                tile.attach(selected);
                this.selectedTile = tile;
                //유닛 설치에 관한 다이얼로그 표시
                //TODO : 다이얼로그 만들고 이 클래스에 인터페이스를 추가하는식으로 이어줘야 한다
                OKCancelDialog dialog = okCancelDialog.GetComponent<OKCancelDialog>();

                dialog.SetDialogListener(this);
                okCancelDialog.SetActive(true);
                Text text = okCancelDialog.transform.Find("Message").GetComponent<Text>();
                text.text = "이 곳에 해당 유닛을 위치시키겠습니까?";
            }
        }

        public void OnOK()
        {
            GameObject obj = Instantiate(selectedUnit, this.transform);
            this.selectedTile.attach(obj);
            renderer2.enabled = false;
            renderer1.enabled = false;
        }

        public void OnCancel()
        {
            renderer2.enabled = false;
            renderer1.enabled = false;
        }
    }
}
