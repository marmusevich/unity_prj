using UnityEngine;
using System.Collections;

public class ActorBaseBehaviour : MonoBehaviour
{

	//состояния:
	//1. стоим
	//2. перемещение влево/вправо
	//3. перемещение вверх/вниз
	//4. падаем
	//5. роем ловушку
	//6. попал в ловушку
	//7. смерть
	//8. беру приз

	//раздельные действия: в ловушке, рыть ловушку, взять золото

	#region states - состояния:
	class BaseState
	{
		public BaseState()
		{
		}
		//сотояние начинается
		public virtual void OnStart()
		{
		}
		//обычное обновление
		public virtual void Update()
		{
		}
		//фиксированое (физика) обновление
		public virtual void FixedUpdate()
		{
		}
		//выход из состояния
		public virtual void OnEnd()
		{
		}
	}

	//1. стоим
	class StandState : BaseState
	{
	}

	//2. перемещение влево/вправо
	class MoveHorizontalState : BaseState
	{
		
	}
	
	//3. перемещение вверх/вниз
	class MoveVerticalState : BaseState
	{
		
	}
	
	//4. падаем
	class FallState : BaseState
	{
		
	}
	
	//5. роем ловушку
	class MakeTrampState : BaseState
	{
		
	}
	
	//6. попал в ловушку
	class InTrapState : BaseState
	{
		
	}
	
	//7. смерть
	class DeathState : BaseState
	{
		
	}

	//возможно это не надо
	//8. беру приз
	class TakePrizeState : BaseState
	{
		
	}
	#endregion

	private BaseState mCurrentState;

	public void SetState( BaseState newState )
	{
		mCurrentState.OnEnd();
		mCurrentState = newState;
		mCurrentState.OnStart();
	}


	void Start()
	{
		SetState( new BaseState() );
	}

	void Update()
	{
		mCurrentState.Update();
	}

	void FixedUpdate()
	{
		mCurrentState.FixedUpdate();
	}
}

