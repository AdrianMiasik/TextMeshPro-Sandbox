// Author: Adrian Miasik
// Personal Portfolio: https://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

// Creation Date: 2018-08-24
// Purpose: Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component). There is a delay in-between each character reveal. 

using UnityEngine;

public class CharacterReveal : MonoBehaviour
{
	[SerializeField] private TMPro.TextMeshProUGUI _text;
	[SerializeField] private TMPro.TextMeshProUGUI _statistics;
	[SerializeField] private KeyCode _startKey = KeyCode.Space;
	[SerializeField] private float _delayBetweenCharacterReveal = 0.05f;

	private bool _isRevealing = false;
	private float _characterTime = 0f;
	private int _numberOfCharacters = 0;
	private int _numberOfCharactersRevealed;

	private void Reset()
	{
		_text = GetComponent<TMPro.TextMeshProUGUI>();
		_statistics = GetComponent<TMPro.TextMeshProUGUI>();
	}

	private void Awake()
	{
		if (!_text)
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

		if (!_statistics)
		{
			Debug.LogWarning("No TextMeshProUGUI component found.");
		}
		else
		{
			InitializeTextReveal();
			UpdateStatisticsText();
		}
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

	/// <summary>
	/// Refreshes the statistics UI string
	/// </summary>
	private void UpdateStatisticsText()
	{
		_statistics.text = "Number of characters revealed: " + _numberOfCharactersRevealed + "/" + _numberOfCharacters + "\nDelay in-between character reveal: " + _delayBetweenCharacterReveal + " seconds.\n";
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
			_characterTime += Time.deltaTime;

			// While loop used to calculate how many letters on the same frame needs to be drawn
			while (_characterTime > _delayBetweenCharacterReveal)
			{
				// Reveal a character
				_text.maxVisibleCharacters = _numberOfCharactersRevealed + 1 % _numberOfCharacters;

				_numberOfCharactersRevealed++;
				_characterTime -= _delayBetweenCharacterReveal;

				// If all characters are revealed, set the _isRevealing flag as dirty and break out of this while loop 
				if (_numberOfCharactersRevealed == _numberOfCharacters)
				{
					_isRevealing = false;
					break;
				}
			}

			UpdateStatisticsText();
		}
	}
}