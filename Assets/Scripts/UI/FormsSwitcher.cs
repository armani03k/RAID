using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FormsSwitcher : MonoBehaviour {

    public Image FormsImage;
    public List<Sprite> Forms;
    int m_index;
    public void IndexUp()
    {
        if (m_index >= (Forms.Count - 1) && Forms.Count != 0)
            return;

        m_index++;
        SwitchImage();
    }

    public void IndexDown()
    {
        if (m_index <= 0)
            return;
        m_index--;
        SwitchImage();
    }

    void SwitchImage()
    {
        FormsImage.sprite = Forms[m_index];
    }
}
