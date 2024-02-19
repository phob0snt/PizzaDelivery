using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Zenject;

public class PizzeriaMarker : MonoBehaviour
{
    [Inject] private ViewManager viewManager;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            viewManager.Show<PizzeriaView>(true, false);
        }
    }
}
