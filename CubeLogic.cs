using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CubeLogic : MonoBehaviour
{
    [SerializeField] private Material _material; 
    [SerializeField] private List<Trigger> _triggers = new();
    [SerializeField] private Spawner _spawner;

    private MeshRenderer _meshRenderer;    

    private void OnEnable()
    {
        foreach (var trigger in _triggers)
        {
            trigger.TriggerEntered += OnTrigger;
        }
    }

    private void OnDisable()
    {
        foreach (var trigger in _triggers)
        {
            trigger.TriggerEntered -= OnTrigger;
        }        
    }

    private void OnTrigger(GameObject cube)
    {
        if (cube.GetComponent<MeshRenderer>() != null)
        {
            _meshRenderer = cube.GetComponent<MeshRenderer>();

            if (_meshRenderer.material != _material)
            {
                _meshRenderer.material = _material;
                StartCoroutine(LifeCount(cube));      
            }
        }
    }

    private IEnumerator LifeCount(GameObject cube)
    {               
        yield return new WaitForSeconds(Random.Range(2, 6));        
        _spawner.Release(cube);
    }
}
