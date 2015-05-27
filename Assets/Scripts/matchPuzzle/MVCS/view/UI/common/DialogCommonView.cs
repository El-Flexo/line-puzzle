using System;
using UnityEngine;
using UnityEngine.UI;
using strange.extensions.mediation.impl;

namespace matchPuzzle.MVCS.view.UI.common
{
    public class DialogCommonView : View
    {
        [SerializeField]
        Text title;

        [SerializeField]
        Text content;

        [SerializeField]
        Button yesButton;

        [SerializeField]
        Button noButton;

        public void InitYesNo(string title, string content, string yesText, Action yesDelegate, string noText, Action noDelegate = null)
        {
            InitOk(title, content, yesText, yesDelegate);

            noButton.gameObject.SetActive(true);
            noButton.onClick.AddListener(() => {
                if (noDelegate != null)
                    noDelegate();
                Close();
            });
        }

        public void InitOk(string title, string content, string yesText, Action yesDelegate)
        {
            this.title.text = title;
            this.content.text = content;

            yesButton.gameObject.SetActive(true);
            yesButton.onClick.AddListener(() => {
                if (yesDelegate != null)
                    yesDelegate();
                Close();
            });
        }

        protected override void OnDestroy()
        {
            yesButton.onClick.RemoveAllListeners();
            noButton.onClick.RemoveAllListeners();
            base.OnDestroy();
        }

        public void Close()
        {
            Destroy(this.gameObject);
        }
    }
}