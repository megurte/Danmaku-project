using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Scene.Inscriptions
{
    public class TextDisplay: MonoBehaviour
    {
        public static UnityEvent<string, string> DisplayContent = new UnityEvent<string, string>();

        [SerializeField] private GameObject popup;
        [SerializeField] private TextMeshProUGUI popupTextHeader;
        [SerializeField] private TextMeshProUGUI popupTextContent;

        private void Start()
        {
            DisplayContent.AddListener(ShowBookContent);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) && popup)
            {
                popup.SetActive(false);
            }
        }

        private void ShowBookContent(string header, string content)
        {
            popup.SetActive(true);
            popupTextHeader.gameObject.SetActive(true);
            popupTextContent.gameObject.SetActive(true);
            popupTextHeader.text = header;
            popupTextContent.text = content;
        }
    }
}