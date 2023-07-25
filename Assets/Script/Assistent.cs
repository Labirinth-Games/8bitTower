using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Helpers;

public class Assistent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Helpers.StoryTeller storyTeller;

    private int _currentChapterStoryTeller;
    private bool _isOpenDialog = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player") && !_isOpenDialog)
        {
            _isOpenDialog = true;

            storyTeller.StartChapter(_currentChapterStoryTeller);
        }
    }

    private void Start()
    {
        _currentChapterStoryTeller = 0;
        storyTeller.OnFinish.AddListener(() => { _isOpenDialog = false; });
    }
}
