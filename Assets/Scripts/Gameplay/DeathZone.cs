using UnityEngine;

namespace TraineeGame
{
    public class DeathZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "GateObstacle" || other.gameObject.tag == "StoneObstacle")
            {
                other.gameObject.SetActive(false);
            }
        }
    }
}
