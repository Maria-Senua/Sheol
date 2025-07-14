using UnityEngine;
using UnityEngine.UI;
public class HerbariumPuzzleHandler : MonoBehaviour
{
   [SerializeField, TextArea] private string debugString;
   
   [Header("Setup")]
   [SerializeField] private Button buttonOne;
   [SerializeField] private Button buttonTwo;
   [SerializeField] private Button buttonThree;
   [SerializeField] private Button buttonFour;
   
   private Button[] pressedButtons = new Button[2];
   private int pressCount = 0;
   
   private void Start()
   {
      buttonOne.onClick.AddListener(() => OnButtonPressed(buttonOne));
      buttonTwo.onClick.AddListener(() => OnButtonPressed(buttonTwo));
      buttonThree.onClick.AddListener(() => OnButtonPressed(buttonThree));
      buttonFour.onClick.AddListener(() => OnButtonPressed(buttonFour));
   }

   private void OnButtonPressed(Button button)
   {
      if (pressCount < 2)
      {
         pressedButtons[pressCount] = button;
         pressCount++;
         debugString = $"Pressed {button}. Press count: {pressCount}";
         if (pressCount == 2)
         {
            SwapButtonLocations();
            pressCount = 0; 
         }
      }
   }
   
   private void SwapButtonLocations()
   {
      RectTransform rectTransform1 = pressedButtons[0].GetComponent<RectTransform>();
      RectTransform rectTransform2 = pressedButtons[1].GetComponent<RectTransform>();

      Vector3 tempPosition = rectTransform1.localPosition;
      rectTransform1.localPosition = rectTransform2.localPosition;
      rectTransform2.localPosition = tempPosition;
   }
}
