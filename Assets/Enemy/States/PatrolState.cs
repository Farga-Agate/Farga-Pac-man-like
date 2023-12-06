using UnityEngine;

public class PatrolState : BaseState
{
    private bool _isMoving;
    private Vector3 _destination;
    private int _randomIndex = 0;
    public void EnterState(Enemy enemy)
    {
        _isMoving = false;
    }

    public void UpdateState(Enemy enemy)
    {
        if (Vector3.Distance(enemy.transform.position, enemy.Player.transform.position) < enemy.ChaseDistance)
        {
            enemy.SwitchState(enemy.ChaseState);
        }

        if (!_isMoving)
        {
            _isMoving = true;

            _randomIndex = UnityEngine.Random.Range(0, enemy.WayPoints.Count);

            _destination = enemy.WayPoints[_randomIndex].position;

            enemy.NavMeshAgent.destination = _destination;
        }
        else
        {
            if (Vector3.Distance(_destination, enemy.transform.position) <= 0.1)
            {
                _isMoving = false;
            }
        }
    }

    public void ExitState(Enemy enemy)
    {

    }
}
