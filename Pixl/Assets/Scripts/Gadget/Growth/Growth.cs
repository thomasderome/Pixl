using System.Collections;
using UnityEngine;

public class Growth : MonoBehaviour, Gadget_Interface
{
    private Player _player;

    [SerializeField] private SpriteRenderer spr_icon;
    
    private float growth_power = 1.2f;
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
        
        hold = true;
        _player.rb.constraints = RigidbodyConstraints2D.FreezeAll;
        orignal_spr = _player.spr.sprite;
        orignalAngle = _player.transform.rotation.eulerAngles;
        _player.spr.sprite = gun;
    }
        
    public void Trigger()
    {
        if (cooldown) return;
        
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, _player.transform.position);

        RaycastHit2D hit = Physics2D.Raycast(_player.transform.position, _player.transform.right, range,
            _player.ground_layer);
        
        if (hit)
        {
            lineRenderer.SetPosition(1, hit.point);
            Transform hit_transform = hit.collider.gameObject.transform;
            StartCoroutine(Smooth_scale(hit_transform, hit_transform.localScale * growth_power, 1f));
            hit.collider.gameObject.AddComponent<Reset_Scale>();
        }
        else
        {
            Vector3 directionMax = _player.transform.position + (_player.transform.right * range);
            lineRenderer.SetPosition(1, directionMax);
        }
        
        Invoke("Hide_laser", 0.2f);
        StartCoroutine(Cooldown_calculate());
        
        hold = false;
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
        to = _object.localScale / 1.2f;
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
