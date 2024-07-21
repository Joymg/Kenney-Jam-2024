using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class PathEnemyBehavior : BaseEnemyBehaviour
{
    private Transform enemy;
    private SplineContainer spline;
    /*private List<BezierKnot> Knots => spline.Spline.Knots.ToList();
    private List<bool> Knots => spline.Spline.Knots.ToList();*/
    private SplineAnimate splineAnimation;

    public PathEnemyBehavior(SplineAnimate splineAnimation)
    {
        this.splineAnimation = splineAnimation;
    }

    public override void Tick()
    {
        if (!splineAnimation.IsPlaying) {
            splineAnimation.Play();
        }
        
    }

}
