using UnityEngine;
using UnityEngine.Events;

public class GameRuler : MonoBehaviour
{
    [SerializeField] private Roulette roulette1;
    [SerializeField] private Arrow arrow1;
    [SerializeField] private Roulette roulette2;
    [SerializeField] private Arrow arrow2;
    [SerializeField] private Roulette roulette3;
    [SerializeField] private Arrow arrow3; 
    [SerializeField] private Roulette roulette4;
    [SerializeField] private Arrow arrow4;
    
    [SerializeField] private ScoreCounter scoreCounter;
    
    

    public UnityEvent arrowCoincidedEvent;
    public UnityEvent endSpinsEvent;
    
    public UnityEvent addedScoreEvent;
    public UnityEvent remowedScoreEvent;

    private int endWorkSlots = 0;
    private bool addScore = true;

    public void SetEndSlot()
    {
        endWorkSlots++;
        GetResults();
    }

    private void OnEnable()
    {
        roulette1.endRotateEvent?.AddListener(SetEndSlot);
        roulette2.endRotateEvent?.AddListener(SetEndSlot);
        roulette3.endRotateEvent?.AddListener(SetEndSlot);
        roulette4.endRotateEvent?.AddListener(SetEndSlot);
    }

    private void OnDisable()
    {
        roulette1.endRotateEvent.RemoveListener(SetEndSlot);
        roulette2.endRotateEvent.RemoveListener(SetEndSlot);
        roulette3.endRotateEvent.RemoveListener(SetEndSlot);
        roulette4.endRotateEvent.RemoveListener(SetEndSlot);
    }

    private float GetValue()
    {
  
        return arrow1.collidedObject.Value * 1000 + arrow2.collidedObject.Value * 100 +
               arrow3.collidedObject.Value * 10 + arrow4.collidedObject.Value;
    }

    public void GetResults()
    {
        if (endWorkSlots == 4)
        {
            if (addScore)
            {
                addedScoreEvent?.Invoke();
                scoreCounter.Add(GetValue());
            }
            else
            {
                scoreCounter.TakeAway(GetValue());
                remowedScoreEvent?.Invoke();
            }
            endSpinsEvent?.Invoke();
            addScore = !addScore;
            endWorkSlots = 0;
        }
    }
}
