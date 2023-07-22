using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Assistent : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Helpers.StoryTeller storyTeller;

    private void Start()
    {
        storyTeller.StartChapter(0);
    }
}
