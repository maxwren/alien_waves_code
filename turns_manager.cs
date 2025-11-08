using UnityEngine;

public class turns_manager : MonoBehaviour
{
    private int turns_concluded;

    private bool message_sent;
    private bool message_received;
    private bool turn_concluded;

    public bool is_turn_concluded()
    {
        return turn_concluded;
    }

    public void conclude_turn()
    {
        turns_concluded++;
        message_sent = false;
        message_received = false;
        turn_concluded = true;
    }
}
