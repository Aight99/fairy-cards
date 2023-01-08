using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.PlayerLoop.PreLateUpdate;

public class UpdateController : MonoBehaviour
{
    [SerializeField] private Updateble CurrentUpdateble;
    [SerializeField] private Updateble EnemyController;
    [SerializeField] private Updateble TableController;
    
    
    [SerializeField] private Button endTurnButton;



    // Start is called before the first frame update
    void Start()
    {
        CurrentUpdateble._Start();
        endTurnButton.onClick.AddListener(() => {
            if (CurrentUpdateble != EnemyController) ChangeCurrentUpdate(EnemyController);
        });
    }

    // Update is called once per frame
    void Update()
    {

        if (CurrentUpdateble == null)
            Debug.LogError("Current Updateble was null");

        CurrentUpdateble?._Update();


        var newUpdateble = CurrentUpdateble.GetNextUpdateble();

        if (newUpdateble != CurrentUpdateble)
        {
            CurrentUpdateble._End();
            newUpdateble._Start();
            CurrentUpdateble = newUpdateble;
        }

    }

    public void ChangeCurrentUpdate(Updateble newUpdateble)
    {
        CurrentUpdateble._End();
        newUpdateble._Start();
        CurrentUpdateble = newUpdateble;
    }

}

public abstract class Updateble : MonoBehaviour
{   
    public abstract void _Update();

    public virtual Updateble GetNextUpdateble() => this;

    public virtual void _Start() { }
    public virtual void _End() { }


}
