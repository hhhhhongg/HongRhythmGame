using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))] // �� ��ũ��Ʈ�� ������Ʈ�� �߰������� VideoPlayer�� �ڵ����� ����
public class SongDataMaker : MonoBehaviour
{
    private KeyCode[] _keys = { KeyCode.S, KeyCode.D, KeyCode.F, KeyCode.J, KeyCode.K, KeyCode.L };
    private SongData _songData;
    private VideoPlayer _videoPlayer;
    private bool _isRecording;

    // ��ȭ����
    public void StartRecording()
    {
        if (_isRecording)
        {
            return;
        }
        _isRecording = true;
        _songData = new SongData(_videoPlayer.clip.name);
        _videoPlayer.Play();
    }

    // ��ȭ����
    public void StopRecording()
    {
        if (_isRecording == false)
        {
            return;
        }
        _videoPlayer.Stop();
        SaveRecording();
        _songData = null;
    }

    // ��ȭ����
    public void SaveRecording()
    {
        string dir = UnityEditor.EditorUtility.SaveFilePanelInProject("�뷡 ������ ����", _songData.name, "json", "");
        System.IO.File.WriteAllText(dir, JsonUtility.ToJson(_songData));
    }

    private void Awake()
    {
        _videoPlayer = GetComponent<VideoPlayer>();
    }

    private void Update()
    {
        if (_isRecording)
        {
            Recording();
        }
    }

    // ��ȭ�� ���� �Լ�
    private void Recording()
    {
        foreach (KeyCode key in _keys)
        {
            if (Input.GetKeyDown(key))
            {
                _songData.notes.Add(CreateNoteData(key));
            }
        }
    }

    // ��Ʈ�����͸� �����ϴ� �Լ� (�º� ����ð��� ���� Ű��)
    private NoteData CreateNoteData(KeyCode key)
    {
        NoteData noteData = new NoteData()
        {
            key = key,
            time = (float)System.Math.Round(_videoPlayer.time, 2)
        };
        Debug.Log($"[SongDataMaker] : NoteData ������, {noteData.key} {noteData.time}");

        return noteData;
    }
}
