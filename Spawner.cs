using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _repeatRate = 1f;
    [SerializeField] private int _defaultCapacity = 5;
    [SerializeField] private int _maxCapacity = 5;    

    private float _minPositionX = -14.5f;
    private float _maxPositionX = 14.5f;
    private float _minPositionZ = -14.5f;
    private float _maxPositionZ = 14.5f;
    private float _positionY = 10;

    private ObjectPool <GameObject> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
         createFunc: () => Instantiate(_prefab),
         actionOnGet: (obj) => ActionOnGet(obj),
         actionOnRelease: (obj) => obj.SetActive(false),
         actionOnDestroy: (obj) => Destroy(obj),
         collectionCheck: true,
         defaultCapacity: _defaultCapacity,
         maxSize: _maxCapacity);
    }

    void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    public void Release(GameObject obj)
    { 
        _pool.Release(obj);
    }

    private void ActionOnGet(GameObject obj)
    {
        float positionX = Random.Range(_minPositionX, _maxPositionX);
        float positionZ = Random.Range(_minPositionZ, _maxPositionZ);
        Vector3 position = new Vector3(positionX, _positionY, positionZ);

        obj.transform.position = position;
        obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
        obj.SetActive(true);
    }

    private void GetCube()
    {
        _pool.Get();
    }    
}
