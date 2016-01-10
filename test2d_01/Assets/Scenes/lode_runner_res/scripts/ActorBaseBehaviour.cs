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

    public class BaseState
    {
        protected ActorBaseBehaviour mActor;

        public BaseState( ActorBaseBehaviour actor )
        {
            mActor = actor;
        }
        //сотояние начинается
        public virtual bool OnStart()
        {
            return true;
        }
        //обычное обновление
        public virtual void Update()
        {
        }
        //фиксированое (физика) обновление
        public virtual void FixedUpdate()
        {
        }
        //колизия
        public virtual void OnCollisionEnter2D( Collision2D collision )
        {

        }

        //выход из состояния
        public virtual bool OnEnd()
        {
            return true;
        }
    }

    //1. стоим
    public class StandState : BaseState
    {
        public StandState( ActorBaseBehaviour actor )
            : base( actor )
        {
            
        }
    }

    //2. перемещение влево/вправо
    public class MoveHorizontalState : BaseState
    {
        public MoveHorizontalState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    //3. перемещение вверх/вниз
    public class MoveVerticalState : BaseState
    {
        public MoveVerticalState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    //4. падаем
    public class FallState : BaseState
    {
        public FallState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    //5. роем ловушку
    public class MakeTrampState : BaseState
    {
        public MakeTrampState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    //6. попал в ловушку
    public class InTrapState : BaseState
    {
        public InTrapState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    //7. смерть
    public class DeathState : BaseState
    {
        public DeathState( ActorBaseBehaviour actor )
            : base( actor )
        {

        }
    }

    /*	
        //возможно это не надо
        //8. беру приз
        public class TakePrizeState : BaseState
        {

        }
    */

    #endregion



    private BaseState mCurrentState = null;
    private BaseState mPreviosState = null;

    //устоновить новое
    public void SetNewState( BaseState newState )
    {
        if( newState == null ) //если есть что установит
            return;

        if( mCurrentState != null ) //если текущеее определено
            if( !mCurrentState.OnEnd() ) // если текущее может закончится
                return;

        if( !newState.OnStart() ) //если новое может установится
            return;

        mPreviosState = mCurrentState; //запомниле новое
        mCurrentState = newState; //установили новое
    }

    //вернутся к предыдущему
    public void BackPreviosState()
    {
        if( mPreviosState == null ) //если старое определенно
            return;

        if( mCurrentState != null ) //если текущеее определено
            if( !mCurrentState.OnEnd() ) // если текущее может закончится
                return;

        if( !mPreviosState.OnStart() ) //если старое может установится
            return;

        mPreviosState = null; //обнулить старое
        mCurrentState = mPreviosState; //установили текущим старое
    }


    void Start()
    {
        //SetState( new BaseState( this ) );
    }

    void Update()
    {
        mCurrentState.Update();
    }

    void FixedUpdate()
    {
        mCurrentState.FixedUpdate();
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
        mCurrentState.OnCollisionEnter2D( collision );
    }

}

