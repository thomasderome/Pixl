using UnityEngine;
using System.Collections;

public class Bow : MonoBehaviour, Gadget_Interface
{
    [SerializeField] private GameObject arrow;
    [SerializeField] private SpriteRenderer spr;

    private float cooldown_time = 3f;
    private bool cooldown = false;
    private bool hold = false;
    private Sprite orignal_spr;
    private Vector3 orignalAngle;

    private float calculate_power;
    private float power_speed = 3.5f;
    private float max_power = 15f;
    
    private Player _player;
    public void Init(Player player)
    {
        _player = player;
    }

    public void Hold()
    {
        if (cooldown) return;
        _player.GetComponent<Gadget_Manager>().Set_gadget(true, false);
        _player.stop_animation = true;
        
        calculate_power = 0f;
        hold = true;
        
        _player.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        orignal_spr = _player.spr.sprite;
        orignalAngle = _player.transform.rotation.eulerAngles;
        _player.spr.sprite = spr.sprite;
        _player.spr.flipX = false;
    }

    public void Trigger()
    {
        if (cooldown) return;

        GameObject newArrow = Instantiate(arrow, _player.transform.position, _player.transform.rotation);
        
        Collider2D arrowCollider = newArrow.GetComponent<Collider2D>();
        Collider2D playerCollider = _player.GetComponent<Collider2D>();
        
        Physics2D.IgnoreCollision(arrowCollider, playerCollider);
        newArrow.GetComponent<Rigidbody2D>().AddForce(-transform.right * calculate_power, ForceMode2D.Impulse);
        
        StartCoroutine(Cooldown_calculate());
        
        hold = false;
        _player.stop_animation = false;
        _player.GetComponent<Gadget_Manager>().Set_gadget(false, false);

        _player.spr.sprite = orignal_spr;
        _player.rb.constraints = RigidbodyConstraints2D.None;
        _player.rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        _player.transform.rotation = Quaternion.Euler(orignalAngle.x, orignalAngle.y,orignalAngle.z);
    }
    
    public void FixedUpdate()
    {
        if (!hold) return;
        if (_player.moveInput.x != 0 && _player.moveInput.y != 0)
        {
            float angle = Mathf.Atan2(_player.moveInput.y, _player.moveInput.x) * Mathf.Rad2Deg;
            _player.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
        
        if (calculate_power < max_power) calculate_power += Time.deltaTime * power_speed;
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
