using System;
using Unity.Entities;
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

   [SerializeField] private GameObject puzzlePanel;
   
   private string buttoneOneName = "tulip";
   private string buttonTwoName = "rose";
   private string buttonThreeName = "Daisy";
   private string buttonFourName = "lilie";
   
   private Button[] pressedButtons = new Button[2];
   private int pressCount = 0;
   [SerializeField] private Animator animator;
   [SerializeField] private GameObject herbariumBook;
   
   [Header("Detection")]
   [SerializeField] private RectTransform uiElement;
   [SerializeField] private CapsuleCollider capsuleCollider;

   private void Start()
   {
      buttonOne.onClick.AddListener(() => OnButtonPressed(buttonOne));
      buttonTwo.onClick.AddListener(() => OnButtonPressed(buttonTwo));
      buttonThree.onClick.AddListener(() => OnButtonPressed(buttonThree));
      buttonFour.onClick.AddListener(() => OnButtonPressed(buttonFour));
   }

   public void OnButtonPressed(Button button)
   {
      if (pressCount < 2)
      {
         pressedButtons[pressCount] = button;
         pressCount++;
         Debug.Log("VAFFANCULO");
         if (pressCount == 2)
         {
            SwapButtonLocations();
            pressCount = 0; 
         }
      }
   }
   
   private void SwapButtonLocations()
   {
      Image image1 = pressedButtons[0].GetComponent<Image>();
      Image image2 = pressedButtons[1].GetComponent<Image>();

      if (image1 != null && image2 != null)
      {
         Sprite tempSprite = image1.sprite;
         image1.sprite = image2.sprite;
         image2.sprite = tempSprite;

      }

      if (image1 != null && image1.sprite != null && image2 != null && image2.sprite != null)
      {

         string button1Name = pressedButtons[0] == buttonOne ? buttoneOneName :
            pressedButtons[0] == buttonTwo ? buttonTwoName :
            pressedButtons[0] == buttonThree ? buttonThreeName :
            pressedButtons[0] == buttonFour ? buttonFourName : null;

         string button2Name = pressedButtons[1] == buttonOne ? buttoneOneName :
            pressedButtons[1] == buttonTwo ? buttonTwoName :
            pressedButtons[1] == buttonThree ? buttonThreeName :
            pressedButtons[1] == buttonFour ? buttonFourName : null;

         if (image1.sprite.name == button1Name && image2.sprite.name == button2Name && buttonThree.gameObject.activeSelf)
         {
            herbariumBook.SetActive((true));
            Invoke("OpenHeft", 1.5f);
         }
      }

   }

   private void OpenHeft()
   {
      animator.Play("Opened");
   }
   
   bool IsUIOverlappingCollider(RectTransform uiRect, CapsuleCollider collider)
   {
      Vector3[] corners = new Vector3[4];
      uiRect.GetWorldCorners(corners);
      Bounds uiBounds = new Bounds(corners[0], Vector3.zero);
      for (int i = 1; i < 4; i++)
      {
         uiBounds.Encapsulate(corners[i]);
      }

      Bounds colliderBounds = collider.bounds;
      // collider.gameObject.SetActive(false);
      return uiBounds.Intersects(colliderBounds);
   }

   private void OnTriggerEnter(Collider other)
   {
      if(other == capsuleCollider)
      {
         capsuleCollider.gameObject.SetActive(false);
         buttonThree.gameObject.SetActive(true);
      }
   }

   private void RevealPuzzle()
   {
      puzzlePanel.SetActive(true);
   }

   public class HerbariumPuzzleHandlerBaker : Baker<HerbariumPuzzleHandler>
   {
      public override void Bake(HerbariumPuzzleHandler authoring)
      {
         var entity = GetEntity(TransformUsageFlags.Dynamic);
         AddComponent(entity, new HerbariumPuzzleHandlerComponentData());
      }
   }
}

public struct HerbariumPuzzleHandlerComponentData : IComponentData
{
}
