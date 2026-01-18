using System.Collections;
using UnityEngine;

public class Spike : MonoBehaviour, Gadget_Interface
{
    private Player _player;

    private float MaxDistance = 10f;
    private GameObject old_spike;

    private float cooldown_time = 5f;
    private bool cooldown = false;
    
    [SerializeField] private SpriteRenderer spr;
    [SerializeField] private GameObject spike;
    [SerializeField] private LayerMask ground;
    
    public void Init(Player player)
    {
        _player = player;
    }

    public void Hold()
    {
        if (cooldown || !_player.glue) return;   
        
        RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, -_player.transform.up, 5f, ground);
        if (hit.collider)
        {
            Vector2 start = hit.point - (hit.normal * MaxDistance);
            RaycastHit2D[] hit2 = Physics2D.RaycastAll(start, hit.normal, MaxDistance, ground);
            
            foreach (RaycastHit2D verif in hit2)
            {
                if (verif.collider == hit.collider)
                {
                    if (old_spike) Destroy(old_spike);
                    Quaternion rotation = Quaternion.FromToRotation(Vector2.up, -verif.normal);
                    old_spike = Instantiate(spike, verif.point, rotation);
                    StartCoroutine(Cooldown_calculate());
                    break;
                }
            }

        }
    }

    
    public void Trigger()
    {
    }
    
    IEnumerator Cooldown_calculate()
    {
        cooldown = true;
        spr.color = Color.red;
        yield return new WaitForSeconds(cooldown_time);
        spr.color = Color.white;
        cooldown = false;
    }
}