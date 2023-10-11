using OpenAI_API.Chat;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SingleCharacter : MonoBehaviour
{
    public static SingleCharacter Instance;

    public List<ChatMessage> cardCreationMessage;
    public List<BaseCard> cards;

    public class QuestionResponse
    {
        public string question;
        public string response;
    }
    public List<QuestionResponse> AskedQuestions = new List<QuestionResponse>();

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
        if (!AskedQuestions.Any(qr => qr.question.Equals(question))) {
            AskedQuestions.Add(new QuestionResponse { question=question, response=response });
        }
    }
}
