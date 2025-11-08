using System.Collections.Generic;
using TMPro;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class radio : MonoBehaviour
{
    [SerializeField] TMP_InputField player_message;
    [SerializeField] TMP_InputField player_key;
    [SerializeField] TextMeshProUGUI log;
    private string message;
    private string key;
    private string encrypted_message;
    private bool is_message_complete;
    private List<string> alphabet = new List<string>();
    private List<string> alphabet_shifted = new List<string>();
    private List<string> letters_encrypted = new List<string>();
    private List<string> alphabet_trimmed = new List<string>();

    private Dictionary<string, string> alphabet_encrypted = new Dictionary<string, string>();
    private void Awake()
    {
        //Adding alphabet manually because idk how to do it automatically
        alphabet.Add("A"); alphabet.Add("B"); alphabet.Add("C"); alphabet.Add("D");
        alphabet.Add("E"); alphabet.Add("F"); alphabet.Add("G"); alphabet.Add("H");
        alphabet.Add("I"); alphabet.Add("J"); alphabet.Add("K"); alphabet.Add("L");
        alphabet.Add("M"); alphabet.Add("N"); alphabet.Add("O"); alphabet.Add("P");
        alphabet.Add("Q"); alphabet.Add("R"); alphabet.Add("S"); alphabet.Add("T");
        alphabet.Add("U"); alphabet.Add("V"); alphabet.Add("W"); alphabet.Add("X");
        alphabet.Add("Y"); alphabet.Add("Z");

        for (int i = 0; i < alphabet.Count; i++)
        {
            alphabet_shifted.Add(alphabet[i]);
            alphabet_trimmed.Add(alphabet[i]);
            alphabet_encrypted.Add(alphabet[i], alphabet[i]);
        }
    }
    public void get_text(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            encrypt_message();
        }
    }
    public void set_message_text()
    {
        message = player_message.text.ToUpper();
    }
    public void get_message_recepient()
    {

    }
    public void set_encryption_key()
    {
        key = player_key.text.ToUpper();
    }
    public void check_letters()
    {
        for (int i = 0; i < alphabet_trimmed.Count; i++)
        {
            if (letters_encrypted.Contains(alphabet_trimmed[i]))
            {
                alphabet_trimmed.RemoveAt(i);
                check_letters();
                return;
            }
        }
    }
    public void encrypt_message()
    {
        message = player_message.text.ToUpper();
        key = player_key.text.ToUpper();
        encrypted_message = "";

        letters_encrypted.Clear();
        alphabet_shifted.Clear();
        alphabet_trimmed.Clear();
        alphabet_encrypted.Clear();

        for (int i = 0; i < alphabet.Count; i++)
        {
            alphabet_shifted.Add(alphabet[i]);
            alphabet_trimmed.Add(alphabet[i]);
            alphabet_encrypted.Add(alphabet[i], alphabet[i]);
        }

        for (int i = 0; i < key.Length; i++)
        {
            alphabet_shifted[i] = key[i].ToString();
            letters_encrypted.Add(alphabet_shifted[i]);
        }

        check_letters();

        for (int i = letters_encrypted.Count; i < alphabet_trimmed.Count; i++)
        {
            alphabet_shifted[i] = alphabet_trimmed[i-letters_encrypted.Count];
        }
        for (int i = 0; i < alphabet_encrypted.Count; i++)
        {
            alphabet_encrypted[alphabet[i]] = alphabet_shifted[i];
        }
        for (int i = 0; i < message.Length; i++)
        {
            if (message[i].ToString() != " ")
            {
                encrypted_message += alphabet_encrypted[message[i].ToString()];
            }
            else
            {
                encrypted_message += message[i];
            }
        }
        encrypted_message = encrypted_message.ToUpper();
        log.text += encrypted_message + "\n";
    }
    public void send_message(string recepient, string encryption_key, string message_type, string encryption_level)
    {
        message = "To " + recepient + ". " + message_type;
        is_message_complete = true;
    }

    public void decode_message()
    {
        if (message == null) { return; }
        
    }
}
