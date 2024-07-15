
using UnityEditor;
//Callbacks
using UnityEngine;
using Random = UnityEngine.Random;

namespace TarodevController {
   
    public class PlayerAnimator : MonoBehaviour {
        
        
        private IPlayerController _player;
        
        [SerializeField] public bool isFacingRight = true;

       
       //flip the character using y transform instead of other methods like flipping sprite (for easier implementation of camera system)
         void Start(){
            TurnCheck();
        }
        
      
        
        private void FixedUpdate(){
            
            if(transform.parent.rotation.y==0f){
                isFacingRight = true;

            }
            else{
                isFacingRight = false;

            }

            
            if(_player.Input.X != 0){
                TurnCheck();
            }
        }
       
       
       
       private void TurnCheck(){
            if (_player.Input.X > 0 && !isFacingRight){
                Turn();
            }
            else if (_player.Input.X < 0 && isFacingRight){
                Turn();
            }
       }

        private void Turn(){
            if (isFacingRight){
                
                Vector3 rotator = new Vector3(transform.rotation.x, -180f, transform.rotation.z);
                transform.parent.rotation = Quaternion.Euler(rotator);
                
                

            }
            else{
                Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                transform.parent.rotation = Quaternion.Euler(rotator);
               
                
            }
            
       }

       

        void Awake() => _player = GetComponentInParent<IPlayerController>();

        void Update() {
            
            
            if (_player == null) return;
            
            //TODO 
            //player audio, animation and particles
            
    
        }

        
       
        
        
        
        
    }
}