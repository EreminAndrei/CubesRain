using System.Collections;
using UnityEngine;


public class Platform : MonoBehaviour
{
    [SerializeField] private Material[] _materials = new Material[2]; 
    [SerializeField] private Spawner _spawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<MeshRenderer>().material = _materials[0])
        {
            other.gameObject.GetComponent<MeshRenderer>().material = _materials[1];
            StartCoroutine(LifeCount(other.gameObject));
        }
    }

    private IEnumerator LifeCount(GameObject cube)
    {
        var delay = new WaitForSeconds(Random.Range(2, 10));
        yield return delay;
        cube.GetComponent<MeshRenderer>().material = _materials[0];
        _spawner.Release(cube);
    }
}
