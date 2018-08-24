// Author: Adrian Miasik
// Personal Portfolio: https://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

// Creation Date: 2018-08-24
// Purpose: Reveals text (TextMeshProUGUI) over time much like a typewriter. There is a delay in-between each character reveal.

using System;
using UnityEngine;

#region TASK LIST
// TODO: Restructure script to support a tiny _delayBetweenCharacterReveal value; Don't be next frame reliant
#endregion

public class TextReveal : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI _text;
    [SerializeField] private TMPro.TextMeshProUGUI _debugText;
    [SerializeField] private KeyCode _startKey = KeyCode.Space;
    [SerializeField] private float _delayBetweenCharacterReveal = 0.05f;

    private bool _isRevealing = false;
    private float _characterTime = 0f;
    private int _numberOfCharacters = 0;
    private int _numberOfCharactersRevealed;

    private void Reset()
    {
        _text = GetComponent<TMPro.TextMeshProUGUI>();
        _debugText = GetComponent<TMPro.TextMeshProUGUI>();
    }

    private void Awake()
    {
        if(!_text)
        {
            _text = GetComponent<TMPro.TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        if (!_text)
        {
            Debug.LogWarning("No TextMeshProUGUI component found.");
        }
        if (!_debugText)
        {
            Debug.LogWarning("No TextMeshProUGUI component found.");
        }
        else
        {
            InitializeTextReveal();
            UpdateDebugText();
        }
    }

    private void Update()
    {
        // Prevent a negative value from happening
        _delayBetweenCharacterReveal = Mathf.Clamp(_delayBetweenCharacterReveal, 0, _delayBetweenCharacterReveal);
        
        // Start text reveal on input
        if (Input.GetKeyDown((_startKey)))
        {
            InitializeTextReveal();
            _isRevealing = true;
        }

        // If we are text revealing...
        if (_isRevealing)
        {
            // Reveal text instantly (Essentially support 0 seconds)
            if (Math.Abs(_delayBetweenCharacterReveal) <= Mathf.Epsilon)
            {
                _text.maxVisibleCharacters = _numberOfCharacters;
                _numberOfCharactersRevealed = _text.maxVisibleCharacters;
            }
            else
            {
                // Reveal character over time
                _characterTime += Time.deltaTime;
                if (_characterTime > _delayBetweenCharacterReveal)
                {
                    _text.maxVisibleCharacters = _numberOfCharactersRevealed + 1 % (_numberOfCharacters);
                    _numberOfCharactersRevealed++;
                    _characterTime = 0;
                }
            }
        }

        // If all characters are revealed, set the _isRevealing flag as dirty. 
        if (_numberOfCharactersRevealed == _numberOfCharacters)
        {
            _isRevealing = false;
        }
        
        UpdateDebugText();
    }

    /// <summary>
    /// Refreshes the debug UI string
    /// </summary>
    private void UpdateDebugText()
    {
        _debugText.text = "Number of characters revealed: " + _numberOfCharactersRevealed + "/" + _numberOfCharacters + "\nDelay in-between character reveal: " + _delayBetweenCharacterReveal + " seconds.\n";
    }
    
    /// <summary>
    /// Initializes the text reveal by hiding the text and defaulting some variables
    /// </summary>
    private void InitializeTextReveal()
    {
        _text.maxVisibleCharacters = 0;
        _numberOfCharactersRevealed = 0;
        _numberOfCharacters = _text.textInfo.characterCount;
    }
}