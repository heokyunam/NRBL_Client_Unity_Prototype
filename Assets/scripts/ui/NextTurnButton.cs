using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using System;
using UnityEngine.UI;

namespace Assets.scripts.ui
{
    public class NextTurnButton : MonoBehaviour, CheckListener, DialogListener
    {
        private Player player;
        private Enemy enemy;
        public GameObject goCheckDialog, goOkCancelDialog;
        // Use this for initialization
        void Awake()
        {
            player = GameObject.Find("Player").GetComponent<Player>();
            enemy = GameObject.Find("Enemy").GetComponent<Enemy>();

            GameObject canvas = GameObject.Find("Canvas");
            goCheckDialog = canvas.transform.Find("OKDialog").gameObject;
            goOkCancelDialog = canvas.transform.Find("OKCancelDialog").gameObject;

            Button btn = this.GetComponent<Button>();
            btn.onClick.AddListener(onTouch);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onTouch()
        {
            if(isDialogActive() == false)
            {
                if (player.isMyTurn)
                {
                    OKCancelDialog okCancelDialog = goOkCancelDialog.GetComponent<OKCancelDialog>();
                    okCancelDialog.SetText("아직 턴을 사용하지 않았습니다.\n 그래도 넘기시겠습니까?");
                    okCancelDialog.SetDialogListener(this);
                    goOkCancelDialog.SetActive(true);
                }
                else
                {
                    OnOK();
                }
            }
        }

        public bool isDialogActive()
        {
            return goCheckDialog.activeInHierarchy && goOkCancelDialog.activeInHierarchy;
        }
        
        public void OnOK()
        {
            //상대에게 턴을 넘겨줌. 하지만 프로토타입에선 상대가 없으니
            //그냥 임시로 해줌
            //player.IsMyTurn = true;
            CheckDialog checkDialog = goCheckDialog.GetComponent<CheckDialog>();
            checkDialog.SetText("상대에게 턴을 넘겼습니다");
            checkDialog.SetCheckListener(this);
            goCheckDialog.SetActive(true);

        }

        public void OnCheck()
        {
            //모든 유닛들이 다음턴을 넘길때 시행하는 이벤트들을 처리
            player.UnitGroup.UpdateEveryTurn();
            player.NextTurn();
        }


        public void OnCancel()
        {

        }
    }
}
