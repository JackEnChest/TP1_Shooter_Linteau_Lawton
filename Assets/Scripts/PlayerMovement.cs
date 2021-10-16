using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook cinematicCamera;
    [SerializeField] private Transform viewingCamera;
    [SerializeField] private CharacterController characterController;
    [SerializeField] private float speed = 5f;

    private Vector3 direction;
    private float rotationTime = 0.1f;
    private float rotationSpeed;

    private float gravity = 30f;//9.81f;
    private float jumpSpeed = 20f;//6f;
    private float vecticalMovement = 0f;

    private float isJoystickPresentTimer = 1f;
    private float isJoystickPresentTimerLimit = 1f;
    private bool isJoystickPresent;
    private bool recordedJoystickPresence = false;

    private void Start()
    {
        testIfJoystickIsPresent();
        recordedJoystickPresence = isJoystickPresent;
        changeCinematicCameraInput();
    }

    // Update is called once per frame
    void Update()
    {

        testIfJoystickIsPresent();

        if (recordedJoystickPresence != isJoystickPresent)
        {
            recordedJoystickPresence = isJoystickPresent;
            changeCinematicCameraInput();
        }

        //On a droit � un seul move par framerate.  Bien que le d�placement et le saut n'ont rien � voir
        //chaque partie doit "construire" le vector de direction � sa fa�on.  Avoir un move pour le saut et un autre pour le mouvement
        //ferait en sorte que le joueur ne pourrait pas se d�placer et sauter en m�me temps.
        BuildSurfaceMovement();
        BuildVerticalMovement();

        characterController.Move(direction);
    }

    private void BuildSurfaceMovement()
    {
        //Get axis donne au d�placement un effet d'ajout progressif au d�placement, comme si on utilisait un joystick analogique
        //GetAxisRaw enl�ve cet effet.  Par contre il est pr�sent naturellement pour un joystick
        //Avec un Joystick le raw n'est pas recommand�.
        //float horizontal = Input.GetAxisRaw("Horizontal");
        //float vertical = Input.GetAxisRaw("Vertical");

        //.normalized fait en sorte que nous aurons un vecteur de 1 de longeur, peu importe la direction
        //Emp�che donc le playerController d'aller plus vite en diagonale.
        //Par contre avec un gamepad cela fait en sorte qu'on a plus l'effet de vitesse progressive qui on enfonce le gamepad � moiti�
        //La vitesse est totale ou elle ne l'est pas.
        //direction = new Vector3(horizontal, 0f, vertical).normalized;
        if (isJoystickPresent)
            direction = new Vector3(Input.GetAxis("LeftHorizontal"), 0f, Input.GetAxis("LeftVertical"));
        else
            direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        //Pour �liminer les effets de shack du joystick
        //Avec le clavier + normalized, la magnitude sera toujours de 1.
        if (direction.magnitude >= 0.1f)
        {
            //l'angle que l'on vise avec nos contr�les.  Je pense que �a doit vous dire quelque chose.
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + viewingCamera.eulerAngles.y;

            //SmoothDampAngle permet de faire un d�placement progressif entre l'angle actuel et l'angle vis�.
            //Sans cette ligne de code, le pivot du personnage sera brutal.
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotationSpeed, rotationTime);

            //Quaternion.Euler permet de g�rer correctement les rotaions en degr�s malgr� que l'on ai affaire � un quaternion.
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //Si vous voulez que le personnage diminue de vitesse en saut.
            //Si on voudrait que le saut ne change pas de direction: garder le m�me vecteur en x et z qaund le joueur n'est pas grounded
            //Si on veut que la vitesse reste optimal tant qu'on en change pas de direction: enregistrer la direction au saut et ralentir seulement si
            //Cette direction change
            float tempSpeed = speed;
            if (!characterController.isGrounded) tempSpeed /= 2;

            Vector3 moveDirection = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);//.normalized;

            if (!isJoystickPresent) moveDirection = moveDirection.normalized;

            direction.x = moveDirection.x * tempSpeed * Time.deltaTime;
            direction.z = moveDirection.z * tempSpeed * Time.deltaTime;

        }
        else
        {
            direction = Vector3.zero;
        }
    }

    private void BuildVerticalMovement()
    {        
        if (Input.GetButtonDown("Jump"))
        {
            if (characterController.isGrounded)
            {
                vecticalMovement = jumpSpeed;
            }
        }

        vecticalMovement -= gravity * Time.deltaTime;
        direction.y = vecticalMovement * Time.deltaTime;
    }

    private void testIfJoystickIsPresent()
    {
        //Pour faire ce test � toutes les secondes (ou autre valeur, au choix)
        isJoystickPresentTimer += Time.deltaTime;
        if (isJoystickPresentTimer < isJoystickPresentTimerLimit) return;

        //On enl�ve la valeur de la limite: plus pr�cis que de simplement ramener la valeur � 0;
        isJoystickPresentTimer -= isJoystickPresentTimerLimit;

        //S'il n'y a pas de joystick Input.GetJoystickNames().Length sera de z�ro OU
        //S'il y a d�j� eu un joystick, elle sera de 1 par joystick d�j� branch�, mais
        //chacune des strings seront vides ("")
        for (int i = 0; i < Input.GetJoystickNames().Length; i++)
            if (Input.GetJoystickNames()[i].Length != 0)
            {
                isJoystickPresent = true;
                return;
            }

        isJoystickPresent = false;
    }

    private void changeCinematicCameraInput()
    {
        if (isJoystickPresent)
        {
            cinematicCamera.m_XAxis.m_InputAxisName = "RightHorizontal";
            cinematicCamera.m_YAxis.m_InputAxisName = "RightVertical";

            cinematicCamera.m_YAxis.m_MaxSpeed = 2f;
            cinematicCamera.m_YAxis.m_AccelTime = 0.2f;

            cinematicCamera.m_XAxis.m_MaxSpeed = 200f;
        }
        else
        {
            cinematicCamera.m_XAxis.m_InputAxisName = "Mouse X";
            cinematicCamera.m_YAxis.m_InputAxisName = "Mouse Y";

            cinematicCamera.m_YAxis.m_MaxSpeed = 4f;
            cinematicCamera.m_YAxis.m_AccelTime = 0.4f;

            cinematicCamera.m_XAxis.m_MaxSpeed = 600f;
        }
    }
}


