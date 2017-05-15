using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Direction currentDir;
    Vector2 input;
    bool isMoving = false;
    bool isRotating = false;
    Vector2 startPos;
    Vector2 endPos;

    float startRot;
    float endRot;

    float t;

    Vector2 relativePos;

    public Sprite northSprite;
    public Sprite eastSprite;
    public Sprite southSprite;
    public Sprite westSprite;


    public float walkSpeed = 3f;
    public float rotationSpeed = 2f;
    

    // Update is called once per frame
    void Update () {
		if(!isMoving)
        {
            input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            if(Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                input.y = 0;
            }
            else
            {
                input.x = 0;
            }

            if(input != Vector2.zero)
            {
                //detecção de colisão aqui
                if (input.x < 0)
                {
                    currentDir = Direction.West;
                }
                if (input.x > 0)
                {
                    currentDir = Direction.East;
                }
                if (input.y < 0)
                {
                    currentDir = Direction.South;
                }
                if (input.y > 0)
                {
                    currentDir = Direction.North;
                }

                switch (currentDir)
                {
                    //código responsavel para mudar a direção onde o player aponta
                    case Direction.North:
                        gameObject.GetComponent<SpriteRenderer>().sprite = northSprite;
                        //rotação de acordo com o sprite
                        endRot = 270;
                        break;
                    case Direction.East:
                        gameObject.GetComponent<SpriteRenderer>().sprite = eastSprite;
                        //rotação de acordo com o sprite
                        endRot = 180;
                        break;
                    case Direction.South:
                        gameObject.GetComponent<SpriteRenderer>().sprite = southSprite;
                        //rotação de acordo com o sprite
                        endRot = 90;
                        break;
                    case Direction.West:
                        gameObject.GetComponent<SpriteRenderer>().sprite = westSprite;
                        //rotação de acordo com o sprite
                        endRot = 0;
                        break;
                }

                StartCoroutine(Move(transform));
            }
        }
	}

    public IEnumerator Move(Transform entity)
    {

        t = 0;
        startPos = entity.position;
        startRot = entity.transform.eulerAngles.z;

        if (startRot == endRot && !isRotating)
        {
            isMoving = true;
            endPos = new Vector2(startPos.x + System.Math.Sign(input.x), startPos.y + System.Math.Sign(input.y));

            while (t < 1f)
            {
                t += Time.deltaTime * walkSpeed;
                entity.position = Vector2.Lerp(startPos, endPos, t);

                yield return null;
            }

            isMoving = false;
            yield return 0;
        }

        if (startRot != endRot && !isMoving)
        {
            isRotating = true;
            while (t < 1f)
            {
                t += Time.deltaTime * rotationSpeed;

                entity.rotation = Quaternion.Lerp(Quaternion.Euler(0,0,startRot), Quaternion.Euler(0, 0, endRot), t);

                yield return null;
            }

            isRotating = false;
            yield return 0;
        }
    }
}

enum Direction
{
    North,
    East,
    South,
    West
}