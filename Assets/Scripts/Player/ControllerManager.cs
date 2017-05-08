using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerManager : MonoBehaviour
{
    private GameObject player;
    private PlayerFSM playerFSM;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerFSM = player.GetComponent<PlayerFSM>();
    }

    private void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.CompareTag("Road"))
                {
                    playerFSM.MoveTo(hit.point);
                }
                else if (hit.collider.CompareTag("Enemy"))
                {
                    playerFSM.AttackEnemy(hit.collider.gameObject);
                }
            }
        }
    } 
}
