using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] public Color _baseColor, offsetColor;
    private Color color1;
    private Color color2;
    [SerializeField] private SpriteRenderer _renderer;
  //  [SerializeField] private GameObject _highlights;




    private void Start()
    {

    }


    public void Init(bool isOffset)
    {

        color1 = new Color32(177, 241, 165, 255);
        color2 = new Color32(233, 248, 231, 255);

        _renderer.color = isOffset ? color1 : color2;
    }


    private void OnMouseEnter()
    {
      //  _highlights.SetActive(true);

    }

    private void OnMouseUp()
    {
      //  _highlights.SetActive(true);

    }

    private void OnMouseExit()
    {
      //  _highlights.SetActive(false);

    }
}
