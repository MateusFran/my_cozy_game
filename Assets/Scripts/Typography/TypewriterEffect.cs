using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEditor;
using System;

public class TypewriterEffect : MonoBehaviour
{
    [Header("Basic Components")]
    [SerializeField] private TMP_Text m_Text;

    [Header("Just For Test")]
    [SerializeField][TextArea(5, 10)] private string testText;

    // Basic Typewriter Functionality
    private int _currentVisibleCharacterIndex;
    private Coroutine _typewriterCoroutine;
    private bool _readyForNewText = true;

    private char[] punctuationArray = { '?', '.', ';', '!', ':', '-'};

    private WaitForSeconds _simpleDelay;
    private WaitForSeconds _interpunctuationDelay;

    [Header("Typewriter Settings")]
    [SerializeField] private float charactersPerSecond = 20;
    [SerializeField] private float interpunctuationDelay = 0.5f;


    // Skipping Functionality
    public bool CurrentlySkipping { get; private set; }
    private WaitForSeconds _skipDelay;

    [Header("Skip options")]
    [SerializeField] private bool quickSkip;
    [SerializeField][Min(1)] private int skipSpeedup = 5;


    void Awake()
    {
        _simpleDelay = new WaitForSeconds(1 / charactersPerSecond);
        _interpunctuationDelay = new WaitForSeconds(interpunctuationDelay);
    }

    void Start()
    {
        SetText(testText);
    }

    void Update()
    {

    }

    public void SetText(string text)
    {

        if (_typewriterCoroutine != null)
            StopCoroutine(_typewriterCoroutine);

        m_Text.text = text;
        m_Text.maxVisibleCharacters = 0;
        _currentVisibleCharacterIndex = 0;

        _typewriterCoroutine = StartCoroutine(routine: Typewriter());
    }

    private IEnumerator Typewriter()
    {
        TMP_TextInfo textInfo = m_Text.textInfo;

        while (_currentVisibleCharacterIndex < textInfo.characterCount + 1)
        {

            char character = textInfo.characterInfo[_currentVisibleCharacterIndex].character;

            m_Text.maxVisibleCharacters++;

            if (Array.Exists(punctuationArray, ch => ch == character))
            {
                yield return _interpunctuationDelay;
            }
            else
            {
                yield return _simpleDelay;
            }
            _currentVisibleCharacterIndex++;
        }

        yield return null;
    }
}
