using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _objectPrefab;
    [SerializeField]
    private int _poolObjectsCount = 24;
    [SerializeField]
    private float _fallingSpeed = 1;
    [SerializeField]
    private float _spawningSpeed = .5f;
    [SerializeField]
    private float _minSpawningSpeed = 0.5f, _maxSpawningSpeed = 3;


    [SerializeField]
    private int _minRange, _maxRange;

    [SerializeField]
    private int _countSpawnObject = 3;

    private int _sumRange => Mathf.Abs(_minRange) + _maxRange + 1;

    private void OnValidate()
    {
        if(_fallingSpeed < 0)
            _fallingSpeed = 0;
        if (_countSpawnObject < 1 || _countSpawnObject > _sumRange)
            _countSpawnObject = 1;
        if (_minSpawningSpeed > _spawningSpeed || _maxSpawningSpeed < _spawningSpeed)
            _spawningSpeed = _minSpawningSpeed;
    }

    private List<Object> poolObjects = new List<Object>();
    private void Start()
    {
        for (int i = 0; i < _poolObjectsCount; i++)
        {
            poolObjects.Add(Instantiate(_objectPrefab, transform).GetComponent<Object>());
            poolObjects[poolObjects.Count - 1].name = i.ToString();
        }
    }
    public void HideAllObject()
    {
        poolObjects.ForEach(item => item.DestroyObject());
    }


    List<Object> clickObjectList = new List<Object>();
    public void SetNonClickableObjects()
    {
        clickObjectList = poolObjects.Where(item => item.CanClick).ToList();
        poolObjects.ForEach(_item => _item.CanClick = false);
    }
    public void SetClickableObjects()
    {
        clickObjectList.ForEach(_item => _item.CanClick = true);
    }

    public void setSpeed(float fallingSpeed, float spawningSpeed)
    {
        _fallingSpeed = fallingSpeed;
        _spawningSpeed = Mathf.Clamp(spawningSpeed, _minSpawningSpeed, _maxSpawningSpeed);
    }

    /*    private void FixedUpdate()
        {
            foreach (var obj in poolObjects.Where(item => item.gameObject.activeSelf))
            {
                obj.transform.position += new Vector3(0, -1 * (fallingSpeed * Time.deltaTime));
            }
        }*/

    private Coroutine SpawnCoroutine = null;

    public void StartSpawnObject()
    {
        SpawnCoroutine = StartCoroutine(SpawnObjectCoroutine());
    }

    public void StopSpawnObject()
    {
        if(SpawnCoroutine != null)
        StopCoroutine(SpawnCoroutine);
    }

    private IEnumerator SpawnObjectCoroutine()
    {
        while (true)
        {
            SpawnNewObject(_countSpawnObject);
            yield return new WaitForSeconds(_spawningSpeed);
        }
    }

    private void SpawnNewObject(int count)
    {
        if (_sumRange < count || count <= 0)
            throw new System.Exception($"Objects more than possible. {_sumRange} > {count}");

        List<int> tempList = GetRandomValueList(_minRange, _maxRange, count);
        for (int i = 0; i < count; i++)
        {
            var spawnedPosition = new Vector3(tempList[i], transform.position.y, transform.position.z);
            GetNotEnabledObject()?.SpawnObject(spawnedPosition, _fallingSpeed);
        }
    }

    private List<int> GetRandomValueList(int min, int max, int count)
    {
        List<int> tempList = new List<int>();

        for (int i = min; i < max + 1; i++)
        {
            tempList.Add(i);
        }
        tempList = tempList.OrderBy(x => Random.Range(min, max)).ToList();
        tempList.Take(count);

        return tempList.Count == 1  ? new List<int> { Random.Range(min, max) } : tempList;
    }

    private Object GetNotEnabledObject()
    {
        return poolObjects.Where(item => !item.gameObject.activeSelf).FirstOrDefault();
    }

}
