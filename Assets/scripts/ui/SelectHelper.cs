using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.scripts.core;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.scripts.ui
{
    public class SelectHelper : MonoBehaviour, DialogListener, CheckListener
    {
        public GameObject selected, selected2;
        public GameObject selectedUnit;
        public GameObject okCancelDialog, okDialog;

        private Player player;
        private AutoTile selectedTile;

        void Start()
        {
            GameObject goPlayer = GameObject.Find("Player");
            selected = goPlayer.transform.Find("selected").gameObject;
            selected2 = goPlayer.transform.Find("selected2").gameObject;
            okCancelDialog = GameObject.Find("OKCancelDialog");
            okDialog = GameObject.Find("OKDialog");

            okCancelDialog.SetActive(false);
            okDialog.SetActive(false);

            player = goPlayer.GetComponent<Player>();
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0) && isDialogActive() == false)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
                //Unit Touch or AutoTile Touch
                if (hit.collider != null)
                {
                    //1. 유닛 팔레트에선 Unit이 있는 오브젝트일 것임
                    //2. 선택타일에선 AutoTile이 나타날 것이다
                    GameObject gameObject = hit.collider.gameObject;
                    Unit unit = gameObject.GetComponent<Unit>();
                    AutoTile tile = gameObject.GetComponent<AutoTile>();
                    
                    if (unit != null)
                    {
                        if(gameObject.tag == "pallette")
                        {
                            SelectPallette(unit);
                            selectedUnit = gameObject;
                        }
                    }
                    else if(tile != null)
                    {
                        if(gameObject.tag == "placable")
                        {
                            SelectAutoTile(tile);
                        }
                    }
                }
            }
        }

        public bool isDialogActive()
        {
            return okCancelDialog.activeInHierarchy || okDialog.activeInHierarchy;
        }

        public void SelectPallette(Unit unit)
        {
            selected2.SetActive(true);
            unit.attach(selected2);
        }

        public void SelectAutoTile(AutoTile tile)
        {
            if(selected2.activeInHierarchy)
            {
                selected.SetActive(true);
                tile.attach(selected);
                this.selectedTile = tile;
                //유닛 설치에 관한 다이얼로그 표시
                //TODO : 다이얼로그 만들고 이 클래스에 인터페이스를 추가하는식으로 이어줘야 한다
                OKCancelDialog dialog = okCancelDialog.GetComponent<OKCancelDialog>();

                dialog.SetDialogListener(this);
                okCancelDialog.SetActive(true);
                dialog.SetText("이 곳에 해당 유닛을 위치시키겠습니까?");
            }
        }

        public void OnCheck()
        {
            selected.SetActive(false);
            selected2.SetActive(false);
        }

        public void OnOK()
        {
            if(player.IsMyTurn == false)
            {
                //턴을 소모시
                CheckDialog dialog = okDialog.GetComponent<CheckDialog>();
                dialog.SetCheckListener(this);

                okDialog.SetActive(true);
                dialog.SetText("턴을 이미 소모했습니다.\n상대방에게 턴을 넘겨주세요");
            }
            else if(player.checkEnoughCoin(selectedUnit))
            {
                //충분한 금액이 있음. 설치 가능
                GameObject obj = Instantiate(selectedUnit, player.transform);
                //Debug.Log(selectedTile.name);
                //this.selectedTile.attach(obj);
                //Debug.Log(obj.transform.localPosition);

                Unit unit = obj.GetComponent<Unit>();
                player.UnitGroup.AddUnit(unit);
                //새로 만들어낸 Unit객체에는 새로운 x,y정보가 포함되어 있지 않음
                unit.SetPosition(this.selectedTile.X, this.selectedTile.Y);
                //오토타일에서 해당 값을 찾아 반영해주는 방식임
                unit.attach();

                obj.tag = "created";

                selected.SetActive(false);
                selected2.SetActive(false);
                player.IsMyTurn = false;
            }
            else
            {
                //금액 부족. 설치 불가
                CheckDialog dialog = okDialog.GetComponent<CheckDialog>();
                dialog.SetCheckListener(this);

                okDialog.SetActive(true);
                dialog.SetText("금액이 부족합니다. 현재 코인 : " + player.Coin);
            }
        }

        public void OnCancel()
        {
            selected.SetActive(false);
            selected2.SetActive(false); 
        }
    }
}
