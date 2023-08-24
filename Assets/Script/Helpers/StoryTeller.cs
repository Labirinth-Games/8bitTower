using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine.Events;
using UnityEngine;

namespace Helpers
{
    public class StoryTeller : MonoBehaviour
    {
        [SerializeField] private BookDialogs bookDialogs;
        [SerializeField] private GameObject dialogBoxPrefab;

        [Header("Callback")]
        public UnityEvent OnFinish;

        //private int _chapterActual = 0; // capitulo, momento separado por bloco de momento da historia
        private int _paragraphActual = 0; // paragrafo, fala em si que a classe irá produzir
        private int _chapterMax;
        private int _paragraphMax;

        private string[,] _book; // array bideimencional com todos os dialogos que seram narrados pela classe
        private GameObject _dialogInstance;
        private bool _isSpeaking = false;

        private void Start()
        {
            _book = bookDialogs.GetBook();

            _chapterMax = _book.GetLength(0) - 1; // get rows amount
            _paragraphMax = _book.GetLength(1) - 1; // get columns
        }

        #region Gets/Sets
        public bool IsSpeaking() { return _isSpeaking; }
        #endregion

        public void StartChapter(int chapter)
        {
            _isSpeaking = true;
            CreateDialogUI();
            StartCoroutine(AnimationDialog(chapter));
        }

        public bool NextParagraph(int chapter)
        {
            _paragraphActual += 1;
            
            if (_book[chapter, _paragraphActual].NullIfEmpty() == null)
            {
                _paragraphActual = 0;
                return false;
            }

            string dialogActual = _book[chapter, _paragraphActual];
            _dialogInstance.GetComponent<Dialog>().SetMessage(dialogActual);

            return true;
        }

        private void CreateDialogUI()
        {
            _dialogInstance = Instantiate(dialogBoxPrefab);
            _dialogInstance.transform.position = new Vector3(transform.position.x - 1f, transform.position.y + .8f, transform.position.z);
        }

        IEnumerator AnimationDialog(int chapter)
        {
            while (NextParagraph(chapter))
            {
                yield return new WaitForSeconds(5f);
            }

            OnFinish?.Invoke();
            _isSpeaking = false;

            Destroy(_dialogInstance);
        }
    }
}