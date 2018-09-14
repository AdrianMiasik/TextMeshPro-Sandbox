// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

// Creation Date: 2018-08-24
// Purpose: Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component). There is a delay in-between each character reveal. 

using UnityEngine;
using TMPro;

namespace AdrianMiasik.Text
{
	public class CharacterReveal : MonoBehaviour
	{
		[SerializeField] [Tooltip("The text we want to reveal.")]
		private TextMeshProUGUI _text;

		[SerializeField] [Tooltip("The text we will use to display statistics about this character reveal (Number of characters revealed, the delay in-between each character reveal, and the current characters reveal time).")]
		private TextMeshProUGUI _statistics;

		[SerializeField] [Tooltip("The keycode that starts the reveal. When pressing this key down, the reveal will start.")]
		private KeyCode _startKey = KeyCode.Space;

		[SerializeField] [Tooltip("The amount of time (in seconds) between each character reveal.")]
		private float _delayBetweenReveal = 0.05f;

		private bool _isRevealing = false;
		private float _characterTime = 0f;
		private int _numberOfCharacters = 0;
		private int _numberOfCharactersRevealed;

		private void Reset()
		{
			_text = GetComponent<TextMeshProUGUI>();
			_statistics = GetComponent<TextMeshProUGUI>();
		}

		private void Awake()
		{
			if (!_text)
			{
				_text = GetComponent<TextMeshProUGUI>();
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
				_isRevealing = true;
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
		/// Updates the statistics UI string
		/// </summary>
		private void UpdateStatisticsText()
		{
			_statistics.text = "Number of characters revealed: " + _numberOfCharactersRevealed + "/" + _numberOfCharacters + "\nDelay in-between character reveal: " + _delayBetweenReveal + " seconds.\nCurrent character reveal time: " + _characterTime.ToString("F2") + " seconds.";
		}

		/// <summary>
		/// Replaces the text string in _text
		/// </summary>
		/// <param name="text"></param>
		public void Replace(TextMeshProUGUI source)
		{
			// Stop reveal
			_isRevealing = false;
			
			// Replace text
			_text.text = source.text;
			
			// Reveals all the characters
			_text.maxVisibleCharacters = source.textInfo.characterCount;
		}

		private void Update()
		{
			// Prevent a negative value from happening
			_delayBetweenReveal = Mathf.Clamp(_delayBetweenReveal, 0, _delayBetweenReveal);

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
				while (_characterTime > _delayBetweenReveal)
				{
					// Reveal a character
					_text.maxVisibleCharacters = _numberOfCharactersRevealed + 1 % _numberOfCharacters;

					_numberOfCharactersRevealed++;
					_characterTime -= _delayBetweenReveal;

					// If all characters are revealed, set the _isRevealing flag as dirty and break out of this while loop 
					if (_numberOfCharactersRevealed == _numberOfCharacters)
					{
						_characterTime = 0f;
						_isRevealing = false;
						break;
					}
				}

				UpdateStatisticsText();
			}
		}
	}
}