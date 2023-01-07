using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class CastScene : Updateble
{


    [SerializeField] List<string> strings = new List<string>();
    [SerializeField] TextMeshPro text;
    [SerializeField] Updateble nextUpdatable; // то есть тот на который надо будет переключиться

    private Updateble _nextUpdateble;


    int currentStringToShow = 0;

    

    public override void _Start()
    {
        text.text = strings[currentStringToShow];
        _nextUpdateble = this;
    }

    public override void _Update()
    {

        if (Input.GetMouseButtonDown(((int)MouseButton.Left))) {
            currentStringToShow++;
            if (currentStringToShow == strings.Count)
            {
                _nextUpdateble = nextUpdatable;
                return;
            }

            text.text = strings[currentStringToShow];
            
        }
    }

    public override Updateble GetNextUpdateble()
    {
        return _nextUpdateble;
    }

    public override void _End()
    {
        text.gameObject.SetActive(false);
    }
}
