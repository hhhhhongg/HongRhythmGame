using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// � �뷡�� � ��Ʈ��� �̷�����ִ����� ���� ������
/// </summary>
[Serializable]
public class SongData : MonoBehaviour
{
    public string name;
    public List<NoteData> notes;
}
