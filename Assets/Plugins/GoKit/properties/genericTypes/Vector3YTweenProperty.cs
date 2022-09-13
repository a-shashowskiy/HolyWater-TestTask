using UnityEngine;
using System;
using System.Collections;


public class Vector3YTweenProperty : Vector3XTweenProperty
{
	public Vector3YTweenProperty( string propertyName, float endValue, bool isRelative = false ) : base( propertyName, endValue, isRelative )
	{}

	
	public override void prepareForUse()
	{
		// retrieve the getter
		_getter = GoTweenUtils.getterForProperty<Func<Vector3>>( _ownerTween.target, propertyName );
		
		_endValue = _originalEndValue;
		
		// if this is a from tween we need to swap the start and end values
		if( _ownerTween.isFrom )
		{
			_startValue = _endValue;
			_endValue = _getter().y;
		}
		else
		{
			_startValue = _getter().y;
		}

		// prep the diff value
		if( _isRelative && !_ownerTween.isFrom )
			_diffValue = _endValue;
		else
			_diffValue = _endValue - _startValue;
	}
	
	
	public override void tick( float totalElapsedTime )
	{
		var currentValue = _getter();
		currentValue.y = _easeFunction( totalElapsedTime, _startValue, _diffValue, _ownerTween.duration );
		
		_setter( currentValue );
	}

}
