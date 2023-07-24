using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Assistent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Helpers.StoryTeller storyTeller;

    private int _currentChapterStoryTeller;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player"))
            storyTeller.StartChapter(_currentChapterStoryTeller);
    }

    private void Start()
    {
        _currentChapterStoryTeller = 0;
    }
}
