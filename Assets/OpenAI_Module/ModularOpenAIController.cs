using OpenAI_API;
using OpenAI_API.Chat;
using OpenAI_API.Models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using UnityEditor;

public class ModularOpenAIController : MonoBehaviour
{
    private OpenAIAPI api;
    private List<ChatMessage> cardCreationMessage;
    List<BaseCard> cards;
    private ModuleConfigGetterSetter moduleConfigGetterSetter;

    // REGEX Expression for card creation

    string rxCardNameString = @"(?<=([0-9]. )).*(?=(:))";
    // New Regex Card Name String (?<=([0-9]. )).*(?=(:))
    // Old Regex (?<=([0-9]. )).*(?=(: Description:))
    string rxDescriptionString = @"(?<=(Description: )).*";
    // New Regext Description String (?<=(Description: )).*
    // Old Regex (?<=(: Description: )).*(?=(, HP:))
    string rxHPString = @"(?<=(HP: )).*";
    // New Regex Description String (?<=(HP: )).*
    // Old Regex (?<=(: HP: )).*(?=(, Speed:))
    string rxSpeedString = @"(?<=(Speed: )).*";
    // New Regex Speed String (?<=(Speed: )).*
    // Old Regex (?<=(, Speed: )).*(?=(, Attack: ))
    string rxAttackString = @"(?<=(Attack: )).*";
    // New Regex Attack String (?<=(Attack: )).*
    // Old Regex (?<=(, Attack: )).*(?=(\n)?)

    public ModularOpenAIController(){
        string configJsonString  = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Assets/OpenAI_Module/moduleConfig.json"));
        moduleConfigGetterSetter = JsonConvert.DeserializeObject<ModuleConfigGetterSetter>(configJsonString)!;
    }
    //Need to be a list because multiple Requests to the API will be made

    // Start is called before the first frame update
    public List<BaseCard> submitCharacterPrompt(string inputPrompt)
    {
        //Create a new instance of the OpenAI API, and give it the APIKEY (Stored in the System Environment Variables)
        api = new OpenAIAPI(Environment.GetEnvironmentVariable("OPEN_AI_APIKEY", EnvironmentVariableTarget.User));
        return StartCharacterCreation(inputPrompt);
    }
    private List<BaseCard> StartCharacterCreation(string inputPrompt)
    {
        Debug.Log("Modular Button function Beginning");
        cardCreationMessage = new List<ChatMessage> { 
            //This is where the prompt limits are imput
            new (ChatMessageRole.System, "You are to create exactly" + moduleConfigGetterSetter.NumberOfObjcets + " creatures related to the character brief that is given, you cannot create more or less. These creatures will be used for " + moduleConfigGetterSetter.ObjectContextDescription + ". You will respond with only the creature's " + moduleConfigGetterSetter.ObjectAttributes + " stats, no other information. The format for each creature should be numbered list similar to this '1. Lizard Frog:\n Description: This creatures lives underground and has scaly skin\n HP: 10\n Speed: 10\n Attack: 10' then double new line to create a gap between objects")
            // Example Brief: The character brief is: I am a noble knight. I was born in a little village and conscripted into the royal army for training at a young age. I fight with sword and shield honourably to protect the king's palace.
        };

        Task<List<BaseCard>> task = Task.Run(() =>
        {
            return GetResponse(inputPrompt);
        });

        task.Wait();

        return task.Result;
    }

    private async Task<List<BaseCard>> GetResponse(string inputPrompt)
    {
        // Fill the user message form the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = inputPrompt;
        if (userMessage.Content.Length > 200) //This is here to save tokens when making an API
        {
            //Shorten user message if over 100
            userMessage.Content = userMessage.Content.Substring(0, 200);
        }
        Debug.Log(string.Format("{0}: {1}", userMessage.rawRole, userMessage.Content));

        //Add Message to list
        cardCreationMessage.Add(userMessage);


        // Send Character creation message to OpenAI to get the reponse in cards
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = moduleConfigGetterSetter.TokenLimit,
            Messages = cardCreationMessage
        });

        // Get Response from API
        ChatMessage APIResponse = new ChatMessage();
        APIResponse.Role = chatResult.Choices[0].Message.Role;
        APIResponse.Content = chatResult.Choices[0].Message.Content;
        Debug.Log(string.Format("{0}: {1}", APIResponse.rawRole, APIResponse.Content));

        cardCreationMessage.Add(APIResponse);

        string apiResponseString = APIResponse.Content;


        // Split Creatures/Objects into individual Strings
        string[] cardUnserialized = apiResponseString.Split(
            new string[] {"\n\n"},
            StringSplitOptions.None
        );

         Debug.Log(cardUnserialized[0]);
        // adds each card name to a string
        string cardNames = "";
        foreach (var item in cardUnserialized)
        {
            cardNames += Regex.Match(item, rxCardNameString) + ", ";
        }

        // removes final comma and space
        cardNames = cardNames.Substring(0, cardNames.Length - 2);
        Debug.Log(cardNames);

        string alloactedImages = await allocateImages(cardNames);

        //Initialize Array of Card Objects
        cards = new List<BaseCard>();
        int i = 0;
        foreach (var item in cardUnserialized)
        {
            Match nameMatch = Regex.Match(item, rxCardNameString);
            Match descriptionMatch = Regex.Match(item, rxDescriptionString);
            Match hpMatch = Regex.Match(item, rxHPString);
            Match speedMatch = Regex.Match(item, rxSpeedString);
            Match attackMatch = Regex.Match(item, rxAttackString);
            Match imageMatch = Regex.Match(alloactedImages, @"(?<=(" + nameMatch.Value + ": )).*");
            Debug.Log(descriptionMatch.Value);
            Debug.Log($"item: {item}");
            BaseCard card = new BaseCard(
                                nameMatch.Value,
                                descriptionMatch.Value,
                                int.Parse(attackMatch.Value), 
                                int.Parse(speedMatch.Value), 
                                int.Parse(hpMatch.Value),
                                imageMatch.Value
                                );
            cards.Add(card);
            i++;
        }

        Debug.Log(cards[0].cardName.ToString());
        Debug.Log(cards[0].strength.ToString());

        return cards;
    }

    private async Task<string> allocateImages(string cardNames)
    {
        string imageOptions = "wug, beast, humanoid, furniture";
        string imageAllocationPrompt = "The following items are playing cards in a card game: " + cardNames + ". The following items are descriptive words: " + imageOptions + ". You are responsible for allocating one, and only one, of the provided descriptive words to each of the provided cards. Format your response in the following way 'card name: descriptive word' and then start a new line.";

        // Fill the user message form the input field
        ChatMessage userMessage = new ChatMessage();
        userMessage.Role = ChatMessageRole.User;
        userMessage.Content = imageAllocationPrompt;

        List<ChatMessage> test = new List<ChatMessage>();
        test.Add(userMessage);

        // Send Character creation message to OpenAI to get the reponse in cards
        var chatResult = await api.Chat.CreateChatCompletionAsync(new ChatRequest()
        {
            Model = Model.ChatGPTTurbo,
            Temperature = 0.1,
            MaxTokens = 300,
            Messages = test
        });

        // Get Response from API
        string apiResponseString = chatResult.Choices[0].Message.Content;

        return apiResponseString;
    }

    internal static void submitCharacterPrompt()
    {
        throw new NotImplementedException();
    }
}
public class ModuleConfigGetterSetter {
    public int NumberOfObjcets { get; set; }
    public int NumberOfObjectAttributes { get; set; }
    public int TokenLimit { get; set; }
    public string ObjectAttributes { get; set; }
    public string ObjectContextDescription { get; set; }
}