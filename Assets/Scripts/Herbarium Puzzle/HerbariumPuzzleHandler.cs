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
   
   private string buttoneOneName = "rose";
   private string buttonTwoName = "lilie";
   private string buttonThreeName = "Daisy";
   private string buttonFourName = "tulip";
   private string[] correctOrder = { "tulip", "lilie", "Daisy", "rose" }; 

   public Button[] pressedButtons = new Button[2];
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
      if (!buttonThree.gameObject.activeSelf)
      {
         return;
      }
      
      Image image1 = pressedButtons[0].GetComponent<Image>();
      Image image2 = pressedButtons[1].GetComponent<Image>();

      if (image1 != null && image2 != null)
      {
         Sprite tempSprite = image1.sprite;
         image1.sprite = image2.sprite;
         image2.sprite = tempSprite;
      }

      if (AreButtonsInCorrectOrder())
      {
         herbariumBook.SetActive(true);
         Invoke("OpenHeft", 1.5f);
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

   private bool AreButtonsInCorrectOrder()
   {
      Button[] buttons = { buttonOne, buttonTwo, buttonThree, buttonFour };

      for (int i = 0; i < buttons.Length; i++)
      {
         Image buttonImage = buttons[i].GetComponent<Image>();
         if (buttonImage == null || buttonImage.sprite == null || buttonImage.sprite.name != correctOrder[i])
         {
            return false;
         }
      }

      return true;
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
