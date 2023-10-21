using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StateBanner : AnimationHandler
{
    public static StateBanner instance;
    public TextMeshPro bannerText;

    new void Start()
    {
        if (instance != null && instance != this) {
            Destroy(this);
        }
        else {
            instance = this;
        }
    }

    public IEnumerator summonBanner(string bannerText, float secondsToHang) {
        yield return enterBanner(bannerText);
        yield return new WaitForSeconds(secondsToHang);
        yield return bannerLeave();
    }

    private IEnumerator enterBanner(string bannerText) {
        this.bannerText.text = bannerText;
        changeAnimationState("Banner Enter");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
    }
    private IEnumerator bannerHang(float secondsToWait){
        changeAnimationState("Banner Idle");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
        yield return new WaitForSeconds(secondsToWait);
    }

    private IEnumerator bannerLeave() {
        changeAnimationState("Banner Exit");
        yield return waitForAnimatorStateChange();
        yield return waitForAnimationToFinish();
    }
}
