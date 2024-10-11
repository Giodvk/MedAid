using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Chat; // Per Photon Chat
using Photon.Pun;
using ExitGames.Client.Photon;  // Per Photon Authentication

public class ChatManager : MonoBehaviour, IChatClientListener
{
    private ChatClient chatClient;
    public TMP_Text chatDisplay;  // Campo di testo per visualizzare i messaggi
    public TMP_InputField inputField;  // Campo di input per scrivere i messaggi
    public string chatChannelName = "myCustomChannel"; // Nome del canale personalizzato

    private List<string> messageHistory = new List<string>();
    private const int maxMessages = 5;

    void Start()
    {
        // Configura il client di Photon Chat usando le credenziali di Photon PUN
        chatClient = new ChatClient(this);
        chatClient.Connect(PhotonNetwork.PhotonServerSettings.AppSettings.AppIdChat, 
                           "1.0", new AuthenticationValues(PhotonNetwork.NickName));
    }

    void Update()
    {
        if (chatClient != null)
        {
            chatClient.Service(); // Aggiornamento regolare del client per gestire la ricezione dei messaggi
        }

        // Se premi il tasto Y, attiva il campo di input
        if (Input.GetKeyDown(KeyCode.Y))
        {
            inputField.Select();  // Seleziona e attiva il campo di input
        }

        // Se premi il tasto Invio, invia il messaggio
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(inputField.text))
        {
            SendChatMessage();  // Invia il messaggio
        }
    }

    // Funzione chiamata quando si ricevono nuovi messaggi
    public void OnGetMessages(string channelName, string[] senders, object[] messages)
    {
        if (channelName == chatChannelName) // Assicurati che il messaggio provenga dal canale corretto
        {
            for (int i = 0; i < messages.Length; i++)
            {
                string newMessage = $"{senders[i]}: {messages[i].ToString()}";
                AddMessageToHistory(newMessage);
            }

            UpdateChatDisplay(); // Aggiorna la UI con gli ultimi messaggi
        }
    }

    // Aggiunge un messaggio alla cronologia e mantiene solo gli ultimi 5
    private void AddMessageToHistory(string message)
    {
        messageHistory.Add(message);

        if (messageHistory.Count > maxMessages)
        {
            messageHistory.RemoveAt(0); // Rimuove il messaggio più vecchio
        }
    }

    // Aggiorna il campo di testo con gli ultimi messaggi
    private void UpdateChatDisplay()
    {
        chatDisplay.text = string.Join("\n", messageHistory);
    }

    // Funzione per inviare un messaggio di chat
    public void SendChatMessage()
    {
        if (!string.IsNullOrEmpty(inputField.text)) // Verifica che il campo non sia vuoto
        {
            chatClient.PublishMessage(chatChannelName, inputField.text); // Invia il messaggio al canale personalizzato
            inputField.text = ""; // Resetta il campo di input
            inputField.DeactivateInputField();  // Disattiva il campo di input dopo l'invio
        }
    }

    // Implementazione dell'interfaccia IChatClientListener
    public void OnDisconnected() { }
    public void OnConnected() 
    { 
        chatClient.Subscribe(new string[] { chatChannelName }); // Iscriviti al canale personalizzato
    }
    public void OnSubscribed(string[] channels, bool[] results) { }
    public void OnUnsubscribed(string[] channels) { }
    public void OnStatusUpdate(string user, int status, bool gotMessage, object message) { }
    public void OnPrivateMessage(string sender, object message, string channelName) { }

    public void DebugReturn(DebugLevel level, string message)
    {
       
    }

    public void OnChatStateChange(ChatState state)
    {
        
    }

    public void OnUserSubscribed(string channel, string user)
    {
       
    }

    public void OnUserUnsubscribed(string channel, string user)
    {
        
    }
}
