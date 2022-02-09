using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// MouseButton
using UnityEngine.EventSystems;
// Mouse
using UnityEngine.UI;
// using input device
// IO
using System.IO;
// TextMeshProUGUI
using TMPro;

class Tiles {
    public const string Center = "CC";
    public const string TopCenter = "TC";

    public const string TopLeft = "TL";

    public const string TopRight = "TR";

    public const string CenterLeft = "CL";

    public const string CenterRight = "CR";

    public const string BottomCenter = "BC";

    public const string BottomLeft = "BL";

    public const string BottomRight = "BR";
}

class Moves {
    Dictionary<string, bool> moves = new Dictionary<string, bool>();

    public Moves() {
        moves[Tiles.Center] = false;
        moves[Tiles.TopCenter] = false;
        moves[Tiles.TopLeft] = false;
        moves[Tiles.TopRight] = false;
        moves[Tiles.CenterLeft] = false;
        moves[Tiles.CenterRight] = false;
        moves[Tiles.BottomCenter] = false;
        moves[Tiles.BottomLeft] = false;
        moves[Tiles.BottomRight] = false;
    }

    public bool nextMove(string n) {
        moves[n] = true;

        bool win = false;

        if (n == Tiles.Center) {
            if (moves[Tiles.TopCenter] && moves[Tiles.BottomCenter]) {
                win = true;
            } else if (moves[Tiles.CenterLeft] && moves[Tiles.CenterRight]) {
                win = true;
            } else if (moves[Tiles.TopLeft] && moves[Tiles.BottomRight]) {
                win = true;
            } else if (moves[Tiles.TopRight] && moves[Tiles.BottomLeft]) {
                win = true;
            }
        } else if (n == Tiles.TopCenter) {
            if (moves[Tiles.TopLeft] && moves[Tiles.TopRight]) {
                win = true;
            } else if (moves[Tiles.Center] && moves[Tiles.BottomRight]) {
                win = true;
            }
        } else if (n == Tiles.TopLeft) {
            if (moves[Tiles.CenterLeft] && moves[Tiles.BottomLeft]) {
                win = true;
            } else if (moves[Tiles.TopCenter] && moves[Tiles.TopRight]) {
                win = true;
            } else if (moves[Tiles.Center] && moves[Tiles.BottomRight]) {
                win = true;
            }
        } else if (n == Tiles.TopRight) {
            if (moves[Tiles.CenterRight] && moves[Tiles.BottomRight]) {
                win = true;
            } else if (moves[Tiles.TopCenter] && moves[Tiles.TopLeft]) {
                win = true;
            } else if (moves[Tiles.Center] && moves[Tiles.BottomLeft]) {
                win = true;
            }
        } else if (n == Tiles.CenterLeft) {
            if (moves[Tiles.Center] && moves[Tiles.CenterRight]) {
                win = true;
            } else if (moves[Tiles.TopLeft] && moves[Tiles.BottomLeft]) {
                win = true;
            }
        } else if (n == Tiles.CenterRight) {
            if (moves[Tiles.Center] && moves[Tiles.CenterLeft]) {
                win = true;
            } else if (moves[Tiles.TopRight] && moves[Tiles.BottomRight]) {
                win = true;
            }
        } else if (n == Tiles.BottomCenter) {
            if (moves[Tiles.Center] && moves[Tiles.TopCenter]) {
                win = true;
            } else if (moves[Tiles.BottomLeft] && moves[Tiles.BottomRight]) {
                win = true;
            }
        } else if (n == Tiles.BottomLeft) {
            if (moves[Tiles.CenterLeft] && moves[Tiles.TopLeft]) {
                win = true;
            } else if (moves[Tiles.BottomCenter] && moves[Tiles.BottomRight]) {
                win = true;
            }
        } else if (n == Tiles.BottomRight) {
            if (moves[Tiles.CenterRight] && moves[Tiles.TopRight]) {
                win = true;
            } else if (moves[Tiles.BottomCenter] && moves[Tiles.BottomLeft]) {
                win = true;
            } else if (moves[Tiles.Center] && moves[Tiles.TopLeft]) {
                win = true;
            }
        }

        return win;
    }
}

public class SecondScript : MonoBehaviour
{
    Moves adamMoves = new Moves();
    Moves eveMoves = new Moves();

    public bool isAdamMove = true;

    public bool OnMouseDown(string n) {
        print("clicked on " + n);

        bool win = false;
        
        TextMeshProUGUI scoreComponent;

        if (isAdamMove) {
            win = adamMoves.nextMove(n);
            scoreComponent = GameObject.Find("AdamScore").GetComponent<TextMeshProUGUI>();
        } else {
            win = eveMoves.nextMove(n);
            scoreComponent = GameObject.Find("EveScore").GetComponent<TextMeshProUGUI>();
        }

        scoreComponent.text = (int.Parse(scoreComponent.text) + 1).ToString();

        isAdamMove = !isAdamMove;

        if (win)   {
            scoreComponent.text = (int.Parse(scoreComponent.text) + 25).ToString();

            print("win by " + (isAdamMove ? "Adam" : "Eve"));

            return true;
        } else {
            return false;
        }
    }

    private void win() {
        print("you win current player");
    }

    // Start is called before the first frame update
    void Start()
    {
        print("STARTED");
    }

    // Update is called once per frame
    void Update()
    {
        // move straight by y axis every second
        // float increaseValue = 2.21f * Time.deltaTime;
        // transform.position = new Vector3(0, 0, transform.position.z + increaseValue);
    }

    void Rotate() {
        float xAngle = 0;
        float yAngle = Input.GetAxis("Mouse X") * 3;
        float zAngle = 0;

        transform.Rotate(xAngle, yAngle, zAngle, Space.World);
    }

    // upon collision
    void onTriggerEnter(Collider other) {
        print("SECONDSCRIPT");
        
        // print("collision on " + gameObject.name + " " + other.gameObject.name);
    }
}
