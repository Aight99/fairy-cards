using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private Updateble CurrentUpdateble {
        set {
            if (value == _CurrentUpdateble)
                return;
            _CurrentUpdateble._End();
            value._Start();
            _CurrentUpdateble = value; 
        }
        get => _CurrentUpdateble; }

    private Updateble _CurrentUpdateble;

    // Start is called before the first frame update
    void Start()
    {
        CurrentUpdateble._Start();
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentUpdateble == null)
            Debug.LogError("Current Updateble was null");

        CurrentUpdateble?._Update();


        var newUpdate = CurrentUpdateble.GetNextUpdateble();

        if (newUpdate != CurrentUpdateble)
        {
            newUpdate._Start();
            CurrentUpdateble._End();
            CurrentUpdateble = newUpdate;
        }

    }
}

public abstract class Updateble : MonoBehaviour
{
    public abstract void _Update();

    public virtual Updateble GetNextUpdateble() => this;

    public virtual void _Start() { }
    public virtual void _End() { }


}
