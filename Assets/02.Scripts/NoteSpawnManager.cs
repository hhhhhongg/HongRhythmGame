using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NoteSpawnManager : MonoBehaviour
{
    public static NoteSpawnManager instance;
    public bool isSpawning {  get; private set; }
    private Dictionary<KeyCode, NoteSpawner> _spawners = new Dictionary<KeyCode, NoteSpawner>();
    private Queue<NoteData> _noteDataQueue;
    private float _timeCheck;
    [SerializeField] private Transform _hitterPoint;
    public float noteFallingDistance => transform.position.y - _hitterPoint.position.y;
    public float noteFallingTime => noteFallingDistance / PlaySettings.speed; // �Ÿ� / �ӵ� = �ð�

    private void Awake()
    {
        instance = this;
        NoteSpawner[] spawners = GetComponentsInChildren<NoteSpawner>();
        for (int i = 0; i < spawners.Length; i++)
        {
            _spawners.Add(spawners[i].key, spawners[i]);
        }
    }

    public void StartSpawn(IEnumerable<NoteData> noteDatas) // ��Ʈ������ ������ �༭ ���� ������ �Ұ����ϵ��� IEnumerable�� ��ȸ�� ��Ų��.
    {
        if (isSpawning)
        {
            return;
        }

        // ��Ʈ������ ������ ť ����
        _noteDataQueue = new Queue<NoteData>(noteDatas.OrderBy(note => note.time));
        _timeCheck = Time.time;
        isSpawning = true;
    }

    private void Update()
    {
        if(isSpawning == false)
        {
            return;
        }

        while (_noteDataQueue.Count > 0)
        {
            if (_noteDataQueue.Peek().time < (Time.time) - _timeCheck)
            {
                _spawners[_noteDataQueue.Dequeue().key].Spawn();
            }
            else
            {
                break;
            }
        }
    }

    
}
