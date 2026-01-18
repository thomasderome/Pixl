using System.Collections;
using UnityEngine;

public class Growth : MonoBehaviour, Gadget_Interface
{
    private Player _player;

    [SerializeField] private SpriteRenderer spr_icon;
    [SerializeField] private LayerMask attack;
    
    private float growth_power = 1.5f;
    private float time_growth = 8f;
    
    private bool hold = false;
    
    private bool cooldown = false;
    private float cooldown_time = 6f;
    
    [SerializeField] private Sprite gun;
    private Sprite orignal_spr;
    private Vector3 orignalAngle;
    
    public LineRenderer lineRenderer;
    public float range = 200f;

    public void Init(Player player)
    {
        _player = player;
    }

    public void Hold()
    {
        if (cooldown) return;
        _player.stop_animation = true;
        _player.GetComponent<Gadget_Manager>().Set_gadget(true, false);
        
        hold = true;
        _player.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        orignal_spr = _player.spr.sprite;
        orignalAngle = _player.transform.rotation.eulerAngles;
        _player.spr.sprite = gun;
        _player.spr.flipX = false;
    }
        
    public void Trigger()
    {
        if (cooldown) return;
        
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, _player.transform.position);
        
        RaycastHit2D[] allhit = Physics2D.RaycastAll(_player.transform.position, _player.transform.right, range);
        if (allhit.Length == 0)
        {
            Vector3 directionMax = _player.transform.position + (_player.transform.right * range);
            lineRenderer.SetPosition(1, directionMax);
        }
        else
        {
            for (int i = 0; i < allhit.Length; i++)
            {
                if (allhit[i].collider != _player.GetComponent<Collider2D>() || i != 0)
                {
                    lineRenderer.SetPosition(1, allhit[i].point);
                    Transform hit_transform = allhit[i].collider.gameObject.transform;
                    StartCoroutine(Smooth_scale(hit_transform, hit_transform.localScale * growth_power, 1f));
                    break;
                }
            }
        }

        Invoke("Hide_laser", 0.2f);
        StartCoroutine(Cooldown_calculate());
        
        hold = false;
        _player.GetComponent<Gadget_Manager>().Set_gadget(false, false);
        _player.stop_animation = false;

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
    }

    IEnumerator Smooth_scale(Transform _object, Vector3 to, float time)
    {
        float finish = 0;
        while (finish < time)
        {
            finish += Time.deltaTime;
            _object.localScale = Vector3.Lerp(_object.localScale, to, finish / time);
            yield return new WaitForEndOfFrame();
        }
        _object.localScale = to;

        yield return new WaitForSecondsRealtime(time_growth);
        
        finish = 0;
        to = _object.localScale / growth_power;
        time = 5f;
        while (finish < time)
        {
            finish += Time.deltaTime;
            _object.localScale = Vector3.Lerp(_object.localScale, to, finish / time);
            yield return new WaitForEndOfFrame();
        }
        _object.localScale = to;
    }

    IEnumerator Cooldown_calculate()
    {
        cooldown = true;
        spr_icon.color = Color.red;
        yield return new WaitForSeconds(cooldown_time);
        spr_icon.color = Color.white;
        cooldown = false;
    }
    
    private void Hide_laser()
    {
        lineRenderer.enabled = false;
    }
}
