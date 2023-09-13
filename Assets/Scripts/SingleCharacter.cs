using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleCharacter : MonoBehaviour
{
    public static SingleCharacter Instance;

    List<string> Questions = new List<string>
    { // Optionally can use 1 question and user enters text within the limit
        "What is your backstory?", 
        "What is your profession?",
        "What motivates you?"
    };


    private void Awake()
    {
        // Ensure that only one instance of this class exists.
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that the object persists across scenes
        }
        else {
            Destroy(gameObject); // Destroy any additional instances
        }
    }

    public void AddQuestionAndResponse(string question, string response)
    {
        if (!questionsAndResponses.ContainsKey(question)) {
            questionsAndResponses.Add(question, response);
        }
        else {
            // Handle duplicate questions, if necessary. For example:
            questionsAndResponses[question] = response; // Overwrite the existing response
        }
    }

    public string GetResponse(string question)
    {
        if (questionsAndResponses.TryGetValue(question, out string response)) {
            return response;
        }
        else {
            return null; // or return a default/fallback response
        }
    }

    // ... Additional methods as needed ...
}
