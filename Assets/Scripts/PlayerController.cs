using UnityEngine;
using Photon.Pun;
[RequireComponent(typeof(PlayerMotor))]

public class PlayerController : MonoBehaviour,IPunObservable
{
    
    [SerializeField]
    private float speed = 5f;
    
    [SerializeField]
    private float lookSensitivity = 3f;

    private PlayerMotor motor;
    private Vector3 _velocity;
    private Vector3 _rotation;
    private Vector3 _cameraRotation;
    private Vector3 _newVelocity;
    private Vector3 _newRotation;
    private Vector3  _newCameraRotaion;

    private GameObject mainCamera;
    public GameObject playerCamera;

    public PhotonView pv;
    void Start()
    {   
        motor = GetComponent<PlayerMotor>();
        if(pv.IsMine){
            
            mainCamera = GameObject.Find("MainCamera");
            mainCamera.SetActive(false);
            playerCamera.SetActive(true);
        }
    }

    void Update()
    {

        if(pv.IsMine){
            ProcessInputs();
        }
        else{
            SmoothInputs();
        }
    }

    public void ProcessInputs()
    {
         //calculating movement
        float _xMov = Input.GetAxisRaw("Horizontal");
        float _zMov = Input.GetAxisRaw("Vertical");
        
        

        Vector3 _moveHorizontal = transform.right * _xMov;
        Vector3 _moveVertical = transform.forward * _zMov;

        _velocity  = (_moveHorizontal + _moveVertical).normalized * speed;
        motor.Move(_velocity);

        float _yRot = Input.GetAxisRaw("Mouse X");

        _rotation =new Vector3(0f,_yRot,0f) * lookSensitivity;

        motor.Rotate(_rotation);

        //camera rotaion 
        float _xRot = Input.GetAxisRaw("Mouse Y");
 
        _cameraRotation =new Vector3(_xRot,0f,0f) * lookSensitivity;

        motor.CameraRotate(_cameraRotation);
    }
    
    public void SmoothInputs()
    {
        _velocity = Vector3.Lerp(_velocity,_newVelocity,Time.deltaTime * 10f);
        motor.Move(_velocity);
        _rotation = Vector3.Lerp(_rotation,_newRotation,Time.deltaTime * 10f);
        motor.Rotate(_rotation);
        _cameraRotation = Vector3.Lerp(_cameraRotation,_newCameraRotaion,Time.deltaTime * 10f);
        motor.CameraRotate(_cameraRotation);
    }
     public void OnPhotonSerializeView(PhotonStream stream,PhotonMessageInfo info)
    {
        if(stream.IsWriting){
            stream.SendNext(_velocity);
            stream.SendNext(_rotation);
            stream.SendNext(_cameraRotation);
        }
        else if(stream.IsReading)
        {
            _newVelocity = (Vector3) stream.ReceiveNext();
            _newRotation = (Vector3) stream.ReceiveNext();
            _newCameraRotaion = (Vector3) stream.ReceiveNext();
        }
    }


}
