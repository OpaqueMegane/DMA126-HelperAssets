using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimatorHelper126 : MonoBehaviour
{
    Animator _animator;
    Animator animator
    {
        get
        {
            if (_animator == null)
            {
                _animator = GetComponent<Animator>();
            }
            return _animator;
        }
    }

    /// <summary>
    /// Wrapper for Animator.SetBool that works with UIButtons, TriggerZones, etc...
    /// </summary>
    /// <param name="idAndValue">e.g. "moving=true"</param>
    public void SetBool(string idAndValue)
    {
        SetAnimatorValGeneric(
            idAndValue, 
            bool.TryParse, 
            (Animator animator, string varName, bool val) => animator.SetBool(varName, val)
            );
    }

    public void FlipBool(string id)
    {
        animator.SetBool(id, !animator.GetBool(id));
    }

    delegate bool ParseDelegate<T>(string stringVal, out T value);
    delegate void AnimatorSetDelegate<T>(Animator animator, string id, T value);

    /// <summary>
    /// Wrapper for Animator.SetBool that works with UIButtons, TriggerZones, etc...
    /// </summary>
    /// <param name="idAndValue">e.g. "moving=true"</param>
    public void SetInteger(string idAndValue)
    {
        SetAnimatorValGeneric(
        idAndValue,
        int.TryParse,
        (Animator animator, string varName, int val) => animator.SetInteger(varName, val)
        );
    }

    /// <summary>
    /// Wrapper for Animator.SetBool that works with UIButtons, TriggerZones, etc...
    /// </summary>
    /// <param name="idAndValue">e.g. "moving=true"</param>
    public void SetFloat(string idAndValue)
    {
        SetAnimatorValGeneric(
        idAndValue,
        float.TryParse,
        (Animator animator, string varName, float val) => animator.SetFloat(varName, val)
        );
    }


    void SetAnimatorValGeneric<T>(string idAndValue, ParseDelegate<T> parse, AnimatorSetDelegate<T> setter)
    {
        try
        {
            var arr = idAndValue.Split('=');
            string varName = arr[0].Trim();
            string valuStr = arr[1].Trim();
            T val = default;

            if (!parse(valuStr, out val))
            {
                throw new System.Exception($"invalid right hand side of '{idAndValue}'");
            }

            setter(animator, varName, val);
        }
        catch (System.Exception e)
        {
            Debug.LogException(e);
        }
    }

    [SerializeField] List<NamedAnimationEvent> namedAnimationEvents = new();

    public void PlaySound(AudioClip soundToPlay)
    {
        AudioSource.PlayClipAtPoint(soundToPlay, Camera.main.transform.position);
    }

    public void CallNamedEvent(string eventId)
    {
        foreach(var evt in namedAnimationEvents)
        {
            if (evt.id == eventId)
            {
                evt.action.Invoke();
            }
        }
    }

    [System.Serializable]
    public class NamedAnimationEvent
    {
        public string id;
        public UnityEvent action;
    }
}
