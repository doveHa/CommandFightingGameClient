using System.Collections.Generic;
using Core.OnClick;
using UnityEngine;

namespace UI.OnClick
{
    public class ShowPanelButton : OnClickBase
    {
        [SerializeField] private List<GameObject> targetPanels;

        protected override void OnClick()
        {
            foreach (GameObject panel in targetPanels)
            {
                panel.SetActive(true);
            }
        }
    }
}