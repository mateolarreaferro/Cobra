using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{
    [SerializeField] private GameObject selectedObject;

    public void EnableSelectedCircle(){selectedObject.gameObject.SetActive(true);}
    public void DisableSelectedCircle(){selectedObject.gameObject.SetActive(false);}
}
