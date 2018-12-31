// Author: Adrian Miasik
// Personal Portfolio: http://AdrianMiasik.com
// Github Account: https://github.com/AdrianMiasik

/*
File Creation Date: (yyyy-mm-dd)
	2018-08-24
Purpose:
	Reveals text characters over time much like a typewriter (Using the TextMeshProUGUI component).
	There is a delay in-between each character reveal.
*/

using UnityEngine;
using TMPro;

namespace Text.Reveal
{
	public class TypeWriter : MonoBehaviour
	{
		[SerializeField] [Tooltip("The text we want to reveal.")]
		private TextMeshProUGUI displayText;

		[SerializeField] [Tooltip("The text we will use to display statistics about this character reveal (Number of characters revealed, the delay in-between each character reveal, and the current characters reveal time).")]
		private TextMeshProUGUI statistics;

		[SerializeField] [Tooltip("The keycode that starts the reveal. When pressing this key down, the reveal will start.")]
		private KeyCode startKey = KeyCode.Space;

		[SerializeField] [Tooltip("The amount of time (in seconds) between each character reveal.")]
		private float delayBetweenReveal = 0.05f;

		private bool _isRevealing = false;
		private float _characterTime = 0f;
		private int _numberOfCharacters = 0;
		private int _numberOfCharactersRevealed;

		private void Reset()
		{
			// Quickly Fetch References.
			displayText = GetComponent<TextMeshProUGUI>();
			statistics = GetComponent<TextMeshProUGUI>();
		}

		private void Awake()
		{
			// If displayText is null...
			if (displayText == null)
			{
				// Attempt to assign
				displayText = GetComponent<TextMeshProUGUI>();

#if DEBUG_TEXT
				// Assign failed...
				if (displayText == null)
				{
					Debug.LogAssertion("No TextMeshProUGUI component found.");
				}
#endif
			}
		}

		private void Start()
		{
			if (!displayText)
			{
				Debug.LogWarning("No TextMeshProUGUI component found.");
			}
			else
			{
				Initialize();
				_isRevealing = true;
			}

			if (!statistics)
			{
				Debug.LogWarning("No TextMeshProUGUI component found.");
			}
		}

		/// <summary>
		/// Initializes the text (which also means hiding it) and starts the character reveal within the update loop.
		/// </summary>
		public void Reveal()
		{
			Initialize();
			Play();
		}

		/// <summary>
		/// Initializes the text reveal by hiding the text and defaulting some variables.
		/// </summary>
		private void Initialize()
		{
			// Hide the text
			displayText.maxVisibleCharacters = 0;
			
			_numberOfCharactersRevealed = 0;
			//_numberOfCharacters = displayText.textInfo.characterCount; // We are avoiding this because it seems to be a frame late	
			_numberOfCharacters = displayText.text.Length;	
		}

		/// <summary>
		/// Updates the statistics UI string.
		/// </summary>
		private void UpdateStatisticsText()
		{
			statistics.text = "Number of characters revealed: " + _numberOfCharactersRevealed + "/" + _numberOfCharacters + "\n" +
			                  "Delay in-between character reveal: " + delayBetweenReveal + " seconds.\n" +
			                  "Current character reveal time: " + _characterTime.ToString("F2") + " seconds.";
		}
		
		/// <summary>
		/// Starts revealing the characters.
		/// </summary>
		private void Play()
		{
			_isRevealing = true;
		}

		// TMP INPUT FIELD - Incorrect character length value of the input fields text component:
		// The reason we are doing TMP_InputField instead of getting the TextMeshProUGUI component within the input field component
		// is because the TextMeshProUGUI text.Length and textInfo.CharacterCount values are incorrect. The text field could be an "empty"
		// string, but it still seems to provides us with a length of 1. 
		// According to a Unity Technologies user "Stephan_B" we shouldn't be accessing the input fields text component anyways.
		// However, if you do need to access the text input's TextMeshProUGUI component and you do need to get the character count value from
		// that component, there does seem to be a workaround a Unity forum user named "Chris-Trueman" has found.
		// Simply trimming the Unicode character 'ZERO WIDTH SPACE' (Code 8203) from the ugui text string does seem to return the correct length.
		// I've tested Chris' solution and it does seem to work if you need to use it, however for this case I won't be doing that.
		// SOURCE: https://forum.unity.com/threads/textmesh-pro-ugui-hidden-characters.505493/
		/// <summary>
		/// Caches the string in the source's text string. (Using the TextMeshProUGUI component).
		/// </summary>
		/// <param name="source"></param>
		public void SetText(TMP_InputField source)
		{
			displayText.text = source.text;
		}

		private void Update()
		{			
			// Prevent a negative value from being assigned
			delayBetweenReveal = Mathf.Clamp(delayBetweenReveal, 0, delayBetweenReveal);

			// Start text reveal on input
			if (Input.GetKeyDown(startKey))
			{
				Play();
			}

			// If we are text revealing...
			if (_isRevealing)
			{
				// If we don't have anything to reveal...
				if (_numberOfCharacters == 0)
				{
					UpdateStatisticsText();

					// Early exit
					return;
				}
				
				_characterTime += Time.deltaTime;

				// While loop used to calculate how many letters on the same frame needs to be drawn
				while (_characterTime > delayBetweenReveal)
				{				
					// Reveal a character
					displayText.maxVisibleCharacters = _numberOfCharactersRevealed + 1 % _numberOfCharacters;

					// If all characters are revealed, set the _isRevealing flag as dirty and break out of this while loop 
					if (_numberOfCharactersRevealed == _numberOfCharacters)
					{
						_characterTime = 0f;
						_isRevealing = false;
						break;
					}
					
					_numberOfCharactersRevealed++;
					_characterTime -= delayBetweenReveal;
				}

				UpdateStatisticsText();
			}
		}
	}
}