using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Helpers;

public class Assistent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Helpers.StoryTeller storyTeller;
    [SerializeField] private GameObject[] SpawnPositions;

    private int _currentChapterStoryTeller;
    private int _currentPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.CompareTag("Player") && !storyTeller.IsSpeaking())
        {
            storyTeller.StartChapter(_currentChapterStoryTeller);
        }
    }

    private void Start()
    {
        _currentChapterStoryTeller = 0;
        _currentPosition = 0;

        transform.position = SpawnPositions[_currentPosition].transform.position;

        storyTeller.StartChapter(_currentChapterStoryTeller);

        // always change position when finish u dialog
        storyTeller.OnFinish.AddListener(() =>
        {
            _currentPosition++;
            _currentChapterStoryTeller++;

            transform.position = SpawnPositions[_currentPosition].transform.position;
        });
    }
}
