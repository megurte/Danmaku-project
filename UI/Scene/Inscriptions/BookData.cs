using System;
using UnityEngine;

namespace UI.Scene.Inscriptions
{
    [Serializable]
    public class BookData: MonoBehaviour
    {
        [SerializeField] private string header;
        [SerializeField, TextArea(5,7)] private string content;

        public void OnMouseDown()
        {
            TextDisplay.DisplayContent.Invoke(header, content);
        }
    }
}