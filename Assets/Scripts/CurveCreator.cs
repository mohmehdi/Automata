using System;
using DefaultNamespace;
using UnityEngine;

public class CurveCreator : MonoBehaviour
{
    [SerializeField] private GameObject curvePrefab;
    private void Start()
    {
        StateConnectionEvents.Instance.OnFirstStateSelected += OnCreateCurveObject;
    }

    private void OnCreateCurveObject()
    {
        Instantiate(curvePrefab, Vector3.zero, Quaternion.identity);
    }
}