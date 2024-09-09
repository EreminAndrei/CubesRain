using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _repeatRate;
    [SerializeField] private Material _startMaterial;

    private ObjectPool <Cube> _cubesPool;

    private Vector3 position;

    private float _minPositionX = -11f;
    private float _maxPositionX = 11f;
    private float _minPositionZ = -11f;
    private float _maxPositionZ = 5f;
    private float _positionY = 7;

    private void Awake()
    {
        _cubesPool = new ObjectPool<Cube>(CreateCube, null, OnPutBackInPool, defaultCapacity: 20);
    }

    private void Start()
    {
        StartCoroutine(SpawnCubes(_repeatRate));
    }

    private Cube CreateCube()
    {
        var cube = Instantiate (_cube);
        return cube;
    }

    private void OnPutBackInPool(Cube cube)
    {
        cube.gameObject.SetActive(false);
    }

    IEnumerator SpawnCubes(float repeatRate)
    {
        var wait = new WaitForSeconds(repeatRate);
        
        while (true)
        {
            yield return wait;
            SpawnCube();
        }
    }

    private void SpawnCube ()
    {
        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);

        position = new Vector3(positionX, _positionY, positionZ);

        var cube = _cubesPool.Get();
        cube.LifeEnded += OnLifeEnded;
        cube.Init(position, _startMaterial);
    } 
    
    private void OnLifeEnded(Cube cube)
    {
        _cubesPool.Release(cube);
        cube.LifeEnded -= OnLifeEnded;
    }    
}
