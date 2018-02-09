﻿using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace DeveloperConsole.UI {

    [RequireComponent(typeof(CanvasGroup))]
    public class ConsoleUIManager : MonoBehaviour {
        
        [Header("Inputs")]
        [SerializeField, Tooltip("What key should be pressed to enable/disable the command?")]
        private KeyCode showConsoleKey = KeyCode.BackQuote;
        [SerializeField]
        private KeyCode forwardHistoryKey = KeyCode.UpArrow;
        [SerializeField]
        private KeyCode backwardHistoryKey = KeyCode.DownArrow;

        [Header("UI Properties")]
        [SerializeField, Tooltip("What is the text input field to reference?")]
        private InputField inputField;
        [SerializeField, Tooltip("Which text field should show the output message?")]
        private Text outputTextField;

        [Header("History Storage")]
        [SerializeField, Tooltip("Which scriptable object store the command history?")]
        private CommandHistory commandHistory;
        [SerializeField, Tooltip("Which scriptable object stores the console outputs?")]
        private ConsoleOutputStorage consoleOutputStorage;

        private CanvasGroup canvasGroup;
        private bool isConsoleShowing;

        private void Awake() {
            isConsoleShowing = false;
        }

        private void Start() {
            canvasGroup = GetComponent<CanvasGroup>();

            Assert.IsNotNull(inputField, "No command input field cached!");
            Assert.IsNotNull(commandHistory, "No command history cached!");

            // Disable the canvas by default
            DisableConsole();
        }

        private void Update() {
            if(Input.GetKeyUp(showConsoleKey)) {
                if(!isConsoleShowing) {
                    EnableConsole();
                } else {
                    DisableConsole();
                }
            }

            if (Input.GetKeyUp(forwardHistoryKey)) {
                commandHistory.DecrementHistory();
                var command = commandHistory.GetRecentCommand();
                SetInputFieldText(command);
            }

            if (Input.GetKeyUp(backwardHistoryKey)) {
                commandHistory.IncrementHistory();
                var command = commandHistory.GetRecentCommand();
                SetInputFieldText(command);
            }
        }

        private void DisplayOutputs() {
            var consoleOutputs = consoleOutputStorage.ConsoleOutputs;
            for (var i = 0; i < consoleOutputs.Length; i++) {
                // TODO: Capture the console output.
                // TODO: Instantiate a text field.
                // TODO: Manage the # of fields instantiated.
            }
        }

        private void DisableConsole() {
            canvasGroup.alpha = 0f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = false;
        }

        private void EnableConsole() {
            canvasGroup.alpha = 1f;
            canvasGroup.blocksRaycasts = canvasGroup.interactable = isConsoleShowing = true;
        }

        private void SetInputFieldText(string input) {
            inputField.text = input;
        }

        /// <summary>
        /// Sets the input field to be empty. The input parameter
        /// is not needed, but this is used for UnityEvents.
        /// </summary>
        /// <param name="input">The input from the input field.</param>
        public void SetEmptyField(string input) {
            inputField.text = string.Empty;
        }

    }
}
