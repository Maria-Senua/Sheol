using UnityEngine;
using UnityEngine.Events;

public class CombinationLock : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Dial[] dials;


    [Header("Settings")]
    [SerializeField] private string combination;


    [Header("Events")]
    [SerializeField] private UnityEvent onCorrectCombinationFound;

    public void CheckCombination(Dial dial)
    {
        for (int i = 0; i < dials.Length; i++)
        {
            int combinationNumber = int.Parse(combination[i].ToString());

            Debug.Log("CheckCombination " + dials[i].GetNumber());
            CorrectCombination();
            //if (combinationNumber != dials[i].GetNumber())
           // {
             //  dial.Unlock();
               // return;
           // }
        }

        
    }

    private void CorrectCombination()
    {
       for (int i = 0; i < dials.Length; i++) dials[i].Lock();

        onCorrectCombinationFound?.Invoke();
        for (int i = 0; i < dials.Length; i++) dials[i].Unlock();
    }
}
