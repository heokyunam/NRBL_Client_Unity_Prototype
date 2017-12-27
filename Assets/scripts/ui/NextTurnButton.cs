using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.scripts.core;
using System;

namespace Assets.scripts.ui
{
    public class NextTurnButton : MonoBehaviour, CheckListener, DialogListener
    {
        private Player player;
        public GameObject goCheckDialog, goOkCancelDialog;
        // Use this for initialization
        void Start()
        {
            player = GameObject.Find("Player").GetComponent<Player>();

            GameObject canvas = GameObject.Find("Canvas");
            goCheckDialog = canvas.transform.Find("OKDialog").gameObject;
            goOkCancelDialog = canvas.transform.Find("OKCancelDialog").gameObject;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void onTouch()
        {
            if(player.isMyTurn)
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

        
        public void OnOK()
        {
            //상대에게 턴을 넘겨줌. 하지만 프로토타입에선 상대가 없으니
            //그냥 임시로 해줌
            player.IsMyTurn = true;
            CheckDialog checkDialog = goCheckDialog.GetComponent<CheckDialog>();
            checkDialog.SetText("상대에게 턴을 넘겼습니다");
            checkDialog.SetCheckListener(this);
            goCheckDialog.SetActive(true);

            //모든 유닛들이 다음턴을 넘길때 시행하는 이벤트들을 처리
            player.UpdateEveryTurn();
        }

        public void OnCheck()
        {

        }


        public void OnCancel()
        {
        }
    }
}
