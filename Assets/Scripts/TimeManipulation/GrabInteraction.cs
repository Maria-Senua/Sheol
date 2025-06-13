using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GrabInteraction : MonoBehaviour
{
    List<XRBaseInteractable> targets = new List<XRBaseInteractable>();
    XRBaseInteractable currentNearestTarget;
    
    [SerializeField] private float grabThreashold = 2f;
    [SerializeField] private GameObject cursor;
    [SerializeField] private Transform forwardVector;
    
    private List<XRBaseInteractable> grabbableObjects = new List<XRBaseInteractable>();
    private SphereCollider SphereCollider;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
