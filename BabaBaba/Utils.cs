using UnityEngine;
using System;

namespace BabaBaba
{
	public static class Utils
	{
		public static Vector3 SnapToGrid(Vector3 position)
		{
			return new Vector3(
				Utils.SnapAxis(position.x),
				Utils.SnapAxis(position.y),
				0);
		}

		public static float SnapAxis(float axis)
		{
			float adjustedAxis = axis < 0 ? axis - Constants.BlockSize : axis;
			return ((int)(adjustedAxis / Constants.BlockSize)) * Constants.BlockSize;
		}
	}
}