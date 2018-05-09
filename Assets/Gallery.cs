using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Gallery : MonoBehaviour {

    public GameObject Content;
    public List<Button> m_Gallery;
    public Image GalleryImage;

    int index;
	// Use this for initialization
	void Start () {
        if (Content == null)
            return;
        m_Gallery.AddRange(Content.GetComponentsInChildren<Button>());
        gameObject.SetActive(false);
	}

    public void SelectImage(int number)
    {
        if (number == m_Gallery.Count - 1 || m_Gallery.Count == 0)
            return;
        index = number;
        UpdateImage();
    }

    public void SetSelected()
    {
        EventSystem.current.SetSelectedGameObject(m_Gallery[index].gameObject);
    }

    private void OnEnable()
    {
        UpdateImage();
    }

    void UpdateImage()
    {
        if (Content == null || m_Gallery.Count == 0)
            return;
        GalleryImage.sprite = m_Gallery[index].image.sprite;
    }
}
